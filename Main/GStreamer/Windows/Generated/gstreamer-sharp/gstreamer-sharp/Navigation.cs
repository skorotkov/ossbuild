// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Gst.Interfaces {

	using System;

#region Autogenerated code
	public partial interface Navigation : Gst.GLib.IWrapper {

		void SendMouseEvent(string evnt, int button, double x, double y);
		void SendEvent(Gst.Structure structure);
		void SendKeyEvent(string evnt, string key);
		void SendCommand(Gst.Interfaces.NavigationCommand command);
	}

	[Gst.GLib.GInterface (typeof (NavigationAdapter))]
	public partial interface NavigationImplementor : Gst.GLib.IWrapper {

		void SendEvent (Gst.Structure structure);
	}
#endregion
}
