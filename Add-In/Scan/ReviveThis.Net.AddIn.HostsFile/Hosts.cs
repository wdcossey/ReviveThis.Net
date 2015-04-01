using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ReviveThis.AddIn.HostsFile.Entities;
using ReviveThis.AddIn.HostsFile.Enums;
using ReviveThis.Entities;
using ReviveThis.Enums;
using ReviveThis.Interfaces;

namespace ReviveThis.AddIn.HostsFile
{
  #region [O1] CheckHostsFile / CheckOther1Item
  [Export(typeof(IDetectionAddIn))]
  public class Hosts : IDetectionAddIn
  {
    public string Author
    {
      get { return @"William David Cossey"; }
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
      get { return @"HostsFile"; }
    }

    private string[] _description = new string[0];

    public string[] Description
    {
      get
      {
        if (_description.Any())
          return _description;

        return
          _description =
            new[] {string.Format("Scans \"{0}\" for hijacked domains.", ReviveThis.Entities.Modules.Hosts.GetHostsFile)};
      }
    }

    public async Task<ICollection<IDetectionResultItem>> Scan()
    {
      var result = new Collection<IDetectionResultItem>();

      var sHostsFile = ReviveThis.Entities.Modules.Hosts.GetHostsFile;

      if (string.IsNullOrEmpty(sHostsFile) || !File.Exists(sHostsFile))
        return null;

      HostsFileReader readResult = null;
      try
      {
        readResult = await ReviveThis.Entities.Modules.Hosts.ReadHostsFile(sHostsFile);
      }
      catch (FileNotFoundException)
      {
        result.Add(new HostsResult(HostsFileResultType.NotFound));
      }

      if (readResult != null)
      {
        if (
          !readResult.Location.Equals(ReviveThis.Entities.Modules.Hosts.GetHostsFileDefault,
            StringComparison.InvariantCultureIgnoreCase))
        {
          //TODO: Implement IgnoreList
          result.Add(new HostsResult(HostsFileResultType.InvalidLocation, readResult.Location));

          //sHit = "O1 - Hosts file is located at: " & sHostsFile
          //If Not IsOnIgnoreList(sHit) Then frmMain.lstResults.AddItem sHit
        }

        //Legacy file format?
        if (readResult.FileType != HostsFileType.Default)
          result.Add(new HostsResult(HostsFileResultType.InvalidFormat));

        //Remove all commented/empty lines (they aren't needed).
        var lines =
          readResult.Lines.Where(
            w =>
              !string.IsNullOrEmpty(w.Trim()) && !w.Trim().StartsWith("#", StringComparison.InvariantCultureIgnoreCase))
            .Select(s => s.Trim())
            .ToList();
        if (lines.Any())
        {
          //TODO: Future use, get all local IP addresses for white-listing
          //foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork || ip.AddressFamily == AddressFamily.InterNetworkV6))
          //{
          //}

          //Remove anything for loopback addresses (IPv4 = "127.0.0.1" & IPv6 = "::1")
          //TODO: Implement IgnoreList
          var hiJackList =
            lines.Where(
#if DEBUG
              w => true
#else
              w => !w.StartsWith(IPAddress.Loopback.ToString()) && !w.StartsWith(IPAddress.IPv6Loopback.ToString())
#endif
              ).ToList();

          foreach (var line in hiJackList)
          {
            result.Add(new HostsResult(HostsFileResultType.LineHiJack, line));
          }
        }
      }

      return result;
    }

    public ScanResultType ResultType
    {
      get { return ScanResultType.HostsFileHiJack; }
    }

    public void Dispose()
    {
      //Nothing to dispose?
    }
  }
  #endregion

  #region Original Visual Basic (6.0) Code Block
  /*
   Private Sub CheckOther1Item()
       Dim sLine$, sFile$, sHit$, sDomains$(), i%
       Dim iAttr%
       On Error GoTo Error:
   
       If Not FileExists(sHostsFile) Then Exit Sub
       If FileLen(sHostsFile) = 0 Then Exit Sub
   
       On Error Resume Next
       iAttr = GetAttr(sHostsFile)
       If (iAttr And 2048) Then iAttr = iAttr - 2048
       SetAttr sHostsFile, vbNormal
       SetAttr sHostsFile, vbArchive
       If Err Then
           MsgBox Replace(Translate(300), "[]", sHostsFile), vbExclamation
   '        MsgBox "For some reason your system denied write " & _
   '        "access to the Hosts file." & vbCrLf & "If any hijacked domains " & _
   '        "are in this file, HijackThis may NOT be able to " & _
   '        "fix this." & vbCrLf & vbCrLf & "If that happens, you need " & _
   '        "to edit the file yourself. To do this, click " & _
   '        "Start, Run and type:" & vbCrLf & vbCrLf & _
   '        "   notepad """ & sHostsFile & """" & vbCrLf & vbCrLf & _
   '        "and press Enter. Find the line(s) HijackThis " & _
   '        "reports and delete them. Save the file as " & _
   '        """hosts."" (with quotes), and reboot.", vbExclamation
       End If
       SetAttr sHostsFile, iAttr
       On Error GoTo Error:
   
       If LCase(sHostsFile) <> LCase(sWinDir & "\hosts") And _
          LCase(sHostsFile) <> LCase(sWinSysDir & "\drivers\etc\hosts") Then
           sHit = "O1 - Hosts file is located at: " & sHostsFile
           If Not IsOnIgnoreList(sHit) Then frmMain.lstResults.AddItem sHit
       End If
   
       Open sHostsFile For Input As #1
           Do
               Line Input #1, sLine
               If InStr(sLine, Chr(10)) > 0 Then
                   'hosts file has line delimiters
                   'that confuse Line Input - so
                   'convert them to vbCrLf :)
                   Close #1
                   If Not bTriedFixUnixHostsFile Then
                       FixUNIXHostsFile
                       bTriedFixUnixHostsFile = True
                       CheckOther1Item
                   Else
                       MsgBox Translate(301), vbExclamation
   '                    MsgBox "Your hosts file has invalid linebreaks and " & _
   '                           "HijackThis is unable to fix this. O1 items will " & _
   '                           "not be displayed." & vbCrLf & vbCrLf & _
   '                           "Click OK to continue the rest of the scan.", vbExclamation
                   End If
                   Exit Sub
               End If
           
               'ignore all lines that start with loopback
               '(127.0.0.1), null (0.0.0.0) and private IPs
               '(192.168. / 10.)
               sLine = Replace(sLine, vbTab, " ")
               sLine = Trim(sLine)
               If sLine <> vbNullString Then
                   If InStr(sLine, "127.0.0.1") <> 1 And _
                      InStr(sLine, "0.0.0.0") <> 1 And _
                      InStr(sLine, "192.168.") <> 1 And _
                      InStr(sLine, "10.") <> 1 And _
                      InStr(sLine, "#") <> 1 And _
                      Not (bIgnoreSafe And InStr(sLine, "216.239.37.101") > 0) Or _
                      bIgnoreAllWhitelists Then
                       '216.239.37.101 = google.com
                       Do
                           sLine = Replace(sLine, "  ", " ")
                       Loop Until InStr(sLine, "  ") = 0
                   
                       sHit = "O1 - Hosts: " & sLine
                       If Not IsOnIgnoreList(sHit) Then
                           frmMain.lstResults.AddItem sHit
                           i = i + 1
                       End If
                   
                       If i > 100 Then
                           MsgBox Replace(Translate(302), "[]", sHostsFile), vbExclamation
   '                        MsgBox "You have an particularly large " & _
   '                        "amount of hijacked domains. It's probably " & _
   '                        "better to delete the file itself then to " & _
   '                        "fix each item (and create a backup)." & vbCrLf & _
   '                        vbCrLf & "If you see the same IP address in all " & _
   '                        "the reported O1 items, consider deleting your " & _
   '                        "Hosts file, which is located at " & sHostsFile & _
   '                        ".", vbExclamation
                           Close #1
                           Exit Sub
                       End If
                   End If
               End If
           Loop Until EOF(1)
       Close #1
       On Error Resume Next
       SetAttr sHostsFile, iAttr
       Exit Sub
   
   Error:
       Close #1
       ErrorMsg "modMain_CheckOther1Item", Err.Number, Err.Description
   End Sub
  */
  #endregion
}
