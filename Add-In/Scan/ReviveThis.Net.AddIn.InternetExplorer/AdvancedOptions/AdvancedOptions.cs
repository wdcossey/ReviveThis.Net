using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Win32;
using ReviveThis.AddIn.InternetExplorer.AdvancedOptions.Entities;
using ReviveThis.AddIn.InternetExplorer.AdvancedOptions.Enums;
using ReviveThis.Entities;
using ReviveThis.Enums;
using ReviveThis.Interfaces;

namespace ReviveThis.AddIn.InternetExplorer.AdvancedOptions
{

  #region [11] Internet Explorer Advanced Options / CheckOther11Item

  [Export(typeof (IDetectionAddIn))]
  public class AdvancedOptions : IDetectionAddIn
  {
    #region private consts
    private const string KEY_NAME_TEXT = "Text";
    #endregion

    private static IEnumerable<string> WhiteList
    {
      get
      {
        //Original HJT White-List, looks a bit suspect?
        //Find the original filter in the Code Block at the bottom of this file.
        return new[]
        {
          "JAVA_VM",
          "JAVA_SUN",
          "BROWSE",
          "ACCESSIBILITY",
          "SEARCHING",
          "HTTP",
          //"HTTP1",            //Not sure what's going on here..?
          //"1",                //Or here..?
          "MULTIMEDIA",
          "Multimedia",
          "CRYPTO",
          "PRINT",
          "TOEGANKELIJKHEID",
          "TABS",
          "INTERNATIONAL",
          //"INTERNATIONAL*"    //Or here..?
        };
      }
    }

    private static Collection<RegistryParser> _registryParserCollection;

    private static IEnumerable<RegistryParser> RegistryParsers
    {
      get
      {
        if (_registryParserCollection != null)
          return _registryParserCollection;

        const string REGISTRY_PATH = @"Software\Microsoft\Internet Explorer\AdvancedOptions";

        return _registryParserCollection = new Collection<RegistryParser>(new[]
        {
          #region LocalMachine
          new RegistryParser(RegistryHive.LocalMachine, REGISTRY_PATH,
            new[] {RegistryView.Registry32, RegistryView.Registry64}),

          #endregion

          #region CurrentUser
          new RegistryParser(RegistryHive.CurrentUser, REGISTRY_PATH,
            new[] {RegistryView.Default})

          #endregion
        });
      }
    }

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
      get { return @"Advanced Options"; }
    }

    public string[] Description
    {
      get
      {
        return new[]
        {
          "Scans for Internet Explorer Advanced Options."
        };
      }
    }

    public ScanResultType ResultType
    {
      get { return ScanResultType.InternetExplorerAdvancedOptions; }
    }

    public void Dispose()
    {
      //Nothing to dispose?
    }

    public async Task<ICollection<IDetectionResultItem>> Scan()
    {
      //await Task.FromResult(0);
      
      var result = new Collection<IDetectionResultItem>();

      foreach (var parser in RegistryParsers)
      {
        foreach (var view in parser.RegistryViews)
        {
          using (var regKey = RegistryKey.OpenBaseKey(parser.RegistryHive, view).OpenSubKey(parser.SubKey))
          {
            if (regKey == null || regKey.SubKeyCount <= 0)
              continue;

            var subKeys = regKey.GetSubKeyNames().Where(w => !WhiteList.Contains(w));

            foreach (var item in subKeys)
            {
              using (var subKey = regKey.OpenSubKey(item))
              {
                if (subKey == null)
                  continue;

                var value = subKey.GetValue(KEY_NAME_TEXT, null) as string;
                if (string.IsNullOrEmpty(value))
                {
                  value = "(none)";
                }

                result.Add(new AdvancedOptionsResult(AdvancedOptionsResultType.Default, parser.RegistryHive, subKey.View, subKey.Name, KEY_NAME_TEXT, value));
              }
            }
          }
        }
      }

      return result;
    }
  }

  #endregion

  #region Original Visual Basic (6.0) Code Block

  /* 
   Public Sub CheckOther11Item()
       'HKLM\Software\Microsoft\Internet Explorer\AdvancedOptions
       Dim hKey&, i&, sKey$, sName$, sHit$
       On Error GoTo Error:
       If RegOpenKeyEx(HKEY_LOCAL_MACHINE, "Software\Microsoft\Internet Explorer\AdvancedOptions", 0, KEY_ENUMERATE_SUB_KEYS, hKey) = 0 Then
           sKey = String(255, 0)
           If RegEnumKeyEx(hKey, i, sKey, 255, 0, vbNullString, ByVal 0, ByVal 0) <> 0 Then RegCloseKey hKey: Exit Sub
           Do
               sKey = Left(sKey, InStr(sKey, Chr(0)) - 1)
               sName = RegGetString(HKEY_LOCAL_MACHINE, "Software\Microsoft\Internet Explorer\AdvancedOptions\" & sKey, "Text")
               If InStr("JAVA_VM.JAVA_SUN.BROWSE.ACCESSIBILITY.SEARCHING." & _
                        "HTTP1.1.MULTIMEDIA.Multimedia.CRYPTO.PRINT." & _
                        "TOEGANKELIJKHEID.TABS.INTERNATIONAL*", sKey) = 0 And _
                  sName <> vbNullString Then
                   sHit = "O11 - Options group: [" & sKey & "] " & sName
                   If bIgnoreAllWhitelists = True Then
                       frmMain.lstResults.AddItem sHit
                   ElseIf Not IsOnIgnoreList(sHit) Then
                       frmMain.lstResults.AddItem sHit
                   End If
               End If
               sKey = String(255, 0)
               i = i + 1
           Loop Until RegEnumKeyEx(hKey, i, sKey, 255, 0, vbNullString, ByVal 0, ByVal 0) <> 0
           RegCloseKey hKey
       End If
       Exit Sub
   
   Error:
       ErrorMsg "modMain_CheckOther11Item", Err.Number, Err.Description
   End Sub
   */

  #endregion
}
