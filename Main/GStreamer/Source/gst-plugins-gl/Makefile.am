DISTCHECK_CONFIGURE_FLAGS=--enable-gtk-doc

SUBDIRS = 			\
	gst-libs 		\
	gst		 	\
	tests 			\
	docs			\
	po 			\
	common 			\
	m4 			\
	pkgconfig

DIST_SUBDIRS = 			\
	docs			\
	gst-libs		\
	gst			\
	tests 			\
	po 			\
	common 			\
	m4 			\
	pkgconfig

# include before EXTRA_DIST for win32 assignment
include $(top_srcdir)/common/win32.mak

EXTRA_DIST = \
	gst-plugins-gl.spec depcomp \
	AUTHORS COPYING NEWS README RELEASE REQUIREMENTS \
	ChangeLog gst-plugins-gl.doap autogen.sh $(win32)

DISTCLEANFILES = _stdint.h

ACLOCAL_AMFLAGS = -I m4 -I common/m4

include $(top_srcdir)/common/release.mak
include $(top_srcdir)/common/po.mak

check-valgrind:
	cd tests/check && make check-valgrind

if HAVE_GST_CHECK
check-torture:
	cd tests/check && make torture
else
check-torture:
	true
endif

WIN32_COPY = \
	$(top_builddir)/gst-libs/gst/*/*-enumtypes.[ch] \
	$(top_builddir)/_stdint.h

win32-update:
	for f in $(WIN32_COPY); do cp $$f win32/common;done

include $(top_srcdir)/common/coverage/lcov.mak

check: check-exports

# cruft: plugins that have been merged or moved or renamed

CRUFT_FILES = \
	$(top_builddir)/common/shave \
	$(top_builddir)/common/shave-libtool

include $(top_srcdir)/common/cruft.mak

all-local: check-cruft
