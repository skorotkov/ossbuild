
noinst_PROGRAMS = simple-call simple-call-shm


AM_CFLAGS = \
	-I$(top_srcdir)/gst/fsrtpconference/ \
	$(FS2_INTERNAL_CFLAGS) \
	$(FS2_CFLAGS) \
	$(GST_CFLAGS) \
	$(CFLAGS)

LDADD = \
	$(top_builddir)/gst-libs/gst/farsight/libgstfarsight-0.10.la \
	$(GST_LIBS) 


simple_call_shm_CFLAGS = \
	$(AM_CFLAGS) \
	$(GIO_UNIX_CFLAGS)

simple_call_shm_LDADD = \
	$(LDADD) \
	$(GIO_UNIX_LIBS)
