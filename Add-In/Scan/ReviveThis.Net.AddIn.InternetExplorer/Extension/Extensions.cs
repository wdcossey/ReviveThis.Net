using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Win32;
using ReviveThis.AddIn.InternetExplorer.Extension.Entites;
using ReviveThis.AddIn.InternetExplorer.Extension.Enums;
using ReviveThis.Entities;
using ReviveThis.Entities.PInvoke;
using ReviveThis.Enums;
using ReviveThis.Interfaces;

namespace ReviveThis.AddIn.InternetExplorer.Extension
{

  #region [O9] IE Extensions / CheckOther9Item

  [Export(typeof(IDetectionAddIn))]
  public class Extensions: IDetectionAddIn
  {

    #region private
    #region consts

    private const string INPROC_SERVER32 = "InprocServer32";
    private const string TREAT_AS = "TreatAs";

    private const string RES_PREFIX = "res://";
    private const string FILE_PREFIX = "file://";
    private const string HTTP_PREFIX = "http://";
    
    //private const string REGEX_STRIPPER = "res\\:\\/\\/|file\\:\\/\\/|(?<FileName>.*?)";

    private const string NO_FILE = @"(no file)";

    private const string KEY_NAME_BUTTON_TEXT = @"ButtonText";
    private const string KEY_NAME_MENU_TEXT = @"MenuText";
    
    #endregion

    #region IE Extensions Registry Parsers
    private static RegistryParser[] _autoRunRegistryParsers;

    private static IEnumerable<RegistryParser> ExtensionsRegistryParsers
    {
      get
      {
        if (_autoRunRegistryParsers != null && _autoRunRegistryParsers.Any())
          return _autoRunRegistryParsers;

        return _autoRunRegistryParsers = new[]
        {
          #region LocalMachine
          new RegistryParser(RegistryHive.LocalMachine, @"Software\Microsoft\Internet Explorer\Extensions",
            new[] {RegistryView.Registry32, RegistryView.Registry64}),
          #endregion

          #region CurrentUser
          new RegistryParser(RegistryHive.CurrentUser, @"Software\Microsoft\Internet Explorer\Extensions",
            new[] {RegistryView.Default}),

          #endregion
        };
      }
    }
    #endregion

    #endregion
    public string Author
    {
      get { return Consts.General.Author; }
    }

    public Version Version
    {
      get
      {
        return new Version(1, 0, 0, 0);
      }
    }

    public string Name
    {
      get { return @"Extensions"; }
      
    }

    public string[] Description
    {
      get { return new[] { @"Scans for Internet Explorer Extensions." }; }
    }
    public void Dispose()
    {
      //Nothing to dispose.
    }

    private static string GetClassesRootValue(RegistryView view, string clsid, string valueName, string defaultValue = null)
    {
      string result = null;

      using (
        var regKey =
          RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, view).OpenSubKey(string.Format(@"CLSID\{0}", clsid)))
      {
        if (regKey != null)
        {
          result = regKey.GetValue(valueName, null) as string;
        }
      }

      //Fallback if we can't find the information with the 64bit hive.
      if (string.IsNullOrEmpty(result) && view == RegistryView.Registry64)
      {
        result = GetClassesRootValue(RegistryView.Registry32, clsid, valueName);
      }

      return result ?? defaultValue;
    }

    private string GetResourceString(string resourceString)
    {
      if (!string.IsNullOrEmpty(resourceString))
      {
        var strResource = resourceString.Trim();
        if (strResource.StartsWith("@", StringComparison.InvariantCultureIgnoreCase) && strResource.Contains(","))
        {
          var lastIndex = strResource.LastIndexOf(',');
          try
          {
            var resFile = strResource.Substring(1, lastIndex - 1).Trim();
            var strIndex = strResource.Substring(lastIndex + 1, strResource.Length - lastIndex - 1).Trim();
            var resIndex = 0;
            if (Int32.TryParse(strIndex, out resIndex) && File.Exists(resFile))
            {
              using (var resource = new StringLoader(resFile))
              {
                var result = resource.Load(resIndex);
                return result ?? resourceString;
              }
            }
          }
          catch (Exception)
          {
            //throw;
          }
        }

      }

      return resourceString;
    }

    public async Task<ICollection<IDetectionResultItem>> Scan()
    {
      //await Task.FromResult(0);
      
      var result = new Collection<IDetectionResultItem>();

      foreach (var item in ExtensionsRegistryParsers)
      {
        foreach (var view in item.RegistryViews)
        {
          using (var regKey = RegistryKey.OpenBaseKey(item.RegistryHive, view).OpenSubKey(item.SubKey))
          {
            if (regKey == null || (regKey.SubKeyCount <= 0))
              continue;

            var subKeys = regKey.GetSubKeyNames().Where(w => !w.Equals("CmdMapping", StringComparison.InvariantCultureIgnoreCase));

            foreach (var subKeyName in subKeys)
            {
              var clsidKey = subKeyName;

              using (var subKey = regKey.OpenSubKey(subKeyName, false))
              {
                if (subKey == null)
                  continue;

                var buttonText = subKey.GetValue(KEY_NAME_BUTTON_TEXT, null) as string;
                var menuText = subKey.GetValue(KEY_NAME_MENU_TEXT, null) as string;

                //this clsid is mostly useless, always pointing to SHDOCVW.DLL
                //places to look for correct dll:
                //* Exec
                //* Script
                //* BandCLSID
                //* CLSIDExtension
                //* CLSIDExtension -> TreatAs CLSID
                //* CLSID
                //* ???
                //* actual CLSID of regkey (not used)

                var sFile = subKey.GetValue("Exec", null) as string;
                if (string.IsNullOrEmpty(sFile))
                {
                  sFile = subKey.GetValue("Script", null) as string;

                  if (string.IsNullOrEmpty(sFile))
                  {
                    var clsid2 = subKey.GetValue("BandCLSID", null) as string;

                    if (!string.IsNullOrEmpty(clsid2))
                    {
                      sFile = GetClassesRootValue(subKey.View, Path.Combine(clsid2, INPROC_SERVER32), null);
                    }

                    if (string.IsNullOrEmpty(sFile))
                    {
                      clsid2 = subKey.GetValue("CLSIDExtension", null) as string;

                      if (!string.IsNullOrEmpty(clsid2))
                      {
                        sFile = GetClassesRootValue(subKey.View, Path.Combine(clsid2, INPROC_SERVER32), null);

                        if (string.IsNullOrEmpty(sFile))
                        {
                          clsid2 = GetClassesRootValue(subKey.View, Path.Combine(clsid2, TREAT_AS), null);

                          if (string.IsNullOrEmpty(clsid2))
                            clsid2 = subKey.GetValue("CLSID", null) as string;

                          if (!string.IsNullOrEmpty(clsid2))
                          {
                            sFile = GetClassesRootValue(subKey.View, Path.Combine(clsid2, INPROC_SERVER32), null);
                          }
                        }
                      }
                    }
                  }
                }

                if (!string.IsNullOrEmpty(sFile))
                {
                  //expand %systemroot% var
                  //sFile = Replace(sFile, "%systemroot%", sWinDir, , , vbTextCompare)
                  sFile = Environment.ExpandEnvironmentVariables(sFile);

                  //strip stuff from res://[dll]/page.htm to just [dll]
                  if (sFile.StartsWith(RES_PREFIX, StringComparison.InvariantCultureIgnoreCase))
                  {
                    sFile = sFile.Substring(RES_PREFIX.Length, sFile.Length - RES_PREFIX.Length);
                  }

                  //remove other stupid prefixes
                  if (sFile.StartsWith(FILE_PREFIX, StringComparison.InvariantCultureIgnoreCase) && !sFile.StartsWith(HTTP_PREFIX, StringComparison.InvariantCultureIgnoreCase))
                  {
                    sFile = sFile.Substring(FILE_PREFIX.Length, sFile.Length - FILE_PREFIX.Length);
                  }


                  sFile = sFile.TrimStart('"').TrimEnd('"');
                  //if (!File.Exists(sFile))
                  //{
                  //  sFile += string.Format(" {0}", FILE_MISSING);
                  //}
                }
                else
                {
                  sFile = NO_FILE;
                }

                if (!string.IsNullOrEmpty(buttonText))
                {
                  //buttonText = GetResourceString(buttonText);

                  //TODO: Implement IgnoreList
                  result.Add(new ExtensionsResult(ExtensionsResultType.Button, item.RegistryHive, subKey.View,
                    subKey.Name, KEY_NAME_BUTTON_TEXT, clsidKey, buttonText, sFile, !string.IsNullOrEmpty(sFile) && File.Exists(sFile)));
                }

                if (!string.IsNullOrEmpty(menuText))
                {
                  //menuText = GetResourceString(menuText);

                  //TODO: Implement IgnoreList
                  result.Add(new ExtensionsResult(ExtensionsResultType.ToolMenu, item.RegistryHive, subKey.View,
                    subKey.Name, KEY_NAME_MENU_TEXT, clsidKey, menuText, sFile, !string.IsNullOrEmpty(sFile) && File.Exists(sFile)));
                }
              }
            }
          }
        }
      }
      return result;
    }

    public ScanResultType ResultType
    {
      get { return ScanResultType.InternetExplorerExtension; }
    }
  }

  #endregion

  #region Original Visual Basic (6.0) Code Block

  /* Original Visual Basic (6.0) Code Block
   Public Sub CheckOther9Item()
       'HKLM\Software\Microsoft\Internet Explorer\Extensions
       'HKCU\..\etc
     
       On Error GoTo Error:
       Dim hKey&, hKey2&, i&, sData$, sCLSID$, sCLSID2$, sFile$, sHit$
       'open root key
       If RegOpenKeyEx(HKEY_LOCAL_MACHINE, "Software\Microsoft\Internet Explorer\Extensions", 0, KEY_ENUMERATE_SUB_KEYS, hKey) = 0 Then
           i = 0
           sCLSID = String(255, 0)
           'start enum of root key subkeys (i.e., extensions)
           If RegEnumKeyEx(hKey, i, sCLSID, 255, 0, vbNullString, ByVal 0, ByVal 0) <> 0 Then RegCloseKey hKey: Exit Sub
           Do
               sCLSID = TrimNull(sCLSID)
               If sCLSID = "CmdMapping" Then GoTo NextExtHKLM:
             
               'check for 'MenuText' or 'ButtonText'
               sData = RegGetString(HKEY_LOCAL_MACHINE, "Software\Microsoft\Internet Explorer\Extensions\" & sCLSID, "ButtonText")
             
               'this clsid is mostly useless, always pointing to SHDOCVW.DLL
               'places to look for correct dll:
               '* Exec
               '* Script
               '* BandCLSID
               '* CLSIDExtension
               '* CLSIDExtension -> TreatAs CLSID
               '* CLSID
               '* ???
               '* actual CLSID of regkey (not used)
               sFile = RegGetString(HKEY_LOCAL_MACHINE, "Software\Microsoft\Internet Explorer\Extensions\" & sCLSID, "Exec")
               If sFile = vbNullString Then
                   sFile = RegGetString(HKEY_LOCAL_MACHINE, "Software\Microsoft\Internet Explorer\Extensions\" & sCLSID, "Script")
                   If sFile = vbNullString Then
                       sCLSID2 = RegGetString(HKEY_LOCAL_MACHINE, "Software\Microsoft\Internet Explorer\Extensions\" & sCLSID, "BandCLSID")
                       sFile = RegGetString(HKEY_CLASSES_ROOT, "CLSID\" & sCLSID2 & "\InprocServer32", "")
                       If sFile = vbNullString Then
                           sCLSID2 = RegGetString(HKEY_LOCAL_MACHINE, "Software\Microsoft\Internet Explorer\Extensions\" & sCLSID, "CLSIDExtension")
                           sFile = RegGetString(HKEY_CLASSES_ROOT, "CLSID\" & sCLSID2 & "\InprocServer32", "")
                           If sFile = vbNullString Then
                               sCLSID2 = RegGetString(HKEY_CLASSES_ROOT, "CLSID\" & sCLSID2 & "\TreatAs", "")
                               sFile = RegGetString(HKEY_CLASSES_ROOT, "CLSID\" & sCLSID2 & "\InprocServer32", "")
                               If sFile = vbNullString Then
                                   sCLSID2 = RegGetString(HKEY_LOCAL_MACHINE, "Software\Microsoft\Internet Explorer\Extensions\" & sCLSID, "CLSID")
                                   sFile = RegGetString(HKEY_CLASSES_ROOT, "CLSID\" & sCLSID2 & "\InprocServer32", "")
                               End If
                           End If
                       End If
                   End If
               End If
             
               If sFile <> vbNullString Then
                   'expand %systemroot% var
                   'sFile = Replace(sFile, "%systemroot%", sWinDir, , , vbTextCompare)
                   sFile = NormalizePath(sFile)
                 
                   'strip stuff from res://[dll]/page.htm to just [dll]
                   If InStr(1, sFile, "res://", vbTextCompare) = 1 And _
                      (LCase(Right(sFile, 4)) = ".htm" Or LCase(Right(sFile, 4)) = "html") Then
                       sFile = Mid(sFile, 7)
                       sFile = Left(sFile, InStrRev(sFile, "/") - 1)
                   End If
                 
                   'remove other stupid prefixes
                   If InStr(sFile, "file://") = 1 And _
                      InStr(sFile, "http://") <> 1 Then
                       If Not FileExists(Mid(sFile, 8)) Then sFile = sFile & " (file missing)"
                   Else
                       If Not FileExists(sFile) Then sFile = sFile & " (file missing)"
                   End If
               Else
                   sFile = "(no file)"
               End If
             
               If sData = vbNullString Then sData = "(no name)"
               If InStr(sData, "@shdoclc.dll,-866") > 0 Then sData = "Related"
             
               sHit = "O9 - Extra button: " & sData & " - " & sCLSID & " - " & sFile '& " (HKLM)"
               If Not IsOnIgnoreList(sHit) Then
                   If bMD5 Then sHit = sHit & GetFileMD5(sFile)
                   frmMain.lstResults.AddItem sHit
               End If
                 
               sData = RegGetString(HKEY_LOCAL_MACHINE, "Software\Microsoft\Internet Explorer\Extensions\" & sCLSID, "MenuText")
               'don't show it again in case sdata=null
               If sData <> vbNullString Then
                   If InStr(sData, "@shdoclc.dll,-864") > 0 Then sData = "Show &Related Links"
                   sHit = "O9 - Extra 'Tools' menuitem: " & sData & " - " & sCLSID & " - " & sFile '& " (HKLM)"
                   If Not IsOnIgnoreList(sHit) Then
                       If bMD5 Then sHit = sHit & GetFileMD5(sFile)
                       frmMain.lstResults.AddItem sHit
                   End If
               End If
   NextExtHKLM:
               sCLSID = String(255, 0)
               i = i + 1
           Loop Until RegEnumKeyEx(hKey, i, sCLSID, 255, 0, vbNullString, ByVal 0, ByVal 0) <> 0
           RegCloseKey hKey
       End If
     
       '-----------------------------
       'repeat for HKCU
       If RegOpenKeyEx(HKEY_CURRENT_USER, "Software\Microsoft\Internet Explorer\Extensions", 0, KEY_ENUMERATE_SUB_KEYS, hKey) = 0 Then
           i = 0
           sCLSID = String(255, 0)
           'start enum of root key subkeys (i.e., extensions)
           If RegEnumKeyEx(hKey, i, sCLSID, 255, 0, vbNullString, ByVal 0, ByVal 0) <> 0 Then RegCloseKey hKey: Exit Sub
           Do
               sCLSID = TrimNull(sCLSID)
               If sCLSID = "CmdMapping" Then GoTo NextExtHKCU:
             
               'check for 'MenuText' or 'ButtonText'
               sData = RegGetString(HKEY_CURRENT_USER, "Software\Microsoft\Internet Explorer\Extensions\" & sCLSID, "ButtonText")
             
               sFile = RegGetString(HKEY_CURRENT_USER, "Software\Microsoft\Internet Explorer\Extensions\" & sCLSID, "Exec")
               If sFile = vbNullString Then
                   sFile = RegGetString(HKEY_CURRENT_USER, "Software\Microsoft\Internet Explorer\Extensions\" & sCLSID, "Script")
                   If sFile = vbNullString Then
                       sCLSID2 = RegGetString(HKEY_CURRENT_USER, "Software\Microsoft\Internet Explorer\Extensions\" & sCLSID, "BandCLSID")
                       sFile = RegGetString(HKEY_CLASSES_ROOT, "CLSID\" & sCLSID2 & "\InprocServer32", "")
                       If sFile = vbNullString Then
                           sCLSID2 = RegGetString(HKEY_CURRENT_USER, "Software\Microsoft\Internet Explorer\Extensions\" & sCLSID, "CLSIDExtension")
                           sFile = RegGetString(HKEY_CLASSES_ROOT, "CLSID\" & sCLSID2 & "\InprocServer32", "")
                           If sFile = vbNullString Then
                               sCLSID2 = RegGetString(HKEY_CLASSES_ROOT, "CLSID\" & sCLSID2 & "\TreatAs", "")
                               sFile = RegGetString(HKEY_CLASSES_ROOT, "CLSID\" & sCLSID2 & "\InprocServer32", "")
                               If sFile = vbNullString Then
                                   sCLSID2 = RegGetString(HKEY_CURRENT_USER, "Software\Microsoft\Internet Explorer\Extensions\" & sCLSID, "CLSID")
                                   sFile = RegGetString(HKEY_CLASSES_ROOT, "CLSID\" & sCLSID2 & "\InprocServer32", "")
                               End If
                           End If
                       End If
                   End If
               End If
             
   '            sFile = RegGetString(HKEY_CURRENT_USER, "Software\Microsoft\Internet Explorer\Extensions\" & sCLSID, "Exec")
   '            If sFile = vbNullString Then
   '                sFile = RegGetString(HKEY_CURRENT_USER, "Software\Microsoft\Internet Explorer\Extensions\" & sCLSID, "Script")
   '                If sFile = vbNullString Then
   '                    sCLSID2 = RegGetString(HKEY_CURRENT_USER, "Software\Microsoft\Internet Explorer\Extensions\" & sCLSID, "BandCLSID")
   '                    sFile = RegGetString(HKEY_CLASSES_ROOT, "CLSID\" & sCLSID2 & "\InprocServer32", "")
   '                End If
   '            End If
             
               If sFile <> vbNullString Then
                   'sFile = Replace(sFile, "%systemroot%", sWinDir, , , vbTextCompare)
                   sFile = NormalizePath(sFile)
                 
                   If InStr(sFile, "file://") = 1 And InStr(sFile, "http://") <> 1 Then
                       If Not FileExists(Mid(sFile, 8)) Then sFile = sFile & " (file missing)"
                   Else
                       If Not FileExists(sFile) Then sFile = sFile & " (file missing)"
                   End If
               Else
                   sFile = "(no file)"
               End If
             
               If sData = vbNullString Then sData = "(no name)"
               If InStr(sData, "@shdoclc.dll,-866") > 0 Then sData = "Related"
             
               sHit = "O9 - Extra button: " & sData & " - " & sCLSID & " - " & sFile & " (HKCU)"
               If Not IsOnIgnoreList(sHit) Then
                   If bMD5 Then sHit = sHit & GetFileMD5(sFile)
                   frmMain.lstResults.AddItem sHit
               End If
                 
               sData = RegGetString(HKEY_CURRENT_USER, "Software\Microsoft\Internet Explorer\Extensions\" & sCLSID, "MenuText")
               If sData <> vbNullString Then
                   If InStr(sData, "@shdoclc.dll,-864") > 0 Then sData = "Show &Related Links"
                   sHit = "O9 - Extra 'Tools' menuitem: " & sData & " - " & sCLSID & " - " & sFile & " (HKCU)"
                   If Not IsOnIgnoreList(sHit) Then
                       If bMD5 Then sHit = sHit & GetFileMD5(sFile)
                       frmMain.lstResults.AddItem sHit
                   End If
               End If
   NextExtHKCU:
               sCLSID = String(255, 0)
               i = i + 1
           Loop Until RegEnumKeyEx(hKey, i, sCLSID, 255, 0, vbNullString, ByVal 0, ByVal 0) <> 0
           RegCloseKey hKey
       End If
     
       Exit Sub
     
   Error:
       ErrorMsg "modMain_CheckOther9Item", Err.Number, Err.Description
   End Sub
   */

  #endregion
}