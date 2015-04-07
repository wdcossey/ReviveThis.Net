using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ComponentOwl.BetterListView;
using Microsoft.Win32;
using ReviveThis.Consts;
using ReviveThis.Entities;
using ReviveThis.Entities.ExtensionMethods;
using ReviveThis.Entities.Modules;
using ReviveThis.Enums;
using ReviveThis.Interfaces;

namespace ReviveThis.Forms
{
  public partial class FrmMain : Form
  {

    private const string DECRYPT_KEY = @"THOU SHALT NOT STEAL";

    public FrmMain()
    {
      InitializeComponent();


      var result = Hosts.ListHostsFile();
      if (result != null)
      {
        //listBox1.Items.AddRange(result.Lines);
      }

    }

    private void FrmMain_Load(object sender, System.EventArgs e)
    {
      if (Registry.CurrentUser.OpenSubKey(@"Software\ReviveThis.Net") != null)
        return;

      if (new FrmEula().ShowDialog() != DialogResult.OK)
      {
        this.Close();
      }
      else
      {
        Registry.CurrentUser.CreateSubKey(@"Software\ReviveThis.Net");
      }


    }

    protected override void OnShown(EventArgs e)
    {
      //int idx =
      //PSTaskDialog.cTaskDialog.ShowCommandBox(this,
      //                                        "Main Menu",
      //                                        "The main instruction text for the commandbox is shown here.",
      //                                        "The content text for the commandbox is shown here and the text will automatically wrap as needed.",
      //                                        "Any expanded content text for the commandbox is shown here and the text will automatically wrap as needed.",
      //                                        "Optional footer text with an icon can be included",
      //                                        "Don't show this message again",
      //                                        "Typical Scan\nRuns a system scan.\nGenerates (and saves) a logfile.|Simple Scan\nRuns a system scan.|Backup Manager\nView and\\or restore previous backups.|Open Tools\nDisplays various tools.",
      //                                        true,
      //                                        PSTaskDialog.eSysIcons.Information,
      //                                        PSTaskDialog.eSysIcons.Warning);

      base.OnShown(e);
    }

    #region [R0, R1, R2] ProcessRuleReg

    private Collection<IRegistryResultItem> ProcessRuleReg(string sRule)
    {
      var result = new Collection<IRegistryResultItem>();

      try
      {
        sRule = Encrypt.Decrypt(sRule, DECRYPT_KEY);

        if (string.IsNullOrEmpty(sRule))
          return null;

        //If Right(sRule, 1) = Chr(0) Then sRule = Left(sRule, Len(sRule) - 1)

        //Registry rule syntax:
        //[regkey],[regvalue],[infected data],[default data]
        //* [regkey]           = "" -> abort - no way man!
        // * [regvalue]        = "" -> delete entire key
        //  * [default data]   = "" -> delete value
        //   * [infected data] = "" -> any value (other than default) is considered infected
        var vRule = sRule.Split(new[] {','}, StringSplitOptions.None);

        if (vRule.Count() != 4 || !vRule[0].StartsWith("HK"))
          //decryption failed or spelling error
          return null;

        var mode = RegistryScanMode.None;

        if (string.IsNullOrEmpty(vRule[0]))
          return null;

        if (string.IsNullOrEmpty(vRule[1]))
          mode = RegistryScanMode.KeyPresent; // iMode = 2 -> check if regkey is present
        else if (string.IsNullOrEmpty(vRule[2]))
          mode = RegistryScanMode.ValuePresent; // iMode = 1 -> check if value is present
        else if (string.IsNullOrEmpty(vRule[3]))
          mode = RegistryScanMode.ValueInfected; // iMode = 0 -> check if value is infected

        var regPath = vRule[0].Split(new[] {'\\'});
        if (!regPath.Any() || regPath[0].Length != 4)
          return null;

        RegistryHive? lhive = null;

        switch (regPath[0].ToUpper())
        {
          case "HKLM":
            lhive = RegistryHive.LocalMachine;
            break;
          case "HKCU":
            lhive = RegistryHive.CurrentUser;
            break;
          case "HKCR":
            lhive = RegistryHive.ClassesRoot;
            break;
          default:
            return null;
        }

        //vRule(0) = Mid(CStr(vRule(0)), 6)

        if (vRule[1].Equals("(Default)", StringComparison.InvariantCultureIgnoreCase))
          vRule[1] = null;

        var rootKey = RegistryKey.OpenBaseKey(lhive.Value, RegistryView.Registry32);
        var loopFor64 = false;
        var keyPath = string.Join("\\", regPath, 1, regPath.Count() - 1);

        loop64:

        switch (mode)
        {
          case RegistryScanMode.ValueInfected: //0 'check for incorrect value

            using (var regKey = rootKey.OpenSubKey(keyPath))
            {
              //if (regKey == null)
              //  break;

              var sValue = regKey != null ? regKey.GetValue(vRule[1]) as string ?? string.Empty : string.Empty;
              //if (!string.IsNullOrEmpty(sValue))
              {

                var sWinDir = Environment.GetEnvironmentVariable("SystemRoot");

                if (sValue.IndexOf("%systemroot%", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                  sValue = sValue.ToLower().Replace("%systemroot%", sWinDir).ToLower();
                  vRule[2] = vRule[2].ToLower();
                }

                if (sValue.IndexOf(vRule[2], StringComparison.InvariantCultureIgnoreCase) == -1)
                {
                  var bIsNsbsd = false;
                  //check if domain is on safe list
                  if (ReviveThisApplication.Default.Settings.IgnoreSafe)
                    bIsNsbsd =
                      SafeDomains.Array.Select(s => Encrypt.Decrypt(s, DECRYPT_KEY))
                        .FirstOrDefault(w => sValue.Contains(w)) != null;

                  //make hit
                  if (!bIsNsbsd)
                  {
                    //if (sValue.Contains("%2e"))
                    //  sValue = Unescape(sValue);

                    //TODO: Implement IgnoreList
                    result.Add(new RegistryResultItem(
                      ScanResultType.RegistryValueChanged,
                      lhive.Value,
                      rootKey.View,
                      Path.Combine(rootKey.Name, keyPath),
                      vRule[1],
                      sValue));
                  }

                }

              }
            }
            break;
          case RegistryScanMode.ValuePresent: //1 'check for present value

            using (var regKey = rootKey.OpenSubKey(keyPath))
            {
              //if (regKey == null)
              //  break;

              var sValue = regKey != null ? regKey.GetValue(vRule[1]) as string ?? string.Empty : string.Empty;
              if (!string.IsNullOrEmpty(sValue))
              {
                //check if domain is on safe list
                var bIsNsbsd = false;
                if (ReviveThisApplication.Default.Settings.IgnoreSafe)
                  bIsNsbsd =
                    SafeDomains.Array.Select(s => Encrypt.Decrypt(s, DECRYPT_KEY))
                      .FirstOrDefault(w => sValue.Contains(w)) !=
                    null;

                //make hit
                if (!bIsNsbsd)
                {
                  //if (sValue.Contains("%2e"))
                  //  sValue = Unescape(sValue);

                  //TODO: Implement IgnoreList
                  result.Add(new RegistryResultItem(
                    ScanResultType.RegistryValueCreated,
                    lhive.Value,
                    rootKey.View,
                    Path.Combine(rootKey.Name, keyPath),
                    vRule[1],
                    sValue));
                }
              }
            }
            break;
          case RegistryScanMode.KeyPresent: //2 '

            using (var regKey = rootKey.OpenSubKey(keyPath))
            {
              if (regKey != null)
              {
                //TODO: Implement IgnoreList
                result.Add(new RegistryResultItem(
                  ScanResultType.RegistryKeyCreated,
                  lhive.Value,
                  rootKey.View,
                  Path.Combine(rootKey.Name, keyPath)));
              }
            }
            break;
          default:
            return null;
        }

        //if (rootKey != null)
        rootKey.Close();

        if (lhive == RegistryHive.LocalMachine && Environment.Is64BitOperatingSystem && !loopFor64)
        {
          rootKey = RegistryKey.OpenBaseKey(lhive.Value, RegistryView.Registry64);
          loopFor64 = true;
          goto loop64;
        }

      }
      catch (Exception)
      {
        throw;
      }

      return result.Any() ? result : null;
    }

    /*
    Private Sub ProcessRuleReg(ByVal sRule$)
        Dim vRule As Variant, iMode%, i%, bIsNSBSD As Boolean
        Dim sValue$, lHive&, sHit$
        On Error GoTo Error:
        If sRule = vbNullString Then Exit Sub
    
        'decrypt rule
        sRule = Crypt(sRule, sProgramVersion)
    
        If Right(sRule, 1) = Chr(0) Then sRule = Left(sRule, Len(sRule) - 1)
        'Registry rule syntax:
        '[regkey],[regvalue],[infected data],[default data]
        '* [regkey]           = "" -> abort - no way man!
        ' * [regvalue]        = "" -> delete entire key
        '  * [default data]   = "" -> delete value
        '   * [infected data] = "" -> any value (other than default) is considered infected
        vRule = Split(sRule, ",")
        If UBound(vRule) <> 3 Or _
           Left(CStr(vRule(0)), 2) <> "HK" Then
            'decryption failed or spelling error
            Exit Sub
        End If
    
        ' iMode = 0 -> check if value is infected
        ' iMode = 1 -> check if value is present
        ' iMode = 2 -> check if regkey is present
        If CStr(vRule(0)) = "" Then Exit Sub
        If CStr(vRule(3)) = "" Then iMode = 0
        If CStr(vRule(2)) = "" Then iMode = 1
        If CStr(vRule(1)) = "" Then iMode = 2
    
        Select Case Left(CStr(vRule(0)), 4)
            Case "HKLM": lHive = HKEY_LOCAL_MACHINE
            Case "HKCU": lHive = HKEY_CURRENT_USER
            Case "HKCR": lHive = HKEY_CLASSES_ROOT
            Case Else: Exit Sub
        End Select
        vRule(0) = Mid(CStr(vRule(0)), 6)
        If CStr(vRule(1)) = "(Default)" Then vRule(1) = ""
    
        Select Case iMode
            Case 0 'check for incorrect value
                sValue = RegGetString(lHive, CStr(vRule(0)), CStr(vRule(1)))
                If InStr(1, sValue, "%SYSTEMROOT%", vbTextCompare) Then
                    sValue = Replace(sValue, "%SYSTEMROOT%", sWinDir, , , vbTextCompare)
                    sValue = LCase(sValue)
                    vRule(2) = LCase(CStr(vRule(2)))
                End If
            
                'use instr instead of = to prevent stupid VB errs
                If InStr(1, sValue, CStr(vRule(2)), vbTextCompare) <> 1 Then
                    bIsNSBSD = False
                    For i = 0 To UBound(sSafeRegDomains)
                        If InStr(1, sValue, sSafeRegDomains(i), vbTextCompare) = 1 _
                           And sSafeRegDomains(i) <> vbNullString Then
                            bIsNSBSD = True
                            Exit For
                        End If
                    Next i
                    If bIgnoreSafe = False Then bIsNSBSD = False
                    If Not bIsNSBSD Then
                        If InStr(1, sValue, "%2e", vbTextCompare) > 0 Then sValue = Unescape(sValue)
                        sHit = "R0 - " & Left(sRule, InStr(sRule, ",") - 1) & "," & CStr(vRule(1)) & " = " & sValue
                        If IsOnIgnoreList(sHit) Then Exit Sub
                        frmMain.lstResults.AddItem sHit
                    End If
                End If
            Case 1  'check for present value
                sValue = RegGetString(lHive, CStr(vRule(0)), CStr(vRule(1)))
                If sValue <> vbNullString Then
                    'check if domain is on safe list
                    bIsNSBSD = False
                    For i = 0 To UBound(sSafeRegDomains)
                        If InStr(1, sValue, sSafeRegDomains(i), vbTextCompare) = 1 _
                           And sSafeRegDomains(i) <> vbNullString Then
                            bIsNSBSD = True
                            Exit For
                        End If
                    Next i
                    If bIgnoreSafe = False Then bIsNSBSD = False
                    'make hit
                    If Not bIsNSBSD Then
                        If InStr(1, sValue, "%2e", vbTextCompare) > 0 Then sValue = Unescape(sValue)
                        sHit = "R1 - " & Left(sRule, InStr(sRule, ",") - 1) & "," & IIf(CStr(vRule(1)) = "", "(Default)", CStr(vRule(1))) & IIf(sValue <> vbNullString, " = " & sValue, "")
                        If IsOnIgnoreList(sHit) Then Exit Sub
                        frmMain.lstResults.AddItem sHit
                    End If
                End If
            Case 2
                If RegKeyExists(lHive, CStr(vRule(0))) Then
                    sHit = "R2 - " & Left(sRule, InStr(sRule, ",") - 1)
                    If IsOnIgnoreList(sHit) Then Exit Sub
                    frmMain.lstResults.AddItem sHit
                End If
            Case Else: Exit Sub
        End Select
        Exit Sub
    
    Error:
        ErrorMsg "modMain_ProcessRuleReg", Err.Number, Err.Description, "sRule=" & sRule
    End Sub
    */

    #endregion

    #region [F0, F1, F2, F3] ProcessRuleIniFile

    private Collection<IDetectionResultItem> ProcessRuleIniFile(string sRule)
    {
      var result = new Collection<IDetectionResultItem>();

      try
      {
        //decrypt rule
        //sRule = Encrypt.Decrypt(sRule, DECRYPT_KEY);

        if (string.IsNullOrEmpty(sRule))
          return null;

        //IniFile rule syntax:
        //[inifile],[section],[value],[default data],[infected data]
        //* [inifile]          = "" -> abort
        // * [section]         = "" -> abort
        //  * [value]          = "" -> abort
        //   * [default data]  = "" -> delete if found
        //    * [infected data]= "" -> fix if infected

        var vRule = sRule.Split(new[] {','}, StringSplitOptions.None);

        if (vRule.Count() != 5 || vRule[0].IndexOf(".ini", StringComparison.InvariantCultureIgnoreCase) < 0)
          //decryption failed or spelling error
          return null;

        if (string.IsNullOrEmpty(vRule[0]) || string.IsNullOrEmpty(vRule[1]) || string.IsNullOrEmpty(vRule[2]))
          return null;

        var mode = IniScanMode.None;

        if (string.IsNullOrEmpty(vRule[3]))
          mode = vRule[0].StartsWith("REG", StringComparison.InvariantCultureIgnoreCase)
            ? IniScanMode.ValuePresentRegistry
            : IniScanMode.ValuePresent;
        else if (string.IsNullOrEmpty(vRule[4]))
          mode = vRule[0].StartsWith("REG", StringComparison.InvariantCultureIgnoreCase)
            ? IniScanMode.ValueInfectedRegistry
            : IniScanMode.ValueInfected;

        if (vRule[3].IndexOf("UserInit", StringComparison.InvariantCultureIgnoreCase) > -1)
          vRule[3] += ",";

        switch (mode)
        {
          case IniScanMode.ValueInfected: //0

            using (var iniFile = new IniParser(vRule[0]))
            {
              var sValue = iniFile.GetSetting(vRule[1], vRule[2]);
#if DEBUG
              //DEBUG build, lets spit out everything!
              if (true)
#else
              if (!string.IsNullOrEmpty(sValue) && !sValue.Equals(vRule[3]))
#endif
              {
                //TODO: Implement IgnoreList
                result.Add(new IniResultItem(ScanResultType.IniValueChanged, vRule[0], vRule[1], vRule[2], sValue,
                  vRule[3]));
              }
            }


            /*
            'sValue = String(255, " ")
            'GetPrivateProfileString CStr(vRule(1)), CStr(vRule(2)), "", sValue, 255, CStr(vRule(0))
            'sValue = RTrim(sValue)
            sValue = IniGetString(CStr(vRule(0)), CStr(vRule(1)), CStr(vRule(2)))
            If Right(sValue, 1) = Chr(0) Then sValue = Left(sValue, Len(sValue) - 1)
            'If RightB(sValue, 2) = Chr(0) Then sValue = LeftB(sValue, LenB(sValue) - 2)
            If Trim(LCase(sValue)) <> LCase(CStr(vRule(3))) Then
                If bIsWinNT And Trim(LCase(sValue)) <> vbNullString Then
                    sHit = "F0 - " & CStr(vRule(0)) & ": " & CStr(vRule(2)) & "=" & sValue
                    If IsOnIgnoreList(sHit) Then Exit Sub
                    If bMD5 Then sHit = sHit & GetFileFromAutostart(sValue)
                    frmMain.lstResults.AddItem sHit
                End If
            End If
             */
            break;
          case IniScanMode.ValuePresent: //1


            using (var iniFile = new IniParser(vRule[0]))
            {
              var sValue = iniFile.GetSetting(vRule[1], vRule[2]);
#if DEBUG
              //DEBUG build, lets spit out everything!
              if (true)
#else
              if (!string.IsNullOrEmpty(sValue))
#endif
              {
                //TODO: Implement IgnoreList
                result.Add(new IniResultItem(ScanResultType.IniValueCreated, vRule[0], vRule[1], vRule[2], sValue,
                  vRule[3]));
              }
            }

            /*
            'sValue = String(255, " ")
            'GetPrivateProfileString CStr(vRule(1)), CStr(vRule(2)), "", sValue, 255, CStr(vRule(0))
            'sValue = RTrim(sValue)
            sValue = IniGetString(CStr(vRule(0)), CStr(vRule(1)), CStr(vRule(2)))
            If Right(sValue, 1) = Chr(0) Then sValue = Left(sValue, Len(sValue) - 1)
            'If RightB(sValue, 2) = Chr(0) Then sValue = LeftB(sValue, LenB(sValue) - 2)
            If Trim(sValue) <> vbNullString Then
                sHit = "F1 - " & CStr(vRule(0)) & ": " & CStr(vRule(2)) & "=" & sValue
                If IsOnIgnoreList(sHit) Then Exit Sub
                If bMD5 Then sHit = sHit & GetFileFromAutostart(sValue)
                frmMain.lstResults.AddItem sHit
            End If 
            */
            break;
          case IniScanMode.ValueInfectedRegistry: //2
            //so far F2 is only reg:Shell and reg:UserInit

            var localMachineRoot = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
            var loopFor64 = false;

            loop64:
            using (
              var regKey = localMachineRoot.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\WinLogon", false))
            {
              string sValue = null;
              if (regKey != null)
              {
                sValue = regKey.GetValue(vRule[2], null) as string;
              }
#if DEBUG
              //DEBUG build, lets spit out everything!
              if (true)
#else
              if (!string.IsNullOrEmpty(sValue))
#endif
              {
                //TODO: Implement IgnoreList
                result.Add(new IniResultItem(ScanResultType.IniValueChangedRegistry, vRule[0], vRule[1], vRule[2],
                  sValue, vRule[3]));
              }
            }

            localMachineRoot.Close();

            if (Environment.Is64BitOperatingSystem && !loopFor64)
            {
              loopFor64 = true;
              localMachineRoot = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
              goto loop64;
            }

            /*
            'so far F2 is only reg:Shell and reg:UserInit
            sValue = RegGetString(HKEY_LOCAL_MACHINE, "Software\Microsoft\Windows NT\CurrentVersion\WinLogon", CStr(vRule(2)))
            If LCase(sValue) <> LCase(CStr(vRule(3))) Then
                sHit = "F2 - " & CStr(vRule(0)) & ": " & CStr(vRule(2)) & "=" & sValue
                If IsOnIgnoreList(sHit) Then Exit Sub
                If bMD5 Then sHit = sHit & GetFileFromAutostart(sValue)
                frmMain.lstResults.AddItem sHit
            End If
             */
            break;
          case IniScanMode.ValuePresentRegistry: //3
            //this is not really smart when more INIFile items get
            //added, but so far F3 is only reg:load and reg:run

            using (var rootKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32))
            using (var regKey = rootKey.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\WinLogon", false))
            {
              string sValue = null;
              if (regKey != null)
              {
                sValue = regKey.GetValue(vRule[2], null) as string;
              }
#if DEBUG
              //DEBUG build, lets spit out everything!
              if (true)
#else
              if (!string.IsNullOrEmpty(sValue))
#endif
              {
                //TODO: Implement IgnoreList
                result.Add(new IniResultItem(ScanResultType.IniValueCreatedRegistry, vRule[0], vRule[1], vRule[2],
                  sValue, vRule[3]));
              }
            }

            /*
            sValue = RegGetString(HKEY_CURRENT_USER, "Software\Microsoft\Windows NT\CurrentVersion\Windows", CStr(vRule(2)))
            If sValue <> vbNullString Then
                sHit = "F3 - " & CStr(vRule(0)) & ": " & CStr(vRule(2)) & "=" & sValue
                If IsOnIgnoreList(sHit) Then Exit Sub
                If bMD5 Then sHit = sHit & GetFileFromAutostart(sValue)
                frmMain.lstResults.AddItem sHit
            End If 
            */
            break;
          default:
            return null;
        }

      }
      catch (Exception)
      {
        throw;
      }

      return result.Any() ? result : null;
    }

/*
    Private Sub ProcessRuleIniFile(ByVal sRule$)
        Dim vRule As Variant, iMode%, sValue$, sHit$
        On Error GoTo Error:
        'IniFile rule syntax:
        '[inifile],[section],[value],[default data],[infected data]
        '* [inifile]          = "" -> abort
        ' * [section]         = "" -> abort
        '  * [value]          = "" -> abort
        '   * [default data]  = "" -> delete if found
        '    * [infected data]= "" -> fix if infected
    
        'decrypt rule
        'sRule = Crypt(sRule, sProgramVersion)
    
        If Right(sRule, 1) = Chr(0) Then sRule = Left(sRule, Len(sRule) - 1)
        vRule = Split(sRule, ",")
        If UBound(vRule) <> 4 Or _
           InStr(CStr(vRule(0)), ".ini") = 0 Then
            'spelling error or decrypting error
            Exit Sub
        End If
        If CStr(vRule(0)) = "" Then Exit Sub
        If CStr(vRule(1)) = "" Then Exit Sub
        If CStr(vRule(2)) = "" Then Exit Sub
        If CStr(vRule(4)) = "" Then iMode = 0
        If CStr(vRule(3)) = "" Then iMode = 1
    
        If InStr(CStr(vRule(3)), "UserInit") > 0 Then vRule(3) = CStr(vRule(3)) & ","
    
        If Left(CStr(vRule(0)), 3) = "REG" Then
            If Not bIsWinNT Then Exit Sub
        
            If CStr(vRule(4)) = "" Then iMode = 2
            If CStr(vRule(3)) = "" Then iMode = 3
        End If
    
        'iMode:
        ' 0 = check if value is infected
        ' 1 = check if value is present
        ' 2 = check if value is infected, in the Registry
        ' 3 = check if value is present, in the Registry
    
        Select Case iMode
            Case 0
                'sValue = String(255, " ")
                'GetPrivateProfileString CStr(vRule(1)), CStr(vRule(2)), "", sValue, 255, CStr(vRule(0))
                'sValue = RTrim(sValue)
                sValue = IniGetString(CStr(vRule(0)), CStr(vRule(1)), CStr(vRule(2)))
                If Right(sValue, 1) = Chr(0) Then sValue = Left(sValue, Len(sValue) - 1)
                'If RightB(sValue, 2) = Chr(0) Then sValue = LeftB(sValue, LenB(sValue) - 2)
                If Trim(LCase(sValue)) <> LCase(CStr(vRule(3))) Then
                    If bIsWinNT And Trim(LCase(sValue)) <> vbNullString Then
                        sHit = "F0 - " & CStr(vRule(0)) & ": " & CStr(vRule(2)) & "=" & sValue
                        If IsOnIgnoreList(sHit) Then Exit Sub
                        If bMD5 Then sHit = sHit & GetFileFromAutostart(sValue)
                        frmMain.lstResults.AddItem sHit
                    End If
                End If
            Case 1
                'sValue = String(255, " ")
                'GetPrivateProfileString CStr(vRule(1)), CStr(vRule(2)), "", sValue, 255, CStr(vRule(0))
                'sValue = RTrim(sValue)
                sValue = IniGetString(CStr(vRule(0)), CStr(vRule(1)), CStr(vRule(2)))
                If Right(sValue, 1) = Chr(0) Then sValue = Left(sValue, Len(sValue) - 1)
                'If RightB(sValue, 2) = Chr(0) Then sValue = LeftB(sValue, LenB(sValue) - 2)
                If Trim(sValue) <> vbNullString Then
                    sHit = "F1 - " & CStr(vRule(0)) & ": " & CStr(vRule(2)) & "=" & sValue
                    If IsOnIgnoreList(sHit) Then Exit Sub
                    If bMD5 Then sHit = sHit & GetFileFromAutostart(sValue)
                    frmMain.lstResults.AddItem sHit
                End If
            Case 2
                'so far F2 is only reg:Shell and reg:UserInit
                sValue = RegGetString(HKEY_LOCAL_MACHINE, "Software\Microsoft\Windows NT\CurrentVersion\WinLogon", CStr(vRule(2)))
                If LCase(sValue) <> LCase(CStr(vRule(3))) Then
                    sHit = "F2 - " & CStr(vRule(0)) & ": " & CStr(vRule(2)) & "=" & sValue
                    If IsOnIgnoreList(sHit) Then Exit Sub
                    If bMD5 Then sHit = sHit & GetFileFromAutostart(sValue)
                    frmMain.lstResults.AddItem sHit
                End If
            Case 3
                'this is not really smart when more INIFile items get
                'added, but so far F3 is only reg:load and reg:run
                sValue = RegGetString(HKEY_CURRENT_USER, "Software\Microsoft\Windows NT\CurrentVersion\Windows", CStr(vRule(2)))
                If sValue <> vbNullString Then
                    sHit = "F3 - " & CStr(vRule(0)) & ": " & CStr(vRule(2)) & "=" & sValue
                    If IsOnIgnoreList(sHit) Then Exit Sub
                    If bMD5 Then sHit = sHit & GetFileFromAutostart(sValue)
                    frmMain.lstResults.AddItem sHit
                End If
        End Select
        Exit Sub
    
    Error:
        ErrorMsg "modMain_ProcessRuleIniFile", Err.Number, Err.Description, "sRule=" & sRule
    End Sub
*/

    #endregion

    private BetterListViewGroup GetGroup(BetterListView listView, IDetectionResultItem resultItem)
    {
      var result = listView.Groups.FirstOrDefault(f => f.Name.Equals(resultItem.ResultType.ToString()));

      if (result == null && resultItem.ResultType == ScanResultType.CustomAddIn && resultItem is ICustomAddInSection)
      {
        var customSection = (ICustomAddInSection) resultItem;

        result = listView.Groups.FirstOrDefault(f => f.Name.Equals(customSection.CustomAddInSection.Id));

        if (result == null)
        {
          result = new BetterListViewGroup(customSection.CustomAddInSection.Id,
            string.Format("{0} - {1}", customSection.CustomAddInSection.Id, customSection.CustomAddInSection.Text));
          betterListView1.Groups.Add(result);
        }

      }

      return result;
    }

    private async void button1_Click(object sender, System.EventArgs e)
    {

      var sw = new Stopwatch();
      try
      {

        sw.Start();


        betterListView1.BeginUpdate();
        betterListView1.Items.Clear();


        //var resultTypes = typeof (ScanResultType).GetEnumNames();
        foreach (var value in Enum.GetValues(typeof (ScanResultType)))
        {
          //(ScanResultType)value
          //typeof(ScanResultType).GetMember(scanResultType.ToString())
        }

        if (!betterListView1.Groups.Any())
          betterListView1.Groups.AddRange(
            Enum.GetValues(typeof (ScanResultType))
              .Cast<ScanResultType>()
              .Where(w => w != ScanResultType.CustomAddIn)
              .Select(item => new BetterListViewGroup(item.ToString(), item.FormatToString(true)))
              .ToArray());

        foreach (var value in RegistryValues.Array)
        {
          var item = ProcessRuleReg(value);
          if (item != null && item.Any())
            betterListView1.Items.AddRange(
              item.Select(
                s =>
                  new BetterListViewItem(new[] {s.LegacyString, string.Empty})
                  {
                    Tag = s,
                    Group = betterListView1.Groups.FirstOrDefault(f => f.Name.Equals(s.ResultType.ToString()))
                  })
                .ToArray());
        }

        foreach (var value in IniFileValues.Array)
        {
          var item = ProcessRuleIniFile(value);
          if (item != null && item.Any())
            betterListView1.Items.AddRange(
              item.Select(
                s =>
                  new BetterListViewItem(new[] {s.LegacyString, string.Empty})
                  {
                    Tag = s,
                    Group = betterListView1.Groups.FirstOrDefault(f => f.Name.Equals(s.ResultType.ToString()))
                  })
                .ToArray());
        }

        foreach (var addIn in ReviveThisApplication.Default.AddIns.Detection.OrderBy(o => o.ResultType))
        {
          try
          {
            var result = await addIn.Scan();
            if (result != null && result.Any())
            {
              betterListView1.Items.AddRange(
                result.Select(
                  s =>
                    new BetterListViewItem(new[] {s.LegacyString, string.Empty})
                    {
                      Tag = s,
                      Group = GetGroup(betterListView1, s)
                    })
                  .ToArray());
            }
          }
          catch (Exception ex)
          {
            MessageBox.Show(
              string.Format("{0}\n\n{1}\n\n{2}\n\n{3}", addIn.GetType().Assembly.ManifestModule.ScopeName, addIn.Name,
                ex.Message, addIn.GetType().Assembly.FullName), null, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //throw;
          }

        }

        //Netscape/Mozilla stuff
        //CheckNetscapeMozilla        'N1-4

        ////CheckOther1Item
        //using (var hostsFile = new Module.HostsFile.Plugin())
        //{
        //  var result = hostsFile.Execute();
        //  if (result != null && result.Any())
        //    checkedListBox1.Items.AddRange(result.Select(s => s.AsString).ToArray());
        //}

        ////CheckOther2Item
        //using (var bhoPlugin = new Module.BrowserHelperObject.Plugin())
        //{
        //  var result = bhoPlugin.Execute();
        //  if (result != null && result.Any())
        //    checkedListBox1.Items.AddRange(result.Select(s => s.AsString).ToArray());
        //}

        ////CheckOther3Item
        //using (var bhoPlugin = new Module.InternetExplorerToolbar.Plugin())
        //{
        //  var result = bhoPlugin.Execute();
        //  if (result != null && result.Any())
        //    checkedListBox1.Items.AddRange(result.Select(s => s.AsString).ToArray());
        //}

        ////CheckOther4Item
        //using (var bhoPlugin = new Module.AutoRun.Plugin())
        //{
        //  var result = bhoPlugin.Execute();
        //  if (result != null && result.Any())
        //    checkedListBox1.Items.AddRange(result.Select(s => s.AsString).ToArray());
        //}

        ////CheckOther5Item
        //using (var bhoPlugin = new Module.ControlIni.Plugin())
        //{
        //  var result = bhoPlugin.Execute();
        //  if (result != null && result.Any())
        //    checkedListBox1.Items.AddRange(result.Select(s => s.AsString).ToArray());
        //}

      }
      finally
      {
        betterListView1.EndUpdate();
        sw.Stop();
        MessageBox.Show(string.Format("Elapsed Time: {0}", sw.Elapsed), @"Scan Time:");
      }
    }

    private void button2_Click(object sender, EventArgs e)
    {
      var result = ReviveThisApplication.Default.AddIns.Detection.OrderBy(o => o.ResultType)
        .Aggregate(string.Empty, (current, addIn) => current + string.Format("{0} - v{1}\n", addIn.Name, addIn.Version));
      MessageBox.Show(result);
    }

    private void addinManagerToolStripMenuItem_Click(object sender, EventArgs e)
    {
      using (var dialog = new FrmAddInManager())
      {
        dialog.ShowDialog();
      }
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Close();
    }

    private async void AnalyserEventHandler(object sender, EventArgs e)
    {

      if (sender == null)
        return;

      var senderObj = sender as ToolStripMenuItem;

      if (senderObj == null)
        return;

      var addIn = senderObj.Tag as IAnalysisAddIn;

      if (addIn == null)
        return;

      var dlgProgress = new FrmProgress();

      try
      {
        this.Enabled = false;
        dlgProgress.Left = (this.Left + ((this.Width / 2) - (dlgProgress.Width / 2)));
        dlgProgress.Top = (this.Top + ((this.Height / 2) - (dlgProgress.Height / 2)));
        dlgProgress.Show(this);

        dlgProgress.UseWaitCursor = true;

        var processList = betterListView1.Items.Select(s => ((IDetectionResultItem) s.Tag)).ToList();
        var results = await addIn.Analyse(processList);

        if (!results.Any()) 
          return;

        betterListView1.BeginUpdate();

        foreach (var result in results)
        {
          var item = betterListView1.Items.FirstOrDefault(f => f.Tag == result.Match);
          if (item == null)
            continue;

          switch (result.Result)
          {
            case AnalyseResultType.Safe:
              item.SubItems[1].ImageIndex = 2;
              break;
            case AnalyseResultType.Critical:
              item.SubItems[1].ImageIndex = 0;
              break;
            case AnalyseResultType.Caution:
              item.SubItems[1].ImageIndex = 1;
              break;
            default: // AnalyseResultType.Unknown
              item.SubItems[1].ImageIndex = 3;
              break;
          }

          item.SubItems.Add(result.Text);
        }

        results.Clear();


        //betterListView1.Items.Select(s => ((IScanResultItem) s.Tag).LegacyString.Trim();
      }
      catch (Exception)
      {

        throw;
      }
      finally
      {
        this.Enabled = true;
        dlgProgress.CloseMe();
        betterListView1.EndUpdate();
      }



    }

    private void button3_Click(object sender, EventArgs e)
    {
      try
      {
        if (ReviveThisApplication.Default.AddIns.Analysis.Any())
        {
          cmsAnalyse.Items.Clear();
          cmsAnalyse.Items.AddRange(
            ReviveThisApplication.Default.AddIns.Analysis.Select(
              o => new ToolStripMenuItem(o.Name, null, AnalyserEventHandler) {Tag = o}).ToArray());
          cmsAnalyse.Show(button3, 0, button3.Height);
        }
      }
      catch (Exception)
      {

        throw;
      }
    }

    private void betterListView1_MouseClick(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        var senderObj = sender as BetterListView;

        if (senderObj == null || senderObj.SelectedItems.Count <= 0)
        {
          return;
        }

        var item = senderObj.SelectedItems.FirstOrDefault() ?? senderObj.FocusedItem;

        if (item == null || item.Tag == null || !(item.Tag is IDetectionItemContextMenuCollection))
        {
          return;
        }

        var resultItem = item.Tag as IDetectionItemContextMenuCollection;

        resultItem.ShowContextMenu(senderObj, e.Location);

      }
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      using (var about = new FrmAbout())
      {
        about.ShowDialog();
      }
    }

    private async void betterListView1_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        var senderObj = sender as BetterListView;

        if (senderObj == null || senderObj.SelectedItems.Count != 1)
        {
          return;
        }

        var item = senderObj.FocusedItem;

        if (item == null || item.Tag == null || !(item.Tag is IDetectionResultItem) || !(item.Tag is IDetectionRepair))
        {
          return;
        }

        var resultItem = item.Tag as IDetectionRepair;

        try
        {
          var result = await resultItem.Repair();

          var msgBoxIcon = MessageBoxIcon.None;
          switch (result.Result)
          {
            case RepairResultType.Failed:
              msgBoxIcon = MessageBoxIcon.Error;
              break;
            case RepairResultType.Partial:
              msgBoxIcon = MessageBoxIcon.Warning;
              break;
            case RepairResultType.Successful:
              msgBoxIcon = MessageBoxIcon.Information;
              break;
            case RepairResultType.Unknown:
            default:
              break;
          }

          MessageBox.Show(result.Text, ((IDetectionResultItem)resultItem).LegacyString, MessageBoxButtons.OK, msgBoxIcon);
        }
        catch (Exception ex)
        {
          MessageBox.Show(
            string.Format("{0}\n\n{1}\n\n{2}\n\n{3}", resultItem.GetType().Assembly.ManifestModule.ScopeName, ((IDetectionResultItem)resultItem).LegacyString,
              ex.Message, resultItem.GetType().Assembly.FullName), null, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


      }
    }

  }
}
