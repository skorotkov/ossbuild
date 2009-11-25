// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Gst {

	using System;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class ChildProxyAdapter : Gst.GLib.GInterfaceAdapter, Gst.ChildProxy {

		[StructLayout (LayoutKind.Sequential)]
		struct GstChildProxyInterface {
			public GetChildByIndexNativeDelegate GetChildByIndex;
			public GetChildrenCountNativeDelegate GetChildrenCount;
			IntPtr ChildAdded;
			IntPtr ChildRemoved;
			[MarshalAs (UnmanagedType.ByValArray, SizeConst=4)]
			public IntPtr[] GstReserved;
		}

		static GstChildProxyInterface iface;

		static ChildProxyAdapter ()
		{
			Gst.GLib.GType.Register (_gtype, typeof(ChildProxyAdapter));
			iface.GetChildByIndex = new GetChildByIndexNativeDelegate (GetChildByIndex_cb);
			iface.GetChildrenCount = new GetChildrenCountNativeDelegate (GetChildrenCount_cb);
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate IntPtr GetChildByIndexNativeDelegate (IntPtr inst, uint index);

		static IntPtr GetChildByIndex_cb (IntPtr inst, uint index)
		{
			try {
				ChildProxyImplementor __obj = Gst.GLib.Object.GetObject (inst, false) as ChildProxyImplementor;
				Gst.Object __result = __obj.GetChildByIndex (index);
				return __result == null ? IntPtr.Zero : __result.OwnedHandle;
			} catch (Exception e) {
				Gst.GLib.ExceptionManager.RaiseUnhandledException (e, true);
				// NOTREACHED: above call does not return.
				throw e;
			}
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate uint GetChildrenCountNativeDelegate (IntPtr inst);

		static uint GetChildrenCount_cb (IntPtr inst)
		{
			try {
				ChildProxyImplementor __obj = Gst.GLib.Object.GetObject (inst, false) as ChildProxyImplementor;
				uint __result = __obj.ChildrenCount;
				return __result;
			} catch (Exception e) {
				Gst.GLib.ExceptionManager.RaiseUnhandledException (e, true);
				// NOTREACHED: above call does not return.
				throw e;
			}
		}

		static int class_offset = 2 * IntPtr.Size;

		static void Initialize (IntPtr ptr, IntPtr data)
		{
			IntPtr ifaceptr = new IntPtr (ptr.ToInt64 () + class_offset);
			GstChildProxyInterface native_iface = (GstChildProxyInterface) Marshal.PtrToStructure (ifaceptr, typeof (GstChildProxyInterface));
			native_iface.GetChildByIndex = iface.GetChildByIndex;
			native_iface.GetChildrenCount = iface.GetChildrenCount;
			Marshal.StructureToPtr (native_iface, ifaceptr, false);
			GCHandle gch = (GCHandle) data;
			gch.Free ();
		}

		Gst.GLib.Object implementor;

		public ChildProxyAdapter ()
		{
			InitHandler = new Gst.GLib.GInterfaceInitHandler (Initialize);
		}

		public ChildProxyAdapter (ChildProxyImplementor implementor)
		{
			if (implementor == null)
				throw new ArgumentNullException ("implementor");
			else if (!(implementor is Gst.GLib.Object))
				throw new ArgumentException ("implementor must be a subclass of Gst.GLib.Object");
			this.implementor = implementor as Gst.GLib.Object;
		}

		public ChildProxyAdapter (IntPtr handle)
		{
			if (!_gtype.IsInstance (handle))
				throw new ArgumentException ("The gobject doesn't implement the GInterface of this adapter", "handle");
			implementor = Gst.GLib.Object.GetObject (handle);
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_child_proxy_get_type();

		private static Gst.GLib.GType _gtype = new Gst.GLib.GType (gst_child_proxy_get_type ());

		public override Gst.GLib.GType GType {
			get {
				return _gtype;
			}
		}

		public override IntPtr Handle {
			get {
				return implementor.Handle;
			}
		}

		public IntPtr OwnedHandle {
			get {
				return implementor.OwnedHandle;
			}
		}

		public static ChildProxy GetObject (IntPtr handle, bool owned)
		{
			Gst.GLib.Object obj = Gst.GLib.Object.GetObject (handle, owned);
			return GetObject (obj);
		}

		public static ChildProxy GetObject (Gst.GLib.Object obj)
		{
			if (obj == null)
				return null;
			else if (obj is ChildProxyImplementor)
				return new ChildProxyAdapter (obj as ChildProxyImplementor);
			else if (obj as ChildProxy == null)
				return new ChildProxyAdapter (obj.Handle);
			else
				return obj as ChildProxy;
		}

		public ChildProxyImplementor Implementor {
			get {
				return implementor as ChildProxyImplementor;
			}
		}

		[Gst.GLib.Signal("child-added")]
		public event Gst.ChildAddedHandler ChildAdded {
			add {
				Gst.GLib.Signal sig = Gst.GLib.Signal.Lookup (Gst.GLib.Object.GetObject (Handle), "child-added", typeof (Gst.ChildAddedArgs));
				sig.AddDelegate (value);
			}
			remove {
				Gst.GLib.Signal sig = Gst.GLib.Signal.Lookup (Gst.GLib.Object.GetObject (Handle), "child-added", typeof (Gst.ChildAddedArgs));
				sig.RemoveDelegate (value);
			}
		}

		[Gst.GLib.Signal("child-removed")]
		public event Gst.ChildRemovedHandler ChildRemoved {
			add {
				Gst.GLib.Signal sig = Gst.GLib.Signal.Lookup (Gst.GLib.Object.GetObject (Handle), "child-removed", typeof (Gst.ChildRemovedArgs));
				sig.AddDelegate (value);
			}
			remove {
				Gst.GLib.Signal sig = Gst.GLib.Signal.Lookup (Gst.GLib.Object.GetObject (Handle), "child-removed", typeof (Gst.ChildRemovedArgs));
				sig.RemoveDelegate (value);
			}
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gst_child_proxy_child_added(IntPtr raw, IntPtr child);

		public void EmitChildAdded(Gst.Object child) {
			gst_child_proxy_child_added(Handle, child == null ? IntPtr.Zero : child.Handle);
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_child_proxy_get_child_by_name(IntPtr raw, IntPtr name);

		public Gst.Object GetChildByName(string name) {
			IntPtr native_name = Gst.GLib.Marshaller.StringToPtrGStrdup (name);
			IntPtr raw_ret = gst_child_proxy_get_child_by_name(Handle, native_name);
			Gst.Object ret = Gst.GLib.Object.GetObject(raw_ret, true) as Gst.Object;
			Gst.GLib.Marshaller.Free (native_name);
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_child_proxy_get_child_by_index(IntPtr raw, uint index);

		public Gst.Object GetChildByIndex(uint index) {
			IntPtr raw_ret = gst_child_proxy_get_child_by_index(Handle, index);
			Gst.Object ret = Gst.GLib.Object.GetObject(raw_ret, true) as Gst.Object;
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern uint gst_child_proxy_get_children_count(IntPtr raw);

		public uint ChildrenCount { 
			get {
				uint raw_ret = gst_child_proxy_get_children_count(Handle);
				uint ret = raw_ret;
				return ret;
			}
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gst_child_proxy_child_removed(IntPtr raw, IntPtr child);

		public void EmitChildRemoved(Gst.Object child) {
			gst_child_proxy_child_removed(Handle, child == null ? IntPtr.Zero : child.Handle);
		}

#endregion
	}
}
