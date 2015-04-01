using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using ReviveThis.Enums;

namespace ReviveThis.Entities.Modules
{
  public static class Hosts
  {
    #region GetHostsFile
    private static string _getHostsFile = string.Empty;
    private static string _getHostsFileDefault = string.Empty;

    public static string GetHostsFileDefault
    {
      get
      {
        if (!string.IsNullOrEmpty(_getHostsFileDefault))
          return _getHostsFileDefault;


        return _getHostsFileDefault = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), @"drivers\etc\hosts");
      }
    }

    public static string GetHostsFile 
    {
      get
      {
        if (!string.IsNullOrEmpty(_getHostsFile))
          return _getHostsFile;

        _getHostsFile = null;

        using (var tcpipKey = Registry.LocalMachine.OpenSubKey(@"System\CurrentControlSet\Services\Tcpip\Parameters", false))
        {
          if (tcpipKey != null)
          {
            var databasePath = tcpipKey.GetValue("DataBasePath", null) as string;
            if (!string.IsNullOrEmpty(databasePath))
            {
              _getHostsFile = Path.Combine(Environment.ExpandEnvironmentVariables(databasePath), "hosts");
            }
          }
        }

        if (string.IsNullOrEmpty(_getHostsFile))
          _getHostsFile = GetHostsFileDefault;

        return _getHostsFile;

        //sDatabasePath = RegGetString(HKEY_LOCAL_MACHINE, "System\CurrentControlSet\Services\Tcpip\Parameters", "DataBasePath")
        //'%systemroot% may be in path - replace it
        //sDatabasePath = Replace(sDatabasePath, "%SystemRoot%", sWinDir, , , vbTextCompare)
        //sHostsFile = sDatabasePath & "\hosts"

      }
    }
    #endregion

    public async static Task<HostsFileReader> ReadHostsFile(string fileName = null)
    {
      if (string.IsNullOrEmpty(fileName))
        fileName = GetHostsFile;

      try
      {
        if (!File.Exists(fileName))
        {
          throw new FileNotFoundException(string.Format("Could not locate Hosts sile (\"{0}\").", fileName));
        }

        var fileAttrib = File.GetAttributes(fileName);

        using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
        using (var reader = new StreamReader(fileStream))
        {
          var sDummy = await reader.ReadToEndAsync();

          return new HostsFileReader(
            sDummy.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None),
            sDummy.IndexOf("\r\n", StringComparison.InvariantCultureIgnoreCase) > -1 ? HostsFileType.Default : HostsFileType.Unix,
            GetHostsFile,
            fileAttrib);
        }
      }
      catch (Exception)
      {
        //Re-throws the exception 
        //(this is not required, perhaps we'll handle exceptions differently in future).
        throw;
      }
    }

    #region ListHostsFile
    public static async Task<HostsFileReader> ListHostsFile(string fileName = null)
    {
      if (string.IsNullOrEmpty(fileName))
        fileName = GetHostsFile;

      try
      {
        try
        {
          return await ReadHostsFile(fileName);
        }
        catch (FileNotFoundException)
        {
          if (
            MessageBox.Show("Cannot find the hosts file.\nDo you want to create a new, default hosts file?",
              null,
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Exclamation,
              MessageBoxDefaultButton.Button2) ==
            DialogResult.No)
          {
            return null;
          }
        }

        return await ReadHostsFile(CreateDefaultHostsFile(fileName));
      }
      catch (Exception)
      {

        throw;
      }
    }

    /*
     * Original Visual Basic (6.0) Code Block
     * 
    Public Sub ListHostsFile(objList As ListBox, objInfo As Label)
        'custom hosts file handling?
        Dim sAttr$, iAttr%, sDummy$, vContent As Variant, i&
        On Error Resume Next
        objInfo.Caption = "Loading hosts file, please wait..."
        frmMain.cmdHostsManDel.Enabled = False
        frmMain.cmdHostsManToggle.Enabled = False
        DoEvents
        If Not FileExists(sHostsFile) Then
            If MsgBox("Cannot find the hosts file." & vbCrLf & _
                      "Do you want to create a new, default " & _
                      "hosts file?", vbExclamation + vbYesNo) = vbNo Then
                objInfo.Caption = "No hosts file found."
                Exit Sub
            Else
                CreateDefaultHostsFile
            End If
        End If

        objInfo.Caption = "Loading hosts file, please wait..."
        frmMain.cmdHostsManDel.Enabled = False
        frmMain.cmdHostsManToggle.Enabled = False
        DoEvents
        iAttr = GetAttr(sHostsFile)
        If (iAttr And 1) Then sAttr = sAttr & "R"
        If (iAttr And 32) Then sAttr = sAttr & "A"
        If (iAttr And 2) Then sAttr = sAttr & "H"
        If (iAttr And 4) Then sAttr = sAttr & "S"
        If (iAttr And 2048) Then sAttr = sAttr & "C"

        Open sHostsFile For Binary As #1
            sDummy = Input(FileLen(sHostsFile), #1)
        Close #1
        vContent = Split(sDummy, vbCrLf)
        If UBound(vContent) = 0 And InStr(vContent(0), Chr(10)) > 0 Then
            'unix style hosts file
            vContent = Split(sDummy, Chr(10))
        End If

        objList.Clear
        For i = 0 To UBound(vContent)
            objList.AddItem CStr(vContent(i))
        Next i

        'objInfo.Caption = "Hosts file is located at: " & sHostsFile &
        objInfo.Caption = Translate(271) & " " & sHostsFile & _
                          " (" & objList.ListCount & " lines, " & _
                          sAttr & ")"
        frmMain.cmdHostsManDel.Enabled = True
        frmMain.cmdHostsManToggle.Enabled = True
    End Sub
    */
    #endregion

    #region CreateDefaultHostsFile
    /// <summary>
    /// <para>Creates a default Hosts file.</para>
    /// </summary>
    /// <param name="fileName">Optional filename.</param>
    /// <returns>Location of the Hosts file.</returns>
    public static string CreateDefaultHostsFile(string fileName = null)
    {
      if (string.IsNullOrEmpty(fileName))
        fileName = GetHostsFile;

      try
      {
        using (var fileStream = new FileStream(fileName, File.Exists(fileName) ? FileMode.Truncate : FileMode.CreateNew, FileAccess.Write))
        using (var writer = new StreamWriter(fileStream))
        {
          writer.WriteLine("# Copyright (c) 1993-2009 Microsoft Corp.");
          writer.WriteLine("#");
          writer.WriteLine("# This is a sample HOSTS file used by Microsoft TCP/IP for Windows.");
          writer.WriteLine("#");
          writer.WriteLine("# This file contains the mappings of IP addresses to host names. Each");
          writer.WriteLine("# entry should be kept on an individual line. The IP address should");
          writer.WriteLine("# be placed in the first column followed by the corresponding host name.");
          writer.WriteLine("# The IP address and the host name should be separated by at least one");
          writer.WriteLine("# space.");
          writer.WriteLine("#");
          writer.WriteLine("# Additionally, comments (such as these) may be inserted on individual");
          writer.WriteLine("# lines or following the machine name denoted by a '#' symbol.");
          writer.WriteLine("#");
          writer.WriteLine("# For example:");
          writer.WriteLine("#");
          writer.WriteLine("#      102.54.94.97     rhino.acme.com          # source server");
          writer.WriteLine("#       38.25.63.10     x.acme.com              # x client host");
          writer.WriteLine("");
          writer.WriteLine("127.0.0.1       localhost");
          writer.WriteLine("::1             localhost");
        }

        return fileName;
      }
      catch (Exception)
      {
        //
        throw;
      }
    }

    /*
     * Original Visual Basic (6.0) Code Block
     *    
    Public Sub CreateDefaultHostsFile()
    On Error Resume Next
    Open sHostsFile For Output As #1
        Print #1, "# Copyright (c) 1993-2009 Microsoft Corp."
        Print #1, "#"
        Print #1, "# This is a sample HOSTS file used by Microsoft TCP/IP for Windows."
        Print #1, "#"
        Print #1, "# This file contains the mappings of IP addresses to host names. Each"
        Print #1, "# entry should be kept on an individual line. The IP address should"
        Print #1, "# be placed in the first column followed by the corresponding host name."
        Print #1, "# The IP address and the host name should be separated by at least one"
        Print #1, "# space."
        Print #1, "#"
        Print #1, "# Additionally, comments (such as these) may be inserted on individual"
        Print #1, "# lines or following the machine name denoted by a '#' symbol."
        Print #1, "#"
        Print #1, "# For example:"
        Print #1, "#"
        Print #1, "#      102.54.94.97     rhino.acme.com          # source server"
        Print #1, "#       38.25.63.10     x.acme.com              # x client host"
        Print #1,
        Print #1, "127.0.0.1       localhost"
        Print #1, "::1             localhost"
    Close #1
    End Sub
    */
    #endregion

    #region HostsDeleteLine

    /// <summary>
    /// Deletes the requesed line in hosts file.
    /// </summary>
    /// <param name="hostsFile"></param>
    /// <param name="indices"></param>
    /// <param name="fileName"></param>
    public static void HostsDeleteLine(ref HostsFileReader hostsFile, int[] indices, string fileName = null)
    {
      if (indices.Count() <= 0)
        return;

      if (string.IsNullOrEmpty(fileName))
        fileName = hostsFile.Location;

      var writeLines = new List<string>();

      var i = 0;

      foreach (var line in hostsFile.Lines)
      {
        if (!indices.Contains(i))
        {
          writeLines.Add(line);
        }
        i++;
      }

      try
      {
        using (var fileStream = new FileStream(fileName, File.Exists(fileName) ? FileMode.Truncate : FileMode.CreateNew, FileAccess.ReadWrite))
        using (var writer = new StreamWriter(fileStream))
        {
          writer.Write(string.Join(hostsFile.FileType == HostsFileType.Unix ? "\n" : "\r\n", writeLines.ToArray()));
        }

        hostsFile.Lines = writeLines.ToArray();
      }
      catch (Exception)
      {
        //
        throw;
      }
    }

    /*
     * Original Visual Basic (6.0) Code Block
     * 
    Public Sub HostsDeleteLine(objList As ListBox)
        'delete ith line in hosts file (zero-based)
        Dim iAttr%, sDummy$, vContent As Variant, i&
        On Error Resume Next
        iAttr = GetAttr(sHostsFile)
        If (iAttr And 2048) Then iAttr = iAttr - 2048
        SetAttr sHostsFile, vbArchive
        If Err Then
            MsgBox "The hosts file is locked for reading and cannot be edited. " & vbCrLf & _
                   "Make sure you have privileges to modify the hosts file and " & _
                   "no program is protecting it against changes.", vbCritical
            Exit Sub
        End If

        Open sHostsFile For Binary As #1
            sDummy = Input(FileLen(sHostsFile), #1)
        Close #1
        vContent = Split(sDummy, vbCrLf)
        If UBound(vContent) = 0 And InStr(vContent(0), Chr(10)) > 0 Then
            'unix style hosts file
            vContent = Split(sDummy, Chr(10))
        End If

        Open sHostsFile For Output As #1
            With objList
                For i = 0 To UBound(vContent) - 1
                    If Not .Selected(i) Then Print #1, vContent(i)
                Next i
            End With
        Close #1
    End Sub
    */
    #endregion

    #region HostsToggleLine
    public static void HostsToggleLine(ref HostsFileReader hostsFile, int[] indices, string fileName = null)
    {
      if (indices.Count() <= 0)
        return;

      if (string.IsNullOrEmpty(fileName))
        fileName = GetHostsFile;

      var writeLines = new List<string>();

      var i = 0;
      foreach (var line in hostsFile.Lines)
      {
        if (indices.Contains(i))
        {
          writeLines.Add(line.StartsWith("#") ? line.Substring(1) : line.Insert(0, "#"));
        }
        else
        {
          writeLines.Add(line);
        }
        i++;
      }

      try
      {
        using (var fileStream = new FileStream(fileName, File.Exists(fileName) ? FileMode.Truncate : FileMode.CreateNew, FileAccess.ReadWrite))
        using (var writer = new StreamWriter(fileStream))
        {
          writer.Write(string.Join(hostsFile.FileType == HostsFileType.Unix ? "\n" : "\r\n", writeLines.ToArray()));
        }

        hostsFile.Lines = writeLines.ToArray();
      }
      catch (Exception)
      {
        //
        throw;
      }
    }

    /*
     * Original Visual Basic (6.0) Code Block
     * 
    Public Sub HostsToggleLine(objList As ListBox)
        'enable/disable ith line in hosts file (zero-based)
        Dim iAttr%, sDummy$, vContent As Variant, i&
        On Error Resume Next
        iAttr = GetAttr(sHostsFile)
        If (iAttr And 2048) Then iAttr = iAttr - 2048
        SetAttr sHostsFile, vbArchive
        If Err Then
            MsgBox "The hosts file is locked for reading and cannot be edited. " & vbCrLf & _
                   "Make sure you have privileges to modify the hosts file and " & _
                   "no program is protecting it against changes.", vbCritical
            Exit Sub
        End If

        Open sHostsFile For Binary As #1
            sDummy = Input(FileLen(sHostsFile), #1)
        Close #1
        vContent = Split(sDummy, vbCrLf)
        If UBound(vContent) = 0 And InStr(vContent(0), Chr(10)) > 0 Then
            'unix style hosts file
            vContent = Split(sDummy, Chr(10))
        End If

        With objList
            For i = 0 To UBound(vContent)
                If .Selected(i) Then
                    If InStr(vContent(i), "#") = 1 Then
                        vContent(i) = Mid(vContent(i), 2)
                    Else
                        vContent(i) = "#" & vContent(i)
                    End If
                End If
            Next i
        End With

        Open sHostsFile For Output As #1
            Print #1, Join(vContent, vbCrLf)
        Close #1
        SetAttr sHostsFile, iAttr
    End Sub
    */
    #endregion

    #region OpenInNotepad

    const string NOTEPAD_EXE = "notepad.exe";

    public static void OpenInNotepad(bool runElevated = false)
    {
      try
      {
        var pathToFile = Path.Combine(Environment.SystemDirectory, NOTEPAD_EXE);
        pathToFile = File.Exists(pathToFile) ? pathToFile : NOTEPAD_EXE;

        Process.Start(new ProcessStartInfo(pathToFile)
        {
          Arguments = GetHostsFile,
          Verb = runElevated ? "runas" : string.Empty,
          UseShellExecute = true,
          WindowStyle = ProcessWindowStyle.Normal,
        });
      }
      catch (Win32Exception)
      {
        //Exception throm when aborting the UAC prompt.
      }
    }

    #endregion

    #region OpenDefault

    public static void OpenDefault()
    {
      try
      {
        Process.Start(new ProcessStartInfo(GetHostsFile)
        {
          Verb = string.Empty,
          UseShellExecute = true,
          WindowStyle = ProcessWindowStyle.Normal,
        });
      }
      catch (Win32Exception)
      {
        //Exception throm when aborting the UAC prompt.
      }
    }

    #endregion
  }
}