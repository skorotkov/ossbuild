// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Gst {

	using System;
	using System.Collections;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class ElementFactory : Gst.PluginFeature {

		public ElementFactory(IntPtr raw) : base(raw) {}

		protected ElementFactory() : base(IntPtr.Zero)
		{
			CreateNativeObject (new string [0], new Gst.GLib.Value [0]);
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_element_factory_get_uri_protocols(IntPtr raw);

		public string[] UriProtocols {
			get  {
				IntPtr raw_ret = gst_element_factory_get_uri_protocols(Handle);
				string[] ret = Gst.Marshaller.NullTermPtrToStringArray (raw_ret, false);
				return ret;
			}
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern int gst_element_factory_get_uri_type(IntPtr raw);

		public Gst.URIType UriType {
			get  {
				int raw_ret = gst_element_factory_get_uri_type(Handle);
				Gst.URIType ret = (Gst.URIType) raw_ret;
				return ret;
			}
		}

		[StructLayout (LayoutKind.Sequential)]
		struct GstElementFactoryClass {
			[MarshalAs (UnmanagedType.ByValArray, SizeConst=4)]
			public IntPtr[] GstReserved;
		}

		static uint class_offset = ((Gst.GLib.GType) typeof (Gst.PluginFeature)).GetClassSize ();
		static Hashtable class_structs;

		static GstElementFactoryClass GetClassStruct (Gst.GLib.GType gtype, bool use_cache)
		{
			if (class_structs == null)
				class_structs = new Hashtable ();

			if (use_cache && class_structs.Contains (gtype))
				return (GstElementFactoryClass) class_structs [gtype];
			else {
				IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
				GstElementFactoryClass class_struct = (GstElementFactoryClass) Marshal.PtrToStructure (class_ptr, typeof (GstElementFactoryClass));
				if (use_cache)
					class_structs.Add (gtype, class_struct);
				return class_struct;
			}
		}

		static void OverrideClassStruct (Gst.GLib.GType gtype, GstElementFactoryClass class_struct)
		{
			IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
			Marshal.StructureToPtr (class_struct, class_ptr, false);
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_element_factory_get_longname(IntPtr raw);

		public string Longname { 
			get {
				IntPtr raw_ret = gst_element_factory_get_longname(Handle);
				string ret = Gst.GLib.Marshaller.Utf8PtrToString (raw_ret);
				return ret;
			}
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern uint gst_element_factory_get_num_pad_templates(IntPtr raw);

		public uint NumPadTemplates { 
			get {
				uint raw_ret = gst_element_factory_get_num_pad_templates(Handle);
				uint ret = raw_ret;
				return ret;
			}
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_element_factory_get_element_type(IntPtr raw);

		public Gst.GLib.GType ElementType { 
			get {
				IntPtr raw_ret = gst_element_factory_get_element_type(Handle);
				Gst.GLib.GType ret = new Gst.GLib.GType(raw_ret);
				return ret;
			}
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_element_factory_make(IntPtr factoryname, IntPtr name);

		public static Gst.Element Make(string factoryname, string name) {
			IntPtr native_factoryname = Gst.GLib.Marshaller.StringToPtrGStrdup (factoryname);
			IntPtr native_name = Gst.GLib.Marshaller.StringToPtrGStrdup (name);
			IntPtr raw_ret = gst_element_factory_make(native_factoryname, native_name);
			Gst.Element ret = Gst.GLib.Object.GetObject(raw_ret, true) as Gst.Element;
			Gst.GLib.Marshaller.Free (native_factoryname);
			Gst.GLib.Marshaller.Free (native_name);
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_element_factory_get_klass(IntPtr raw);

		public string Klass { 
			get {
				IntPtr raw_ret = gst_element_factory_get_klass(Handle);
				string ret = Gst.GLib.Marshaller.Utf8PtrToString (raw_ret);
				return ret;
			}
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_element_factory_find(IntPtr name);

		public static Gst.ElementFactory Find(string name) {
			IntPtr native_name = Gst.GLib.Marshaller.StringToPtrGStrdup (name);
			IntPtr raw_ret = gst_element_factory_find(native_name);
			Gst.ElementFactory ret = Gst.GLib.Object.GetObject(raw_ret) as Gst.ElementFactory;
			Gst.GLib.Marshaller.Free (native_name);
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_element_make_from_uri(int type, IntPtr uri, IntPtr elementname);

		public static Gst.Element MakeFromUri(Gst.URIType type, string uri, string elementname) {
			IntPtr native_uri = Gst.GLib.Marshaller.StringToPtrGStrdup (uri);
			IntPtr native_elementname = Gst.GLib.Marshaller.StringToPtrGStrdup (elementname);
			IntPtr raw_ret = gst_element_make_from_uri((int) type, native_uri, native_elementname);
			Gst.Element ret = Gst.GLib.Object.GetObject(raw_ret, true) as Gst.Element;
			Gst.GLib.Marshaller.Free (native_uri);
			Gst.GLib.Marshaller.Free (native_elementname);
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool gst_element_register(IntPtr plugin, IntPtr name, uint rank, IntPtr type);

		public static bool Register(Gst.Plugin plugin, string name, uint rank, Gst.GLib.GType type) {
			IntPtr native_name = Gst.GLib.Marshaller.StringToPtrGStrdup (name);
			bool raw_ret = gst_element_register(plugin == null ? IntPtr.Zero : plugin.Handle, native_name, rank, type.Val);
			bool ret = raw_ret;
			Gst.GLib.Marshaller.Free (native_name);
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool gst_element_factory_can_src_caps(IntPtr raw, IntPtr caps);

		public bool CanSrcCaps(Gst.Caps caps) {
			bool raw_ret = gst_element_factory_can_src_caps(Handle, caps == null ? IntPtr.Zero : caps.Handle);
			bool ret = raw_ret;
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool gst_element_factory_can_sink_caps(IntPtr raw, IntPtr caps);

		public bool CanSinkCaps(Gst.Caps caps) {
			bool raw_ret = gst_element_factory_can_sink_caps(Handle, caps == null ? IntPtr.Zero : caps.Handle);
			bool ret = raw_ret;
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool gst_element_factory_has_interface(IntPtr raw, IntPtr interfacename);

		public bool HasInterface(string interfacename) {
			IntPtr native_interfacename = Gst.GLib.Marshaller.StringToPtrGStrdup (interfacename);
			bool raw_ret = gst_element_factory_has_interface(Handle, native_interfacename);
			bool ret = raw_ret;
			Gst.GLib.Marshaller.Free (native_interfacename);
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_element_factory_get_description(IntPtr raw);

		public string Description { 
			get {
				IntPtr raw_ret = gst_element_factory_get_description(Handle);
				string ret = Gst.GLib.Marshaller.Utf8PtrToString (raw_ret);
				return ret;
			}
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_element_factory_create(IntPtr raw, IntPtr name);

		public Gst.Element Create(string name) {
			IntPtr native_name = Gst.GLib.Marshaller.StringToPtrGStrdup (name);
			IntPtr raw_ret = gst_element_factory_create(Handle, native_name);
			Gst.Element ret = Gst.GLib.Object.GetObject(raw_ret, true) as Gst.Element;
			Gst.GLib.Marshaller.Free (native_name);
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_element_factory_get_type();

		public static new Gst.GLib.GType GType { 
			get {
				IntPtr raw_ret = gst_element_factory_get_type();
				Gst.GLib.GType ret = new Gst.GLib.GType(raw_ret);
				return ret;
			}
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_element_factory_get_author(IntPtr raw);

		public string Author { 
			get {
				IntPtr raw_ret = gst_element_factory_get_author(Handle);
				string ret = Gst.GLib.Marshaller.Utf8PtrToString (raw_ret);
				return ret;
			}
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_element_factory_get_static_pad_templates(IntPtr raw);

		public Gst.StaticPadTemplate[] StaticPadTemplates { 
			get {
				IntPtr raw_ret = gst_element_factory_get_static_pad_templates(Handle);
				Gst.StaticPadTemplate[] ret = (Gst.StaticPadTemplate[]) Gst.GLib.Marshaller.ListPtrToArray (raw_ret, typeof(Gst.GLib.List), false, false, typeof(Gst.StaticPadTemplate));
				return ret;
			}
		}

#endregion
#region Customized extensions
#line 1 "ElementFactory.custom"
public static Gst.Element Make (string factoryname) {
  return Make (factoryname, null);
}

#endregion
	}
}
