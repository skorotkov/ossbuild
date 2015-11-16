/* GStreamer
 * Copyright (C) <2007> Wim Taymans <wim.taymans@gmail.com>
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Library General Public
 * License as published by the Free Software Foundation; either
 * version 2 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Library General Public License for more details.
 *
 * You should have received a copy of the GNU Library General Public
 * License along with this library; if not, write to the
 * Free Software Foundation, Inc., 59 Temple Place - Suite 330,
 * Boston, MA 02111-1307, USA.
 */

#ifdef HAVE_CONFIG_H
#include "config.h"
#endif
#include <stdio.h>
#include <string.h>

#include "gstudpbin1-marshal.h"
#include "gstudpbin1.h"

GST_DEBUG_CATEGORY_STATIC (gst_udp_bin_debug);
#define GST_CAT_DEFAULT gst_udp_bin_debug

/* sink pads */
static GstStaticPadTemplate udpbin_recv_rtp_sink_template =
GST_STATIC_PAD_TEMPLATE ("recv_rtp_sink_%d",
    GST_PAD_SINK,
    GST_PAD_REQUEST,
    // skor:
    // no need to limit to MPEG2/TS - we will just gstpay anything and
    // downstream elements (like typefind) will detect the actual caps
    //GST_STATIC_CAPS ("video/mpegts")
    GST_STATIC_CAPS_ANY
    );

/* src pads */
static GstStaticPadTemplate udpbin_recv_rtp_src_template =
GST_STATIC_PAD_TEMPLATE ("recv_rtp_src_%d_%d_%d",
    GST_PAD_SRC,
    GST_PAD_SOMETIMES,
    GST_STATIC_CAPS ("application/x-rtp")
    );

#define GST_UDP_BIN_GET_PRIVATE(obj)  \
   (G_TYPE_INSTANCE_GET_PRIVATE ((obj), GST_TYPE_UDP_BIN, GstUdpBinPrivate))

#define GST_UDP_BIN_LOCK(bin)   g_mutex_lock ((bin)->priv->bin_lock)
#define GST_UDP_BIN_UNLOCK(bin) g_mutex_unlock ((bin)->priv->bin_lock)

/* lock to protect dynamic callbacks, like pad-added and new ssrc. */
#define GST_UDP_BIN_DYN_LOCK(bin)    g_mutex_lock ((bin)->priv->dyn_lock)
#define GST_UDP_BIN_DYN_UNLOCK(bin)  g_mutex_unlock ((bin)->priv->dyn_lock)

/* lock for shutdown */
#define GST_UDP_BIN_SHUTDOWN_LOCK(bin,label)     \
G_STMT_START {                                   \
  if (g_atomic_int_get (&bin->priv->shutdown))   \
    goto label;                                  \
  GST_UDP_BIN_DYN_LOCK (bin);                    \
  if (g_atomic_int_get (&bin->priv->shutdown)) { \
    GST_UDP_BIN_DYN_UNLOCK (bin);                \
    goto label;                                  \
  }                                              \
} G_STMT_END

/* unlock for shutdown */
#define GST_UDP_BIN_SHUTDOWN_UNLOCK(bin)         \
  GST_UDP_BIN_DYN_UNLOCK (bin);                  \

struct _GstUdpBinPrivate
{
  GMutex *bin_lock;

  /* lock protecting dynamic adding/removing */
  GMutex *dyn_lock;

  /* if we are shutting down or not */
  gint shutdown;
};

/* signals and args */
enum
{
  SIGNAL_REQUEST_PT_MAP,
  SIGNAL_CLEAR_PT_MAP,
  SIGNAL_ON_NPT_STOP,

  SIGNAL_CREATE_SRC_PAD,
  LAST_SIGNAL
};

#define DEFAULT_LATENCY_MS           200
#define DEFAULT_BUFFER_MODE          RTP_JITTER_BUFFER_MODE_SLAVE

enum
{
  PROP_0,
  PROP_LATENCY,
  PROP_BUFFER_MODE,
  PROP_LAST
};

/* helper objects */
typedef struct _GstUdpBinSession GstUdpBinSession;

static guint gst_udp_bin_signals[LAST_SIGNAL] = { 0 };

static GstCaps *pt_map_requested (GstElement * element, guint pt,
    GstUdpBinSession * session);

#define GST_UDP_SESSION_LOCK(sess)   g_mutex_lock ((sess)->lock)
#define GST_UDP_SESSION_UNLOCK(sess) g_mutex_unlock ((sess)->lock)

/* Manages the receiving end of the packets.
 *
 * There is one such structure for each UDP session (audio/video/...).
 * We get the UDP/RTCP packets and stuff them into the session manager. From
 * there they are pushed into an SSRC demuxer that splits the stream based on
 * SSRC. Each of the SSRC streams go into their own jitterbuffer (managed with
 * the GstUdpBinStream above).
 */
struct _GstUdpBinSession
{
  /* session id */
  gint id;
  /* the parent bin */
  GstUdpBin *bin;
  /* the rtpgstpay element */
  GstElement *rtpgstpay;

  GMutex *lock;

  /* mapping of payload type to caps */
  GHashTable *ptmap;

  /* the pads of the session */
  GstPad *recv_rtp_sink;
  GstPad *recv_rtp_sink_ghost;
  GstPad *recv_rtp_src;
};

/* find a session with the given id. Must be called with UDP_BIN_LOCK */
static GstUdpBinSession *
find_session_by_id (GstUdpBin * udpbin, gint id)
{
  GSList *walk;

  for (walk = udpbin->sessions; walk; walk = g_slist_next (walk)) {
    GstUdpBinSession *sess = (GstUdpBinSession *) walk->data;

    if (sess->id == id)
      return sess;
  }
  return NULL;
}

/* find a session with the given request pad. Must be called with UDP_BIN_LOCK */
static GstUdpBinSession *
find_session_by_pad (GstUdpBin * udpbin, GstPad * pad)
{
  GSList *walk;

  for (walk = udpbin->sessions; walk; walk = g_slist_next (walk)) {
    GstUdpBinSession *sess = (GstUdpBinSession *) walk->data;

    if ((sess->recv_rtp_sink_ghost == pad))
      return sess;
  }
  return NULL;
}

static void
on_create_src_pad (GstUdpBin *udpbin, gint id, guint ssrc)
{
  GstUdpBinSession *session = find_session_by_id (udpbin, id);

  if (session == NULL) {
    return;
  }

  GST_UDP_SESSION_LOCK (session);
  GST_UDP_BIN_SHUTDOWN_LOCK (udpbin, shutdown);

  {
    /* add rtpgstpay src pad to pads */
    GstElementClass *klass;
    GstPadTemplate *templ;
    gchar *padname;
    GstPad *gpad, *pad;

    pad = gst_element_get_static_pad (session->rtpgstpay, "src");

    /* ghost the pad to the parent */
    klass = GST_ELEMENT_GET_CLASS (udpbin);
    templ = gst_element_class_get_pad_template (klass, "recv_rtp_src_%d_%d_%d");
    padname = g_strdup_printf ("recv_rtp_src_%d_%u_%d",
        id, ssrc, 255);
    gpad = gst_ghost_pad_new_from_template (padname, pad, templ);
    g_free (padname);

    gst_pad_set_caps (gpad, GST_PAD_CAPS (pad));
    gst_pad_set_active (gpad, TRUE);
    gst_element_add_pad (GST_ELEMENT_CAST (udpbin), gpad);

    gst_object_unref (pad);
  }

  GST_UDP_SESSION_UNLOCK (session);
  GST_UDP_BIN_SHUTDOWN_UNLOCK (udpbin);

  return;

shutdown:
  {
    GST_DEBUG ("ignoring, we are shutting down");
    return;
  }
}

/* create a session with the given id.  Must be called with UDP_BIN_LOCK */
static GstUdpBinSession *
create_session (GstUdpBin * udpbin, gint id)
{
  GstUdpBinSession *sess;
  GstElement *rtpgstpay;
  GstState target;

  if (!(rtpgstpay = gst_element_factory_make ("rtpgstpay", NULL)))
    goto no_rtpgstpay;

  sess = g_new0 (GstUdpBinSession, 1);
  sess->lock = g_mutex_new ();
  sess->id = id;
  sess->bin = udpbin;
  sess->rtpgstpay = rtpgstpay;
  sess->ptmap = g_hash_table_new_full (NULL, NULL, NULL,
      (GDestroyNotify) gst_caps_unref);
  udpbin->sessions = g_slist_prepend (udpbin->sessions, sess);

  /* configure latency */
  g_object_set (rtpgstpay, "latency", udpbin->latency_ms, NULL);

  gst_bin_add (GST_BIN_CAST (udpbin), rtpgstpay);

  GST_OBJECT_LOCK (udpbin);
  target = GST_STATE_TARGET (udpbin);
  GST_OBJECT_UNLOCK (udpbin);

  /* change state only to what's needed */
  gst_element_set_state (rtpgstpay, target);

  return sess;

  /* ERRORS */
no_rtpgstpay:
  {
    g_warning ("gstudpbin: could not create rtpgstpay element");
    return NULL;
  }
}

static void
free_session (GstUdpBinSession * sess, GstUdpBin * bin)
{
  GST_DEBUG_OBJECT (bin, "freeing session %p", sess);

  gst_element_set_locked_state (sess->rtpgstpay, TRUE);

  gst_element_set_state (sess->rtpgstpay, GST_STATE_NULL);

  if (sess->recv_rtp_sink != NULL) {
    gst_element_release_request_pad (sess->rtpgstpay, sess->recv_rtp_sink);
    gst_object_unref (sess->recv_rtp_sink);
  }
  if (sess->recv_rtp_src != NULL)
    gst_object_unref (sess->recv_rtp_src);

  gst_bin_remove (GST_BIN_CAST (bin), sess->rtpgstpay);

  g_mutex_free (sess->lock);
  g_hash_table_destroy (sess->ptmap);

  g_free (sess);
}

/* get the payload type caps for the specific payload @pt in @session */
static GstCaps *
get_pt_map (GstUdpBinSession * session, guint pt)
{
  GstCaps *caps = NULL;
  GstUdpBin *bin;
  GValue ret = { 0 };
  GValue args[3] = { {0}, {0}, {0} };

  GST_DEBUG ("searching pt %d in cache", pt);

  GST_UDP_SESSION_LOCK (session);

  /* first look in the cache */
  caps = g_hash_table_lookup (session->ptmap, GINT_TO_POINTER (pt));
  if (caps) {
    gst_caps_ref (caps);
    goto done;
  }

  bin = session->bin;

  GST_DEBUG ("emiting signal for pt %d in session %d", pt, session->id);

  /* not in cache, send signal to request caps */
  g_value_init (&args[0], GST_TYPE_ELEMENT);
  g_value_set_object (&args[0], bin);
  g_value_init (&args[1], G_TYPE_UINT);
  g_value_set_uint (&args[1], session->id);
  g_value_init (&args[2], G_TYPE_UINT);
  g_value_set_uint (&args[2], pt);

  g_value_init (&ret, GST_TYPE_CAPS);
  g_value_set_boxed (&ret, NULL);

  GST_UDP_SESSION_UNLOCK (session);

  g_signal_emitv (args, gst_udp_bin_signals[SIGNAL_REQUEST_PT_MAP], 0, &ret);

  GST_UDP_SESSION_LOCK (session);

  g_value_unset (&args[0]);
  g_value_unset (&args[1]);
  g_value_unset (&args[2]);

  /* look in the cache again because we let the lock go */
  caps = g_hash_table_lookup (session->ptmap, GINT_TO_POINTER (pt));
  if (caps) {
    gst_caps_ref (caps);
    g_value_unset (&ret);
    goto done;
  }

  caps = (GstCaps *) g_value_dup_boxed (&ret);
  g_value_unset (&ret);
  if (!caps)
    goto no_caps;

  GST_DEBUG ("caching pt %d as %" GST_PTR_FORMAT, pt, caps);

  /* store in cache, take additional ref */
  g_hash_table_insert (session->ptmap, GINT_TO_POINTER (pt),
      gst_caps_ref (caps));

done:
  GST_UDP_SESSION_UNLOCK (session);

  return caps;

  /* ERRORS */
no_caps:
  {
    GST_UDP_SESSION_UNLOCK (session);
    GST_DEBUG ("no pt map could be obtained");
    return NULL;
  }
}

static gboolean
return_true (gpointer key, gpointer value, gpointer user_data)
{
  return TRUE;
}

static void
gst_udp_bin_clear_pt_map (GstUdpBin * bin)
{
}

static void
gst_udp_bin_propagate_property_to_rtpgstpay (GstUdpBin * bin,
    const gchar * name, const GValue * value)
{
  GSList *sessions;

  GST_UDP_BIN_LOCK (bin);
  for (sessions = bin->sessions; sessions; sessions = g_slist_next (sessions)) {
    GstUdpBinSession *session = (GstUdpBinSession *) sessions->data;

    GST_UDP_SESSION_LOCK (session);
    g_object_set_property (G_OBJECT (session->rtpgstpay), name, value);
    GST_UDP_SESSION_UNLOCK (session);
  }
  GST_UDP_BIN_UNLOCK (bin);
}

/* GObject vmethods */
static void gst_udp_bin_dispose (GObject * object);
static void gst_udp_bin_finalize (GObject * object);
static void gst_udp_bin_set_property (GObject * object, guint prop_id,
    const GValue * value, GParamSpec * pspec);
static void gst_udp_bin_get_property (GObject * object, guint prop_id,
    GValue * value, GParamSpec * pspec);

/* GstElement vmethods */
static GstStateChangeReturn gst_udp_bin_change_state (GstElement * element,
    GstStateChange transition);
static GstPad *gst_udp_bin_request_new_pad (GstElement * element,
    GstPadTemplate * templ, const gchar * name);
static void gst_udp_bin_release_pad (GstElement * element, GstPad * pad);
static void gst_udp_bin_handle_message (GstBin * bin, GstMessage * message);

GST_BOILERPLATE (GstUdpBin, gst_udp_bin, GstBin, GST_TYPE_BIN);

static void
gst_udp_bin_base_init (gpointer klass)
{
  GstElementClass *element_class = GST_ELEMENT_CLASS (klass);

  /* sink pads */
  gst_element_class_add_pad_template (element_class,
      gst_static_pad_template_get (&udpbin_recv_rtp_sink_template));

  /* src pads */
  gst_element_class_add_pad_template (element_class,
      gst_static_pad_template_get (&udpbin_recv_rtp_src_template));

  gst_element_class_set_details_simple (element_class, "UDP Bin",
      "Filter/Network/UDP",
      "Manager for RAW/RAW/UDP",
      "Not Wim Taymans <wim.taymans@gmail.com>");
}

static void
gst_udp_bin_class_init (GstUdpBinClass * klass)
{
  GObjectClass *gobject_class;
  GstElementClass *gstelement_class;
  GstBinClass *gstbin_class;

  gobject_class = (GObjectClass *) klass;
  gstelement_class = (GstElementClass *) klass;
  gstbin_class = (GstBinClass *) klass;

  g_type_class_add_private (klass, sizeof (GstUdpBinPrivate));

  gobject_class->dispose = gst_udp_bin_dispose;
  gobject_class->finalize = gst_udp_bin_finalize;
  gobject_class->set_property = gst_udp_bin_set_property;
  gobject_class->get_property = gst_udp_bin_get_property;

  g_object_class_install_property (gobject_class, PROP_LATENCY,
      g_param_spec_uint ("latency", "Buffer latency in ms",
          "Default amount of ms to buffer in the jitterbuffers", 0,
          G_MAXUINT, DEFAULT_LATENCY_MS,
          G_PARAM_READWRITE | G_PARAM_STATIC_STRINGS));

  g_object_class_install_property (gobject_class, PROP_BUFFER_MODE,
      g_param_spec_enum ("buffer-mode", "Buffer Mode",
          "Control the buffering algorithm in use", RTP_TYPE_JITTER_BUFFER_MODE,
          DEFAULT_BUFFER_MODE, G_PARAM_READWRITE | G_PARAM_STATIC_STRINGS));

  gst_udp_bin_signals[SIGNAL_REQUEST_PT_MAP] =
      g_signal_new ("request-pt-map", G_TYPE_FROM_CLASS (klass),
      G_SIGNAL_RUN_LAST, G_STRUCT_OFFSET (GstUdpBinClass, request_pt_map),
      NULL, NULL, gst_udp_bin_marshal_BOXED__UINT_UINT, GST_TYPE_CAPS, 2,
      G_TYPE_UINT, G_TYPE_UINT);

  gst_udp_bin_signals[SIGNAL_CLEAR_PT_MAP] =
      g_signal_new ("clear-pt-map", G_TYPE_FROM_CLASS (klass),
      G_SIGNAL_RUN_LAST | G_SIGNAL_ACTION, G_STRUCT_OFFSET (GstUdpBinClass,
          clear_pt_map), NULL, NULL, g_cclosure_marshal_VOID__VOID, G_TYPE_NONE,
      0, G_TYPE_NONE);

  gst_udp_bin_signals[SIGNAL_ON_NPT_STOP] =
      g_signal_new ("on-npt-stop", G_TYPE_FROM_CLASS (klass),
      G_SIGNAL_RUN_LAST, G_STRUCT_OFFSET (GstUdpBinClass, on_npt_stop),
      NULL, NULL, gst_udp_bin_marshal_VOID__UINT_UINT, G_TYPE_NONE, 2,
      G_TYPE_UINT, G_TYPE_UINT);

  // request to create src pad for selected session (with id and ssrc)
  gst_udp_bin_signals[SIGNAL_CREATE_SRC_PAD] =
      g_signal_new ("create-src-pad", G_TYPE_FROM_CLASS (klass),
      G_SIGNAL_RUN_LAST, G_STRUCT_OFFSET (GstUdpBinClass, on_create_src_pad),
      NULL, NULL, gst_udp_bin_marshal_VOID__UINT_UINT, G_TYPE_NONE, 2,
      G_TYPE_UINT, G_TYPE_UINT);

  gstelement_class->change_state = GST_DEBUG_FUNCPTR (gst_udp_bin_change_state);
  gstelement_class->request_new_pad =
      GST_DEBUG_FUNCPTR (gst_udp_bin_request_new_pad);
  gstelement_class->release_pad = GST_DEBUG_FUNCPTR (gst_udp_bin_release_pad);

  gstbin_class->handle_message = GST_DEBUG_FUNCPTR (gst_udp_bin_handle_message);

  klass->clear_pt_map = GST_DEBUG_FUNCPTR (gst_udp_bin_clear_pt_map);
  klass->on_create_src_pad = GST_DEBUG_FUNCPTR (on_create_src_pad);

  GST_DEBUG_CATEGORY_INIT (gst_udp_bin_debug, "udpbin", 0, "UDP bin");
}

static void
gst_udp_bin_init (GstUdpBin * udpbin, GstUdpBinClass * klass)
{
  udpbin->priv = GST_UDP_BIN_GET_PRIVATE (udpbin);
  udpbin->priv->bin_lock = g_mutex_new ();
  udpbin->priv->dyn_lock = g_mutex_new ();

  udpbin->latency_ms = DEFAULT_LATENCY_MS;
  udpbin->latency_ns = DEFAULT_LATENCY_MS * GST_MSECOND;

  udpbin->buffer_mode = DEFAULT_BUFFER_MODE;
}

static void
gst_udp_bin_dispose (GObject * object)
{
  GstUdpBin *udpbin;

  udpbin = GST_UDP_BIN (object);

  GST_DEBUG_OBJECT (object, "freeing sessions");
  g_slist_foreach (udpbin->sessions, (GFunc) free_session, udpbin);
  g_slist_free (udpbin->sessions);
  udpbin->sessions = NULL;

  G_OBJECT_CLASS (parent_class)->dispose (object);
}

static void
gst_udp_bin_finalize (GObject * object)
{
  GstUdpBin *udpbin;

  udpbin = GST_UDP_BIN (object);

  g_mutex_free (udpbin->priv->bin_lock);
  g_mutex_free (udpbin->priv->dyn_lock);

  G_OBJECT_CLASS (parent_class)->finalize (object);
}

static void
gst_udp_bin_set_property (GObject * object, guint prop_id,
    const GValue * value, GParamSpec * pspec)
{
  GstUdpBin *udpbin;

  udpbin = GST_UDP_BIN (object);

  switch (prop_id) {
    case PROP_LATENCY:
      GST_UDP_BIN_LOCK (udpbin);
      udpbin->latency_ms = g_value_get_uint (value);
      udpbin->latency_ns = udpbin->latency_ms * GST_MSECOND;
      GST_UDP_BIN_UNLOCK (udpbin);
      /* propagate the property down to the rtpgstpay */
      gst_udp_bin_propagate_property_to_rtpgstpay (udpbin, "latency", value);
      break;
    case PROP_BUFFER_MODE:
      GST_UDP_BIN_LOCK (udpbin);
      udpbin->buffer_mode = g_value_get_enum (value);
      GST_UDP_BIN_UNLOCK (udpbin);
      break;
    default:
      G_OBJECT_WARN_INVALID_PROPERTY_ID (object, prop_id, pspec);
      break;
  }
}

static void
gst_udp_bin_get_property (GObject * object, guint prop_id,
    GValue * value, GParamSpec * pspec)
{
  GstUdpBin *udpbin;

  udpbin = GST_UDP_BIN (object);

  switch (prop_id) {
    case PROP_LATENCY:
      GST_UDP_BIN_LOCK (udpbin);
      g_value_set_uint (value, udpbin->latency_ms);
      GST_UDP_BIN_UNLOCK (udpbin);
      break;
    case PROP_BUFFER_MODE:
      g_value_set_enum (value, udpbin->buffer_mode);
      break;
    default:
      G_OBJECT_WARN_INVALID_PROPERTY_ID (object, prop_id, pspec);
      break;
  }
}

static void
gst_udp_bin_handle_message (GstBin * bin, GstMessage * message)
{
  GstUdpBin *udpbin;

  udpbin = GST_UDP_BIN (bin);

  switch (GST_MESSAGE_TYPE (message)) {
    default:
    {
      GST_BIN_CLASS (parent_class)->handle_message (bin, message);
      break;
    }
  }
}

static GstStateChangeReturn
gst_udp_bin_change_state (GstElement * element, GstStateChange transition)
{
  GstStateChangeReturn res;
  GstUdpBin *udpbin;
  GstUdpBinPrivate *priv;

  udpbin = GST_UDP_BIN (element);
  priv = udpbin->priv;

  switch (transition) {
    case GST_STATE_CHANGE_NULL_TO_READY:
      break;
    case GST_STATE_CHANGE_READY_TO_PAUSED:
      GST_LOG_OBJECT (udpbin, "clearing shutdown flag");
      g_atomic_int_set (&priv->shutdown, 0);
      break;
    case GST_STATE_CHANGE_PAUSED_TO_READY:
      GST_LOG_OBJECT (udpbin, "setting shutdown flag");
      g_atomic_int_set (&priv->shutdown, 1);
      /* wait for all callbacks to end by taking the lock. No new callbacks will
       * be able to happen as we set the shutdown flag. */
      GST_UDP_BIN_DYN_LOCK (udpbin);
      GST_LOG_OBJECT (udpbin, "dynamic lock taken, we can continue shutdown");
      GST_UDP_BIN_DYN_UNLOCK (udpbin);
      break;
    default:
      break;
  }

  res = GST_ELEMENT_CLASS (parent_class)->change_state (element, transition);

  switch (transition) {
    case GST_STATE_CHANGE_PLAYING_TO_PAUSED:
      break;
    case GST_STATE_CHANGE_PAUSED_TO_READY:
      break;
    case GST_STATE_CHANGE_READY_TO_NULL:
      break;
    default:
      break;
  }
  return res;
}

static GstCaps *
pt_map_requested (GstElement * element, guint pt, GstUdpBinSession * session)
{
  GstUdpBin *udpbin;
  GstCaps *caps;

  udpbin = session->bin;

  GST_DEBUG_OBJECT (udpbin, "payload map requested for pt %d in session %d", pt,
      session->id);

  caps = get_pt_map (session, pt);
  if (!caps)
    goto no_caps;

  return caps;

  /* ERRORS */
no_caps:
  {
    GST_DEBUG_OBJECT (udpbin, "could not get caps");
    return NULL;
  }
}

/* emited when caps changed for the session */
static void
caps_changed (GstPad * pad, GParamSpec * pspec, GstUdpBinSession * session)
{
  GstUdpBin *bin;
  GstCaps *caps;
  gint payload;
  const GstStructure *s;

  bin = session->bin;

  g_object_get (pad, "caps", &caps, NULL);

  if (caps == NULL)
    return;

  GST_DEBUG_OBJECT (bin, "got caps %" GST_PTR_FORMAT, caps);

  s = gst_caps_get_structure (caps, 0);

  /* get payload, finish when it's not there */
  if (!gst_structure_get_int (s, "payload", &payload))
    return;

  GST_UDP_SESSION_LOCK (session);
  GST_DEBUG_OBJECT (bin, "insert caps for payload %d", payload);
  g_hash_table_insert (session->ptmap, GINT_TO_POINTER (payload), caps);
  GST_UDP_SESSION_UNLOCK (session);
}

/* Create a pad for receiving UDP for the session in @name. Must be called with
 * UDP_BIN_LOCK.
 */
static GstPad *
create_recv_rtp (GstUdpBin * udpbin, GstPadTemplate * templ, const gchar * name)
{
  guint sessid;
  GstUdpBinSession *session;

  /* first get the session number */
  if (name == NULL || sscanf (name, "recv_rtp_sink_%d", &sessid) != 1)
    goto no_name;

  GST_DEBUG_OBJECT (udpbin, "finding session %d", sessid);

  /* get or create session */
  session = find_session_by_id (udpbin, sessid);
  if (!session) {
    GST_DEBUG_OBJECT (udpbin, "creating session %d", sessid);
    /* create session now */
    session = create_session (udpbin, sessid);
    if (session == NULL)
      goto create_error;
  }

  /* check if pad was requested */
  if (session->recv_rtp_sink_ghost != NULL)
    return session->recv_rtp_sink_ghost;

  GST_DEBUG_OBJECT (udpbin, "getting UDP sink pad");
  /* get rtpgstpay sinkpad pad and store */
  session->recv_rtp_sink =
      gst_element_get_static_pad (session->rtpgstpay, "sink");
  if (session->recv_rtp_sink == NULL)
    goto pad_failed;

  g_signal_connect (session->recv_rtp_sink, "notify::caps",
      (GCallback) caps_changed, session);

  GST_DEBUG_OBJECT (udpbin, "getting UDP src pad");
  /* get rtpgstpay src */
  session->recv_rtp_src =
      gst_element_get_static_pad (session->rtpgstpay, "src");
  if (session->recv_rtp_src == NULL)
    goto pad_failed;

  GST_DEBUG_OBJECT (udpbin, "ghosting session sink pad");
  session->recv_rtp_sink_ghost =
      gst_ghost_pad_new_from_template (name, session->recv_rtp_sink, templ);
  gst_pad_set_active (session->recv_rtp_sink_ghost, TRUE);
  gst_element_add_pad (GST_ELEMENT_CAST (udpbin), session->recv_rtp_sink_ghost);

  return session->recv_rtp_sink_ghost;

  /* ERRORS */
no_name:
  {
    g_warning ("gstudpbin: invalid name given");
    return NULL;
  }
create_error:
  {
    /* create_session already warned */
    return NULL;
  }
pad_failed:
  {
    g_warning ("gstudpbin: failed to get session pad");
    return NULL;
  }
}

static void
remove_recv_rtp (GstUdpBin * udpbin, GstUdpBinSession * session)
{
  if (session->recv_rtp_src) {
    gst_object_unref (session->recv_rtp_src);
    session->recv_rtp_src = NULL;
  }
  if (session->recv_rtp_sink) {
    gst_element_release_request_pad (session->rtpgstpay, session->recv_rtp_sink);
    gst_object_unref (session->recv_rtp_sink);
    session->recv_rtp_sink = NULL;
  }
  if (session->recv_rtp_sink_ghost) {
    gst_pad_set_active (session->recv_rtp_sink_ghost, FALSE);
    gst_element_remove_pad (GST_ELEMENT_CAST (udpbin),
        session->recv_rtp_sink_ghost);
    session->recv_rtp_sink_ghost = NULL;
  }
}

/* If the requested name is NULL we should create a name with
 * the session number assuming we want the lowest posible session
 * with a free pad like the template */
static gchar *
gst_udp_bin_get_free_pad_name (GstElement * element, GstPadTemplate * templ)
{
  gboolean name_found = FALSE;
  gint session = 0;
  GstIterator *pad_it = NULL;
  gchar *pad_name = NULL;

  GST_DEBUG_OBJECT (element, "find a free pad name for template");
  while (!name_found) {
    gboolean done = FALSE;
    g_free (pad_name);
    pad_name = g_strdup_printf (templ->name_template, session++);
    pad_it = gst_element_iterate_pads (GST_ELEMENT (element));
    name_found = TRUE;
    while (!done) {
      gpointer data;

      switch (gst_iterator_next (pad_it, &data)) {
        case GST_ITERATOR_OK:
        {
          GstPad *pad;
          gchar *name;

          pad = GST_PAD_CAST (data);
          name = gst_pad_get_name (pad);

          if (strcmp (name, pad_name) == 0) {
            done = TRUE;
            name_found = FALSE;
          }
          g_free (name);
          gst_object_unref (pad);
          break;
        }
        case GST_ITERATOR_ERROR:
        case GST_ITERATOR_RESYNC:
          /* restart iteration */
          done = TRUE;
          name_found = FALSE;
          session = 0;
          break;
        case GST_ITERATOR_DONE:
          done = TRUE;
          break;
      }
    }
    gst_iterator_free (pad_it);
  }

  GST_DEBUG_OBJECT (element, "free pad name found: '%s'", pad_name);
  return pad_name;
}

/*
 */
static GstPad *
gst_udp_bin_request_new_pad (GstElement * element,
    GstPadTemplate * templ, const gchar * name)
{
  GstUdpBin *udpbin;
  GstElementClass *klass;
  GstPad *result;

  gchar *pad_name = NULL;

  g_return_val_if_fail (templ != NULL, NULL);
  g_return_val_if_fail (GST_IS_UDP_BIN (element), NULL);

  udpbin = GST_UDP_BIN (element);
  klass = GST_ELEMENT_GET_CLASS (element);

  GST_UDP_BIN_LOCK (udpbin);

  if (name == NULL) {
    /* use a free pad name */
    pad_name = gst_udp_bin_get_free_pad_name (element, templ);
  } else {
    /* use the provided name */
    pad_name = g_strdup (name);
  }

  GST_DEBUG_OBJECT (udpbin, "Trying to request a pad with name %s", pad_name);

  /* figure out the template */
  if (templ == gst_element_class_get_pad_template (klass, "recv_rtp_sink_%d")) {
    result = create_recv_rtp (udpbin, templ, pad_name);
  } else
    goto wrong_template;

  g_free (pad_name);
  GST_UDP_BIN_UNLOCK (udpbin);

  return result;

  /* ERRORS */
wrong_template:
  {
    g_free (pad_name);
    GST_UDP_BIN_UNLOCK (udpbin);
    g_warning ("gstudpbin: this is not our template");
    return NULL;
  }
}

static void
gst_udp_bin_release_pad (GstElement * element, GstPad * pad)
{
  GstUdpBinSession *session;
  GstUdpBin *udpbin;

  g_return_if_fail (GST_IS_GHOST_PAD (pad));
  g_return_if_fail (GST_IS_UDP_BIN (element));

  udpbin = GST_UDP_BIN (element);

  GST_UDP_BIN_LOCK (udpbin);
  GST_DEBUG_OBJECT (udpbin, "Trying to release pad %s:%s",
      GST_DEBUG_PAD_NAME (pad));

  if (!(session = find_session_by_pad (udpbin, pad)))
    goto unknown_pad;

  if (session->recv_rtp_sink_ghost == pad) {
    remove_recv_rtp (udpbin, session);

  /* no more request pads, free the complete session */
    if (session->recv_rtp_sink_ghost == NULL) {
      GST_DEBUG_OBJECT (udpbin, "no more pads for session %p", session);
      udpbin->sessions = g_slist_remove (udpbin->sessions, session);
      free_session (session, udpbin);
    }
  }
  GST_UDP_BIN_UNLOCK (udpbin);

  return;

  /* ERROR */
unknown_pad:
  {
    GST_UDP_BIN_UNLOCK (udpbin);
    g_warning ("gstudpbin: %s:%s is not one of our request pads",
        GST_DEBUG_PAD_NAME (pad));
    return;
  }
}
