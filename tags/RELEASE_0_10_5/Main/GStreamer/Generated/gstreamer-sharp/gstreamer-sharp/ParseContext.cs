// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Gst {

	using System;
	using System.Collections;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class ParseContext : Gst.GLib.Opaque {

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_parse_context_get_missing_elements(IntPtr raw);

		public string[] MissingElements { 
			get {
				IntPtr raw_ret = gst_parse_context_get_missing_elements(Handle);
				string[] ret = Gst.Marshaller.NullTermPtrToStringArray (raw_ret, true);
				return ret;
			}
		}

		public ParseContext(IntPtr raw) : base(raw) {}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_parse_context_new();

		public ParseContext () 
		{
			Raw = gst_parse_context_new();
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gst_parse_context_free(IntPtr raw);

		protected override void Free (IntPtr raw)
		{
			gst_parse_context_free (raw);
		}

		class FinalizerInfo {
			IntPtr handle;

			public FinalizerInfo (IntPtr handle)
			{
				this.handle = handle;
			}

			public bool Handler ()
			{
				gst_parse_context_free (handle);
				return false;
			}
		}

		~ParseContext ()
		{
			if (!Owned)
				return;
			FinalizerInfo info = new FinalizerInfo (Handle);
			Gst.GLib.Timeout.Add (50, new Gst.GLib.TimeoutHandler (info.Handler));
		}

#endregion
	}
}
