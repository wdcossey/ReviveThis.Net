using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Win32;
using ReviveThis.AddIn.InternetExplorer.Plugins.Entities;
using ReviveThis.AddIn.InternetExplorer.Plugins.Enums;
using ReviveThis.Enums;
using ReviveThis.Interfaces;

namespace ReviveThis.AddIn.InternetExplorer.Plugins
{
  #region [12] Internet Explorer Plugins / CheckOther12Item

  [Export(typeof(IDetectionAddIn))]
  public class Plugins : IDetectionAddIn
  {
    #region private
    #region consts

    private const string REGISTRY_BASE_PATH = @"Software\Microsoft\Internet Explorer\Plugins";
    private const string REGISTRY_EXTENSION_PATH = REGISTRY_BASE_PATH + @"\Extension";
    private const string REGISTRY_MIME_PATH = REGISTRY_BASE_PATH + @"\MIME";
    #endregion

    #region Plugin Registry Parsers
    private static PluginRegistryParser[] _pluginRegistryParsers;

    private static IEnumerable<PluginRegistryParser> PluginRegistryParsers
    {
      get
      {
        if (_pluginRegistryParsers != null && _pluginRegistryParsers.Any())
          return _pluginRegistryParsers;

        return _pluginRegistryParsers = new[]
        {
          #region LocalMachine
          new PluginRegistryParser(RegistryHive.LocalMachine, REGISTRY_EXTENSION_PATH,
            new[] {RegistryView.Registry32, RegistryView.Registry64}, PluginResultType.Extension),

          new PluginRegistryParser(RegistryHive.LocalMachine, REGISTRY_MIME_PATH,
            new[] {RegistryView.Registry32, RegistryView.Registry64}, PluginResultType.MimeType)
          #endregion

          #region CurrentUser

          #endregion
        };
      }
    }
    #endregion

    #endregion

    public string Author
    {
      get { return Consts.General.Author; }
    }

    private Version _version;
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
      get { return "Plugins"; }
    }

    public string[] Description
    {
      get
      {
        return new[]
        {
          "Scans for Internet Explorer \"File Extension\" and \"MIME Type\" hi-jacking.",
          string.Empty,
          string.Format("Registry locations include (but not limited to):\r\n\t{0}",
            string.Join("\r\n\t",
              PluginRegistryParsers.Where(w => w.RegistryHive == RegistryHive.LocalMachine).Select(s => s.SubKey)))
        };
      }
    }

    public ScanResultType ResultType
    {
      get { return ScanResultType.InternetExplorerPlugin; }
    }

    public async Task<ICollection<IDetectionResultItem>> Scan()
    {
      //await Task.FromResult(0);
      
      var result = new Collection<IDetectionResultItem>();

      foreach (var item in PluginRegistryParsers)
      {
        foreach (var view in item.RegistryViews)
        {
          using (var regKey = RegistryKey.OpenBaseKey(item.RegistryHive, view).OpenSubKey(item.SubKey, false))
          {
            if (regKey == null)
              continue;

            var subKeyNames = regKey.GetSubKeyNames();

            if (!subKeyNames.Any())
              continue;

            foreach (var subKeyName in subKeyNames)
            {
              using (var subKey = regKey.OpenSubKey(subKeyName, false))
              {
                if (subKey == null)
                  continue;

                var sFileName = subKey.GetValue("Location", null) as string;

                //if (string.IsNullOrEmpty(sFileName) && item.ResultType == PluginResultType.MimeType)
                //  sFileName = subKey.GetValue("Extension", null) as string;

                var sName = subKey.GetValue(null, null) as string ?? subKeyName;

                if (!string.IsNullOrEmpty(sFileName))
                {
                  result.Add(new PluginResult(item.ResultType, item.RegistryHive, subKey.View, subKey.Name, sName, sFileName, File.Exists(sFileName)));
                }
              }
            }
          }
        }
      }
      return result;
    }

    public void Dispose()
    {
      //Nothing to dispose?
    }

  }

  #endregion

  #region Original Visual Basic (6.0) Code Block
  /*
   Public Sub CheckOther12Item()
       'HKLM\Software\Microsoft\Internet Explorer\Plugins\Extensions
       'HKLM\Software\Microsoft\Internet Explorer\Plugins\MIME
     
       Dim hKey&, i&, sName$, sFile$, sHit$
       On Error GoTo Error:
       If RegOpenKeyEx(HKEY_LOCAL_MACHINE, "Software\Microsoft\Internet Explorer\Plugins\Extension", 0, KEY_ENUMERATE_SUB_KEYS, hKey) = 0 Then
           sName = String(255, 0)
           If RegEnumKeyEx(hKey, i, sName, 255, 0, vbNullString, ByVal 0, ByVal 0) <> 0 Then RegCloseKey hKey: Exit Sub
           Do
               sName = Left(sName, InStr(sName, Chr(0)) - 1)
               sFile = RegGetString(HKEY_LOCAL_MACHINE, "Software\Microsoft\Internet Explorer\Plugins\Extension\" & sName, "Location")
               If sFile <> vbNullString Then
                   sHit = "O12 - Plugin for " & sName & ": " & sFile
                   If Not IsOnIgnoreList(sHit) Then
                       If bMD5 Then sHit = sHit & GetFileMD5(sFile)
                       frmMain.lstResults.AddItem sHit
                   End If
               End If
             
               sName = String(255, 0)
               i = i + 1
           Loop Until RegEnumKeyEx(hKey, i, sName, 255, 0, vbNullString, ByVal 0, ByVal 0) <> 0
           RegCloseKey hKey
       End If
     
       hKey = 0
       i = 0
       If RegOpenKeyEx(HKEY_LOCAL_MACHINE, "Software\Microsoft\Internet Explorer\Plugins\MIME", 0, KEY_ENUMERATE_SUB_KEYS, hKey) = 0 Then
           sName = String(255, 0)
           If RegEnumKeyEx(hKey, i, sName, 255, 0, vbNullString, ByVal 0, ByVal 0) <> 0 Then RegCloseKey hKey: Exit Sub
           Do
               sName = Left(sName, InStr(sName, Chr(0)) - 1)
               sFile = RegGetString(HKEY_LOCAL_MACHINE, "Software\Microsoft\Internet Explorer\Plugins\MIME\" & sName, "Location")
               If sFile <> vbNullString Then
                   sHit = "O12 - Plugin for " & sName & ": " & sFile
                   If Not IsOnIgnoreList(sHit) Then
                       If bMD5 Then sHit = sHit & GetFileMD5(sFile)
                       frmMain.lstResults.AddItem sHit
                   End If
               End If
             
               sName = String(255, 0)
               i = i + 1
           Loop Until RegEnumKeyEx(hKey, i, sName, 255, 0, vbNullString, ByVal 0, ByVal 0) <> 0
           RegCloseKey hKey
       End If
       Exit Sub
     
   Error:
       RegCloseKey hKey
       ErrorMsg "modMain_CheckOther12Item", Err.Number, Err.Description
   End Sub
   */
  #endregion
}