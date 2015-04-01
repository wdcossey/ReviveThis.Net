using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Win32;
using ReviveThis.AddIn.InternetExplorer.DownloadedProgramFiles.Entities;
using ReviveThis.AddIn.InternetExplorer.DownloadedProgramFiles.Enums;
using ReviveThis.Entities;
using ReviveThis.Enums;
using ReviveThis.Interfaces;

namespace ReviveThis.AddIn.InternetExplorer.DownloadedProgramFiles
{

  #region [O16] Downloaded Program Files / CheckOther16Item

  [Export(typeof (IDetectionAddIn))]
  public class Dpf : IDetectionAddIn
  {
    #region private 

    #region consts
    private const string REGISTRY_PATH = @"Software\Microsoft\Code Store Database\Distribution Units";
    private const string REGISTRY_PATH_CLSID = @"CLSID";
    #endregion

    #region Registry Location Parsers

    private static RegistryParser[] _registryLocations;

    private static IEnumerable<RegistryParser> RegistryLocations
    {
      get
      {
        if (_registryLocations != null && _registryLocations.Any())
          return _registryLocations;

        return _registryLocations = new[]
        {
          #region LocalMachine
          new RegistryParser(RegistryHive.LocalMachine, REGISTRY_PATH,
            new[] {RegistryView.Registry32, RegistryView.Registry64}),
          #endregion

          #region CurrentUser
          new RegistryParser(RegistryHive.CurrentUser, REGISTRY_PATH,
            new[] {RegistryView.Default}),

          #endregion
        };
      }
    }

    #endregion

    private string GetClsidFriendlyName(RegistryHive hive, RegistryKey registryKey)
    {
      if (registryKey == null)
        return null;

      var result = registryKey.GetValue(string.Empty, null) as string;

      if (string.IsNullOrEmpty(result) && registryKey.View == RegistryView.Registry64)
      {
        using (var altKey = RegistryKey.OpenBaseKey(hive, RegistryView.Registry32).OpenSubKey(registryKey.Name))
        {
          result = GetClsidFriendlyName(hive, altKey);
        }
      }

      return result;
    }

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
      get { return @"Downloaded Program Files"; }
    }

    public string[] Description
    {
      get
      {
        return new[]
        {
          @"Scans for Internet Explorer Downloaded Program Files."
        };
      }
    }

    public void Dispose()
    {
      //Nothing to dispose?
    }

    public async Task<ICollection<IDetectionResultItem>> Scan()
    {
      //await Task.FromResult(0);
      
      var result = new Collection<IDetectionResultItem>();

      foreach (var item in RegistryLocations)
      {
        foreach (var view in item.RegistryViews)
        {
          using (var regKey = RegistryKey.OpenBaseKey(item.RegistryHive, view).OpenSubKey(item.SubKey, false))
          {
            if (regKey == null)
              continue;

            var subKeyNames = regKey.GetSubKeyNames();

            foreach (var subKeyName in subKeyNames)
            {
              using (var subKey = regKey.OpenSubKey(subKeyName, false))
              {
                if (subKey == null)
                  continue;

                using (
                  var clsidKey =
                    RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, view)
                      .OpenSubKey(Path.Combine(REGISTRY_PATH_CLSID, subKeyName), false))
                {
                  var friendlyName = subKey.GetValue(string.Empty, null) as string;

                  friendlyName = friendlyName ?? GetClsidFriendlyName(RegistryHive.ClassesRoot, clsidKey);

                  string codeBase = null;

                  using (var downloadInformationKey = subKey.OpenSubKey("DownloadInformation", false))
                  {
                    if (downloadInformationKey != null)
                      codeBase = downloadInformationKey.GetValue("CODEBASE", null) as string;
                  }

                  result.Add(new DpfResult(DpfResultType.Default, item.RegistryHive, subKey.View, subKey.Name,
                    subKeyName, friendlyName, codeBase));
                }

              }
            }
          }
        }
      }

      return result;
    }

    public ScanResultType ResultType
    {
      get { return ScanResultType.DownloadedProgramFiles; }
    }
  }

  #endregion

  #region Original Visual Basic (6.0) Code Block

  /*
   Public Sub CheckOther16Item()
       'O16 - Downloaded Program Files
       Dim sDPFKey$, sName$, sFriendlyName$, sCodeBase$, i&, hKey&, sHit$
   
       'HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Internet Settings,ActiveXCache
       'is location of actual %WINDIR%\DPF\ folder
       sDPFKey = "Software\Microsoft\Code Store Database\Distribution Units"
       On Error GoTo Error:
   
       If RegOpenKeyEx(HKEY_LOCAL_MACHINE, sDPFKey, 0, KEY_ENUMERATE_SUB_KEYS, hKey) <> 0 Then
           'key doesn't exist
           Exit Sub
       End If
   
       sName = String(255, 0)
       If RegEnumKeyEx(hKey, 0, sName, 255, 0, vbNullString, 0, ByVal 0) <> 0 Then
           'no subkeys
           RegCloseKey hKey
           Exit Sub
       End If
   
       Do
           sName = Left(sName, InStr(sName, Chr(0)) - 1)
           If Left(sName, 1) = "{" And Right(sName, 1) = "}" Then
               sFriendlyName = RegGetString(HKEY_LOCAL_MACHINE, sDPFKey & "\" & sName, "")
               If sFriendlyName = vbNullString Then
                   sFriendlyName = RegGetString(HKEY_CLASSES_ROOT, "CLSID\" & sName, "")
               End If
           End If
           sCodeBase = RegGetString(HKEY_LOCAL_MACHINE, sDPFKey & "\" & sName & "\DownloadInformation", "CODEBASE")
       
           If InStr(sCodeBase, "http://java.sun.com") <> 1 And _
              InStr(sCodeBase, "http://www.microsoft.com") <> 1 And _
              InStr(sCodeBase, "http://webresponse.one.microsoft.com") <> 1 And _
              InStr(sCodeBase, "http://rtc.webresponse.one.microsoft.com") <> 1 And _
              InStr(sCodeBase, "http://office.microsoft.com") <> 1 And _
              InStr(sCodeBase, "http://officeupdate.microsoft.com") <> 1 And _
              InStr(sCodeBase, "http://protect.microsoft.com") <> 1 And _
              InStr(sCodeBase, "http://dql.microsoft.com") <> 1 And _
              InStr(sCodeBase, "http://codecs.microsoft.com") <> 1 And _
              InStr(sCodeBase, "http://download.microsoft.com") <> 1 And _
              InStr(sCodeBase, "http://windowsupdate.microsoft.com") <> 1 And _
              InStr(sCodeBase, "http://v4.windowsupdate.microsoft.com") <> 1 And _
              InStr(sCodeBase, "http://download.macromedia.com") <> 1 And _
              InStr(sCodeBase, "http://fpdownload.macromedia.com") <> 1 And _
              InStr(sCodeBase, "http://active.macromedia.com") <> 1 And _
              InStr(sCodeBase, "http://www.apple.com") <> 1 And _
              InStr(sCodeBase, "http://http://security.symantec.com") <> 1 And _
              InStr(sCodeBase, "http://download.yahoo.com") <> 1 And _
              InStr(sName, "Microsoft XML Parser") = 0 And _
              InStr(sName, "Java Classes") = 0 And _
              InStr(sName, "Classes for Java") = 0 And _
              InStr(sName, "Java Runtime Environment") = 0 Or _
              bIgnoreAllWhitelists Then
          
               sHit = "O16 - DPF: " & sName & IIf(sFriendlyName <> vbNullString, " (" & sFriendlyName & ")", "") & " - " & sCodeBase
               If Not IsOnIgnoreList(sHit) Then frmMain.lstResults.AddItem sHit
           End If
       
           i = i + 1
           sName = String(255, 0)
           sFriendlyName = vbNullString
       Loop Until RegEnumKeyEx(hKey, i, sName, 255, 0, vbNullString, 0, ByVal 0) <> 0
       RegCloseKey hKey
       Exit Sub
   
   Error:
       RegCloseKey hKey
       ErrorMsg "modMain_CheckOther16Item", Err.Number, Err.Description
   End Sub
   */

  #endregion
}
