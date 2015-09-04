using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Win32;
using ReviveThis.AddIn.AutoRun.WinlogonNotify.Interfaces;
using ReviveThis.Entities.ExtensionMethods;
using ReviveThis.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.AutoRun.WinlogonNotify.Entities
{
  public class WinlogonNotifyResult : IWinlogonNotifyResult, IDetectionItemToolTip
  {

    #region
    private const string INVALID_REG = "Invalid registry found";
    private const string FILE_MISSING = @"(file missing)";
    #endregion

    public ScanResultType ResultType
    {
      get { return ScanResultType.WinlogonNotify; }
    }

    private string _string;

    public string LegacyString
    {
      get
      {
        if (!string.IsNullOrEmpty(_string))
          return _string;

        //"O20 - Winlogon Notify: " & sSubKeys(i) & " - " & sFile
        return _string = string.Format("{0}{1} - Winlogon Notify: {2} - {3}{4}", ResultType.FormatToString(), RegistryInformation.FormatViewToString(), RegistryInformation.Name, RegistryInvalid ? INVALID_REG : FileName, !FileExists ? string.Format(" {0}", string.Empty) : null);

      }
    }

    public RegistryInformation RegistryInformation { get; private set; }
    public string FileName { get; set; }
    public bool FileExists { get; private set; }
    public bool RegistryInvalid { get; private set; }

    private WinlogonNotifyResult(RegistryInformation registryInformation, bool registryInvalid)
    {
      RegistryInformation = registryInformation;

      RegistryInvalid = registryInvalid;
    }

    private WinlogonNotifyResult(RegistryHive hive, RegistryView view, string registryPath, string registryKey, bool registryInvalid)
      : this (new RegistryInformation(hive, view, registryPath, registryKey), registryInvalid)
    {

    }

    public WinlogonNotifyResult(RegistryHive hive, RegistryView view, string registryPath, string registryKey, string fileName, bool fileExists, bool registryInvalid = false)
      : this(hive, view, registryPath, registryKey, registryInvalid)
    {
      FileName = fileName;
      FileExists = fileExists;
    }

    public string ToolTip
    {
      get
      {
        return
          string.Format("{0}\n\nFile : {1}", ResultType.FormatToString(true),
          FileName);
      }
    }

    public bool CanRepair
    {
      get { return false; }
    }

    public bool IsChecked
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