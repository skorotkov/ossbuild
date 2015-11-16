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

#ifndef __GST_UDP_BIN_H__
#define __GST_UDP_BIN_H__

#include <gst/gst.h>

#include "rtpjitterbuffer.h"

#define GST_TYPE_UDP_BIN \
  (gst_udp_bin_get_type())
#define GST_UDP_BIN(obj) \
  (G_TYPE_CHECK_INSTANCE_CAST((obj),GST_TYPE_UDP_BIN,GstUdpBin))
#define GST_UDP_BIN_CLASS(klass) \
  (G_TYPE_CHECK_CLASS_CAST((klass),GST_TYPE_UDP_BIN,GstUdpBinClass))
#define GST_IS_UDP_BIN(obj) \
  (G_TYPE_CHECK_INSTANCE_TYPE((obj),GST_TYPE_UDP_BIN))
#define GST_IS_UDP_BIN_CLASS(klass) \
  (G_TYPE_CHECK_CLASS_TYPE((klass),GST_TYPE_UDP_BIN))

typedef struct _GstUdpBin GstUdpBin;
typedef struct _GstUdpBinClass GstUdpBinClass;
typedef struct _GstUdpBinPrivate GstUdpBinPrivate;

struct _GstUdpBin {
  GstBin         bin;

  /*< private >*/
  /* default latency for sessions */
  guint           latency_ms;
  guint64         latency_ns;

  /* we need to provide 'buffer-mode' property, but we don't use it ourselves */
  RTPJitterBufferMode buffer_mode;

  /* a list of session */
  GSList         *sessions;

  /*< private >*/
  GstUdpBinPrivate *priv;
};

struct _GstUdpBinClass {
  GstBinClass  parent_class;

  /* get the caps for pt */
  GstCaps*    (*request_pt_map)       (GstUdpBin *udpbin, guint session, guint pt);

  /* action signals */
  void        (*clear_pt_map)         (GstUdpBin *udpbin);

  /* session manager signals */
  void     (*on_npt_stop)       (GstUdpBin *udpbin, guint session, guint32 ssrc);
  void     (*on_create_src_pad) (GstUdpBin *udpbin, gint id, guint32 ssrc);
};

GType gst_udp_bin_get_type (void);

#endif /* __GST_UDP_BIN_H__ */
