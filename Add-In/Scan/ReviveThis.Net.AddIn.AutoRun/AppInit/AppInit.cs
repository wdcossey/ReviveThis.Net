using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Win32;
using ReviveThis.AddIn.AutoRun.AppInit.Entities;
using ReviveThis.AddIn.AutoRun.AppInit.Enums;
using ReviveThis.Entities;
using ReviveThis.Enums;
using ReviveThis.Interfaces;
using ReviveThis.AddIn.AutoRun.Consts;

namespace ReviveThis.AddIn.AutoRun.AppInit
{

  #region [20] CheckOther20Item

  [Export(typeof(IDetectionAddIn))]
  public class AppInit : IDetectionAddIn
  {
    #region private
    #region consts

    private const string KEY_NAME_LOAD_APP_INIT_DLLS = "LoadAppInit_DLLs";
    private const string KEY_NAME_REQUIRE_SIGNED_APP_INIT_DLLS = "RequireSignedAppInit_DLLs";
    private const string KEY_NAME_VALUE_APP_INIT_DLLS = "AppInit_DLLs";
    #endregion

    #region IE Extensions Registry Parsers
    private static RegistryParser[] _appInitRegistryParsers;

    private static IEnumerable<RegistryParser> AppInitRegistryParsers
    {
      get
      {
        if (_appInitRegistryParsers != null && _appInitRegistryParsers.Any())
          return _appInitRegistryParsers;

        return _appInitRegistryParsers = new[]
        {
          #region LocalMachine
          new RegistryParser(RegistryHive.LocalMachine, @"Software\Microsoft\Windows NT\CurrentVersion\Windows",
            new[] {RegistryView.Registry32, RegistryView.Registry64})
          #endregion

          #region CurrentUser
          //new RegistryParser(RegistryHive.CurrentUser, @"Software\Microsoft\Windows NT\CurrentVersion\Windows",
          //  new[] {RegistryView.Default}),
          #endregion
        };
      }
    }
    #endregion

    #endregion

    public string Author
    {
      get { return General.Author; }
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
      get { return @"AppInit_DLLs"; }
    }

    public string[] Description
    {
      get
      {
        return new[]
        {
          "All the .dll's that are listed in \"AppInit_DLLs\" are loaded by each Windows® application that is running in the current log on session.",
          string.Empty,
          "For more information visit: http://en.wikipedia.org/wiki/DLL_injection"
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
      
      var result = new List<IDetectionResultItem>();

      foreach (var item in AppInitRegistryParsers)
      {
        foreach (var view in item.RegistryViews)
        {
          using (var regKey = RegistryKey.OpenBaseKey(item.RegistryHive, view).OpenSubKey(item.SubKey))
          {
            if (regKey == null)
              continue;

            //LoadAppInit_DLLs - Globally enables or disables AppInit_DLLs
            var loadAppInitDlls = regKey.GetValue(KEY_NAME_LOAD_APP_INIT_DLLS, null) as Int32?;
            if (loadAppInitDlls.HasValue && loadAppInitDlls.Value.Equals(1)) //AppInit_DLLs is enabled.
              result.Add(new AppInitResult(AppInitResultType.AppInitEnabled, item.RegistryHive, regKey.View, regKey.Name, KEY_NAME_LOAD_APP_INIT_DLLS));

            //RequireSignedAppInit_DLLs - Only load code-signed DLLs.
            var signedAppInitDlls = regKey.GetValue(KEY_NAME_REQUIRE_SIGNED_APP_INIT_DLLS, null) as Int32?;
            if (signedAppInitDlls.HasValue && signedAppInitDlls.Value.Equals(0)) //Code Signing for AppInit_DLLs is disabled.
              result.Add(new AppInitResult(AppInitResultType.CodeSigningDisabled, item.RegistryHive, regKey.View, regKey.Name, KEY_NAME_REQUIRE_SIGNED_APP_INIT_DLLS));


            //AppInit_DLLs - Space or comma delimited list of DLLs to load. The complete path to the DLL should be specified using Short Names.
            var appInitDlls = regKey.GetValue(KEY_NAME_VALUE_APP_INIT_DLLS, null) as string;
            if (!string.IsNullOrEmpty(appInitDlls))
            {
              result.AddRange(appInitDlls.Split(',', ' ').Select(s => new AppInitResult(item.RegistryHive, regKey.View, regKey.Name, KEY_NAME_VALUE_APP_INIT_DLLS, s)));
              
              //var appInitList = appInitDlls.Split(new[] { ',', ' ' });
              //foreach (var appInit in appInitList)
              //{
              //  result.Add(new AppInitResult(item.RegistryHive, regKey.View, regKey.Name, KEY_NAME_VALUE_APP_INIT_DLLS, appInit));
              //}
            }
            
          }
        }
      }

      return result;
    }

    public ScanResultType ResultType
    {
      get { return ScanResultType.AppInitDlLs; }
    }
  }

  #endregion

  #region Original Visual Basic (6.0) Code Block

  /*
    Public Sub CheckOther20Item()
        'appinit_dlls + winlogon notify
        Dim sAppInit$, sFile$, sHit$
        sAppInit = "Software\Microsoft\Windows NT\CurrentVersion\Windows"
        sFile = RegGetString(HKEY_LOCAL_MACHINE, sAppInit, "AppInit_DLLs")
        If sFile <> vbNullString Then
            sFile = Replace(sFile, Chr(0), "|")
            If InStr(1, sSafeAppInit, sFile, vbTextCompare) = 0 Or _
               bIgnoreAllWhitelists Then
                'item is not on whitelist
                sHit = "O20 - AppInit_DLLs: " & sFile
            
                If bIgnoreAllWhitelists = True Then
                    frmMain.lstResults.AddItem sHit
                ElseIf Not IsOnIgnoreList(sHit) Then
                     frmMain.lstResults.AddItem sHit
                End If
            End If
        End If
    
        Dim sSubKeys$(), i&, sWinlogon$, ss$
        sWinlogon = "Software\Microsoft\Windows NT\CurrentVersion\Winlogon\Notify"
        sSubKeys = Split(RegEnumSubkeys(HKEY_LOCAL_MACHINE, sWinlogon), "|")
        If UBound(sSubKeys) <> -1 Then
            For i = 0 To UBound(sSubKeys)
                If InStr(1, "*" & sSafeWinlogonNotify & "*", "*" & sSubKeys(i) & "*", vbTextCompare) = 0 Then
                    sFile = RegGetString(HKEY_LOCAL_MACHINE, sWinlogon & "\" & sSubKeys(i), "DllName")
                
                    If Len(sFile) = 0 Then
                        sFile = "Invalid registry found"
                    Else
                        If StrComp(Mid(sFile, 1, 1), "\", vbTextCompare) = 0 Then
                            If FileExists(sWinDir & "\" & sFile) Then sFile = sWinDir & "\" & sFile
                            If FileExists(sWinSysDir & "\" & sFile) Then sFile = sWinSysDir & "\" & sFile
                        End If
                    
                        sFile = NormalizePath(sFile)
                    
                        If Not FileExists(sFile) Then sFile = sFile & " (file missing)"
                        If FileExists(sFile) And bMD5 Then
                            sFile = sFile & GetFileFromAutostart(sFile)
                        End If
                    
    '                    If InStr(1, sFile, "%", vbTextCompare) = 1 Then
    '                       sFile = "Suspicious registry value"
    '                  End If
                  
                    End If
                
                    sHit = "O20 - Winlogon Notify: " & sSubKeys(i) & " - " & sFile
                    If Not IsOnIgnoreList(sHit) Then
                        frmMain.lstResults.AddItem sHit
                    End If
                End If
            Next i
        End If
    End Sub
  */

  #endregion
}