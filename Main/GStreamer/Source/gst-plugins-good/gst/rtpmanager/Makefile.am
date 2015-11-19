plugin_LTLIBRARIES = libgstrtpmanager.la

glib_enum_define = GST_RTP_BIN
glib_gen_prefix = gst_rtp_bin
glib_gen_basename = gstrtpbin

glib_gen_prefix_ub = gst_udp_bin
glib_gen_basename_ub = gstudpbin1

include $(top_srcdir)/common/gst-glib-gen.mak

# same rules as in gst-glib-gen.mak
$(glib_gen_basename_ub)-marshal.h: $(glib_gen_basename_ub)-marshal.list
	$(AM_V_GEN)glib-genmarshal --header --prefix=$(glib_gen_prefix_ub)_marshal $^ > $(glib_gen_basename_ub)-marshal.h.tmp && \
	mv $(glib_gen_basename_ub)-marshal.h.tmp $(glib_gen_basename_ub)-marshal.h

$(glib_gen_basename_ub)-marshal.c: $(glib_gen_basename_ub)-marshal.list
	$(AM_V_GEN)echo "#include \"$(glib_gen_basename_ub)-marshal.h\"" >> $(glib_gen_basename_ub)-marshal.c.tmp && \
	glib-genmarshal --body --prefix=$(glib_gen_prefix_ub)_marshal $^ >> $(glib_gen_basename_ub)-marshal.c.tmp && \
	mv $(glib_gen_basename_ub)-marshal.c.tmp $(glib_gen_basename_ub)-marshal.c

built_sources = gstrtpbin-marshal.c gstudpbin1-marshal.c
built_headers = gstrtpbin-marshal.h gstudpbin1-marshal.h

BUILT_SOURCES = $(built_sources) $(built_headers)

libgstrtpmanager_la_SOURCES = gstrtpmanager.c \
			      gstrtpbin.c \
			      gstudpbin1.c \
			      gstrtpjitterbuffer.c \
			      gstrtpptdemux.c \
			      gstrtpssrcdemux.c \
			      rtpjitterbuffer.c      \
			      rtpsession.c      \
			      rtpsource.c      \
			      rtpstats.c      \
			      gstrtpsession.c

nodist_libgstrtpmanager_la_SOURCES = \
      $(built_sources)

noinst_HEADERS = gstrtpbin.h \
		 gstudpbin1.h \
		 gstrtpjitterbuffer.h \
                 gstrtpptdemux.h \
                 gstrtpssrcdemux.h \
                 rtpjitterbuffer.h \
		 rtpsession.h  \
		 rtpsource.h  \
		 rtpstats.h  \
		 gstrtpsession.h

libgstrtpmanager_la_CFLAGS = $(GST_PLUGINS_BASE_CFLAGS) $(GST_CFLAGS) \
	$(WARNING_CFLAGS) $(ERROR_CFLAGS)
libgstrtpmanager_la_LIBADD = $(GST_PLUGINS_BASE_LIBS) \
	-lgstnetbuffer-@GST_MAJORMINOR@ -lgstrtp-@GST_MAJORMINOR@ \
	$(GST_BASE_LIBS) $(GST_LIBS_LIBS)
libgstrtpmanager_la_LDFLAGS = $(GST_PLUGIN_LDFLAGS)
libgstrtpmanager_la_LIBTOOLFLAGS = --tag=disable-static

CLEANFILES = $(BUILT_SOURCES)

EXTRA_DIST = gstrtpbin-marshal.list


Android.mk: Makefile.am $(BUILT_SOURCES)
	androgenizer \
	-:PROJECT libgstrtpmanager -:SHARED libgstrtpmanager \
	 -:TAGS eng debug \
         -:REL_TOP $(top_srcdir) -:ABS_TOP $(abs_top_srcdir) \
	 -:SOURCES $(libgstrtpmanager_la_SOURCES) \
	 	   $(nodist_libgstrtpmanager_la_SOURCES) \
	 -:CFLAGS $(DEFS) $(DEFAULT_INCLUDES) $(libgstrtpmanager_la_CFLAGS) \
	 -:LDFLAGS $(libgstrtpmanager_la_LDFLAGS) \
	           $(libgstrtpmanager_la_LIBADD) \
	           -ldl \
	 -:PASSTHROUGH LOCAL_ARM_MODE:=arm \
		       LOCAL_MODULE_PATH:='$$(TARGET_OUT)/lib/gstreamer-0.10' \
	> $@
