; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{693CD20A-667F-4441-975D-D62F38BEEBDF}
AppName=GStreamer WinBuild  (LGPL)
AppVerName=GStreamer WinBuild (LGPL) 0.10.4
AppPublisher=GStreamer WinBuild Team
AppPublisherURL=http://www.gstreamer-winbuild.ylatuya.es
AppSupportURL=http://www.gstreamer-winbuild.ylatuya.es
AppUpdatesURL=http://www.gstreamer-winbuild.ylatuya.es
DefaultDirName=c:\gstreamer
DisableDirPage=yes
DefaultGroupName=GStreamer WinBuild
LicenseFile=..\..\Licenses\lgpl.txt
SetupIconFile=..\..\Main\GStreamer\GStreamer.ico
;InfoBeforeFile=..\..\ReadMe.txt
OutputDir=.
OutputBaseFilename=GStreamerWinBuild-0.10.4-LGPL
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
Source: "..\..\Build\Windows\Win32\Release\bin\gstreamer\*.dll"; DestDir: "{app}\bin"; Flags: ignoreversion ; Excludes: "libgstgl.dll"
Source: "..\..\Build\Windows\Win32\Release\bin\gstreamer\*.exe"; DestDir: "{app}\bin"; Flags: ignoreversion
Source: "..\..\Shared\Build\Windows\Win32\bin\*.dll"; DestDir: "{app}\bin"; Flags: ignoreversion   ; Excludes: "xvidcore.dll,libwavpack-1.dll,liba52-0.dll,libmpeg2-0.dll,libdca-0.dll,libfaac-0.dll,libfaad-2.dll,libdl.dll,libdvdread-4.dll,libdvdnav-4.dll,libdvdcss-2.dll, glew32.dll"
Source: "..\..\Licenses\*"; DestDir: "{app}\share\licenses"; Flags: ignoreversion
Source: "..\..\Libraries\ReadMe - GPL.txt"; DestDir: "{app}\share\licenses"; Flags: ignoreversion
Source: "..\..\Libraries\ReadMe-BSD.txt"; DestDir: "{app}\share\licenses"; Flags: ignoreversion
Source: ".\COPYING"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\Build\Windows\Win32\Release\bin\gstreamer\plugins\*.dll"; DestDir: "{app}\lib\gstreamer-0.10"; Flags: "ignoreversion"; Excludes: "libgstffmpeggpl.dll,libgstdshowdecwrapper.dll,libnice.dll, libgstxvid.dll, libgsta52.dll, libgstmpeg2dec.dll, libgstfaad.dll. libgstfaac.dll, libgstdvdread.dll, libgstresindvd.dll, libgstdts.dll, libgstopengl.dll"
Source: "..\..\Shared\Build\Windows\Win32\etc\*"; DestDir: "{app}\etc"; Flags: ignoreversion  recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{cm:UninstallProgram,GStreamer WinBuild}"; Filename: "{uninstallexe}"

[Icons]
Name: "{group}\{cm:UninstallProgram,GStreamer WinBuild}"; Filename: "{uninstallexe}"

[Registry]
Root: HKCU; Subkey: "Environment"; ValueName: "PATH"; ValueType: string; ValueData: "{code:BinPath|{olddata}}"
Root: HKCU; Subkey: "Environment"; ValueName: "GST_PLUGIN_PATH"; ValueType: string; ValueData: "{app}\lib\gstreamer-0.10"; Flags: uninsdeletevalue

[Code]
function BinPath(Param: String): String;
var
   GstPath: String;
   p: Integer;
begin
   GstPath := ExpandConstant('{app}') + '\bin';
   p := Pos(GstPath, Param);
   if 0 < p then
       Result := Param
   else
       Result := Param + ';' + GstPath;
end;
