// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Gst {

	using System;
	using System.Runtime.InteropServices;

#region Autogenerated code
	[Flags]
	[Gst.GLib.GType (typeof (Gst.PluginDependencyFlagsGType))]
	public enum PluginDependencyFlags {

		None,
		Recurse = 1 << 0,
		PathsAreDefaultOnly = 1 << 1,
		FileNameIsSuffix = 1 << 2,
	}

	internal class PluginDependencyFlagsGType {
		[DllImport ("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_plugin_dependency_flags_get_type ();

		public static Gst.GLib.GType GType {
			get {
				return new Gst.GLib.GType (gst_plugin_dependency_flags_get_type ());
			}
		}
	}
#endregion
}
