using System.IO;
using System.Threading.Tasks;
using Microsoft.Win32;
using ReviveThis.AddIn.InternetExplorer.UrlSearchHooks.Enums;
using ReviveThis.AddIn.InternetExplorer.UrlSearchHooks.Interfaces;
using ReviveThis.Entities.ExtensionMethods;
using ReviveThis.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.InternetExplorer.UrlSearchHooks.Entities
{
  public class UrlSearchHookResult : IUrlSearchHookResult
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

        switch (UrlSearchHookResultType)
        {
          case UrlSearchHookResultType.Missing:
            return
              //"R3 - Default URLSearchHook is missing"
              _string = string.Format(
                "{0}{1} - Default URLSearchHook is missing",
                ResultType.FormatToString(),
                RegistryInformation.FormatViewToString());

          case UrlSearchHookResultType.Unknown:
          default:
            //"R3 - URLSearchHook: " & sName & " - " & sCLSID & " - " & sFile
            return
              _string = string.Format(
                "{0}{1} - URLSearchHook: {2} - {3} - {4} {5}",
                ResultType.FormatToString(),
                RegistryInformation.FormatViewToString(),
                Title,
                RegistryInformation.Name,
                FileName ?? "(no file)",
                !FileExists ? " (file missing)" : string.Empty);
        }
      }
    }

    public UrlSearchHookResultType UrlSearchHookResultType { get; private set; }
    public RegistryInformation RegistryInformation { get; private set; }
    public string Title { get; private set; }
    public string FileName { get; private set; }
    public bool FileExists { get; private set; }

    public UrlSearchHookResult(UrlSearchHookResultType resultType, RegistryHive hive, RegistryView view, string registryPath, string registryName, string title, string fileName, bool fileExists)
    {
      ResultType = ScanResultType.RegistryMultipleValues;

      UrlSearchHookResultType = resultType;

      RegistryInformation = new RegistryInformation(hive, view, registryPath, registryName);

      Title = title;
      FileName = fileName;
      FileExists = fileExists;

    }

    /// <summary>
    /// Missing URL Search Hook
    /// </summary>
    /// <param name="hive"></param>
    /// <param name="view"></param>
    /// <param name="registryPath"></param>
    public UrlSearchHookResult(RegistryHive hive, RegistryView view,
      string registryPath)
      : this(UrlSearchHookResultType.Missing, hive, view, registryPath, null, null, null, false)
    {
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