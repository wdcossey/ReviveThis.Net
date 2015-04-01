using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Microsoft.Win32;
using ReviveThis.AddIn.InternetExplorer.MenuExt.Entities;
using ReviveThis.Enums;
using ReviveThis.Interfaces;

namespace ReviveThis.AddIn.InternetExplorer.MenuExt
{

  #region [O8] IE Context Menu Item / CheckOther8Item

  [Export(typeof (IDetectionAddIn))]
  public class MenuExt : IDetectionAddIn
  {
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
      get { return @"Context Menu Extensions"; }
    }

    public string[] Description
    {
      get { return new[] {@"Scans for Internet Explorer Context Menu Extensions."}; }
    }

    public void Dispose()
    {
      //Nothing to dispose?
    }

    public async Task<ICollection<IDetectionResultItem>> Scan()
    {
      //await Task.FromResult(0);
      
      var result = new Collection<IDetectionResultItem>();

      var regHive = RegistryHive.CurrentUser;
      using (var rootKey = RegistryKey.OpenBaseKey(regHive, RegistryView.Default))
      using (var regKey = rootKey.OpenSubKey(@"Software\Microsoft\Internet Explorer\MenuExt"))
      {
        if (regKey != null && regKey.SubKeyCount > 0)
        {
          var subKeys = regKey.GetSubKeyNames();
          foreach (var subKey in subKeys)
          {
            using (var key = regKey.OpenSubKey(subKey))
            {
              if (key != null)
              {
                var sData = key.GetValue(null, null) as string;

                if (string.IsNullOrEmpty(sData))
                  sData = "(unknown)";

                result.Add(new MenuExtResult(regHive, key.View, key.Name, subKey, sData));
              }
            }
          }
        }
      }

      return result;
    }

    public ScanResultType ResultType
    {
      get { return ScanResultType.InternetExplorerContextMenu; }
    }
  }

  #endregion

  #region Original Visual Basic (6.0) Code Block
  /*
  Public Sub CheckOther8Item()
    'HKCU\Software\Microsoft\Internet Explorer\MenuExt
    
    On Error GoTo Error:
    Dim hKey&, hKey2&, i&, sName$, sData$, sHit$
    If RegOpenKeyEx(HKEY_CURRENT_USER, "Software\Microsoft\Internet Explorer\MenuExt", 0, KEY_ENUMERATE_SUB_KEYS, hKey) = 0 Then
        i = 0
        sName = String(255, 0)
        If RegEnumKeyEx(hKey, i, sName, 255, 0, vbNullString, ByVal 0, ByVal 0) <> 0 Then RegCloseKey hKey: Exit Sub
        Do
            sName = Left(sName, InStr(sName, Chr(0)) - 1)
            sData = RegGetString(HKEY_CURRENT_USER, "Software\Microsoft\Internet Explorer\MenuExt\" & sName, vbNullString)
            If sData <> vbNullString Then
                sHit = "O8 - Extra context menu item: " & sName & " - " & sData
                If Not IsOnIgnoreList(sHit) Then
                    'md5 doesn't seem useful here
                    If bMD5 Then sHit = sHit & GetFileMD5(sData)
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
      ErrorMsg "modMain_CheckOther8Item", Err.Number, Err.Description
  End Sub
 */
  #endregion
}