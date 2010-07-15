
/* autogenerated from gstcogorc.orc */

#ifndef _GSTCOGORC_H_
#define _GSTCOGORC_H_

#include <glib.h>

#ifdef __cplusplus
extern "C" {
#endif

#ifndef _ORC_INTEGER_TYPEDEFS_
#define _ORC_INTEGER_TYPEDEFS_
#if defined(__STDC_VERSION__) && __STDC_VERSION__ >= 199901L
#include <stdint.h>
typedef int8_t orc_int8;
typedef int16_t orc_int16;
typedef int32_t orc_int32;
typedef int64_t orc_int64;
typedef uint8_t orc_uint8;
typedef uint16_t orc_uint16;
typedef uint32_t orc_uint32;
typedef uint64_t orc_uint64;
#elif defined(_MSC_VER)
typedef signed __int8 orc_int8;
typedef signed __int16 orc_int16;
typedef signed __int32 orc_int32;
typedef signed __int64 orc_int64;
typedef unsigned __int8 orc_uint8;
typedef unsigned __int16 orc_uint16;
typedef unsigned __int32 orc_uint32;
typedef unsigned __int64 orc_uint64;
#else
#include <limits.h>
typedef signed char orc_int8;
typedef short orc_int16;
typedef int orc_int32;
typedef unsigned char orc_uint8;
typedef unsigned short orc_uint16;
typedef unsigned int orc_uint32;
#if INT_MAX == LONG_MAX
typedef long long orc_int64;
typedef unsigned long long orc_uint64;
#else
typedef long orc_int64;
typedef unsigned long orc_uint64;
#endif
#endif
typedef union { orc_int32 i; float f; } orc_union32;
typedef union { orc_int64 i; double f; } orc_union64;
#endif

void cogorc_downsample_horiz_cosite_1tap (orc_uint8 * d1, const orc_uint16 * s1, int n);
void cogorc_downsample_horiz_cosite_3tap (orc_uint8 * d1, const orc_uint16 * s1, const orc_uint16 * s2, int n);
void cogorc_downsample_420_jpeg (orc_uint8 * d1, const orc_uint16 * s1, const orc_uint16 * s2, int n);
void cogorc_downsample_vert_halfsite_2tap (orc_uint8 * d1, const orc_uint8 * s1, const orc_uint8 * s2, int n);
void cogorc_downsample_vert_cosite_3tap (orc_uint8 * d1, const orc_uint8 * s1, const orc_uint8 * s2, const orc_uint8 * s3, int n);
void cogorc_downsample_vert_halfsite_4tap (orc_uint8 * d1, const orc_uint8 * s1, const orc_uint8 * s2, const orc_uint8 * s3, const orc_uint8 * s4, int n);
void cogorc_upsample_horiz_cosite_1tap (guint8 * d1, const orc_uint8 * s1, int n);
void cogorc_upsample_horiz_cosite (guint8 * d1, const orc_uint8 * s1, const orc_uint8 * s2, int n);
void cogorc_upsample_vert_avgub (orc_uint8 * d1, const orc_uint8 * s1, const orc_uint8 * s2, int n);
void orc_unpack_yuyv_y (orc_uint8 * d1, const orc_uint16 * s1, int n);
void orc_unpack_yuyv_u (orc_uint8 * d1, const orc_uint32 * s1, int n);
void orc_unpack_yuyv_v (orc_uint8 * d1, const orc_uint32 * s1, int n);
void orc_pack_yuyv (orc_uint32 * d1, const guint8 * s1, const orc_uint8 * s2, const orc_uint8 * s3, int n);
void orc_unpack_uyvy_y (orc_uint8 * d1, const orc_uint16 * s1, int n);
void orc_unpack_uyvy_u (orc_uint8 * d1, const orc_uint32 * s1, int n);
void orc_unpack_uyvy_v (orc_uint8 * d1, const orc_uint32 * s1, int n);
void orc_pack_uyvy (orc_uint32 * d1, const guint8 * s1, const orc_uint8 * s2, const orc_uint8 * s3, int n);
void orc_addc_convert_u8_s16 (orc_uint8 * d1, const gint16 * s1, int n);
void orc_subc_convert_s16_u8 (gint16 * d1, const orc_uint8 * s1, int n);
void orc_splat_u8_ns (orc_uint8 * d1, int p1, int n);
void orc_splat_s16_ns (gint16 * d1, int p1, int n);
void orc_matrix2_u8 (guint8 * d1, const guint8 * s1, const guint8 * s2, int p1, int p2, int p3, int n);
void orc_matrix2_11_u8 (guint8 * d1, const guint8 * s1, const guint8 * s2, int p1, int p2, int n);
void orc_matrix2_12_u8 (guint8 * d1, const guint8 * s1, const guint8 * s2, int p1, int p2, int n);
void orc_matrix3_u8 (guint8 * d1, const guint8 * s1, const guint8 * s2, const guint8 * s3, int p1, int p2, int p3, int p4, int n);
void orc_matrix3_100_u8 (guint8 * d1, const guint8 * s1, const guint8 * s2, const guint8 * s3, int p1, int p2, int p3, int n);
void orc_matrix3_100_offset_u8 (guint8 * d1, const guint8 * s1, const guint8 * s2, const guint8 * s3, int p1, int p2, int p3, int p4, int p5, int n);
void orc_matrix3_000_u8 (guint8 * d1, const guint8 * s1, const guint8 * s2, const guint8 * s3, int p1, int p2, int p3, int p4, int p5, int n);
void orc_pack_123x (guint32 * d1, const orc_uint8 * s1, const orc_uint8 * s2, const orc_uint8 * s3, int p1, int n);
void orc_pack_x123 (guint32 * d1, const orc_uint8 * s1, const orc_uint8 * s2, const orc_uint8 * s3, int p1, int n);
void cogorc_combine2_u8 (orc_uint8 * d1, const orc_uint8 * s1, const orc_uint8 * s2, int p1, int p2, int n);
void cogorc_combine4_u8 (orc_uint8 * d1, const orc_uint8 * s1, const orc_uint8 * s2, const orc_uint8 * s3, const orc_uint8 * s4, int p1, int p2, int p3, int p4, int n);
void cogorc_unpack_axyz_0 (orc_uint8 * d1, const orc_uint32 * s1, int n);
void cogorc_unpack_axyz_1 (orc_uint8 * d1, const orc_uint32 * s1, int n);
void cogorc_unpack_axyz_2 (orc_uint8 * d1, const orc_uint32 * s1, int n);
void cogorc_unpack_axyz_3 (orc_uint8 * d1, const orc_uint32 * s1, int n);

#ifdef __cplusplus
}
#endif

#endif

