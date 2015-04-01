

//Public Sub SetListBoxColumns(objListBox As ListBox)
//    Dim lTabStop&(1)
//    On Error GoTo 0:
//    lTabStop(0) = 70
//    lTabStop(1) = 0
//    SendMessage objListBox.hwnd, LB_SETTABSTOPS, UBound(lTabStop), lTabStop(0)
//End Sub


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using ReviveThis.Entities.ExtensionMethods;
using ReviveThis.Entities.PInvoke;

namespace ReviveThis.Entities.Modules
{
  public static class ProcessManager
  {

    #region RefreshProcessList

    //[PrincipalPermission(SecurityAction.Demand, Role = @"BUILTIN\Administrators")]
    public static IEnumerable<Process> RefreshProcessList( /*ListBox objList*/)
    {

      //var sessionId = Process.GetCurrentProcess().SessionId;
      var result = Process.GetProcesses();//.Where(w => w.SessionId == sessionId);
      return result;
      //return System.Diagnostics.Process.GetProcesses();
    }

    /*
    Public Sub RefreshProcessList(objList As ListBox)
        Dim hSnap&, uPE32 As PROCESSENTRY32, i&
        Dim sExeFile$, hProcess&

        hSnap = CreateToolhelpSnapshot(TH32CS_SNAPPROCESS, 0)

        uPE32.dwSize = Len(uPE32)
        If ProcessFirst(hSnap, uPE32) = 0 Then
            CloseHandle hSnap
            Exit Sub
        End If

        objList.Clear
        Do
            sExeFile = TrimNull(uPE32.szExeFile)
            objList.AddItem uPE32.th32ProcessID & vbTab & sExeFile
        Loop Until ProcessNext(hSnap, uPE32) = 0
        CloseHandle hSnap
    End Sub
    */

    #endregion

    #region KillProcess

    public static void KillProcess(Process process)
    {
      if (process == null)
        return;

      process.Kill();
    }

    public static void KillProcess(int processId)
    {
      KillProcess(Process.GetProcessById(processId));
    }

    /*
    Public Sub KillProcess(lPID&)
        Dim hProcess&
        If lPID = 0 Then Exit Sub
        hProcess = OpenProcess(PROCESS_TERMINATE, 0, lPID)
        If hProcess = 0 Then
            MsgBox "The selected process could not be killed." & _
                   " It may have already closed, or it may be protected by Windows.", vbCritical
        Else
            If TerminateProcess(hProcess, 0) = 0 Then
                MsgBox "The selected process could not be killed." & _
                       " It may be protected by Windows.", vbCritical
            Else
                CloseHandle hProcess
                DoEvents
            End If
        End If
    End Sub
    */

    #endregion

    #region KillProcessByFile

    public static void KillProcessByFile(string sPath)
    {
      //Could make this faster with LINQ but I suspect ther would be an exception when reading unsupported .MainModule.
      foreach (var process in RefreshProcessList())
      {
        try
        {
          if (process.MainModule.FileName.Equals(sPath, StringComparison.InvariantCultureIgnoreCase))
          {
            process.Kill();
          }
        }
        catch (Exception)
        {
          //throw;
        }
      }
    }

    /*
    Public Sub KillProcessByFile(sPath$)
        Dim hSnap&, uPE32 As PROCESSENTRY32, i&
        Dim sExeFile$, hProcess&
        'Note: this sub is silent - it displays no errors !
        If sPath = vbNullString Then Exit Sub
        If bIsWinNT Then
            KillProcessNTByFile sPath
            Exit Sub
        End If
        hSnap = CreateToolhelpSnapshot(TH32CS_SNAPPROCESS, 0)

        uPE32.dwSize = Len(uPE32)
        If ProcessFirst(hSnap, uPE32) = 0 Then
            CloseHandle hSnap
            Exit Sub
        End If

        Do
            sExeFile = TrimNull(uPE32.szExeFile)
            If InStr(1, sExeFile, sPath, vbTextCompare) > 0 Then
                CloseHandle hSnap

                'found the process!
                PauseProcess uPE32.th32ProcessID
                hProcess = OpenProcess(PROCESS_TERMINATE, 0, uPE32.th32ProcessID)
                If hProcess <> 0 Then
                    If TerminateProcess(hProcess, 0) <> 0 Then
                        CloseHandle hProcess
                        DoEvents
                    End If
                End If
                Exit Do
            End If
        Loop Until ProcessNext(hSnap, uPE32) = 0
        CloseHandle hSnap
    End Sub
    */

    #endregion

    #region RefreshDllList

    public static ProcessModuleCollection RefreshDllList(Process process)
    {
      try
      {
        return process.Modules;
      }
      catch (Exception)
      {
        //Suppress exception and return null.
        return null;
        //throw;
      }
    }

    public static ProcessModuleCollection RefreshDllList(int processId)
    {
      return RefreshDllList(Process.GetProcessById(processId));
    }

    /*
    Public Sub RefreshDLLList(lPID&, objList As ListBox)
        Dim hSnap&, uME32 As MODULEENTRY32
        Dim sDllFile$
        objList.Clear
        If lPID = 0 Then Exit Sub

        hSnap = CreateToolhelpSnapshot(TH32CS_SNAPMODULE, lPID)
        uME32.dwSize = Len(uME32)
        If Module32First(hSnap, uME32) = 0 Then
            CloseHandle hSnap
            Exit Sub
        End If

        Do
            sDllFile = TrimNull(uME32.szExePath)
            objList.AddItem sDllFile
        Loop Until Module32Next(hSnap, uME32) = 0
        CloseHandle hSnap
    End Sub
    */

    #endregion

    #region PauseProcess

    public static void PauseProcess(Process process, bool pauseOrResume = true)
    {
      throw new NotImplementedException("PauseProcess()");
    }

    /*
    Public Sub PauseProcess(lPID&, Optional bPauseOrResume As Boolean = True)
        Dim hSnap&, uTE32 As THREADENTRY32, hThread&
        If Not bIsWinNT And Not bIsWinME Then Exit Sub
        If lPID = GetCurrentProcessId Then Exit Sub

        hSnap = CreateToolhelpSnapshot(TH32CS_SNAPTHREAD, lPID)
        If hSnap = -1 Then Exit Sub

        uTE32.dwSize = Len(uTE32)
        If Thread32First(hSnap, uTE32) = 0 Then
            CloseHandle hSnap
            Exit Sub
        End If

        Do
            If uTE32.th32ProcessID = lPID Then
                hThread = OpenThread(THREAD_SUSPEND_RESUME, False, uTE32.th32ThreadID)
                If bPauseOrResume Then
                    SuspendThread hThread
                Else
                    ResumeThread hThread
                End If
                CloseHandle hThread
            End If
        Loop Until Thread32Next(hSnap, uTE32) = 0
        CloseHandle hSnap
    End Sub
    */

    #endregion

    #region ProcessListFormatter

    private static StringBuilder ProcessListFormatter(IEnumerable<Process> processes, Process selectedProcess = null,
      bool includeDlls = false)
    {
      var stringBuilder = new StringBuilder();

      stringBuilder.AppendLine(string.Format("Process list saved on {0:T}, on {0:d}", DateTime.Now));
      stringBuilder.AppendLine(string.Format("Platform: {0}", Environment.OSVersion.FormatToString()));
      stringBuilder.AppendLine();
      stringBuilder.AppendLine("[pid]\t[full path to filename]\t\t[file version]\t[company name]");

      if (processes == null)
        return stringBuilder;

      foreach (var process in processes)
      {
        try
        {
          stringBuilder.AppendLine(string.Format("{0}\t{1}\t\t{2}\t{3}", process.Id, process.MainModule.FileName,
            process.MainModule.FileVersionInfo.FormatToString(), process.MainModule.FileVersionInfo.CompanyName));
        }
        catch (Exception)
        {
          //throw;
        }
      }

      if (selectedProcess == null || !includeDlls)
        return stringBuilder;

      stringBuilder.AppendLine();
      stringBuilder.AppendLine();
      stringBuilder.AppendLine(string.Format("DLLs loaded by process {0}:", selectedProcess.MainModule.FileName));
      stringBuilder.AppendLine();
      stringBuilder.AppendLine("[full path to filename]\t\t[file version]\t[company name]");
      foreach (var module in selectedProcess.Modules.Cast<ProcessModule>())
      {
        try
        {
          stringBuilder.AppendLine(string.Format("{0}\t\t{1}\t{2}", module.FileName,
            module.FileVersionInfo.FormatToString(), module.FileVersionInfo.CompanyName));
        }
        catch (Exception)
        {
          //throw;
        }
      }

      return stringBuilder;
    }

    #endregion

    #region SaveProcessList

    public static void SaveProcessList(IEnumerable<Process> processes, Process selectedProcess,
      bool includeDlls = false)
    {
      using (var dialog = new SaveFileDialog())
      {
        dialog.Title = @"Save process list to file..";
        dialog.Filter = @"Text files (*.txt)|*.txt|All files (*.*)|*.*";
        dialog.FileName = "processlist.txt";

        if (dialog.ShowDialog() != DialogResult.OK)
          return;

        var newFile = !File.Exists(dialog.FileName);

        using (
          var fileStream = new FileStream(dialog.FileName, newFile ? FileMode.CreateNew : FileMode.Truncate,
            FileAccess.Write))
        using (var writer = new StreamWriter(fileStream))
        {
          writer.Write(ProcessListFormatter(processes, selectedProcess, includeDlls));
        }

        if (File.Exists(dialog.FileName))
        {
          Process.Start(new ProcessStartInfo(dialog.FileName));
        }

      }
    }

    public static void SaveProcessList(IEnumerable<Process> processes, int? processId,
      bool includeDlls = false)
    {
      SaveProcessList(processes, processId.HasValue ? Process.GetProcessById(processId.Value) : (Process) null,
        includeDlls);
    }

    public static void SaveProcessList(IEnumerable<Process> processes)
    {
      SaveProcessList(processes, (Process) null);
    }

    /*
    Public Sub SaveProcessList(objProcess As ListBox, objDLL As ListBox, Optional bDoDLLs As Boolean = False)
        Dim sFilename$, i%, sProcess$, sModule$
        sFilename = CmnDlgSaveFile("Save process list to file..", "Text files (*.txt)|*.txt|All files (*.*)|*.*", "processlist.txt")
        If sFilename = vbNullString Then Exit Sub

        On Error Resume Next
        Open sFilename For Output As #1
            Print #1, "Process list saved on " & Format(Time, "Long Time") & ", on " & Format(Date, "Short Date")
            Print #1, "Platform: " & GetWindowsVersion & vbCrLf
            Print #1, "[pid]" & vbTab & "[full path to filename]" & vbTab & vbTab & "[file version]" & vbTab & "[company name]"
            For i = 0 To objProcess.ListCount - 1
                sProcess = objProcess.List(i)
                Print #1, sProcess & vbTab & vbTab & _
                          GetFilePropVersion(Mid(sProcess, InStr(sProcess, vbTab) + 1)) & vbTab & _
                          GetFilePropCompany(Mid(sProcess, InStr(sProcess, vbTab) + 1))
            Next i

            If bDoDLLs Then
                sProcess = objProcess.List(objProcess.ListIndex)
                sProcess = Mid(sProcess, InStr(sProcess, vbTab) + 1)
                Print #1, vbCrLf & vbCrLf & "DLLs loaded by process " & sProcess & ":" & vbCrLf
                Print #1, "[full path to filename]" & vbTab & vbTab & "[file version]" & vbTab & "[company name]"
                For i = 0 To objDLL.ListCount - 1
                    sModule = objDLL.List(i)
                    Print #1, sModule & vbTab & vbTab & GetFilePropVersion(sModule) & vbTab & GetFilePropCompany(sModule)
                Next i
            End If

        Close #1

        ShellExecute 0, "open", sFilename, vbNullString, vbNullString, 1
    End Sub
    */

    #endregion

    #region CopyProcessList

    public static void CopyProcessList(IEnumerable<Process> processes, Process selectedProcess,
      bool includeDlls = false)
    {
      Clipboard.SetText(ProcessListFormatter(processes, selectedProcess, includeDlls).ToString());

      MessageBox.Show(
        includeDlls
          ? "The process list and dll list have been copied to your clipboard."
          : "The process list has been copied to your clipboard.", null,
        MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    public static void CopyProcessList(IEnumerable<Process> processes, int? processId,
      bool includeDlls = false)
    {
      CopyProcessList(processes, processId.HasValue ? Process.GetProcessById(processId.Value) : (Process) null,
        includeDlls);
    }

    public static void CopyProcessList(IEnumerable<Process> processes)
    {
      CopyProcessList(processes, (Process) null);
    }

    /*
    Public Sub CopyProcessList(objProcess As ListBox, objDLL As ListBox, Optional bDoDLLs As Boolean = False)
        Dim i%, sList$, sProcess$, sModule$

        On Error Resume Next
        sList = "Process list saved on " & Format(Time, "Long Time") & ", on " & Format(Date, "Short Date") & vbCrLf & _
                "Platform: " & GetWindowsVersion & vbCrLf & vbCrLf & _
                "[pid]" & vbTab & "[full path to filename]" & vbTab & vbTab & "[file version]" & vbTab & "[company name]" & vbCrLf
        For i = 0 To objProcess.ListCount - 1
            sProcess = objProcess.List(i)
            sList = sList & sProcess & vbTab & vbTab & _
                    GetFilePropVersion(Mid(sProcess, InStr(sProcess, vbTab) + 1)) & vbTab & _
                    GetFilePropCompany(Mid(sProcess, InStr(sProcess, vbTab) + 1)) & vbCrLf
        Next i

        If bDoDLLs Then
            sProcess = objProcess.List(objProcess.ListIndex)
            sProcess = Mid(sProcess, InStr(sProcess, vbTab) + 1)
            sList = sList & vbCrLf & vbCrLf & "DLLs loaded by process " & sProcess & ":" & vbCrLf & vbCrLf & _
                    "[full path to filename]" & vbTab & vbTab & "[file version]" & vbTab & "[company name]" & vbCrLf
            For i = 0 To objDLL.ListCount - 1
                sModule = objDLL.List(i)
                sList = sList & sModule & vbTab & vbTab & GetFilePropVersion(sModule) & vbTab & GetFilePropCompany(sModule) & vbCrLf
            Next i
        End If

        Clipboard.Clear
        Clipboard.SetText sList
        If bDoDLLs Then
            MsgBox "The process list and dll list have been copied to your clipboard.", vbInformation
        Else
            MsgBox "The process list has been copied to your clipboard.", vbInformation
        End If
    End Sub
    */

    #endregion

    #region GetFilePropVersion

    public static string GetFilePropVersion(string sFilename)
    {
      if (!File.Exists(sFilename))
        return null;

      var versionInfo = FileVersionInfo.GetVersionInfo(sFilename);
      return versionInfo.FileVersion;
    }

    /*
    Public Function GetFilePropVersion$(sFilename$)
        Dim hData&, lDataLen&, uBuf() As Byte, uCodePage(0 To 3) As Byte
        Dim sCodePage$, sCompanyName$, uVFFI As VS_FIXEDFILEINFO, sVersion$
        If Not FileExists(sFilename) Then Exit Function

        lDataLen = GetFileVersionInfoSize(sFilename, ByVal 0)
        If lDataLen = 0 Then Exit Function

        ReDim uBuf(0 To lDataLen - 1)
        GetFileVersionInfo sFilename, 0, lDataLen, uBuf(0)
        VerQueryValue uBuf(0), "\", hData, lDataLen
        CopyMemory uVFFI, ByVal hData, Len(uVFFI)

        With uVFFI
            sVersion = .dwFileVersionMSh & "." & _
                       .dwFileVersionMSl & "." & _
                       .dwFileVersionLSh & "." & _
                       .dwFileVersionLSl
        End With
        GetFilePropVersion = sVersion
        DoEvents
    End Function
    */

    #endregion

    #region GetFilePropCompany

    public static string GetFilePropCompany(string sFilename)
    {
      if (!File.Exists(sFilename))
        return null;

      var versionInfo = FileVersionInfo.GetVersionInfo(sFilename);
      return versionInfo.CompanyName;
    }

    /*
    Public Function GetFilePropCompany$(sFilename$)
        Dim hData&, lDataLen&, uBuf() As Byte, uCodePage(0 To 3) As Byte
        Dim sCodePage$, sCompanyName$
        If Not FileExists(sFilename) Then Exit Function

        lDataLen = GetFileVersionInfoSize(sFilename, ByVal 0)
        If lDataLen = 0 Then Exit Function

        ReDim uBuf(0 To lDataLen - 1)
        GetFileVersionInfo sFilename, 0, lDataLen, uBuf(0)
        VerQueryValue uBuf(0), "\VarFileInfo\Translation", hData, lDataLen
        If lDataLen = 0 Then Exit Function

        CopyMemory uCodePage(0), ByVal hData, 4
        sCodePage = Format(Hex(uCodePage(1)), "00") & _
                    Format(Hex(uCodePage(0)), "00") & _
                    Format(Hex(uCodePage(3)), "00") & _
                    Format(Hex(uCodePage(2)), "00")

        'get CompanyName string
        If VerQueryValue(uBuf(0), "\StringFileInfo\" & sCodePage & "\CompanyName", hData, lDataLen) = 0 Then Exit Function
        sCompanyName = String(lDataLen, 0)
        lstrcpy sCompanyName, hData
        GetFilePropCompany = TrimNull(sCompanyName)
        DoEvents
    End Function
    */

    #endregion

    #region ShowFileProperties

    public static void ShowFileProperties(string fileName)
    {
      Shell32.ShowFileProperties(fileName);
    }
    /*
    Public Sub ShowFileProperties(sFile$)
        Dim uSEI As SHELLEXECUTEINFO
        With uSEI
            .cbSize = Len(uSEI)
            .fMask = SEE_MASK_INVOKEIDLIST Or SEE_MASK_NOCLOSEPROCESS
            .hwnd = frmMain.hwnd
            .lpFile = sFile
            .lpVerb = "properties"
            .nShow = 1
        End With
        ShellExecuteEx uSEI
    End Sub
    */

    #endregion
  }
}