using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Win32;
using ReviveThis.AddIn.InternetExplorer.Prefix.Entities;
using ReviveThis.AddIn.InternetExplorer.Prefix.Enums;
using ReviveThis.Enums;
using ReviveThis.Interfaces;

namespace ReviveThis.AddIn.InternetExplorer.Prefix
{

  #region [O13] URL Prefix Hi-Jacking / CheckOther13Item

  [Export(typeof (IDetectionAddIn))]
  public class Prefixes : IDetectionAddIn
  {
    #region private 

    #region consts
    private const string REGISTRY_BASE_PATH = @"Software\Microsoft\Windows\CurrentVersion\URL";
    private const string REGISTRY_DEFAULT_PREFIX_PATH = REGISTRY_BASE_PATH + @"\DefaultPrefix";
    private const string REGISTRY_PREFIXES_PATH = REGISTRY_BASE_PATH + @"\Prefixes";
    #endregion

    #region Prefix Match

    private const string PREFIX_HTTP = "http://";
    private const string PREFIX_FTP = "ftp://";
    private const string PREFIX_GOPHER = "gopher://";

    private const string KEY_WWW = "www";
    private const string KEY_WWW_DOT = "www.";
    private const string KEY_HOME = "home";
    private const string KEY_MOSAIC = "mosaic";
    private const string KEY_FTP = "ftp";
    private const string KEY_GOPHER = "gopher";

    private static Dictionary<string, string[]> _defaultPrefixMatch;

    private static Dictionary<string, string[]> DefaultPrefixMatch
    {
      get
      {
        if (_defaultPrefixMatch != null && _defaultPrefixMatch.Any())
          return _defaultPrefixMatch;

        return _defaultPrefixMatch = new Dictionary<string, string[]>
        {
          {string.Empty, new[] {PREFIX_HTTP}},
        };
      }
    }

    private static Dictionary<string, string[]> _prefixMatch;

    private static Dictionary<string, string[]> PrefixMatch
    {
      get
      {
        if (_prefixMatch != null && _prefixMatch.Any())
          return _prefixMatch;

        return _prefixMatch = new Dictionary<string, string[]>
        {
          {KEY_WWW, new [] { PREFIX_HTTP } },
          {KEY_WWW_DOT, new string[] { null } },
          {KEY_HOME, new [] { PREFIX_HTTP }},
          {KEY_MOSAIC, new [] { PREFIX_HTTP }},
          {KEY_FTP, new [] { PREFIX_FTP }},
          {KEY_GOPHER, new[] { PREFIX_GOPHER, null }},
        };
      }
    }

    #endregion

    #region Prefixes Registry Parsers

    private static PrefixRegistryParser[] _prefixesRegistryParsers;

    private static IEnumerable<PrefixRegistryParser> PrefixesRegistryParsers
    {
      get
      {
        if (_prefixesRegistryParsers != null && _prefixesRegistryParsers.Any())
          return _prefixesRegistryParsers;

        return _prefixesRegistryParsers = new[]
        {
          #region LocalMachine
          new PrefixRegistryParser(RegistryHive.LocalMachine, REGISTRY_DEFAULT_PREFIX_PATH,
            new[] {RegistryView.Registry32, RegistryView.Registry64},
            PrefixResultType.DefaultPrefix,
            DefaultPrefixMatch),

          new PrefixRegistryParser(RegistryHive.LocalMachine, REGISTRY_PREFIXES_PATH,
            new[]
            {
              RegistryView.Registry32,
              RegistryView.Registry64
            },
            PrefixResultType.Prefix,
            PrefixMatch),

          #endregion

          #region CurrentUser
          new PrefixRegistryParser(RegistryHive.CurrentUser, REGISTRY_DEFAULT_PREFIX_PATH,
            new[] {RegistryView.Registry32},
            PrefixResultType.DefaultPrefix,
            DefaultPrefixMatch),

          new PrefixRegistryParser(RegistryHive.CurrentUser, REGISTRY_PREFIXES_PATH,
            new[]
            {
              RegistryView.Default
            },
            PrefixResultType.Prefix,
            PrefixMatch)
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
      get { return new Version(1, 0, 0, 0); }
    }

    public string Name
    {
      get { return @"URL Prefixes"; }
    }

    public string[] Description
    {
      get
      {
        return new[]
        {
          @"Scans for Internet Explorer URL Prefix hi-jacking.",
          string.Empty,
          string.Format("Registry locations include (but not limited to):\r\n\t{0}",
            string.Join("\r\n\t",
              PrefixesRegistryParsers.Where(w => w.RegistryHive == RegistryHive.LocalMachine).Select(s => s.SubKey)))
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
      
      var result = new Collection<IDetectionResultItem>();

      foreach (var item in PrefixesRegistryParsers)
      {
        foreach (var view in item.RegistryViews)
        {
          using (var regKey = RegistryKey.OpenBaseKey(item.RegistryHive, view).OpenSubKey(item.SubKey, false))
          {
            if (regKey == null)
              continue;

            foreach (var prefix in item.PrefixDictionary)
            {
              var value = regKey.GetValue(prefix.Key, null) as string;
              if (!prefix.Value.Contains(value))
              {
                result.Add(new PrefixResult(item.PrefixResultType, item.RegistryHive, regKey.View, regKey.Name, prefix.Key, value));
              }
            }
          }
        }
      }

      return result;
    }

    public ScanResultType ResultType
    {
      get { return ScanResultType.RegistryMultipleValues; }
    }
  }

  #endregion

  #region Original Visual Basic (6.0) Code Block

  /*
   Public Sub CheckOther13Item()
       'O13
       Dim sDummy$, sKeyURL$, sHit$
       On Error GoTo Error:
       sKeyURL = "Software\Microsoft\Windows\CurrentVersion\URL"
       sDummy = RegGetString(HKEY_LOCAL_MACHINE, sKeyURL & "\DefaultPrefix", "")
       If sDummy <> "http://" Then
           'infected!
           sHit = "O13 - DefaultPrefix: " & sDummy
           If Not IsOnIgnoreList(sHit) Then
               frmMain.lstResults.AddItem sHit
           End If
       End If
   
       sDummy = RegGetString(HKEY_LOCAL_MACHINE, sKeyURL & "\Prefixes", "www")
       If sDummy <> "http://" Then
           'infected!
           sHit = "O13 - WWW Prefix: " & sDummy
           If Not IsOnIgnoreList(sHit) Then
               frmMain.lstResults.AddItem sHit
           End If
       End If
       sDummy = RegGetString(HKEY_LOCAL_MACHINE, sKeyURL & "\Prefixes", "www.")
       If sDummy <> vbNullString Then
           'infected!
           sHit = "O13 - WWW. Prefix: " & sDummy
           If Not IsOnIgnoreList(sHit) Then
               frmMain.lstResults.AddItem sHit
           End If
       End If
   
       sDummy = RegGetString(HKEY_LOCAL_MACHINE, sKeyURL & "\Prefixes", "home")
       If sDummy <> "http://" Then
           'infected!
           sHit = "O13 - Home Prefix: " & sDummy
           If Not IsOnIgnoreList(sHit) Then
               frmMain.lstResults.AddItem sHit
           End If
       End If
   
       sDummy = RegGetString(HKEY_LOCAL_MACHINE, sKeyURL & "\Prefixes", "mosaic")
       If sDummy <> "http://" Then
           'infected!
           sHit = "O13 - Mosaic Prefix: " & sDummy
           If Not IsOnIgnoreList(sHit) Then
               frmMain.lstResults.AddItem sHit
           End If
       End If
   
       sDummy = RegGetString(HKEY_LOCAL_MACHINE, sKeyURL & "\Prefixes", "ftp")
       If sDummy <> "ftp://" Then
           sHit = "O13 - FTP Prefix: " & sDummy
           If Not IsOnIgnoreList(sHit) Then
               frmMain.lstResults.AddItem sHit
           End If
       End If
   
       sDummy = RegGetString(HKEY_LOCAL_MACHINE, sKeyURL & "\Prefixes", "gopher")
       If sDummy <> "gopher://" And sDummy <> vbNullString Then
           sHit = "O13 - Gopher Prefix: " & sDummy
           If Not IsOnIgnoreList(sHit) Then
               frmMain.lstResults.AddItem sHit
           End If
       End If
       Exit Sub
   
   Error:
       ErrorMsg "modMain_CheckOther13Item", Err.Number, Err.Description
   End Sub
   */

  #endregion
}
