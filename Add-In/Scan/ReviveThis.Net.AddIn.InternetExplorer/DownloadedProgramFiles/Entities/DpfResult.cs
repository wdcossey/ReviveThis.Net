using System;
using System.IO;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.Win32;
using ReviveThis.AddIn.InternetExplorer.DownloadedProgramFiles.Enums;
using ReviveThis.AddIn.InternetExplorer.DownloadedProgramFiles.Interfaces;
using ReviveThis.Entities.ExtensionMethods;
using ReviveThis.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.InternetExplorer.DownloadedProgramFiles.Entities
{
  public class DpfResult : IDpfResult
  {
    #region consts

    #endregion

    public ScanResultType ResultType { get; private set; }

    private string _string;

    public string LegacyString
    {
      get
      {
        if (!string.IsNullOrEmpty(_string))
          return _string;

        switch (DpfResultType)
        {

          case DpfResultType.Default:
          default:
            //"O16 - DPF: " & sName & IIf(sFriendlyName <> vbNullString, " (" & sFriendlyName & ")", "") & " - " & sCodeBase
            return
              _string = string.Format(
                "{0}{1} - DPF: {2} ({3}) - {4}{5}",
                ResultType.FormatToString(),
                RegistryInformation.FormatViewToString(),
                Clsid,
                Title ?? "unknown",
                CodeBase ?? "(no file)",
                !CodeBaseExists ? " (file missing)" : string.Empty);
        }
      }
    }

    public DpfResultType DpfResultType { get; private set; }
    public RegistryInformation RegistryInformation { get; private set; }
    public string Clsid { get; private set; }
    public string Title { get; private set; }
    public string CodeBase { get; private set; }
    public bool CodeBaseExists { get; private set; }
    public string InfFile { get; private set; }
    public string[] FileList { get; private set; }

    public DpfResult(DpfResultType resultType, RegistryHive hive, RegistryView view, string registryPath, string clsid, string title, string codeBase)
    {
      ResultType = ScanResultType.DownloadedProgramFiles;

      DpfResultType = resultType;

      RegistryInformation = new RegistryInformation(hive, view, registryPath);

      Clsid = clsid;
      Title = title;
      CodeBase = codeBase;

      if (Uri.IsWellFormedUriString(codeBase, UriKind.Absolute))
      {
        CodeBaseExists = true;
      }
      else
      {
        CodeBaseExists = !string.IsNullOrEmpty(codeBase) && File.Exists(codeBase);
      }
      

    }

    public bool CanRepair
    {
      get { return false; }
    }

    public async Task<IDetectionRepairResult> Repair()
    {
      //await Task.FromResult(0);
      return null;
    }
  }
}