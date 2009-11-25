// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace GstSharp {

	using System;
	using System.Runtime.InteropServices;

#region Autogenerated code
	[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
	internal delegate bool ClockCallbackNative(IntPtr clock, ulong time, IntPtr id, IntPtr user_data);

	internal class ClockCallbackInvoker {

		ClockCallbackNative native_cb;
		IntPtr __data;
		Gst.GLib.DestroyNotify __notify;

		~ClockCallbackInvoker ()
		{
			if (__notify == null)
				return;
			__notify (__data);
		}

		internal ClockCallbackInvoker (ClockCallbackNative native_cb) : this (native_cb, IntPtr.Zero, null) {}

		internal ClockCallbackInvoker (ClockCallbackNative native_cb, IntPtr data) : this (native_cb, data, null) {}

		internal ClockCallbackInvoker (ClockCallbackNative native_cb, IntPtr data, Gst.GLib.DestroyNotify notify)
		{
			this.native_cb = native_cb;
			__data = data;
			__notify = notify;
		}

		internal Gst.ClockCallback Handler {
			get {
				return new Gst.ClockCallback(InvokeNative);
			}
		}

		bool InvokeNative (Gst.Clock clock, ulong time, Gst.ClockEntry id)
		{
			bool result = native_cb (clock == null ? IntPtr.Zero : clock.Handle, time, id == null ? IntPtr.Zero : id.Handle, __data);
			return result;
		}
	}

	internal class ClockCallbackWrapper {

		public bool NativeCallback (IntPtr clock, ulong time, IntPtr id, IntPtr user_data)
		{
			try {
				bool __ret = managed (Gst.GLib.Object.GetObject(clock) as Gst.Clock, time, id == IntPtr.Zero ? null : (Gst.ClockEntry) Gst.GLib.Opaque.GetOpaque (id, typeof (Gst.ClockEntry), false));
				if (release_on_call)
					gch.Free ();
				return __ret;
			} catch (Exception e) {
				Gst.GLib.ExceptionManager.RaiseUnhandledException (e, false);
				return false;
			}
		}

		bool release_on_call = false;
		GCHandle gch;

		public void PersistUntilCalled ()
		{
			release_on_call = true;
			gch = GCHandle.Alloc (this);
		}

		internal ClockCallbackNative NativeDelegate;
		Gst.ClockCallback managed;

		public ClockCallbackWrapper (Gst.ClockCallback managed)
		{
			this.managed = managed;
			if (managed != null)
				NativeDelegate = new ClockCallbackNative (NativeCallback);
		}

		public static Gst.ClockCallback GetManagedDelegate (ClockCallbackNative native)
		{
			if (native == null)
				return null;
			ClockCallbackWrapper wrapper = (ClockCallbackWrapper) native.Target;
			if (wrapper == null)
				return null;
			return wrapper.managed;
		}
	}
#endregion
}
