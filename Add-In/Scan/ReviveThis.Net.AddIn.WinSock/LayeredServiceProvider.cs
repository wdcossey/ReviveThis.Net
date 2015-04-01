using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using ReviveThis.AddIn.WinSock.Entities;
using ReviveThis.AddIn.WinSock.Enums;
using ReviveThis.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.WinSock
{

  #region [10] WinSock Layered Service Provider / CheckOther10Item

  [Export(typeof (IDetectionAddIn))]
  public class LayeredServiceProvider : IDetectionAddIn
  {
    #region private
    #region consts
    private const string REGISTRY_PATH_PARAMETERS = @"System\CurrentControlSet\Services\WinSock2\Parameters";
    private const string CURRENT_NAMESPACE_CATALOG = @"Current_NameSpace_Catalog";
    private const string CURRENT_PROTOCOL_CATALOG = @"Current_Protocol_Catalog";
    private const string NUM_CATALOG_ENTRIES = "Num_Catalog_Entries";
    private const string NUM_CATALOG_ENTRIES64 = "Num_Catalog_Entries64";

    private const string NAMESPACE_LIBRARY_PATH = "LibraryPath";
    private const string PROTOCOL_PACKED_CATALOG_ITEM = "PackedCatalogItem";
    #endregion

    #region Safe Layered Service Providers
    private static string[] _safeLayeredServiceProviders;

    private static IEnumerable<string> SafeLayeredServiceProviders
    {
      get
      {
        if (_safeLayeredServiceProviders != null && _safeLayeredServiceProviders.Any())
          return _safeLayeredServiceProviders;

        return _safeLayeredServiceProviders = new[]
        {
          //new Tuple<string, string, string>(PartialFileName     , Description          , Company),
          //new Tuple<string, string, string>(@"A2antispamlsp.dll", "a-squared Anti-Spam", "Emsi Software GmbH"),
          @"A2antispamlsp.dll",
          @"Adlsp.dll",
          @"Agbfilt.dll",
          @"Antiyfilter.dll",
          @"Ao2lsp.dll",
          @"Aphish.dll",
          @"Asdns.dll",
          @"Aslsp.dll",
          @"Asnsp.dll",
          @"Avgfwafu.dll",
          @"Avsda.dll",
          @"Betsp.dll",
          @"Biolsp.dll",
          @"Bmi_lsp.dll",
          @"Caslsp.dll",
          @"Cavemlsp.dll",
          @"Cdnns.dll",
          @"Connwsp.dll",
          @"Cplsp.dll",
          @"Csesck32.dll",
          @"Cslsp.dll",
          @"Cssp.al",
          @"Ctxlsp.dll",
          @"Ctxnsp.dll",
          @"Cwhook.dll",
          @"Cwlsp.dll",
          @"Dcsws2.dll",
          @"Disksearchservicestub.dll",
          @"Drwebsp.dll",
          @"Drwhook.dll",
          @"Espsock2.dll",
          @"Farlsp.dll",
          @"Fbm.dll",
          @"Fbm_lsp.dll",
          @"Fortilsp.dll",
          @"Fslsp.dll",
          @"Fwcwsp.dll",
          @"Fwtunnellsp.dll",
          @"Gapsp.dll",
          @"Googledesktopnetwork1.dll",
          @"Hclsock5.dll",
          @"Iapplsp.dll",
          @"Iapp_lsp.dll",
          @"Ickgw32i.dll",
          @"Ictload.dll",
          @"Idmmbc.dll",
          @"Iga.dll",
          @"Imon.dll",
          @"Imslsp.dll",
          @"Inetcntrl.dll",
          @"Ippsp.dll",
          @"Ipsp.dll",
          @"Iss_clsp.dll",
          @"Iss_slsp.dll",
          @"Kvwsp.dll",
          @"Kvwspxp.dll",
          @"Lslsimon.dll",
          @"Lsp32.dll",
          @"Lspcs.dll",
          @"Mclsp.dll",
          @"Mdnsnsp.dll",
          @"Msafd.dll",
          @"Msniffer.dll",
          @"Mswsock.dll",
          @"Mswsosp.dll",
          @"Mwtsp.dll",
          @"Mxavlsp.dll",
          @"Napinsp.dll",
          @"Nblsp.dll",
          @"Ndpwsspr.dll",
          @"Netd.dll",
          @"Nihlsp.dll",
          @"Nlaapi.dll",
          @"Nl_lsp.dll",
          @"Nnsp.dll",
          @"Normanpf.dll",
          @"Nutafun4.dll",
          @"Nvappfilter.dll",
          @"Nwws2nds.dll",
          @"Nwws2sap.dll",
          @"Nwws2slp.dll",
          @"Odsp.dll",
          @"Pavlsp.dll",
          @"Pclsp.dll",
          @"Pctlsp.dll",
          @"Pfftsp.dll",
          @"Pgplsp.dll",
          @"Pidlsp.dll",
          @"Pnrpnsp.dll",
          @"Prifw.dll",
          @"Proxy.dll",
          @"Prplsf.dll",
          @"Pxlsp.dll",
          @"Rnr20.dll",
          @"Rsvpsp.dll",
          @"S5spi.dll",
          @"Samnsp.dll",
          @"Sarah.dll",
          @"Scopinet.dll",
          @"Skysocks.dll",
          @"Sliplsp.dll",
          @"Smnsp.dll",
          @"Spacklsp.dll",
          @"Spampallsp.dll",
          @"Spi.dll",
          @"Spidll.dll",
          @"Spishare.dll",
          @"Spsublsp.dll",
          @"Sselsp.dll",
          @"Stplayer.dll",
          @"Syspy.dll",
          @"Tasi.dll",
          @"Tasp.dll",
          @"Tcpspylsp.dll",
          @"Ua_lsp.dll",
          @"Ufilter.dll",
          @"Vblsp.dll",
          @"Vetredir.dll",
          @"Vlsp.dll",
          @"Vnsp.dll",
          @"Wglsp.dll",
          @"Whllsp.dll",
          @"Whlnsp.dll",
          @"Winrnr.dll",
          @"Wins4f.dll",
          @"Winsflt.dll",
          @"WinSysAM.dll",
          @"Wps.dll",
          @"Wshbth.dll",
          @"Wspirda.dll",
          @"Wspwsp.dll",
          @"Xfilter.dll",
          @"xfire_lsp.dll",
          @"Xnetlsp.dll",
          @"Ypclsp.dll",
          @"Zklspr.dll",
          @"_Easywall.dll",
          @"_Handywall.dll",
          @"vsocklib.dll" //VSockets Library (VMWare, Inc.)
        };
      }
    }
    #endregion

    private IEnumerable<IDetectionResultItem> CheckCommon(RegistryHive regHive, RegistryKey regKey, IEnumerable<string> subKeyNames, string catalogName = CURRENT_NAMESPACE_CATALOG, string fileNameKey = NAMESPACE_LIBRARY_PATH)
    {
      var results = new Collection<IDetectionResultItem>();

      if (regKey == null)
        return results;

      var nameSpaceCatalog = regKey.GetValue(catalogName, null) as string;

      if (string.IsNullOrEmpty(nameSpaceCatalog))
      {
        results.Add(new LayeredServiceProviderResult(LayeredServiceProviderType.EmptyEntry, new RegistryInformation
        {
          Hive = regHive,
          View = regKey.View,
          Path = regKey.Name,
          Name = CURRENT_NAMESPACE_CATALOG,
          Value = null
        }));
      }
      else
      {
        var lspDetected =
          subKeyNames.FirstOrDefault(f => f.Equals(nameSpaceCatalog, StringComparison.InvariantCultureIgnoreCase)) !=
          null;

        if (!lspDetected)
        {
          results.Add(new LayeredServiceProviderResult(LayeredServiceProviderType.RegistryKeyNotFound, new RegistryInformation
          {
            Hive = regHive,
            View = regKey.View,
            Path = Path.Combine(regKey.Name, nameSpaceCatalog),
            Name = null,
            Value = null
          }));
        }
        else
        {
          using (var subKey = regKey.OpenSubKey(nameSpaceCatalog, false))
          {
            if (subKey != null)
            {
              var numEntries = subKey.GetValue(NUM_CATALOG_ENTRIES, null) as Int32?;
              if (numEntries.HasValue && numEntries.Value > 0)
              {
                using (var catalogKey = subKey.OpenSubKey("Catalog_Entries", false))
                {
                  if (catalogKey != null)
                  {
                    var entries = catalogKey.GetSubKeyNames();

                    //check LSP chain gaps
                    for (var i = 1; i < numEntries + 1; i++)
                    {
                      if (!entries.Contains(string.Format("{0:D12}", i)))
                      {
                        results.Add(new LayeredServiceProviderResult(LayeredServiceProviderType.MissingChainGap, new RegistryInformation
                        {
                          Hive = regHive,
                          View = catalogKey.View,
                          Path = catalogKey.Name,
                          Name = i.ToString(CultureInfo.InvariantCulture),
                          Value = numEntries.Value
                        }));
                      }
                    }

                    foreach (var entry in entries)
                    {
                      using (var entryKey = catalogKey.OpenSubKey(entry, false))
                      {
                        if (entryKey != null)
                        {
                          var libPath = string.Empty;

                          var kind = entryKey.GetValueKind(fileNameKey);

                          switch (kind)
                          {
                            case RegistryValueKind.Binary:
                              var libPathArray = entryKey.GetValue("PackedCatalogItem", null) as byte[];
                              if (libPathArray != null && libPathArray.Any())
                              {
                                libPath = Encoding.UTF8.GetString(libPathArray, 0,
                                  Math.Min(Array.IndexOf<byte>(libPathArray, 0), libPathArray.Length));
                              }
                              break;
                            case RegistryValueKind.String:
                              libPath = entryKey.GetValue(fileNameKey, null) as string;
                              break;
                            default:
                              libPath = null;
                              break;
                          }

                          if (!string.IsNullOrEmpty(libPath))
                          {
                            libPath = Environment.ExpandEnvironmentVariables(libPath);
                            var libFile = Path.GetFileName(libPath);

                            if (!File.Exists(libPath))
                            {
                              results.Add(new LayeredServiceProviderResult(LayeredServiceProviderType.MissingProvider, new RegistryInformation
                              {
                                Hive = regHive,
                                View = entryKey.View,
                                Path = entryKey.Name,
                                Name = fileNameKey,
                                Value = libPath,
                              }, libPath));
                            }
                            else
                            {
                              //var fileName = Path.GetFileName(libPath);
                              //System.Diagnostics.Debug.WriteLine(libPath);

                              if (libPath.IndexOf("webhdll.dll", StringComparison.InvariantCultureIgnoreCase) > -1)
                              {
                                results.Add(new LayeredServiceProviderResult(LayeredServiceProviderType.HijackedWebHancer, new RegistryInformation
                                {
                                  Hive = regHive,
                                  View = entryKey.View,
                                  Path = entryKey.Name,
                                  Name = fileNameKey,
                                  Value = libPath,
                                }, libPath));
                              }
                              else if (libPath.IndexOf("newdot", StringComparison.InvariantCultureIgnoreCase) > -1)
                              {
                                results.Add(new LayeredServiceProviderResult(LayeredServiceProviderType.HijackedNewDotNet, new RegistryInformation
                                {
                                  Hive = regHive,
                                  View = entryKey.View,
                                  Path = entryKey.Name,
                                  Name = fileNameKey,
                                  Value = libPath,
                                }, libPath));
                              }
                              else if (libPath.IndexOf("cnmib.dll", StringComparison.InvariantCultureIgnoreCase) >
                                       -1)
                              {
                                results.Add(new LayeredServiceProviderResult(LayeredServiceProviderType.HijackedCommonName, new RegistryInformation
                                {
                                  Hive = regHive,
                                  View = entryKey.View,
                                  Path = entryKey.Name,
                                  Name = fileNameKey,
                                  Value = libPath,
                                }, libPath));
                              }
                              else if (
                                SafeLayeredServiceProviders.All(
                                  a => a.IndexOf(libFile, StringComparison.InvariantCultureIgnoreCase) == -1))
                              {
                                results.Add(new LayeredServiceProviderResult(LayeredServiceProviderType.UnknownFile, new RegistryInformation
                                {
                                  Hive = regHive,
                                  View = entryKey.View,
                                  Path = entryKey.Name,
                                  Name = fileNameKey,
                                  Value = libPath,
                                }, libPath));
                              }
                            }
                          }
                          else
                          {
                            results.Add(new LayeredServiceProviderResult(LayeredServiceProviderType.MissingLibrary, new RegistryInformation
                            {
                              Hive = regHive,
                              View = entryKey.View,
                              Path = entryKey.Name,
                              Name = fileNameKey,
                              Value = null,
                            }));
                          }
                        }
                      }
                    }
                  }
                }
              }
              else
              {
                results.Add(new LayeredServiceProviderResult(LayeredServiceProviderType.NoRegistryEntries, new RegistryInformation
                {
                  Hive = regHive,
                  View = subKey.View,
                  Path = subKey.Name,
                  Name = NUM_CATALOG_ENTRIES,
                  Value = null
                }));
              }
            }
            else
            {
              results.Add(new LayeredServiceProviderResult(LayeredServiceProviderType.RegistryAccessError, new RegistryInformation
              {
                Hive = regHive,
                View = regKey.View,
                Path = Path.Combine(regKey.Name, nameSpaceCatalog),
                Name = null,
                Value = null
              }));
            }
          }
        }
      }

      return results;
    }

    private IEnumerable<IDetectionResultItem> CheckNameSpaceCatalogs(RegistryHive regHive, RegistryKey regKey, IEnumerable<string> subKeyNames)
    {
      return CheckCommon(regHive, regKey, subKeyNames);
    }

    private IEnumerable<IDetectionResultItem> CheckProtocolCatalogs(RegistryHive regHive, RegistryKey regKey, IEnumerable<string> subKeyNames)
    {
      return CheckCommon(regHive, regKey, subKeyNames, CURRENT_PROTOCOL_CATALOG, PROTOCOL_PACKED_CATALOG_ITEM);
    }

    #endregion

    public string Author
    {
      get { return "William David Cossey"; }
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
      get { return @"Layered Service Provider"; }
    }

    public string[] Description
    {
      get
      {
        return new[]
        {
          "Scans for broken WinSock \"Layered Service Provider(s)\" (also known as LSP's)."
        };
      }
    }

    public ScanResultType ResultType
    {
      get { return ScanResultType.WinSockLayeredServiceProvider; }
    }

    public void Dispose()
    {
      //Nothing to dispose?
    }

    public async Task<ICollection<IDetectionResultItem>> Scan()
    {
      //await Task.FromResult(0);

      var result = new List<IDetectionResultItem>();

      const RegistryHive regHive = RegistryHive.LocalMachine;

      using (var rootKey = RegistryKey.OpenBaseKey(regHive, RegistryView.Default))
      {
        using (var regKey = rootKey.OpenSubKey(REGISTRY_PATH_PARAMETERS, false))
        {
          if (regKey == null) 
            return result;

          var subKeyNames = regKey.GetSubKeyNames().ToList();

          result.AddRange(CheckNameSpaceCatalogs(regHive, regKey, subKeyNames));

          result.AddRange(CheckProtocolCatalogs(regHive, regKey, subKeyNames));
        }
      }
      return result;
    }
  }

  #endregion

  #region Original Visual Basic (6.0) Code Block

  /*
   Public Sub GetLSPCatalogNames()
     sKeyNameSpace = "System\CurrentControlSet\Services\WinSock2\Parameters"
     sKeyProtocol = "System\CurrentControlSet\Services\WinSock2\Parameters"
     
     sKeyNameSpace = sKeyNameSpace & "\" & RegGetString(HKEY_LOCAL_MACHINE, sKeyNameSpace, "Current_NameSpace_Catalog")
     sKeyProtocol = sKeyProtocol & "\" & RegGetString(HKEY_LOCAL_MACHINE, sKeyProtocol, "Current_Protocol_Catalog")
   End Sub
   */

  #region Scan
  /* 
   Public Sub CheckLSP()
       Dim lNumNameSpace&, lNumProtocol&, i&, j& ', sSafeFiles$
       Dim sFile$, uData() As Byte, hKey&, sHit$, sDummy$
       On Error GoTo Error:
       lNumNameSpace = RegGetDword(HKEY_LOCAL_MACHINE, sKeyNameSpace, "Num_Catalog_Entries")
       lNumProtocol = RegGetDword(HKEY_LOCAL_MACHINE, sKeyProtocol, "Num_Catalog_Entries")
         
       'check for gaps in LSP chain
       For i = 1 To lNumNameSpace
           If RegKeyExists(HKEY_LOCAL_MACHINE, sKeyNameSpace & "\Catalog_Entries\" & String(12 - Len(CStr(i)), "0") & CStr(i)) Then
               'all fine & peachy
           Else
               'broken LSP detected!
               frmMain.lstResults.AddItem "O10 - Broken Internet access because of LSP chain gap (#" & CStr(i) & " in chain of " & CStr(lNumNameSpace) & " missing)"
               Exit Sub
           End If
       Next i
       For i = 1 To lNumProtocol
           If RegKeyExists(HKEY_LOCAL_MACHINE, sKeyProtocol & "\Catalog_Entries\" & String(12 - Len(CStr(i)), "0") & CStr(i)) Then
               'all fine & dandy
           Else
               'shit, not again!
               frmMain.lstResults.AddItem "O10 - Broken Internet access because of LSP chain gap (#" & CStr(i) & " in chain of " & CStr(lNumProtocol) & " missing)"
               Exit Sub
           End If
       Next i
     
       'check all LSP providers are present
       For i = 1 To lNumNameSpace
           sFile = RegGetString(HKEY_LOCAL_MACHINE, sKeyNameSpace & "\Catalog_Entries\" & String(12 - Len(CStr(i)), "0") & CStr(i), "LibraryPath")
           sFile = LCase(Replace(sFile, "%SYSTEMROOT%", sWinDir, , , vbTextCompare))
           sFile = LCase(Replace(sFile, "%windir%", sWinDir, , , vbTextCompare))
           If sFile <> vbNullString Then
               If FileExists(sFile) Or _
                  FileExists(sWinDir & "\" & sFile) Or _
                  FileExists(sWinSysDir & "\" & sFile) Then
                   'file ok
                   If InStr(sFile, "webhdll.dll") > 0 Then
                       sHit = "O10 - Hijacked Internet access by WebHancer"
                       If Not IsOnIgnoreList(sHit) Then frmMain.lstResults.AddItem sHit
                   ElseIf InStr(sFile, "newdot") > 0 Then
                       sHit = "O10 - Hijacked Internet access by New.Net"
                       If Not IsOnIgnoreList(sHit) Then frmMain.lstResults.AddItem sHit
                   ElseIf InStr(sFile, "cnmib.dll") > 0 Then
                       sHit = "O10 - Hijacked Internet access by CommonName"
                       If Not IsOnIgnoreList(sHit) Then frmMain.lstResults.AddItem sHit
                   Else
                       sDummy = Mid(sFile, InStrRev(sFile, "\") + 1)
                       If InStr(1, sSafeLSPFiles, sDummy, vbTextCompare) = 0 Or bIgnoreAllWhitelists Then
                           sHit = "O10 - Unknown file in Winsock LSP: " & sFile
                           If Not IsOnIgnoreList(sHit) Then frmMain.lstResults.AddItem sHit
                       End If
                   End If
               Else
                   'damn, file is gone
                   If InStr(1, sSafeLSPFiles, sFile, vbTextCompare) = 0 Or bIgnoreAllWhitelists Then
                       frmMain.lstResults.AddItem "O10 - Broken Internet access because of LSP provider '" & sFile & "' missing"
                   End If
                   Exit Sub
               End If
           End If
       Next i
     
       For i = 1 To lNumProtocol
           sFile = RegGetFileFromBinary(HKEY_LOCAL_MACHINE, sKeyProtocol & "\Catalog_Entries\" & String(12 - Len(CStr(i)), "0") & CStr(i), "PackedCatalogItem")
           sFile = LCase(Replace(sFile, "%SYSTEMROOT%", sWinDir, , , vbTextCompare))
           sFile = LCase(Replace(sFile, "%windir%", sWinDir, , , vbTextCompare))
           If sFile <> vbNullString Then
               If FileExists(sFile) Or _
                  FileExists(sWinDir & "\" & sFile) Or _
                  FileExists(sWinSysDir & "\" & sFile) Then
                   'file ok
                   If InStr(1, sFile, "webhdll.dll", vbTextCompare) > 0 Then
                       sHit = "O10 - Hijacked Internet access by WebHancer"
                       If Not IsOnIgnoreList(sHit) Then frmMain.lstResults.AddItem sHit
                   ElseIf InStr(1, sFile, "newdot", vbTextCompare) > 0 Then
                       sHit = "O10 - Hijacked Internet access by New.Net"
                       If Not IsOnIgnoreList(sHit) Then frmMain.lstResults.AddItem sHit
                   ElseIf InStr(1, sFile, "cnmib.dll", vbTextCompare) > 0 Then
                       sHit = "O10 - Hijacked Internet access by CommonName"
                       If Not IsOnIgnoreList(sHit) Then frmMain.lstResults.AddItem sHit
                   Else
                       sDummy = LCase(Mid(sFile, InStrRev(sFile, "\") + 1))
                       If InStr(1, sSafeLSPFiles, sDummy, vbTextCompare) = 0 Or bIgnoreAllWhitelists Then
                           sHit = "O10 - Unknown file in Winsock LSP: " & sFile
                           If Not IsOnIgnoreList(sHit) Then frmMain.lstResults.AddItem sHit
                       End If
                   End If
               Else
                   'damn - crossed again!
                   If InStr(1, sSafeLSPFiles, sFile, vbTextCompare) = 0 Or bIgnoreAllWhitelists Then
                       frmMain.lstResults.AddItem "O10 - Broken Internet access because of LSP provider '" & sFile & "' missing"
                   End If
                   Exit Sub
               End If
           End If
       Next i
       Exit Sub
     
   Error:
       RegCloseKey hKey
       ErrorMsg "modLSP_CheckLSP", Err.Number, Err.Description
   End Sub
   */
  #endregion

  #region Repair

  #endregion

  #region Backup

  #endregion

  #endregion
}
