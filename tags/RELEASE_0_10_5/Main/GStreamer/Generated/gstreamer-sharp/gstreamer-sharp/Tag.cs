// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Gst {

	using System;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class Tag {

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gst_tag_register(IntPtr name, int flag, IntPtr type, IntPtr nick, IntPtr blurb, GstSharp.TagMergeFuncNative func);

		public static void Register(string name, Gst.TagFlags flag, Gst.GLib.GType type, string nick, string blurb, Gst.TagMergeFunc func) {
			IntPtr native_name = Gst.GLib.Marshaller.StringToPtrGStrdup (name);
			IntPtr native_nick = Gst.GLib.Marshaller.StringToPtrGStrdup (nick);
			IntPtr native_blurb = Gst.GLib.Marshaller.StringToPtrGStrdup (blurb);
			GstSharp.TagMergeFuncWrapper func_wrapper = new GstSharp.TagMergeFuncWrapper (func);
			gst_tag_register(native_name, (int) flag, type.Val, native_nick, native_blurb, func_wrapper.NativeDelegate);
			Gst.GLib.Marshaller.Free (native_name);
			Gst.GLib.Marshaller.Free (native_nick);
			Gst.GLib.Marshaller.Free (native_blurb);
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern int gst_tag_get_flag(IntPtr tag);

		public static Gst.TagFlags GetFlag(string tag) {
			IntPtr native_tag = Gst.GLib.Marshaller.StringToPtrGStrdup (tag);
			int raw_ret = gst_tag_get_flag(native_tag);
			Gst.TagFlags ret = (Gst.TagFlags) raw_ret;
			Gst.GLib.Marshaller.Free (native_tag);
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool gst_tag_is_fixed(IntPtr tag);

		public static bool IsFixed(string tag) {
			IntPtr native_tag = Gst.GLib.Marshaller.StringToPtrGStrdup (tag);
			bool raw_ret = gst_tag_is_fixed(native_tag);
			bool ret = raw_ret;
			Gst.GLib.Marshaller.Free (native_tag);
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_tag_get_description(IntPtr tag);

		public static string GetDescription(string tag) {
			IntPtr native_tag = Gst.GLib.Marshaller.StringToPtrGStrdup (tag);
			IntPtr raw_ret = gst_tag_get_description(native_tag);
			string ret = Gst.GLib.Marshaller.Utf8PtrToString (raw_ret);
			Gst.GLib.Marshaller.Free (native_tag);
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool gst_tag_exists(IntPtr tag);

		public static bool Exists(string tag) {
			IntPtr native_tag = Gst.GLib.Marshaller.StringToPtrGStrdup (tag);
			bool raw_ret = gst_tag_exists(native_tag);
			bool ret = raw_ret;
			Gst.GLib.Marshaller.Free (native_tag);
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_tag_get_type(IntPtr tag);

		public static Gst.GLib.GType GetGType(string tag) {
			IntPtr native_tag = Gst.GLib.Marshaller.StringToPtrGStrdup (tag);
			IntPtr raw_ret = gst_tag_get_type(native_tag);
			Gst.GLib.GType ret = new Gst.GLib.GType(raw_ret);
			Gst.GLib.Marshaller.Free (native_tag);
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_tag_get_nick(IntPtr tag);

		public static string GetNick(string tag) {
			IntPtr native_tag = Gst.GLib.Marshaller.StringToPtrGStrdup (tag);
			IntPtr raw_ret = gst_tag_get_nick(native_tag);
			string ret = Gst.GLib.Marshaller.Utf8PtrToString (raw_ret);
			Gst.GLib.Marshaller.Free (native_tag);
			return ret;
		}

#endregion
#region Customized extensions
#line 1 "Tag.custom"
public static System.Type GetType (string tag) {
  Gst.GLib.GType gt = GetGType (tag);
  return (Type) gt;
}

public static void Register (string name, Gst.TagFlags flag, System.Type type, string nick, string blurb, Gst.TagMergeFunc func) {
  Gst.GLib.GType gt = (Gst.GLib.GType) (type);

  Register (name, flag, gt, nick, blurb, func);
}

[DllImport ("libgstreamer-0.10.dll") ]
static extern void gst_tag_merge_strings_with_comma (out Gst.GLib.Value dest, ref Gst.GLib.Value src);

public static void MergeStringsWithComma (out Gst.GLib.Value dest, ref Gst.GLib.Value src) {
  gst_tag_merge_strings_with_comma (out dest, ref src);
}

[DllImport ("libgstreamer-0.10.dll") ]
static extern void gst_tag_merge_use_first (out Gst.GLib.Value dest, ref Gst.GLib.Value src);

public static void MergeUseFirst (out Gst.GLib.Value dest, ref Gst.GLib.Value src) {
  gst_tag_merge_use_first (out dest, ref src);
}

		 public const string Title = "title";
		 public const string TitleSortname = "title-sortname";
		 public const string Artist = "artist";
		 public const string ArtistSortname = "musicbrainz-sortname";
		 public const string Album = "album";
		 public const string AlbumSortname = "album-sortname";
		 public const string AlbumArtist = "album-artist";
		 public const string AlbumArtistSortname = "album-artist-sortname";
		 public const string Composer = "composer";
		 public const string Date = "date";
		 public const string Genre = "genre";
		 public const string Comment = "comment";
		 public const string ExtendedComment = "extended-comment";
		 public const string TrackNumber = "track-number";
		 public const string TrackCount = "track-count";
		 public const string AlbumVolumeNumber = "album-disc-number";
		 public const string AlbumVolumeCount = "album-disc-count";
		 public const string Location = "location";
		 public const string Homepage = "homepage";
		 public const string Description = "description";
		 public const string Version = "version";
		 public const string Isrc = "isrc";
		 public const string Organization = "organization";
		 public const string Copyright = "copyright";
		 public const string CopyrightUri = "copyright-uri";
		 public const string Contact = "contact";
		 public const string License = "license";
		 public const string LicenseUri = "license-uri";
		 public const string Performer = "performer";
		 public const string Duration = "duration";
		 public const string Codec = "codec";
		 public const string VideoCodec = "video-codec";
		 public const string AudioCodec = "audio-codec";
		 public const string SubtitleCodec = "subtitle-codec";
		 public const string ContainerFormat = "container-format";
		 public const string Bitrate = "bitrate";
		 public const string NominalBitrate = "nominal-bitrate";
		 public const string MinimumBitrate = "minimum-bitrate";
		 public const string MaximumBitrate = "maximum-bitrate";
		 public const string Serial = "serial";
		 public const string Encoder = "encoder";
		 public const string EncoderVersion = "encoder-version";
		 public const string TrackGain = "replaygain-track-gain";
		 public const string TrackPeak = "replaygain-track-peak";
		 public const string AlbumGain = "replaygain-album-gain";
		 public const string AlbumPeak = "replaygain-album-peak";
		 public const string ReferenceLevel = "replaygain-reference-level";
		 public const string LanguageCode = "language-code";
		 public const string Image = "image";
		 public const string PreviewImage = "preview-image";
		 public const string Attachment = "attachment";
		 public const string BeatsPerMinute = "beats-per-minute";
		 public const string Keywords = "keywords";
		 public const string GeoLocationName = "geo-location-name";
		 public const string GeoLocationLatitude = "geo-location-latitude";
		 public const string GeoLocationLongitude = "geo-location-longitude";
		 public const string GeoLocationElevation = "geo-location-elevation";

#endregion
	}
}
