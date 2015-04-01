//Option Explicit

//Public Declare Function RegOpenKeyEx Lib "advapi32.dll" Alias "RegOpenKeyExA" (ByVal hKey As Long, ByVal lpSubKey As String, ByVal ulOptions As Long, ByVal samDesired As Long, phkResult As Long) As Long
//Public Declare Function RegCloseKey Lib "advapi32.dll" (ByVal hKey As Long) As Long
//Public Declare Function RegQueryValueEx Lib "advapi32.dll" Alias "RegQueryValueExA" (ByVal hKey As Long, ByVal lpValueName As String, ByVal lpReserved As Long, lpType As Long, lpData As Any, lpcbData As Long) As Long
//Public Declare Function RegSetValueEx Lib "advapi32.dll" Alias "RegSetValueExA" (ByVal hKey As Long, ByVal lpValueName As String, ByVal Reserved As Long, ByVal dwType As Long, lpData As Any, ByVal cbData As Long) As Long
//Public Declare Function RegDeleteValue Lib "advapi32.dll" Alias "RegDeleteValueA" (ByVal hKey As Long, ByVal lpValueName As String) As Long
//Public Declare Function RegDeleteKey Lib "advapi32.dll" Alias "RegDeleteKeyA" (ByVal hKey As Long, ByVal lpSubKey As String) As Long
//Public Declare Function RegCreateKeyEx Lib "advapi32.dll" Alias "RegCreateKeyExA" (ByVal hKey As Long, ByVal lpSubKey As String, ByVal Reserved As Long, ByVal lpClass As String, ByVal dwOptions As Long, ByVal samDesired As Long, lpSecurityAttributes As Any, phkResult As Long, lpdwDisposition As Long) As Long
//Public Declare Function RegEnumValue Lib "advapi32.dll" Alias "RegEnumValueA" (ByVal hKey As Long, ByVal dwIndex As Long, ByVal lpValueName As String, lpcbValueName As Long, ByVal lpReserved As Long, lpType As Long, lpData As Byte, lpcbData As Long) As Long
//Public Declare Function RegEnumKeyEx Lib "advapi32.dll" Alias "RegEnumKeyExA" (ByVal hKey As Long, ByVal dwIndex As Long, ByVal lpName As String, lpcbName As Long, ByVal lpReserved As Long, ByVal lpClass As String, lpcbClass As Long, lpftLastWriteTime As Any) As Long

//Private Declare Function SHFileExists Lib "shell32" Alias "#45" (ByVal szPath As String) As Long
//Private Declare Function SHDeleteKey Lib "shlwapi.dll" Alias "SHDeleteKeyA" (ByVal lRootKey As Long, ByVal szKeyToDelete As String) As Long

//Public Function NormalizePath$(sFile$)
    
//    Dim sBegin$, sValue$, sNext$
//    Dim EnvVar As String
//    Dim RealEnvVar As String
    
//    If False Then
//    Dim EnvRegExp As RegExp
//    Dim ObjMatch As Match
//    Dim ObjMatches As MatchCollection
//    'Dim EnvVar As String
    
//    Set EnvRegExp = New RegExp
//    EnvRegExp.Pattern = "%[\w_-]+%"
//    EnvRegExp.IgnoreCase = True
//    EnvRegExp.Global = True
    
//    If EnvRegExp.Test(sFile) = True Then
//        Set ObjMatches = EnvRegExp.Execute(sFile)
//        For Each ObjMatch In ObjMatches
//            EnvVar = Replace(ObjMatch.Value, "%", "", , , vbTextCompare)
//            If Len(Environ$(EnvVar)) > 0 Then
//                sFile = Replace(sFile, ObjMatch.Value, Environ$(EnvVar), , , vbTextCompare)
//            End If
//        Next
//    End If
//    End If
    
//'If False Then
//    sBegin = 1
//    Do
//        sValue = InStr(sBegin, sFile, "%", vbTextCompare)
//        If sValue = 0 Or sValue = Len(sFile) Or sBegin > Len(sFile) Then
//            Exit Do
//        End If
            
//        sBegin = sValue + 1
//        sNext = InStr(sBegin + 1, sFile, "%", vbTextCompare)
//        If sNext = 0 Or sNext > Len(sFile) Or sBegin > Len(sFile) Then
//            Exit Do
//        End If
        
//        EnvVar = Mid(sFile, sValue, sNext - sValue + 1)
//        RealEnvVar = Mid(sFile, sValue + 1, sNext - sValue - 1)
        
//        If Len(Environ$(RealEnvVar)) > 0 Then
//            sFile = Replace(sFile, EnvVar, Environ$(RealEnvVar), sValue, sNext - sValue + 1, vbTextCompare)
//            sBegin = sNext + 1 + Len(Environ$(RealEnvVar)) - Len(EnvVar)
//        Else
//            sBegin = sNext + 1
//        End If
        
//    Loop While True
//    'End If
//    NormalizePath = sFile
//End Function

//Public Function GetChromeVersion$()
//    Dim sVer$, ChromeVer$
//    Dim i&
    
//    sVer = RegGetString(HKEY_LOCAL_MACHINE, "Software\Google\Update\Clients\{8A69D345-D564-463c-AFF1-A69D9E530F96}", "pv")
//    'not found try current user - win7(x86)
//    If sVer = vbNullString Then
//        sVer = RegGetString(HKEY_CURRENT_USER, "Software\Google\Update\Clients\{8A69D345-D564-463c-AFF1-A69D9E530F96}", "pv")
//    End If
//    If sVer = vbNullString Then
//        sVer = RegGetString(HKEY_LOCAL_MACHINE, "Software\Wow6432Node\Google\Update\Clients\{8A69D345-D564-463c-AFF1-A69D9E530F96}", "pv")
//    End If
    
//    If sVer <> vbNullString Then
//        ChromeVer = "CHROME: " & sVer
//    End If
    
//    GetChromeVersion = ChromeVer
//End Function


//Public Function GetFirefoxVersion$()
//    Dim sVer$, FirefoxVer$
//    Dim i&
    
//    sVer = RegGetString(HKEY_LOCAL_MACHINE, "Software\Mozilla\Mozilla Firefox", "CurrentVersion")
//    If sVer <> vbNullString Then
//        FirefoxVer = "FIREFOX: " & sVer
//    End If

//    GetFirefoxVersion = FirefoxVer
//End Function

//'---------------------------------------------------------------------------------------
//' Procedure : GetOperaVersion
//' Purpose   : Gets the version of the installed Opera program
//' Return    : The version as a string or an error message if it cannot be found
//' Notes     : Required Project Reference: Microsoft Scripting Runtime
//'---------------------------------------------------------------------------------------
//' Revision History:
//' Date       Author        Purpose
//' ---------  ------------  -------------------------------------------------------------
//' 02Jul2013  Claire Streb  Original
//'
//Public Function GetOperaVersion() As String

//    Const MyProcName = "GetOperaVersion"
//    Const DoubleQuote = """"
    
//    Dim sResult As String: sResult = "Unable to get Opera version!"
    
//    Dim sOperaPath As String, sOperaVer As String, sOperaFriendlyVer As String

//    On Error GoTo ErrorHandler

//    sOperaFriendlyVer = "0"

//    sOperaPath = RegGetString(HKEY_LOCAL_MACHINE, "Software\Microsoft\Windows\CurrentVersion\App Paths\Opera.exe", vbNullString)

//    If Len(sOperaPath) > 0 Then
        
//        If Left$(sOperaPath, 1) = DoubleQuote Then sOperaPath = Mid$(sOperaPath, 2)
//        If Right$(sOperaPath, 1) = DoubleQuote Then sOperaPath = Left$(sOperaPath, Len(sOperaPath) - 1)
        
//        If DoesFileExist(sOperaPath) Then
//            Dim Fso As Scripting.FileSystemObject
//            Set Fso = New Scripting.FileSystemObject
//            sResult = "OPERA: " & Fso.GetFileVersion(sOperaPath)
//        End If
        
//    End If
//    GoTo EndProcedure
    
//ErrorHandler:
//    ErrorMsg Err.Number, Err.Description, MyProcName
    
//EndProcedure:
//    GetOperaVersion = sResult
//    On Error GoTo 0

//End Function

//'---------------------------------------------------------------------------------------
//' Procedure : DoesFileExist
//' Purpose   : Determines whether a file exists
//' Return    : True if it exists, False if it doesn't
//'---------------------------------------------------------------------------------------
//' Revision History:
//' Date       Author        Purpose
//' ---------  ------------  -------------------------------------------------------------
//' 02Jul2013  Claire Streb  Original
//'
//Public Function DoesFileExist(ByVal sFilename As String) As Boolean
//    On Error Resume Next
//    DoesFileExist = (GetAttr(sFilename) And vbDirectory) <> vbDirectory
//    On Error GoTo 0
//End Function

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Win32;
using ReviveThis.Entities.ExtensionMethods;

namespace ReviveThis.Entities.Modules
{
  public static class Utils
  {
    public static string GetChromeVersion(string prefix = "CHROME: ")
    {
      var version = string.Empty;

      try
      {

        using (var baseKey =
          RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
        {
          using (
            var regKey =
              baseKey.OpenSubKey(@"Software\Google\Update\Clients\{8A69D345-D564-463c-AFF1-A69D9E530F96}"))
          {
            if (regKey != null)
              version = regKey.GetValue("pv") as string;
          }
        }

        if (string.IsNullOrEmpty(version))
          using (
            var regKey =
              Registry.CurrentUser.OpenSubKey(@"Software\Google\Update\Clients\{8A69D345-D564-463c-AFF1-A69D9E530F96}"))
          {
            if (regKey != null)
              version = regKey.GetValue("pv") as string;
          }

        if (string.IsNullOrEmpty(version) && Environment.Is64BitOperatingSystem)
          using (var baseKey =
            RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
          {
            using (
              var regKey =
                baseKey.OpenSubKey(@"Software\Google\Update\Clients\{8A69D345-D564-463c-AFF1-A69D9E530F96}"))
            {
              if (regKey != null)
                version = regKey.GetValue("pv") as string;
            }
          }

      }
      catch (Exception)
      {
        //throw;
      }


      return string.Format("{0}{1}", prefix, version);

    }

    public static string GetFireFoxVersion(string prefix = "FIREFOX: ")
    {
      var version = string.Empty;

      try
      {
        using (
          var regKey =
            Registry.LocalMachine.OpenSubKey(@"Software\Mozilla\Mozilla Firefox"))
        {
          if (regKey != null)
            version = regKey.GetValue("CurrentVersion") as string;
        }
      }
      catch (Exception)
      {
        //throw;
      }


      return string.Format("{0}{1}", prefix, version);

    }

    public static string GetOperaVersion(string prefix = "OPERA: ")
    {
      FileVersionInfo version = null;

      try
      {
        using (
          var regKey =
            Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\App Paths\Opera.exe"))
        {
          string sOperaPath;

          if ((regKey != null) && !string.IsNullOrEmpty(sOperaPath = (regKey.GetValue(string.Empty) as string)))
            if (File.Exists(sOperaPath))
              version = FileVersionInfo.GetVersionInfo(sOperaPath);

        }
      }
      catch (Exception)
      {
        //throw;
      }

      return string.Format("{0}{1}", prefix, version != null ? version.FormatToString() : null);

    }
    public static string GetInternetExplorerVersion(string prefix = "MSIE: ")
    {

      //try
      //{
      //  using (var browser = new WebBrowser())
      //  {
      //    return string.Format("{0}{1}", prefix, browser.Version.FormatToString());
      //    var ver = (new WebBrowser()).Version.FormatToString(true);
      //  }

      //}
      //catch (Exception) 
      //{
        Version version = null;

      var updateVersion = string.Empty;

      try
      {
        using (
          var regKey =
            Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\App Paths\IEXPLORE.EXE"))
        {
          string sFilePath;

          if ((regKey != null) && !string.IsNullOrEmpty(sFilePath = (regKey.GetValue(string.Empty) as string)))
            if (File.Exists(sFilePath))
            {
              try
              {
                var versionInfo = FileVersionInfo.GetVersionInfo(sFilePath);
                if (versionInfo.CompanyName.IndexOf("Microsoft", StringComparison.InvariantCultureIgnoreCase) != -1)
                  version = new Version(versionInfo.FileMajorPart, versionInfo.FileMinorPart, versionInfo.FileBuildPart,
                    versionInfo.FilePrivatePart);
              }
              catch (Exception)
              {
                //throw;
              }
            }
        }


        using (
          var regKey =
            Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Internet Explorer")
          )
        {
          if (regKey != null)
          {
            var values = regKey.GetValueNames();

            if (version == null)
            {
              var svcVersion =
                values.FirstOrDefault(f => f.Equals("svcVersion", StringComparison.InvariantCultureIgnoreCase));

              if (string.IsNullOrEmpty(svcVersion))
                svcVersion =
                  values.FirstOrDefault(f => f.Equals("Version", StringComparison.InvariantCultureIgnoreCase));

              var value = regKey.GetValue(svcVersion, "0.0.0.0") as string;
              if (!string.IsNullOrEmpty(value))
              {
                Version.TryParse(value, out version);
              }
            }

            var svcUpdateVersion =
              values.FirstOrDefault(f => f.Equals("svcUpdateVersion", StringComparison.InvariantCultureIgnoreCase));

            if (!string.IsNullOrEmpty(svcUpdateVersion))
            {
              updateVersion = regKey.GetValue(svcUpdateVersion, null) as string;
            }

          }
        }

        if (string.IsNullOrEmpty(updateVersion) && version != null)
        {
          updateVersion = string.Format("{0}", version.Major);
        }

      }
      catch (Exception)
      {
        //throw;
      }

      return string.Format("{0}{1}{2}", prefix, /*!string.IsNullOrEmpty(updateVersion) ? string.Format("{0} ", updateVersion) :*/ null, version != null ? version.FormatToString(true) : null);
      //}

      /*
       Public Function GetMSIEVersion$()
          Dim sMSIEPath$, sMSIEVer$, sMSIEHotfixes$, sMSIEFriendlyVer$
          On Error GoTo Error:
          sMSIEPath = RegGetString(HKEY_LOCAL_MACHINE, "Software\Microsoft\Windows\CurrentVersion\App Paths\IEXPLORE.EXE", "")
          If sMSIEPath = "" Then GoTo EndOfFun:
          If FileExists(sMSIEPath) = False Then GoTo EndOfFun:
    
          Dim hData&, lDataLen&, uBuf() As Byte, uVFFI As VS_FIXEDFILEINFO
          lDataLen = GetFileVersionInfoSize(sMSIEPath, ByVal 0)
          If lDataLen = 0 Then
              GoTo EndOfFun:
          End If
        
          ReDim uBuf(0 To lDataLen - 1)
          'get handle to file props
          GetFileVersionInfo sMSIEPath, 0, lDataLen, uBuf(0)
          VerQueryValue uBuf(0), "\", hData, lDataLen
          CopyMemory uVFFI, ByVal hData, Len(uVFFI)
          With uVFFI
              sMSIEVer = Format(.dwFileVersionMSh, "0") & "." & _
                         Format(.dwFileVersionMSl, "00") & "." & _
                         Format(.dwProductVersionLSh, "0000") & "." & _
                         Format(.dwProductVersionLSl, "0000")
          End With
          If sMSIEVer = "0.00.0000.0000" Then GoTo EndOfFun:
    
          sMSIEFriendlyVer = Left(sMSIEVer, 4)
    
          sMSIEHotfixes = RegGetString(HKEY_LOCAL_MACHINE, "Software\Microsoft\Windows\CurrentVersion\Internet Settings", "MinorVersion")
          If sMSIEHotfixes = vbNullString Then GoTo EndOfFun:
          If InStr(1, sMSIEHotfixes, "SP5", vbTextCompare) > 0 Then
              sMSIEFriendlyVer = sMSIEFriendlyVer & " SP5"
          Else
              If InStr(1, sMSIEHotfixes, "SP4", vbTextCompare) > 0 Then
                  sMSIEFriendlyVer = sMSIEFriendlyVer & " SP4"
              Else
                  If InStr(1, sMSIEHotfixes, "SP3", vbTextCompare) > 0 Then
                      sMSIEFriendlyVer = sMSIEFriendlyVer & " SP3"
                  Else
                
                      If InStr(1, sMSIEHotfixes, "SP2", vbTextCompare) > 0 Then
                          sMSIEFriendlyVer = sMSIEFriendlyVer & " SP2"
                      Else
                          If InStr(1, sMSIEHotfixes, "SP1", vbTextCompare) > 0 Then
                              sMSIEFriendlyVer = sMSIEFriendlyVer & " SP1"
                          End If
                      End If
                  End If
              End If
          End If
    
      EndOfFun:
          If lDataLen > 0 And Left(sMSIEFriendlyVer, 1) <> "0" Then
              GetMSIEVersion = "Internet Explorer v" & sMSIEFriendlyVer & " (" & sMSIEVer & ")"
          Else
              GetMSIEVersion = "Unable to get Internet Explorer version!"
          End If
          Exit Function
    
      Error:
          ErrorMsg Err.Number, Err.Description, "GetMSIEVersion"
      End Function
       */
    }

  }
}