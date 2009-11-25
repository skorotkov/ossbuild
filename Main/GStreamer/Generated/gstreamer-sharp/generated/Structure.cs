// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Gst {

	using System;
	using System.Collections;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class Structure : Gst.GLib.Opaque {

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_structure_to_string(IntPtr raw);

		public override string ToString() {
			IntPtr raw_ret = gst_structure_to_string(Handle);
			string ret = Gst.GLib.Marshaller.PtrToStringGFree(raw_ret);
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_structure_copy(IntPtr raw);

		public Gst.Structure Copy() {
			IntPtr raw_ret = gst_structure_copy(Handle);
			Gst.Structure ret = raw_ret == IntPtr.Zero ? null : (Gst.Structure) Gst.GLib.Opaque.GetOpaque (raw_ret, typeof (Gst.Structure), true);
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool gst_structure_has_field_typed(IntPtr raw, IntPtr fieldname, IntPtr type);

		public bool HasFieldTyped(string fieldname, Gst.GLib.GType type) {
			IntPtr native_fieldname = Gst.GLib.Marshaller.StringToPtrGStrdup (fieldname);
			bool raw_ret = gst_structure_has_field_typed(Handle, native_fieldname, type.Val);
			bool ret = raw_ret;
			Gst.GLib.Marshaller.Free (native_fieldname);
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool gst_structure_has_field(IntPtr raw, IntPtr fieldname);

		public bool HasField(string fieldname) {
			IntPtr native_fieldname = Gst.GLib.Marshaller.StringToPtrGStrdup (fieldname);
			bool raw_ret = gst_structure_has_field(Handle, native_fieldname);
			bool ret = raw_ret;
			Gst.GLib.Marshaller.Free (native_fieldname);
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_structure_get_value(IntPtr raw, IntPtr fieldname);

		public Gst.GLib.Value GetValue(string fieldname) {
			IntPtr native_fieldname = Gst.GLib.Marshaller.StringToPtrGStrdup (fieldname);
			IntPtr raw_ret = gst_structure_get_value(Handle, native_fieldname);
			Gst.GLib.Value ret = (Gst.GLib.Value) Marshal.PtrToStructure (raw_ret, typeof (Gst.GLib.Value));
			Gst.GLib.Marshaller.Free (native_fieldname);
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern int gst_structure_n_fields(IntPtr raw);

		public int Count { 
			get {
				int raw_ret = gst_structure_n_fields(Handle);
				int ret = raw_ret;
				return ret;
			}
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_structure_get_type();

		public static Gst.GLib.GType GType { 
			get {
				IntPtr raw_ret = gst_structure_get_type();
				Gst.GLib.GType ret = new Gst.GLib.GType(raw_ret);
				return ret;
			}
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_structure_nth_field_name(IntPtr raw, uint index);

		private string NthFieldName(uint index) {
			IntPtr raw_ret = gst_structure_nth_field_name(Handle, index);
			string ret = Gst.GLib.Marshaller.Utf8PtrToString (raw_ret);
			return ret;
		}

		public Structure(IntPtr raw) : base(raw) {}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_structure_empty_new(IntPtr name);

		public Structure (string name) 
		{
			IntPtr native_name = Gst.GLib.Marshaller.StringToPtrGStrdup (name);
			Raw = gst_structure_empty_new(native_name);
			Gst.GLib.Marshaller.Free (native_name);
		}

#endregion
#region Customized extensions
#line 1 "Structure.custom"
internal bool FreeNative = true;

[DllImport ("libgstreamer-0.10.dll") ]
static extern void gst_structure_free (IntPtr raw);

protected override void Free (IntPtr raw) {
  if (!FreeNative)
    return;

  gst_structure_free (raw);
}

class FinalizerInfo {
  IntPtr handle;

  public FinalizerInfo (IntPtr handle) {
    this.handle = handle;
  }

  public bool Handler () {
    gst_structure_free (handle);
    return false;
  }
}

~Structure () {
  if (!Owned || !FreeNative)
    return;
  FinalizerInfo info = new FinalizerInfo (Handle);
  Gst.GLib.Timeout.Add (50, new Gst.GLib.TimeoutHandler (info.Handler));
}

[DllImport ("libgstreamer-0.10.dll") ]
static extern IntPtr gst_structure_get_name (IntPtr raw);

[DllImport ("libgstreamer-0.10.dll") ]
static extern void gst_structure_set_name (IntPtr raw, IntPtr name);

public string Name {
  get  {
    IntPtr raw_ret = gst_structure_get_name (Handle);
    string ret = Gst.GLib.Marshaller.Utf8PtrToString (raw_ret);
    return ret;
  } set  {
    if (!IsMutable)
      throw new ApplicationException ();

    IntPtr native_value = Gst.GLib.Marshaller.StringToPtrGStrdup (value);
    gst_structure_set_name (Handle, native_value);
    Gst.GLib.Marshaller.Free (native_value);
  }
}

[DllImport ("libgstreamer-0.10.dll") ]
static extern void gst_structure_set_value (IntPtr raw, IntPtr fieldname, IntPtr value);

public void SetValue (string fieldname, Gst.GLib.Value value) {
  if (!IsMutable)
    throw new ApplicationException ();

  IntPtr native_fieldname = Gst.GLib.Marshaller.StringToPtrGStrdup (fieldname);
  IntPtr native_value = Gst.GLib.Marshaller.StructureToPtrAlloc (value);
  gst_structure_set_value (Handle, native_fieldname, native_value);
  Gst.GLib.Marshaller.Free (native_fieldname);
  value = (Gst.GLib.Value) Marshal.PtrToStructure (native_value, typeof (Gst.GLib.Value));
  Marshal.FreeHGlobal (native_value);
}

[DllImport ("libgstreamer-0.10.dll") ]
static extern bool gst_structure_fixate_field_boolean (IntPtr raw, IntPtr field_name, bool target);

public bool FixateFieldBoolean (string field_name, bool target) {
  if (!IsMutable)
    throw new ApplicationException ();

  IntPtr native_field_name = Gst.GLib.Marshaller.StringToPtrGStrdup (field_name);
  bool raw_ret = gst_structure_fixate_field_boolean (Handle, native_field_name, target);
  bool ret = raw_ret;
  Gst.GLib.Marshaller.Free (native_field_name);
  return ret;
}

[DllImport ("libgstreamer-0.10.dll") ]
static extern void gst_structure_remove_all_fields (IntPtr raw);

public void RemoveAllFields() {
  if (!IsMutable)
    throw new ApplicationException ();

  gst_structure_remove_all_fields (Handle);
}

[DllImport ("libgstreamer-0.10.dll") ]
static extern void gst_structure_remove_field (IntPtr raw, IntPtr fieldname);

public void RemoveField (string fieldname) {
  if (!IsMutable)
    throw new ApplicationException ();

  IntPtr native_fieldname = Gst.GLib.Marshaller.StringToPtrGStrdup (fieldname);
  gst_structure_remove_field (Handle, native_fieldname);
  Gst.GLib.Marshaller.Free (native_fieldname);
}

[DllImport ("libgstreamer-0.10.dll") ]
static extern bool gst_structure_fixate_field_nearest_double (IntPtr raw, IntPtr field_name, double target);

public bool FixateFieldNearestDouble (string field_name, double target) {
  if (!IsMutable)
    throw new ApplicationException ();

  IntPtr native_field_name = Gst.GLib.Marshaller.StringToPtrGStrdup (field_name);
  bool raw_ret = gst_structure_fixate_field_nearest_double (Handle, native_field_name, target);
  bool ret = raw_ret;
  Gst.GLib.Marshaller.Free (native_field_name);
  return ret;
}

[DllImport ("libgstreamer-0.10.dll") ]
static extern bool gst_structure_fixate_field_nearest_int (IntPtr raw, IntPtr field_name, int target);

public bool FixateFieldNearestInt (string field_name, int target) {
  if (!IsMutable)
    throw new ApplicationException ();

  IntPtr native_field_name = Gst.GLib.Marshaller.StringToPtrGStrdup (field_name);
  bool raw_ret = gst_structure_fixate_field_nearest_int (Handle, native_field_name, target);
  bool ret = raw_ret;
  Gst.GLib.Marshaller.Free (native_field_name);
  return ret;
}

[DllImport ("libgstreamer-0.10.dll") ]
static extern bool gst_structure_fixate_field_nearest_fraction (IntPtr raw, IntPtr field_name, int target_numerator, int target_denominator);

public bool FixateFieldNearestFraction (string field_name, int target_numerator, int target_denominator) {
  if (!IsMutable)
    throw new ApplicationException ();

  IntPtr native_field_name = Gst.GLib.Marshaller.StringToPtrGStrdup (field_name);
  bool raw_ret = gst_structure_fixate_field_nearest_fraction (Handle, native_field_name, target_numerator, target_denominator);
  bool ret = raw_ret;
  Gst.GLib.Marshaller.Free (native_field_name);
  return ret;
}


public Structure (string name, params object[] fields) : this (name) {
  Set (fields);
}

public object Get (string field) {
  Gst.GLib.Value v;

  v = GetValue (field);
  return v.Val;
}

public void Set (string field, object value) {
  Gst.GLib.Value v = new Gst.GLib.Value (value);
  SetValue (field, v);
  v.Dispose ();
}

public void Set (params object[] fields) {
  int i, length = fields.Length;

  if (length % 2 != 0)
    throw new ArgumentException ();

  for (i = 0; i < length; i += 2) {
    if (fields[i].GetType () != typeof (string))
      throw new ArgumentException ();

    Gst.GLib.Value v = new Gst.GLib.Value (fields[i+1]);
    SetValue (fields[i] as string, v);
    v.Dispose ();
  }
}

public object this [string field] {
  set {
    if (field == null)
      throw new ArgumentNullException ();

    Set (field, value);
  }
  get {
    if (field == null)
      throw new ArgumentNullException ();

    return Get (field);
  }
}

public string[] Fields {
  get {
    string[] fields = new string[Count];
    for (uint i = 0; i < Count; i++)
      fields[i] = NthFieldName (i);

    return fields;
  }
}

public static Structure FromString (string structure) {
  IntPtr raw_string = Gst.GLib.Marshaller.StringToPtrGStrdup (structure);
  IntPtr raw_ret = gst_structure_from_string (raw_string, IntPtr.Zero);
  Gst.GLib.Marshaller.Free (raw_string);
  Gst.Structure ret = raw_ret == IntPtr.Zero ? null : (Gst.Structure) Gst.GLib.Opaque.GetOpaque (raw_ret, typeof (Gst.Structure), true);
  return ret;
}

[DllImport ("libgstreamer-0.10.dll") ]
private static extern IntPtr gst_structure_from_string (IntPtr structure, IntPtr end);

public bool FixateFieldNearestFraction (string field_name, Fraction target) {
  return FixateFieldNearestFraction (field_name, target.Numerator, target.Denominator);
}


[DllImport ("gstreamersharpglue-0.10.dll") ]
extern static uint gstsharp_gst_structure_get_parent_refcount_offset ();

static uint parent_refcount_offset = gstsharp_gst_structure_get_parent_refcount_offset ();

public bool IsMutable {
  get {
    unsafe {
      int **parent_refcount = (int **) ( ( (byte*) Handle) + parent_refcount_offset);

      if (*parent_refcount == (int *) IntPtr.Zero)
        return true;
      if (**parent_refcount == 1)
        return true;

      return false;
    }
  }
}

internal void CreateNativeCopy () {
  FreeNative = false;
  Raw = gst_structure_copy (Raw);
  FreeNative = true;
}

#endregion
	}
}
