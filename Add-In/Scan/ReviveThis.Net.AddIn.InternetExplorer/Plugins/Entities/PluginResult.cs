using System.IO;
using System.Threading.Tasks;
using Microsoft.Win32;
using ReviveThis.AddIn.InternetExplorer.Plugins.Enums;
using ReviveThis.AddIn.InternetExplorer.Plugins.Interfaces;
using ReviveThis.Entities.ExtensionMethods;
using ReviveThis.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.InternetExplorer.Plugins.Entities
{
  public class PluginResult : IPluginResult
  {
    #region consts
    private const string FILE_MISSING = @"(file missing)";
    #endregion

    public ScanResultType ResultType { get; private set; }

    private string _string;

    public string LegacyString
    {
      get
      {
        if (!string.IsNullOrEmpty(_string))
          return _string;

        switch (PluginResultType)
        {
            //"O12 - Plugin for " & sName & ": " & sFile
          default:
            return
              _string =
                string.Format("{0}{1} - Plugin for {2}: {3}{4}", ResultType.FormatToString(),
                  RegistryInformation.FormatViewToString(), 
                  Path.GetFileName(RegistryInformation.Path), 
                  FileName,
                  !FileExists ? string.Format(" {0}", FILE_MISSING) : null);
        }
      }
    }

    public PluginResultType PluginResultType { get; private set; }
    public RegistryInformation RegistryInformation { get; private set; }
    public string Title { get; private set; }
    public string FileName { get; private set; }
    public bool FileExists { get; private set; }

    public PluginResult(PluginResultType resultType, RegistryHive hive, RegistryView view, string registryPath, string title, string fileName, bool fileExists)
    {
      ResultType = ScanResultType.InternetExplorerPlugin;
      PluginResultType = resultType;
      RegistryInformation = new RegistryInformation(hive, view, registryPath);
      Title = title;
      FileName = fileName;
      FileExists = fileExists;
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