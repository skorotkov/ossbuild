// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Gst.Interfaces {

	using System;

	public delegate void FrequencyChangedHandler(object o, FrequencyChangedArgs args);

	public class FrequencyChangedArgs : Gst.GLib.SignalArgs {
		public ulong Frequency{
			get {
				return (ulong) Args [0];
			}
		}

	}
}
