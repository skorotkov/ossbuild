// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Gst {

	using System;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class Util {

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_format_get_name(int format);

		public static string FormatGetName(Gst.Format format) {
			IntPtr raw_ret = gst_format_get_name((int) format);
			string ret = Gst.GLib.Marshaller.Utf8PtrToString (raw_ret);
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern int gst_format_get_by_nick(IntPtr nick);

		public static Gst.Format FormatGetByNick(string nick) {
			IntPtr native_nick = Gst.GLib.Marshaller.StringToPtrGStrdup (nick);
			int raw_ret = gst_format_get_by_nick(native_nick);
			Gst.Format ret = (Gst.Format) raw_ret;
			Gst.GLib.Marshaller.Free (native_nick);
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern ulong gst_util_uint64_scale_int(ulong val, int num, int denom);

		public static ulong ScaleUInt64(ulong val, int num, int denom) {
			ulong raw_ret = gst_util_uint64_scale_int(val, num, denom);
			ulong ret = raw_ret;
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_format_get_details(int format);

		public static Gst.FormatDefinition FormatGetDetails(Gst.Format format) {
			IntPtr raw_ret = gst_format_get_details((int) format);
			Gst.FormatDefinition ret = Gst.FormatDefinition.New (raw_ret);
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern ulong gst_util_uint64_scale_int_ceil(ulong val, int num, int denom);

		public static ulong ScaleUInt64Ceil(ulong val, int num, int denom) {
			ulong raw_ret = gst_util_uint64_scale_int_ceil(val, num, denom);
			ulong ret = raw_ret;
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern int gst_format_register(IntPtr nick, IntPtr description);

		public static Gst.Format FormatRegister(string nick, string description) {
			IntPtr native_nick = Gst.GLib.Marshaller.StringToPtrGStrdup (nick);
			IntPtr native_description = Gst.GLib.Marshaller.StringToPtrGStrdup (description);
			int raw_ret = gst_format_register(native_nick, native_description);
			Gst.Format ret = (Gst.Format) raw_ret;
			Gst.GLib.Marshaller.Free (native_nick);
			Gst.GLib.Marshaller.Free (native_description);
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_element_state_change_return_get_name(int state_ret);

		public static string StateChangeReturnGetName(Gst.StateChangeReturn state_ret) {
			IntPtr raw_ret = gst_element_state_change_return_get_name((int) state_ret);
			string ret = Gst.GLib.Marshaller.Utf8PtrToString (raw_ret);
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern ulong gst_util_uint64_scale_int_round(ulong val, int num, int denom);

		public static ulong ScaleUInt64Round(ulong val, int num, int denom) {
			ulong raw_ret = gst_util_uint64_scale_int_round(val, num, denom);
			ulong ret = raw_ret;
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern ulong gst_util_uint64_scale(ulong val, ulong num, ulong denom);

		public static ulong ScaleUInt64(ulong val, ulong num, ulong denom) {
			ulong raw_ret = gst_util_uint64_scale(val, num, denom);
			ulong ret = raw_ret;
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern ulong gst_util_get_timestamp();

		public static ulong Timestamp { 
			get {
				ulong raw_ret = gst_util_get_timestamp();
				ulong ret = raw_ret;
				return ret;
			}
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool gst_uri_protocol_is_supported(int type, IntPtr protocol);

		public static bool UriProtocolIsSupported(Gst.URIType type, string protocol) {
			IntPtr native_protocol = Gst.GLib.Marshaller.StringToPtrGStrdup (protocol);
			bool raw_ret = gst_uri_protocol_is_supported((int) type, native_protocol);
			bool ret = raw_ret;
			Gst.GLib.Marshaller.Free (native_protocol);
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_element_state_get_name(int state);

		public static string StateGetName(Gst.State state) {
			IntPtr raw_ret = gst_element_state_get_name((int) state);
			string ret = Gst.GLib.Marshaller.Utf8PtrToString (raw_ret);
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_flow_get_name(int flow_ret);

		public static string FlowGetName(Gst.FlowReturn flow_ret) {
			IntPtr raw_ret = gst_flow_get_name((int) flow_ret);
			string ret = Gst.GLib.Marshaller.Utf8PtrToString (raw_ret);
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern uint gst_util_seqnum_next();

		public static uint SeqnumNext() {
			uint raw_ret = gst_util_seqnum_next();
			uint ret = raw_ret;
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern ulong gst_util_uint64_scale_ceil(ulong val, ulong num, ulong denom);

		public static ulong ScaleUInt64Ceil(ulong val, ulong num, ulong denom) {
			ulong raw_ret = gst_util_uint64_scale_ceil(val, num, denom);
			ulong ret = raw_ret;
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern ulong gst_util_uint64_scale_round(ulong val, ulong num, ulong denom);

		public static ulong ScaleUInt64Round(ulong val, ulong num, ulong denom) {
			ulong raw_ret = gst_util_uint64_scale_round(val, num, denom);
			ulong ret = raw_ret;
			return ret;
		}

#endregion
	}
}
