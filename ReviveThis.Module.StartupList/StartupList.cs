using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using ReviveThis.Module.Enums;
using Microsoft.Win32;
using ReviveThis.Entities.ExtensionMethods;
using ReviveThis.Entities.Modules;
using ReviveThis.Entities.PInvoke;
using ReviveThis.Interfaces;

namespace ReviveThis.Module
{
  public class StartupList : IModule
  {
    private static volatile StartupList _instance;
    private static readonly object LockObj = new Object();

    private bool _htmlOutput = false;
    private bool _verboseMode = false;
    private bool _complete = false;
    private bool _forceAll = false;
    private bool _full = false;

    public static IModule Default
    {
      get
      {
        if (_instance == null)
        {
          lock (LockObj)
          {
            if (_instance == null)
              _instance = new StartupList();
          }
        }

        return _instance;
      }
    }

    public void Execute()
    {

      #region Confirmation Message

      if (
        MessageBox.Show(
          "This will create a list of all startup entries in the Registry and various Windows files\nand display them in Notepad. The process may take up to a few seconds on slow systems.\nIt will in no way alter anything on your system.\n\nDo you want to continue?",
          "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;

      /*
            sMsg = "This will create a list of all startup entries "
            sMsg = sMsg & "in the Registry and various Windows files" & vbCrLf
            sMsg = sMsg & "and display them in Notepad. "
            sMsg = sMsg & "The process may take up to a few seconds on "
            sMsg = sMsg & "slow systems." & vbCrLf
            sMsg = sMsg & "It will in no way alter anything on your system." & vbCrLf & vbCrLf
            sMsg = sMsg & "Do you want to continue?"
            If MsgBox(sMsg, vbQuestion + vbYesNo, "StartupList") = vbNo Then Exit Sub
        */

      #endregion

      #region Arguments

      var arguments = Environment.GetCommandLineArgs().Select(s => s.ToLower()).ToArray();

      _verboseMode = false;
      _complete = false;
      //var bForceWin9x = false;
      //var bForceWinNT = false;
      _forceAll = false;
      _htmlOutput = false;
      _full = false;


      if (arguments.Any())
      {
        _verboseMode = arguments.Contains("/verbose");
        _complete = arguments.Contains("/complete");
        _forceAll = arguments.Contains("/forceall");
        //bForceWin9x = !bForceAll && arguments.Contains("/force9x");
        //bForceWinNT = !bForceAll && arguments.Contains("/forcent");
        _htmlOutput = arguments.Contains("/html");
        _full = arguments.Contains("/full");
      }
      /*        
            If Len(Command$) > 0 Then
                If InStr(Command$, "/verbose") > 0 Then bVerbose = True
                If InStr(Command$, "/complete") > 0 Then bComplete = True
                If InStr(Command$, "/force9x") > 0 Then bForceWin9x = True
                If InStr(Command$, "/forcent") > 0 Then bForceWinNT = True
                If InStr(Command$, "/forceall") > 0 Then bForceAll = True
                If InStr(Command$, "/html") > 0 Then bHTML = True
                If InStr(Command$, "/full") > 0 Then bFull = True
                If bForceAll Then
                    bForceWin9x = False
                    bForceWinNT = False
                End If
            End If
        */

      #endregion

      var className = this.GetType().Name;
      var version = Assembly.GetExecutingAssembly().GetName().Version.FormatToString(true);
      var fileName = Path.GetFullPath(new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath);

      var iexplorerVersion = Utils.GetInternetExplorerVersion(string.Empty);
      var chromeVersion = Utils.GetChromeVersion(string.Empty);
      var fireFoxVersion = Utils.GetFireFoxVersion(string.Empty);
      var operaVersion = Utils.GetOperaVersion(string.Empty);

      using (
        var fileStream = new FileStream("test.txt", File.Exists("test.txt") ? FileMode.Truncate : FileMode.CreateNew,
          FileAccess.Write))
      using (var writer = new StreamWriter(fileStream))
      {
        #region Report Header

        writer.WriteLine("{0} report, {1:d}, {1:T}", className, DateTime.Now);
        writer.WriteLine("{0} version: {1}", className, version);
        writer.WriteLine("Started from : {0}", fileName);
        writer.WriteLine("Detected: {0} x{1} {2} {3} ({4})", OsInfo.Name,
          Environment.Is64BitOperatingSystem ? "64" : "86", OsInfo.Edition, OsInfo.ServicePack, OsInfo.VersionString);

        if (!string.IsNullOrEmpty(iexplorerVersion))
          writer.WriteLine("Detected: Internet Explorer ({0})", iexplorerVersion);

        if (!string.IsNullOrEmpty(chromeVersion))
          writer.WriteLine("Detected: Google Chrome ({0})", chromeVersion);

        if (!string.IsNullOrEmpty(fireFoxVersion))
          writer.WriteLine("Detected: FireFox ({0})", fireFoxVersion);

        if (!string.IsNullOrEmpty(operaVersion))
          writer.WriteLine("Detected: Opera ({0})", operaVersion);

        if (arguments.Any())
        {
          writer.WriteLine("* Using default options");
        }
        else
        {
          if (_verboseMode)
            writer.WriteLine("* Using verbose mode");

          if (_complete)
            writer.WriteLine("* Including empty and uninteresting sections");

          //if (bForceWin9x)
          //  writer.WriteLine("* Forcing include of Win9x-only sections");

          //if (bForceWinNT)
          //  writer.WriteLine("* Forcing include of WinNT-only sections");

          if (_forceAll)
            writer.WriteLine("* Forcing include of all possible sections");

          if (_full)
            writer.WriteLine("* Showing rarely important sections");
        }

        writer.WriteLine(new string('=', 50));

        /*
              sReport = sReport & IIf(Command$ = "", "* Using default options" & vbCrLf, "")
              sReport = sReport & IIf(bVerbose, "* Using verbose mode" & vbCrLf, "")
              sReport = sReport & IIf(bComplete, "* Including empty and uninteresting sections" & vbCrLf, "")
              sReport = sReport & IIf(bForceWin9x, "* Forcing include of Win9x-only sections" & vbCrLf, "")
              sReport = sReport & IIf(bForceWinNT, "* Forcing include of WinNT-only sections" & vbCrLf, "")
              sReport = sReport & IIf(bForceAll, "* Forcing include of all possible sections" & vbCrLf, "")
              sReport = sReport & IIf(bFull, "* Showing rarely important sections" & vbCrLf, "")
              sReport = sReport & String(50, "=") & vbCrLf & vbCrLf 
          */

        #endregion

        writer.WriteLine();
        ListRunningProcesses(writer);
        writer.WriteLine();
        CheckAutoStartFolders(writer);
        writer.WriteLine();
        CheckWinNtUserInit(writer);
        writer.WriteLine();
        RegistryAutoRun(writer);

      }
    }

    #region ListRunningProcesses

    private void ListRunningProcesses(TextWriter writer)
    {
      if (writer == null)
        return;

      writer.WriteLine("Running processes:");
      writer.WriteLine();
      var processList = ProcessManager.RefreshProcessList();
      foreach (var process in processList)
      {
        try
        {
          writer.WriteLine(process.MainModule.FileName);
        }
        catch (Exception)
        {
          //throw;
        }
      }

      if (_verboseMode)
      {
        writer.WriteLine("\r\nThis lists all processes running in memory, which are all active\r\n" +
                         "programs and some non-exe system components.\r\n" /*+
                         "Essential processes include: KERNEL32.DLL, MSGSRV32.EXE, MPREXE.EXE,\r\n" +
                         "MMTASK.TSK, EXPLORER.EXE (only once), DDHELP.EXE, RNAAPP.EXE,\r\n" +
                         "TAPISRV.EXE and EM_EXEC.EXE.\r\n"*/);
      }

      writer.WriteLine(new string('-', 50));
    }

    /*
      Private Sub ListRunningProcesses()
          'sub applies to all windows versions,
          'but uses different methods for WinNT/9x
    
          sReport = sReport & "[tag1]Running processes:[/tag1]" & vbCrLf & vbCrLf
          If (bIsWinNT Or bForceWinNT) And Not bForceAll Then GoTo WinNTMethod:
    
      Win9xMethod:
          Dim hSnap&, uProcess As PROCESSENTRY32, sDummy$
          If bForceAll Then sReport = sReport & "[Using Win9x method]" & vbCrLf & vbCrLf
          On Error Resume Next
          hSnap = CreateToolhelpSnapshot(TH32CS_SNAPPROCESS, 0)
          On Error GoTo Error:
          If hSnap < 1 Then
              sReport = sReport & "*Unable to list processes*" & vbCrLf
              GoTo EndOfSub:
          End If
    
          uProcess.dwSize = Len(uProcess)
    
          If ProcessFirst32(hSnap, uProcess) = 0 Then
              sReport = sReport & "*No running processes found*" & vbCrLf
              GoTo EndOfSub
          End If
    
          Do
              sDummy = Left(uProcess.szExeFile, InStr(uProcess.szExeFile, Chr(0)) - 1)
              If Not bIsWinNT Then
                  sReport = sReport & sDummy & vbCrLf
              Else
                  If sDummy <> "[System Process]" And _
                     sDummy <> "System" Then
                      sReport = sReport & GetLongPath(sDummy) & vbCrLf
                  End If
              End If
          Loop Until ProcessNext32(hSnap, uProcess) = 0
          CloseHandle hSnap
          If bForceAll Then
              sReport = sReport & vbCrLf & "[Using WinNT method]" & vbCrLf & vbCrLf
              GoTo WinNTMethod:
          End If
          GoTo EndOfSub:
    
      WinNTMethod:
          Dim lProcesses&(1 To 1024), lNeeded&, lNumProcesses&
          Dim hProc&, sProcessName$, lModules&(1 To 1024), i%
          On Error Resume Next
          If EnumProcesses(lProcesses(1), CLng(1024) * 4, lNeeded) = 0 Then
              sReport = sReport & "(PSAPI.DLL was not found or is " & _
                        "the wrong version. "
              If bForceAll Then
                  sReport = sReport & ")" & vbCrLf
                  GoTo EndOfSub
              End If
              sReport = sReport & "Using Win9x method instead.)" & vbCrLf & vbCrLf
              GoTo Win9xMethod:
          End If
          On Error GoTo Error:
    
          lNumProcesses = lNeeded / 4
          For i = 1 To lNumProcesses
              hProc = OpenProcess(PROCESS_QUERY_INFORMATION Or PROCESS_VM_READ, 0, lProcesses(i))
              If hProc <> 0 Then
                  lNeeded = 0
                  sProcessName = String(260, 0)
                  If EnumProcessModules(hProc, lModules(1), CLng(1024) * 4, lNeeded) <> 0 Then
                      GetModuleFileNameExA hProc, lModules(1), sProcessName, Len(sProcessName)
                      sProcessName = TrimNull(sProcessName)
                      If Left(sProcessName, 1) = "\" Then sProcessName = Mid(sProcessName, 2)
                      If Left(sProcessName, 3) = "??\" Then sProcessName = Mid(sProcessName, 4)
                      If InStr(1, sProcessName, "%SYSTEMROOT%", vbTextCompare) > 0 Then sProcessName = Replace(sProcessName, "Systemroot", sWinDir, , , vbTextCompare)
                      If InStr(1, sProcessName, "SYSTEMROOT", vbTextCompare) > 0 Then sProcessName = Replace(sProcessName, "Systemroot", sWinDir, , , vbTextCompare)
                
                      sReport = sReport & sProcessName & vbCrLf
                  End If
                  CloseHandle hProc
              End If
          Next i
    
      EndOfSub:
          If bVerbose Then
              sVerbose = "This lists all processes running in memory, "
              sVerbose = sVerbose & "which are all active" & vbCrLf & "programs "
              sVerbose = sVerbose & "and some non-exe system components." & vbCrLf
              '  uncomment when I have list of NT essential components
              'sVerbose = sVerbose  & "Essential processes include: "
              'sVerbose = sVerbose & "KERNEL32.DLL, MSGSRV32.EXE, MPREXE.EXE," & vbCrLf
              'sVerbose = sVerbose & "MMTASK.TSK, EXPLORER.EXE (only once), "
              'sVerbose = sVerbose & "DDHELP.EXE, RNAAPP.EXE," & vbCrLf & "TAPISRV.EXE  "
              'sVerbose = sVerbose & "and EM_EXEC.EXE." & vbCrLf
              sReport = sReport & vbCrLf & sVerbose
          End If
          sReport = sReport & vbCrLf & String(50, "-") & vbCrLf & vbCrLf
          Exit Sub
    
      Error:
          CloseHandle hSnap
          ErrorMsg Err.Number, Err.Description, "ListRunningProcesses"
      End Sub 
      */

    #endregion

    #region CheckAutoStartFolders
    private void CheckAutoStartFolders(TextWriter writer)
    {
      if (writer == null)
        return;

      var strBuilder = new StringBuilder();

      strBuilder.AppendLine("Listing of startup folders:");
      strBuilder.AppendLine();

      strBuilder.AppendLine("Shell folders Startup:");
      var userStartUp = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
      if (Directory.Exists(userStartUp))
      {
        strBuilder.AppendLine(string.Format("[{0}]", userStartUp));
        var fileList = Directory.EnumerateFiles(userStartUp, "*.*", SearchOption.AllDirectories);
        foreach (var file in fileList.Where(w => !w.EndsWith("desktop.ini", StringComparison.InvariantCultureIgnoreCase)))
        {
          strBuilder.AppendLine(string.Format("{0} = {1}", Path.GetFileName(file), new ShLink(file).GetPath));
        }
      }
      else
      {
        //*Folder not found*
      }
      strBuilder.AppendLine();

      strBuilder.AppendLine("Shell folders Common Startup:");
      var commonStartUp = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup);
      if (Directory.Exists(commonStartUp))
      {
        strBuilder.AppendLine(string.Format("[{0}]", commonStartUp));
        var fileList = Directory.EnumerateFiles(commonStartUp, "*.*", SearchOption.AllDirectories);
        foreach (var file in fileList.Where(w => !w.EndsWith("desktop.ini", StringComparison.InvariantCultureIgnoreCase)))
        {
          strBuilder.AppendLine(string.Format("{0} = {1}", Path.GetFileName(file), new ShLink(file).GetPath));
        }
      }
      else
      {
        //*Folder not found*
      }
      
      if (_verboseMode)
      {
        strBuilder.AppendLine("\r\nThis lists all programs or shortcuts in folders marked by Windows as\r\n" +
                         "'Autostart folder', which means any files within these folders are\r\n" +
                         "launched when Windows is started. The Windows standard is that only\r\n" +
                         "shortcuts (*.lnk, *.pif) should be present in these folders.\r\n" +
                         "The location of these folders is set in the Registry.\r\n");
      }

      strBuilder.AppendLine(new string('-', 50));

      writer.Write(strBuilder.ToString());
    }

    /*
    Private Sub CheckAutoStartFolders()
        'sub applies to all windows versions
    
        Dim sResult$, ss$
        'Dim sDummy$, hKey&, uData() As Byte, i%, sData$
        On Error GoTo Error:
    
        'checking all *8* possible folders now - 1.52+
        sResult = sResult & ListFiles(HKEY_CURRENT_USER, "Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", "Startup", "Shell folders Startup")
        ss = ListFiles(HKEY_CURRENT_USER, "Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", "AltStartup", "Shell folders AltStartup")
        sResult = sResult & ss
        sResult = sResult & ListFiles(HKEY_CURRENT_USER, "Software\Microsoft\Windows\CurrentVersion\Explorer\User Shell Folders", "Startup", "User shell folders Startup")
        sResult = sResult & ListFiles(HKEY_CURRENT_USER, "Software\Microsoft\Windows\CurrentVersion\Explorer\User Shell Folders", "AltStartup", "User shell folders AltStartup")
        sResult = sResult & ListFiles(HKEY_LOCAL_MACHINE, "Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", "Common Startup", "Shell folders Common Startup")
        sResult = sResult & ListFiles(HKEY_LOCAL_MACHINE, "Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", "Common AltStartup", "Shell folders Common AltStartup")
        sResult = sResult & ListFiles(HKEY_LOCAL_MACHINE, "Software\Microsoft\Windows\CurrentVersion\Explorer\User Shell Folders", "Common Startup", "User shell folders Common Startup")
        sResult = sResult & ListFiles(HKEY_LOCAL_MACHINE, "Software\Microsoft\Windows\CurrentVersion\Explorer\User Shell Folders", "Common AltStartup", "User shell folders Alternate Common Startup")
    
        '===============================================
        If bVerbose Then
            sVerbose = "This lists all programs or shortcuts in folders "
            sVerbose = sVerbose & "marked by Windows as" & vbCrLf & "'Autostart folder', "
            sVerbose = sVerbose & "which means any files within these folders "
            sVerbose = sVerbose & "are" & vbCrLf & "launched when Windows is started. "
            sVerbose = sVerbose & "The Windows standard is that only" & vbCrLf
            sVerbose = sVerbose & "shortcuts (*.lnk, *.pif) should be present "
            sVerbose = sVerbose & "in these folders." & vbCrLf
            sVerbose = sVerbose & "The location of these folders is set in the Registry." & vbCrLf & vbCrLf
            sResult = sResult & sVerbose
        End If
        sResult = sResult & String(50, "-")
        sResult = sResult & vbCrLf & vbCrLf
    
        If sResult <> sVerbose & String(50, "-") & vbCrLf & vbCrLf Then
            sReport = sReport & "[tag2]Listing of startup folders:[/tag2]" & vbCrLf & vbCrLf
            sReport = sReport & sResult
        End If
        Exit Sub
    
    Error:
        ErrorMsg Err.Number, Err.Description, "CheckAutoStartFolders"
    End Sub
     */
    #endregion

    #region CheckWinNTUserInit

    private void CheckWinNtUserInit(TextWriter writer)
    {
      if (writer == null)
        return;

      var strBuilder = new StringBuilder();

      strBuilder.AppendLine("Checking Windows NT UserInit:");
      strBuilder.AppendLine();

      var registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);

      try
      {
        var is64Bit = Environment.Is64BitOperatingSystem;
        var loopFor64 = false;
        var loopForCurrentUser = false;

      regLoop:

        using (var regKey = registryKey.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Winlogon\"))
        {
          if (regKey != null)
          {
            var userinit = regKey.GetValueNames().FirstOrDefault(f => f.Equals("Userinit", StringComparison.InvariantCultureIgnoreCase));
            if (!string.IsNullOrEmpty(userinit))
            {
              //if (is64Bit && regKey.View != RegistryView.Registry64)
              //  strBuilder.AppendLine("WOW64");
              strBuilder.AppendLine(string.Format("[{0}]", regKey.Name));
              strBuilder.AppendLine(string.Format("UserInit = {0}\r\n", regKey.GetValue(userinit, null)));
            }
          }
        }

        if (is64Bit && !loopFor64)
        {
          registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
          loopFor64 = true;
          goto regLoop;
        }

        if (!loopForCurrentUser)
        {
          registryKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default);
          loopForCurrentUser = true;
          goto regLoop;
        }

      }
      finally
      {
        registryKey.Close();
        registryKey.Dispose();
      }

      strBuilder.AppendLine(new string('-', 50));

      writer.Write(strBuilder.ToString());

    }
    /*
     Private Sub CheckWinNTUserInit()
        'sub applies to NT only
        If Not bIsWinNT Then
            'this is not NT,override?
            If Not (bForceAll Or bForceWinNT) Then Exit Sub
        End If
    
        Dim sDummy$, sResult$, bInteresting As Boolean
        Dim hKey&, sData$, lRet&, i% ', uData() As Byte
        On Error GoTo Error:
    
        'check HKLM\..\Windows NT\CurrenVersion\WinLogon,UserInit
        bInteresting = False
        sDummy = "[HKLM\Software\Microsoft\Windows NT\CurrentVersion\Winlogon]" & vbCrLf
        sData = RegGetString(HKEY_LOCAL_MACHINE, "Software\Microsoft\Windows NT\CurrentVersion\Winlogon", "UserInit")
        If IsRegVal404(sData) Then
            sDummy = sDummy & sData & vbCrLf & vbCrLf
        Else
            bInteresting = True
            sDummy = sDummy & "UserInit = " & sData & vbCrLf & vbCrLf
        End If
        If bInteresting Or bComplete Then sResult = sResult & sDummy
        'check \Windows\CurrentVer as well just in case
        bInteresting = False
        sDummy = "[HKLM\Software\Microsoft\Windows\CurrentVersion\Winlogon]" & vbCrLf
        sData = RegGetString(HKEY_LOCAL_MACHINE, "Software\Microsoft\Windows\CurrentVersion\Winlogon", "UserInit")
        If IsRegVal404(sData) Then
            sDummy = sDummy & sData & vbCrLf & vbCrLf
        Else
            bInteresting = True
            sDummy = sDummy & "UserInit = " & sData & vbCrLf & vbCrLf
        End If
        If bInteresting Or bComplete Then sResult = sResult & sDummy
    
    
    
        'check HKCU\..\Windows NT\CurrentVersion\WinLogon,UserInit
        bInteresting = False
        sDummy = "[HKCU\Software\Microsoft\Windows NT\CurrentVersion\Winlogon]" & vbCrLf
        sData = RegGetString(HKEY_CURRENT_USER, "Software\Microsoft\Windows NT\CurrentVersion\Winlogon", "UserInit")
        If IsRegVal404(sData) Then
            sDummy = sDummy & sData & vbCrLf & vbCrLf
        Else
            bInteresting = True
            sDummy = sDummy & "UserInit = " & sDummy & vbCrLf & vbCrLf
        End If
        If bInteresting Or bComplete Then sResult = sResult & sDummy
        'check \Windows\CurrentVer as well, just in case
        bInteresting = False
        sDummy = "[HKCU\Software\Microsoft\Windows\CurrentVersion\Winlogon]" & vbCrLf
        sData = RegGetString(HKEY_CURRENT_USER, "Software\Microsoft\Windows\CurrentVersion\Winlogon", "UserInit")
        If IsRegVal404(sData) Then
            sDummy = sDummy & sData & vbCrLf & vbCrLf
        Else
            bInteresting = True
            sDummy = sDummy & "UserInit = " & sDummy & vbCrLf & vbCrLf
        End If
        If bInteresting Or bComplete Then sResult = sResult & sDummy
    
    
        If bVerbose Then
            sVerbose = "These are Windows NT/2000/XP specific startup locations. They" & vbCrLf
            sVerbose = sVerbose & "execute when the user logs on to his workstation."
            sResult = sResult & sVerbose & vbCrLf & vbCrLf
        End If
    
    EndOfSub:
        If sResult <> "" And sResult <> sResult & sVerbose & vbCrLf & vbCrLf Then
            sReport = sReport & "[tag3]Checking Windows NT UserInit:[/tag3]" & vbCrLf & vbCrLf
            sReport = sReport & sResult & String(50, "-") & vbCrLf & vbCrLf
        End If
        Exit Sub
    
    Error:
        RegCloseKey hKey
        ErrorMsg Err.Number, Err.Description, "CheckWinNTUserInit"
    End Sub*/
    #endregion

    #region RegistryAutoRun

    private void ParseRegistryKeyValues(RegistryKey registryKey, StringBuilder strBuilder)
    {
      if (registryKey == null || strBuilder == null)
        return;

      var valueNames = registryKey.GetValueNames();
      if (!valueNames.Any()) 
        return;
      
      strBuilder.AppendLine(string.Format("[{0}]", registryKey.Name));
      foreach (var value in valueNames)
      {
        strBuilder.AppendLine(string.Format("{0} = {1}", value, registryKey.GetValue(value, null)));
      }

      strBuilder.AppendLine();
    }

    private
      void ParseRegistryKey(RegistryKey registryKey, StringBuilder strBuilder)
    {
      if (registryKey == null || strBuilder == null)
        return;

      ParseRegistryKeyValues(registryKey, strBuilder);

      var keyNames = registryKey.GetSubKeyNames();
      if (!keyNames.Any()) 
        return;
      
      foreach (var value in keyNames)
      {
        using (var subKey = registryKey.OpenSubKey(value))
        {
          ParseRegistryKey(subKey, strBuilder);
        }
      }
    }

    private void RegistryAutoRun(TextWriter writer)
    {
      if (writer == null)
        return;

      var strBuilder = new StringBuilder();

      strBuilder.AppendLine("Autorun entries from Registry:");
      strBuilder.AppendLine();

      foreach (
        var type in
          new[]
          {
            RegistryRunType.Run, RegistryRunType.RunOnce, RegistryRunType.RunOnceEx, RegistryRunType.RunServices,
            RegistryRunType.RunServicesOnce
          })
      {
        var registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);

        try
        {
          var is64Bit = Environment.Is64BitOperatingSystem;
          var loopFor64 = false;
          var loopForCurrentUser = false;

          regLoop:

          using (
            var regKey = registryKey.OpenSubKey(string.Format(@"Software\Microsoft\Windows\CurrentVersion\{0}", type))
            )
          {
            ParseRegistryKey(regKey, strBuilder);
          }

          if (is64Bit && !loopFor64)
          {
            registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            loopFor64 = true;
            goto regLoop;
          }

          if (!loopForCurrentUser)
          {
            registryKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default);
            loopForCurrentUser = true;
            goto regLoop;
          }

        }
        finally
        {
          registryKey.Close();
          registryKey.Dispose();
        }
      }

      strBuilder.AppendLine(new string('-', 50));

      writer.Write(strBuilder.ToString());

    }

    #endregion

    public void Dispose()
    {
      //
    }
  }
}
