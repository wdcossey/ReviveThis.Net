using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Win32;
using ReviveThis.AddIn.InternetExplorer.UrlSearchHooks.Entities;
using ReviveThis.AddIn.InternetExplorer.UrlSearchHooks.Enums;
using ReviveThis.Enums;
using ReviveThis.Interfaces;

namespace ReviveThis.AddIn.InternetExplorer.UrlSearchHooks
{

  #region [R3] URLSearchHooks / CheckRegistry3Item

  [Export(typeof (IDetectionAddIn))]
  public class UrlSearchHooks : IDetectionAddIn
  {
    #region private consts

    private const string REGISTRY_PATH = @"Software\Microsoft\Internet Explorer\URLSearchHooks";

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
      get { return @"URL Search Hooks"; }
    }

    public string[] Description
    {
      get { return new[] {@"Scans for Internet Explorer URLSearchHooks."}; }
    }

    public void Dispose()
    {
      //Nothing to dispose?
    }

    public async Task<ICollection<IDetectionResultItem>> Scan()
    {
      //await Task.FromResult(0);
      
      var result = new Collection<IDetectionResultItem>();

      const RegistryHive hive = RegistryHive.CurrentUser;
      using (var rootKey = RegistryKey.OpenBaseKey(hive, RegistryView.Default))
      using (var regKey = rootKey.OpenSubKey(REGISTRY_PATH))
      {
        if (regKey == null || regKey.ValueCount <= 0)
        {
          result.Add(new UrlSearchHookResult(hive, regKey == null ? rootKey.View : regKey.View, regKey == null ? REGISTRY_PATH: regKey.Name));
          return result;
        }

        var valueNames = regKey.GetValueNames();

#if DEBUG
        //DEBUG build, lets spit out everything!
        foreach (var valueName in valueNames)
#else
        foreach (var valueName in valueNames.Where(w => !w.Equals("{CFBFAE00-17A6-11D0-99CB-00C04FD64497}")))
#endif
        {
          var sName = NO_NAME;
          var sFile = NO_FILE;
          var bFileExists = false;

          using (var classRootKey = Registry.ClassesRoot.OpenSubKey(Path.Combine("CLSID", valueName)))
          {
            if (classRootKey != null)
            {
              //TODO: Implement IgnoreList
              sName = classRootKey.GetValue(null, NO_NAME) as string;
              using (var inProcKey = classRootKey.OpenSubKey("InProcServer32"))
              {
                if (inProcKey != null)
                {
                  sFile = inProcKey.GetValue(null, null) as string;

                  if (!string.IsNullOrEmpty(sFile))
                  {
                    bFileExists = File.Exists(sFile);
                  }
                  //sFile = string.IsNullOrEmpty(sFile) ? "(no file)" : !File.Exists(sFile) ? "(file missing)" : sFile;
                }
              }
            }

            //TODO: Implement IgnoreList
            result.Add(new UrlSearchHookResult(UrlSearchHookResultType.Unknown,  hive, rootKey.View, regKey.Name, valueName,
              sName, sFile, bFileExists));
          }
        }
      }

      return result;
    }

    public ScanResultType ResultType
    {
      get { return ScanResultType.RegistryMultipleValues; }
    }
  }

  #endregion

  #region Original Visual Basic (6.0) Code Block

  /*
   Public Sub CheckRegistry3Item()
     Dim sURLHook$, hKey&, i&, sName$, uData() As Byte
     Dim sHit$, sCLSID$, sFile$
     sURLHook = "Software\Microsoft\Internet Explorer\URLSearchHooks"
     If RegOpenKeyEx(HKEY_CURRENT_USER, sURLHook, 0, KEY_QUERY_VALUE, hKey) = 0 Then
         sCLSID = String(lEnumBufSize, 0)
         ReDim uData(lEnumBufSize)
         If RegEnumValue(hKey, 0, sCLSID, Len(sCLSID), 0, ByVal 0, uData(0), UBound(uData)) <> 0 Then
             'default URLSearchHook is missing!
             sHit = "R3 - Default URLSearchHook is missing"
             If Not IsOnIgnoreList(sHit) Then frmMain.lstResults.AddItem sHit
             RegCloseKey hKey
             Exit Sub
         End If
         
         Do
             sCLSID = Left(sCLSID, InStr(sCLSID, Chr(0)) - 1)
             sName = RegGetString(HKEY_CLASSES_ROOT, "CLSID\" & sCLSID, "")
             If sCLSID <> "{CFBFAE00-17A6-11D0-99CB-00C04FD64497}" Then
                 'found a new urlsearchhook!
                 If sName = vbNullString Then sName = "(no name)"
                 sFile = RegGetString(HKEY_CLASSES_ROOT, "CLSID\" & sCLSID & "\InProcServer32", "")
                 If sFile = vbNullString Then sFile = "(no file)"
                 If sFile <> "(no file)" And Not FileExists(sFile) Then sFile = sFile & " (file missing)"
                 
                 sHit = "R3 - URLSearchHook: " & sName & " - " & sCLSID & " - " & sFile
                 If Not IsOnIgnoreList(sHit) Then frmMain.lstResults.AddItem sHit
             End If
             
             i = i + 1
             sCLSID = String(lEnumBufSize, 0)
             ReDim uData(lEnumBufSize)
         Loop Until RegEnumValue(hKey, i, sCLSID, Len(sCLSID), 0, ByVal 0, uData(0), UBound(uData)) <> 0
         RegCloseKey hKey
     Else
         'default URLSearchHook is missing!
         sHit = "R3 - Default URLSearchHook is missing"
         If Not IsOnIgnoreList(sHit) Then frmMain.lstResults.AddItem sHit
     End If
     Exit Sub
     
   Error:
     RegCloseKey hKey
     ErrorMsg "modMain_CheckRegistry3Item", Err.Number, Err.Description
   End Sub
   */

  #endregion
}
