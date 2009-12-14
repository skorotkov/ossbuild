// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace GstSharp {

	using System;
	using System.Runtime.InteropServices;

#region Autogenerated code
	[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
	internal delegate int PadChainFunctionNative(IntPtr pad, IntPtr buffer);

	internal class PadChainFunctionInvoker {

		PadChainFunctionNative native_cb;
		IntPtr __data;
		Gst.GLib.DestroyNotify __notify;

		~PadChainFunctionInvoker ()
		{
			if (__notify == null)
				return;
			__notify (__data);
		}

		internal PadChainFunctionInvoker (PadChainFunctionNative native_cb) : this (native_cb, IntPtr.Zero, null) {}

		internal PadChainFunctionInvoker (PadChainFunctionNative native_cb, IntPtr data) : this (native_cb, data, null) {}

		internal PadChainFunctionInvoker (PadChainFunctionNative native_cb, IntPtr data, Gst.GLib.DestroyNotify notify)
		{
			this.native_cb = native_cb;
			__data = data;
			__notify = notify;
		}

		internal Gst.PadChainFunction Handler {
			get {
				return new Gst.PadChainFunction(InvokeNative);
			}
		}

		Gst.FlowReturn InvokeNative (Gst.Pad pad, Gst.Buffer buffer)
		{
			Gst.FlowReturn result = (Gst.FlowReturn) native_cb (pad == null ? IntPtr.Zero : pad.Handle, buffer == null ? IntPtr.Zero : buffer.OwnedHandle);
			return result;
		}
	}

	internal class PadChainFunctionWrapper {

		public int NativeCallback (IntPtr pad, IntPtr buffer)
		{
			try {
				Gst.FlowReturn __ret = managed (Gst.GLib.Object.GetObject(pad) as Gst.Pad, Gst.MiniObject.GetObject(buffer, true) as Gst.Buffer);
				if (release_on_call)
					gch.Free ();
				return (int) __ret;
			} catch (Exception e) {
				Gst.GLib.ExceptionManager.RaiseUnhandledException (e, true);
				// NOTREACHED: Above call does not return.
				throw e;
			}
		}

		bool release_on_call = false;
		GCHandle gch;

		public void PersistUntilCalled ()
		{
			release_on_call = true;
			gch = GCHandle.Alloc (this);
		}

		internal PadChainFunctionNative NativeDelegate;
		Gst.PadChainFunction managed;

		public PadChainFunctionWrapper (Gst.PadChainFunction managed)
		{
			this.managed = managed;
			if (managed != null)
				NativeDelegate = new PadChainFunctionNative (NativeCallback);
		}

		public static Gst.PadChainFunction GetManagedDelegate (PadChainFunctionNative native)
		{
			if (native == null)
				return null;
			PadChainFunctionWrapper wrapper = (PadChainFunctionWrapper) native.Target;
			if (wrapper == null)
				return null;
			return wrapper.managed;
		}
	}
#endregion
}
