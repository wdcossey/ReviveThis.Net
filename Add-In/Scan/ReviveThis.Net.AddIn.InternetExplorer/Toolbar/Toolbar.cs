using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Win32;
using ReviveThis.AddIn.InternetExplorer.Toolbar.Entities;
using ReviveThis.AddIn.InternetExplorer.Toolbar.Enums;
using ReviveThis.Enums;
using ReviveThis.Interfaces;

namespace ReviveThis.AddIn.InternetExplorer.Toolbar
{

  #region [O3] Internet Explorer Toolbars / CheckOther3Item

  [Export(typeof (IDetectionAddIn))]
  public class Toolbar : IDetectionAddIn
  {
    #region consts

    //TODO: Move to common location (localization?)
    private const string NO_FILE = @"(no file)";
    private const string FILE_MISSING = @"(file missing)";
    private const string NO_NAME = @"(no name)";

    #endregion

    public string Author
    {
      get { return Consts.General.Author; }
    }

    public Version Version
    {
      get { return new Version(1, 0, 0, 0); }
    }

    public string Name
    {
      get { return @"Toolbars"; }
    }

    public string[] Description
    {
      get { return new[] {@"Scans for Internet Explorer Toolbars."}; }
    }

    public void Dispose()
    {
      //Nothing to dispose?
    }

    private static string GetFileName(RegistryView view, string clsid)
    {
      string result = null;

      using (
        var regKey =
          RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, view)
            .OpenSubKey(string.Format(@"CLSID\{0}\InprocServer32", clsid)))
      {
        if (regKey != null)
        {
          result = regKey.GetValue(null, null) as string;

          if (result != null)
          {
            result = string.Format("{0}{1}", result,
              !File.Exists(result) ? string.Format(" {0}", FILE_MISSING) : string.Empty);
          }
        }
      }

      if (string.IsNullOrEmpty(result))
      {
        //Fallback if we can't find the information with the 64bit hive.
        result = view == RegistryView.Registry64 ? GetFileName(RegistryView.Registry32, clsid) : null;
      }

      return result ?? NO_FILE;
    }

    private static string GetObjectName(RegistryView view, string clsid)
    {
      string result = null;

      using (
        var regKey =
          RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, view).OpenSubKey(string.Format(@"CLSID\{0}", clsid)))
      {
        if (regKey != null)
        {
          result = regKey.GetValue(null, null) as string;
        }
      }

      if (string.IsNullOrEmpty(result))
      {
        //Fallback if we can't find the information with the 64bit hive.
        result = view == RegistryView.Registry64 ? GetObjectName(RegistryView.Registry32, clsid) : null;
      }

      return result ?? NO_NAME;
    }

    public async Task<ICollection<IDetectionResultItem>> Scan()
    {
      //await Task.FromResult(0);
      
      var result = new Collection<IDetectionResultItem>();

      var regHive = RegistryHive.LocalMachine;

      var rootKey = RegistryKey.OpenBaseKey(regHive, RegistryView.Registry32);

      loopMe:

      //HKLM\Software\Microsoft\Internet Explorer\Toolbar
      using (var regKey = rootKey.OpenSubKey(@"Software\Microsoft\Internet Explorer\Toolbar"))
      {
        if (regKey != null && regKey.ValueCount > 0)
        {
          //enumerate MSIE toolbars
          foreach (var clsid in regKey.GetValueNames().Select(s => new string(s.Replace("}}", "}").ToArray())))
          {
            //found one? then check corresponding HKCR key

            var sName = GetObjectName(regKey.View, clsid);
            var sFile = GetFileName(regKey.View, clsid);

            result.Add(new ToolbarResult(ToolbarResultType.Default, regHive,
              regKey.View, regKey.Name, clsid, sName, sFile));

          }
        }
      }

      //Close the root key when done with it.
      rootKey.Close();

      if (Environment.Is64BitOperatingSystem && regHive == RegistryHive.LocalMachine &&
          rootKey.View != RegistryView.Registry64)
      {
        rootKey = RegistryKey.OpenBaseKey(regHive, RegistryView.Registry64);
        goto loopMe;
      }
      //else if (regHive == RegistryHive.LocalMachine)
      //{
      //  regHive = RegistryHive.CurrentUser;
      //  rootKey = RegistryKey.OpenBaseKey(regHive, RegistryView.Default);
      //  goto loopMe;
      //}

      return result;
    }

    public ScanResultType ResultType
    {
      get { return ScanResultType.InternetExplorerToolbar; }
    }

  }

  #endregion

  #region Original Visual Basic (6.0) Code Block

  /*
   Public Sub CheckOther3Item()
     'HKLM\Software\Microsoft\Internet Explorer\Toolbar
     On Error GoTo Error:
     
     Dim hKey&, hKey2&, i%, j%, sCLSID$, sName$
     Dim uData() As Byte, sFile$, sHit$
     If RegOpenKeyEx(HKEY_LOCAL_MACHINE, "Software\Microsoft\Internet Explorer\Toolbar", 0, KEY_QUERY_VALUE, hKey) <> 0 Then Exit Sub
     Do
         sCLSID = String(lEnumBufSize, 0)
         ReDim uData(lEnumBufSize)
         
         'enumerate MSIE toolbars
         If RegEnumValue(hKey, i, sCLSID, Len(sCLSID), 0, ByVal 0, uData(0), UBound(uData)) <> 0 Then Exit Do
         sCLSID = Left(sCLSID, InStr(sCLSID, Chr(0)) - 1)
         
         'found one? then check corresponding HKCR key
         sName = RegGetString(HKEY_CLASSES_ROOT, "CLSID\" & sCLSID, "")
         If sName = vbNullString Then sName = "(no name)"
         'If HasSpecialCharacters(sName) Then
             'when japanese characters are in toolbar name,
             'it tends to screw up things
         '    sName = "?????"
         'End If
         
         sFile = RegGetString(HKEY_CLASSES_ROOT, "CLSID\" & sCLSID & "\InprocServer32", vbNullString)
         If sFile = vbNullString Then
             sFile = "(no file)"
         Else
             If Not FileExists(sFile) Then sFile = sFile & " (file missing)"
         End If
         
         '   sCLSID <> "BrandBitmap" And _
         '   sCLSID <> "SmBrandBitmap" And _
         '   sCLSID <> "BackBitmap" And _
         '   sCLSID <> "BackBitmapIE5" And _
         '   sCLSID <> "OLE (Part 1 of 5)" And _
         '   sCLSID <> "OLE (Part 2 of 5)" And _
         '   sCLSID <> "OLE (Part 3 of 5)" And _
         '   sCLSID <> "OLE (Part 4 of 5)" And _
         '   sCLSID <> "OLE (Part 5 of 5)" Then
         
         If sName <> vbNullString And _
             InStr(sCLSID, "{") > 0 Then
             sHit = "O3 - Toolbar: " & sName & " - " & sCLSID & " - " & sFile
             If Not IsOnIgnoreList(sHit) Then
                 If bMD5 Then sHit = sHit & GetFileMD5(sFile)
                 frmMain.lstResults.AddItem sHit
             End If
         End If
         
         If InStr(sCLSID, "}}") > 0 Then
             'the new searchwww.com trick - use a double
             '}} in the IE toolbar registration, reg the toolbar
             'with only one } - IE ignores the double }}, but
             'HT didn't. It does now!
             
             sCLSID = Left(sCLSID, Len(sCLSID) - 1)
         
             sName = RegGetString(HKEY_CLASSES_ROOT, "CLSID\" & sCLSID, "")
             If sName = vbNullString Then sName = "(no name)"
             'If HasSpecialCharacters(sName) Then sName = "?????"
             
             sFile = RegGetString(HKEY_CLASSES_ROOT, "CLSID\" & sCLSID & "\InprocServer32", vbNullString)
             If sFile = vbNullString Then
                 sFile = "(no file)"
             Else
                 If Not FileExists(sFile) Then sFile = sFile & " (file missing)"
             End If
             If sName <> vbNullString And _
                 sCLSID <> "BrandBitmap" And _
                 sCLSID <> "SmBrandBitmap" Then
                 sHit = "O3 - Toolbar: " & sName & " - " & sCLSID & " - " & sFile
                 If Not IsOnIgnoreList(sHit) Then
                     If bMD5 Then sHit = sHit & GetFileMD5(sFile)
                     frmMain.lstResults.AddItem sHit
                 End If
             End If
         End If
         
         i = i + 1
     Loop
     RegCloseKey hKey
     Exit Sub
     
   Error:
       RegCloseKey hKey
       ErrorMsg "modMain_CheckOther3Item", Err.Number, Err.Description
   End Sub
   */

  #endregion
}
