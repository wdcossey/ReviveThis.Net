using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Win32;
using ReviveThis.AddIn.InternetExplorer.BrowserHelperObject.Entities;
using ReviveThis.AddIn.InternetExplorer.BrowserHelperObject.Enums;
using ReviveThis.Enums;
using ReviveThis.Interfaces;

namespace ReviveThis.AddIn.InternetExplorer.BrowserHelperObject
{

  #region [O2] CheckBrowserHelperObjects / CheckOther2Item

  [Export(typeof (IDetectionAddIn))]
  public class BrowserHelperObject : IDetectionAddIn
  {
    #region consts

    private const string NO_FILE = @"(no file)";
    private const string FILE_MISSING = @"(file missing)";
    private const string NO_NAME = @"(no name)";

    #endregion

    public string Author
    {
      get { return Consts.General.Author; }
    }

    private Version _version = null;

    public Version Version
    {
      get
      {
        if (_version != null)
          return _version;

        return _version = new Version(1, 0, 0, 0);
      }
    }

    public string Name
    {
      get { return @"Browser Helper Objects (BHO)"; }
    }

    public string[] Description
    {
      get
      {
        return new[]
        {
          "Scans for Internet Explorer \"Browser Helper Objects\" (also known as BHO's).",
          string.Empty,
          "For more information on BHO's please see http://www.wikipedia.org/wiki/Browser_Helper_Object"
        };
      }
    }

    public ScanResultType ResultType
    {
      get { return ScanResultType.BrowserHelperObject; }
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
            var demonIndex = result.IndexOf("__BHODemonDisabled", StringComparison.InvariantCultureIgnoreCase);
            if (demonIndex > -1)
            {
              var fileName = result.Substring(0, demonIndex).Trim();
              result = string.Format("{0} ({1}disabled by BHODemon)", fileName,
                !File.Exists(fileName) ? "file missing / " : string.Empty);
            }
            else
            {
              result = string.Format("{0}{1}", result,
                !File.Exists(result) ? string.Format(" {0}", FILE_MISSING) : string.Empty);
            }
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

    public void Dispose()
    {
      //Nothing to dispose?
    }

    public async Task<ICollection<IDetectionResultItem>> Scan()
    {
      //await Task.FromResult(0);
      
      var result = new Collection<IDetectionResultItem>();

      var regHive = RegistryHive.LocalMachine;

      var rootKey = RegistryKey.OpenBaseKey(regHive, RegistryView.Registry32);

      loopMe:

      using (
        var regKey = rootKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\explorer\Browser Helper Objects"))
      {
        if (regKey != null && regKey.SubKeyCount > 0)
        {
          var subKeys = regKey.GetSubKeyNames().Select(s => new string(s.Replace("}}", "}").ToArray()));
          foreach (var clsid in subKeys)
          {
            string sName = null;
            var sFile = GetFileName(regKey.View, clsid);

            //get bho name from BHO regkey
            using (var subKey = regKey.OpenSubKey(clsid))
            {
              if (subKey != null)
              {
                //get BHO name from CLSID regkey
                var name = subKey.GetValue(null, null) as string;
                sName = !string.IsNullOrEmpty(name) ? name : GetObjectName(regKey.View, clsid);
              }
            }

            //TODO: Implement IgnoreList
            result.Add(new BrowserHelperObjectResult(BrowserHelperObjectResultType.Default, regHive, regKey.View, Path.Combine(regKey.Name, clsid),
              clsid, sName, sFile));

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

  }

  #endregion

  #region Original Visual Basic (6.0) Code Block

  /* 
   Public Sub CheckOther2Item()
      Dim hKey&, i&, j&, sName$, sCLSID$, sFile$, sHit$
      On Error GoTo Error:
    
      If RegOpenKeyEx(HKEY_LOCAL_MACHINE, "Software\Microsoft\Windows\CurrentVersion\explorer\Browser Helper Objects", 0, KEY_ENUMERATE_SUB_KEYS, hKey) <> 0 Then Exit Sub
      Do
          sCLSID = String(255, 0)
          If RegEnumKeyEx(hKey, i, sCLSID, 255, 0, vbNullString, 0, ByVal 0) <> 0 Then Exit Do
          sCLSID = Left(sCLSID, InStr(sCLSID, Chr(0)) - 1)
          If sCLSID <> vbNullString And _
              InStr(1, sCLSID, "MSHist", vbTextCompare) <> 1 Then
              'get filename from HKCR\CLSID\sName
              sFile = RegGetString(HKEY_CLASSES_ROOT, "CLSID\" & sCLSID & "\InprocServer32", "")
            
              If InStr(sFile, "__BHODemonDisabled") > 0 Then
                  sFile = Left(sFile, InStr(sFile, "__BHODemonDisabled") - 1) & _
                  " (disabled by BHODemon)"
              Else
                  If sFile <> vbNullString And Not FileExists(sFile) Then sFile = sFile & " (file missing)"
              End If
              If sFile = vbNullString Then sFile = "(no file)"
            
              'get bho name from BHO regkey
              sName = RegGetString(HKEY_LOCAL_MACHINE, "Software\Microsoft\Windows\CurrentVersion\explorer\Browser Helper Objects\" & sCLSID, "")
              If sName = vbNullString Then
                  'get BHO name from CLSID regkey
                  sName = RegGetString(HKEY_CLASSES_ROOT, "CLSID\" & sCLSID, "")
                  If sName = vbNullString Then sName = "(no name)"
              End If
            
              sHit = "O2 - BHO: " & sName & " - " & sCLSID & " - " & sFile
              If Not IsOnIgnoreList(sHit) Then
                  If bMD5 Then sHit = sHit & GetFileMD5(sFile)
                  frmMain.lstResults.AddItem sHit
              End If
            
              If InStr(sCLSID, "}}") > 0 Then
                  'the new searchwww.com trick - use a double
                  '}} in the IE toolbar registration, reg the toolbar
                  'with only one } - IE ignores the double }}, but
                  'HT didn't. It does now!
                
                  sCLSID = Left(sCLSID, Len(sCLSID) - 1)
            
                  sFile = RegGetString(HKEY_CLASSES_ROOT, "CLSID\" & sCLSID & "\InprocServer32", "")
                  If InStr(sFile, "__BHODemonDisabled") > 0 Then
                      sFile = Left(sFile, InStr(sFile, "__BHODemonDisabled") - 1) & _
                      " (disabled by BHODemon)"
                  Else
                      If sFile <> vbNullString And Not FileExists(sFile) Then sFile = sFile & " (file missing)"
                  End If
                
                  If sFile = vbNullString Then sFile = "(no file)"
                  sName = RegGetString(HKEY_LOCAL_MACHINE, "Software\Microsoft\Windows\CurrentVersion\explorer\Browser Helper Objects\" & sCLSID, "")
                  If sName = vbNullString Then sName = "(no name)"
                
                  sHit = "O2 - BHO: " & sName & " - " & sCLSID & " - " & sFile
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
       ErrorMsg "modMain_CheckOther2Item", Err.Number, Err.Description
   End Sub
   */

  #endregion
}
