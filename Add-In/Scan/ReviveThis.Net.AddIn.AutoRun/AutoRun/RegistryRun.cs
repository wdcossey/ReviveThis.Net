using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Win32;
using ReviveThis.AddIn.AutoRun.AutoRun.Entities;
using ReviveThis.Entities;
using ReviveThis.Enums;
using ReviveThis.Interfaces;
using ReviveThis.AddIn.AutoRun.Consts;

namespace ReviveThis.AddIn.AutoRun.AutoRun
{

  #region [O4] AutoRun Entries / CheckOther4Item, CheckOther4ItemX64, CheckOther4ItemUsers

  [Export(typeof(IDetectionAddIn))]
  public class RegistryRun : IDetectionAddIn
  {
    #region private
    #region consts
    //TODO: Move to common location (localization?)
    private const string NO_FILE = @"(no file)";
    private const string FILE_MISSING = @"(file missing)";
    private const string NO_NAME = @"(no name)";
    #endregion

    #region AutoRun Registry Parsers
    private static RegistryParser[] _autoRunRegistryParsers;

    private static IEnumerable<RegistryParser> AutoRunRegistryParsers
    {
      get
      {
        if (_autoRunRegistryParsers != null && _autoRunRegistryParsers.Any())
          return _autoRunRegistryParsers;

        return _autoRunRegistryParsers = new[]
        {
          #region LocalMachine
          new RegistryParser(RegistryHive.LocalMachine, @"Software\Microsoft\Windows\CurrentVersion\Run",
            new[] {RegistryView.Registry32, RegistryView.Registry64}),

          new RegistryParser(RegistryHive.LocalMachine, @"Software\Microsoft\Windows\CurrentVersion\RunServices",
            new[] {RegistryView.Registry32, RegistryView.Registry64}),

          new RegistryParser(RegistryHive.LocalMachine, @"Software\Microsoft\Windows\CurrentVersion\RunOnce",
            new[] {RegistryView.Registry32, RegistryView.Registry64}),

          new RegistryParser(RegistryHive.LocalMachine, @"Software\Microsoft\Windows\CurrentVersion\RunServicesOnce",
            new[] {RegistryView.Registry32, RegistryView.Registry64}),

          new RegistryParser(RegistryHive.LocalMachine,
            @"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\Run",
            new[] {RegistryView.Registry32, RegistryView.Registry64}),

          #endregion

          #region CurrentUser
          new RegistryParser(RegistryHive.CurrentUser, @"Software\Microsoft\Windows\CurrentVersion\Run",
            new[] {RegistryView.Default}),
          new RegistryParser(RegistryHive.CurrentUser, @"Software\Microsoft\Windows\CurrentVersion\RunServices",
            new[] {RegistryView.Default}),
          new RegistryParser(RegistryHive.CurrentUser, @"Software\Microsoft\Windows\CurrentVersion\RunOnce",
            new[] {RegistryView.Default}),
          new RegistryParser(RegistryHive.CurrentUser, @"Software\Microsoft\Windows\CurrentVersion\RunServicesOnce",
            new[] {RegistryView.Default}),
          new RegistryParser(RegistryHive.CurrentUser,
            @"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\Run", new[] {RegistryView.Default}),

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
      get { return @"Registry Run"; }
    }

    public string[] Description
    {
      get { return new[]
      {
        "Scans the registry for applications that run automatically after a user has logged into Windows®.",
        string.Empty,
        string .Format("Registry locations include (but not limited to):\r\n\t{0}", string.Join("\r\n\t", AutoRunRegistryParsers.Where(w => w.RegistryHive == RegistryHive.LocalMachine).Select(s => s.SubKey))),
      };}
    }

    public void Dispose()
    {
      //Nothing to dispose?
    }

    public async Task<ICollection<IDetectionResultItem>> Scan()
    {
      //await Task.FromResult(0);
      
      var result = new Collection<IDetectionResultItem>();

      foreach (var item in AutoRunRegistryParsers)
      {
        foreach (var view in item.RegistryViews)
        {
          using (var regKey = RegistryKey.OpenBaseKey(item.RegistryHive, view).OpenSubKey(item.SubKey))
          {
            if (regKey != null && (regKey.ValueCount > 0 /*|| regKey.SubKeyCount > 0*/))
            {
              var values = regKey.GetValueNames();

              foreach (var value in values.Where(w => !string.IsNullOrEmpty(w)))
              {
                var sFile = regKey.GetValue(value, null) as string;

                if (string.IsNullOrEmpty(sFile))
                  sFile = NO_FILE;

                result.Add(new AutoRunRegistryResult(item.RegistryHive, regKey.View, regKey.Name, value, sFile));
              }
            }
          }
        }
      }
      return result;
    }

    public ScanResultType ResultType
    {
      get { return ScanResultType.AutoRun; }
    }
  }

  #endregion

  #region Original Visual Basic (6.0) Code Block

  /*
   Public Sub CheckOther4Item()
     
       Const KEY_WOW64_64KEY As Long = &H100& '32 bit app to access 64 bit hive
       Const KEY_WOW64_32KEY As Long = &H200& '64 bit app to access 32 bit hive
     
       Dim i%, j%, k%, hKey&, sName$, uData() As Byte, sMD5$
       Dim lHive&, sKey$, sRegRuns$(1 To 10), sData$, sHit$
       On Error GoTo Error:
     
       sRegRuns(1) = "HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Run"
       sRegRuns(2) = "HKLM\Software\Microsoft\Windows\CurrentVersion\RunServices"
       sRegRuns(3) = "HKLM\Software\Microsoft\Windows\CurrentVersion\RunOnce"
       sRegRuns(4) = "HKLM\Software\Microsoft\Windows\CurrentVersion\RunServicesOnce"
       sRegRuns(5) = "HKCU\Software\Microsoft\Windows\CurrentVersion\Run"
       sRegRuns(6) = "HKCU\Software\Microsoft\Windows\CurrentVersion\RunServices"
       sRegRuns(7) = "HKCU\Software\Microsoft\Windows\CurrentVersion\RunOnce"
       sRegRuns(8) = "HKCU\Software\Microsoft\Windows\CurrentVersion\RunServicesOnce"
       'added in 1.99.2
       sRegRuns(9) = "HKLM\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\Run"
       sRegRuns(10) = "HKCU\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\Run"
       'also see CheckOther4ItemUsers()
     
       For k = 1 To UBound(sRegRuns)
           If Left(sRegRuns(k), 4) = "HKLM" Then
               lHive = HKEY_LOCAL_MACHINE
           ElseIf Left(sRegRuns(k), 4) = "HKCU" Then
               lHive = HKEY_CURRENT_USER
           End If
           sKey = Mid(sRegRuns(k), 6)
     
           RegOpenKeyEx lHive, sKey, 0, KEY_QUERY_VALUE Or KEY_WOW64_32KEY, hKey
           If hKey <> 0 Then
               Do
                   sName = String(lEnumBufSize, 0)
                   ReDim uData(lEnumBufSize)
                   If RegEnumValue(hKey, i, sName, Len(sName), 0, ByVal 0, uData(0), UBound(uData)) = 0 Then
                       sName = TrimNull(sName)
                       'sData = ""
                       'For j = 0 To 510
                       '    If uData(j) = 0 Then Exit For
                       '    sData = sData & Chr(uData(j))
                       'Next j
                       sData = StrConv(uData, vbUnicode)
                       sData = TrimNull(sData)
                     
                       If sData <> vbNullString Then
                           Select Case k
                               Case 1: sHit = "O4 - HKLM\..\Run: "
                               Case 2: sHit = "O4 - HKLM\..\RunServices: "
                               Case 3: sHit = "O4 - HKLM\..\RunOnce: "
                               Case 4: sHit = "O4 - HKLM\..\RunServicesOnce: "
                               Case 5: sHit = "O4 - HKCU\..\Run: "
                               Case 6: sHit = "O4 - HKCU\..\RunServices: "
                               Case 7: sHit = "O4 - HKCU\..\RunOnce: "
                               Case 8: sHit = "O4 - HKCU\..\RunServicesOnce: "
                               Case 9: sHit = "O4 - HKLM\..\Policies\Explorer\Run: "
                               Case 10: sHit = "O4 - HKCU\..\Policies\Explorer\Run: "
                           End Select
                           sHit = sHit & "[" & sName & "] " & sData
                           If Not IsOnIgnoreList(sHit) Then
                               If bMD5 Then sMD5 = GetFileFromAutostart(sData)
                               sHit = sHit & sMD5
                               frmMain.lstResults.AddItem sHit
                           End If
                       End If
                   Else
                       Exit Do
                   End If
                   i = i + 1
               Loop
               'this last one makes some registry problems
               'and I can't figure out why
               RegCloseKey hKey
           End If
           i = 0
       Next k
       'added in HJT 1.99.2
       CheckOther4ItemUsers
     
       Dim sAutostartFolder$(1 To 8), sFile$, sShortCut$
       sAutostartFolder(1) = RegGetString(HKEY_CURRENT_USER, "Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", "Startup")
       sAutostartFolder(2) = RegGetString(HKEY_CURRENT_USER, "Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", "AltStartup")
       sAutostartFolder(3) = RegGetString(HKEY_CURRENT_USER, "Software\Microsoft\Windows\CurrentVersion\Explorer\User Shell Folders", "Startup")
       sAutostartFolder(4) = RegGetString(HKEY_CURRENT_USER, "Software\Microsoft\Windows\CurrentVersion\Explorer\User Shell Folders", "AltStartup")
       sAutostartFolder(5) = RegGetString(HKEY_LOCAL_MACHINE, "Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", "Common Startup")
       sAutostartFolder(6) = RegGetString(HKEY_LOCAL_MACHINE, "Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", "Common AltStartup")
       sAutostartFolder(7) = RegGetString(HKEY_LOCAL_MACHINE, "Software\Microsoft\Windows\CurrentVersion\Explorer\User Shell Folders", "Common Startup")
       sAutostartFolder(8) = RegGetString(HKEY_LOCAL_MACHINE, "Software\Microsoft\Windows\CurrentVersion\Explorer\User Shell Folders", "Common AltStartup")
     
       For k = 1 To UBound(sAutostartFolder)
           If sAutostartFolder(k) <> vbNullString And _
               FolderExists(sAutostartFolder(k)) Then
               sShortCut = Dir(sAutostartFolder(k) & "\*.*", vbArchive + vbHidden + vbReadOnly + vbSystem + vbDirectory)
               If sShortCut <> vbNullString Then
                   Do
                       Select Case k
                           Case 1: sHit = "O4 - Startup: "
                           Case 2: sHit = "O4 - AltStartup: "
                           Case 3: sHit = "O4 - User Startup: "
                           Case 4: sHit = "O4 - User AltStartup: "
                           Case 5: sHit = "O4 - Global Startup: "
                           Case 6: sHit = "O4 - Global AltStartup: "
                           Case 7: sHit = "O4 - Global User Startup: "
                           Case 8: sHit = "O4 - Global User AltStartup: "
                       End Select
                       sFile = GetFileFromShortCut(sAutostartFolder(k) & "\" & sShortCut)
                       sHit = sHit & sShortCut & sFile
                       If LCase(sShortCut) <> "desktop.ini" And _
                           sShortCut <> "." And sShortCut <> ".." And _
                           Not IsOnIgnoreList(sHit) Then
                           If bMD5 And sFile <> vbNullString And sFile <> " = ?" Then
                               sHit = sHit & GetFileMD5(Mid(sFile, 4))
                           End If
                           frmMain.lstResults.AddItem sHit
                       End If
                     
                       sShortCut = Dir
                   Loop Until sShortCut = vbNullString
               End If
           End If
       Next k
       Exit Sub
     
   Error:
       ErrorMsg "modMain_CheckOther4Item", Err.Number, Err.Description
   End Sub
  */

  #endregion
}