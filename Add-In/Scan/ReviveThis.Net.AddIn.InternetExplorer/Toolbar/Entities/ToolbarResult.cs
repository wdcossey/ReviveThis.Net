using System.Threading.Tasks;
using Microsoft.Win32;
using ReviveThis.AddIn.InternetExplorer.Toolbar.Enums;
using ReviveThis.AddIn.InternetExplorer.Toolbar.Interfaces;
using ReviveThis.Entities.ExtensionMethods;
using ReviveThis.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.InternetExplorer.Toolbar.Entities
{
  public class ToolbarResult : IToolbarResult
  {
    public ScanResultType ResultType { get; private set; }

    private string _string = null;

    public string LegacyString
    {
      get
      {
        if (!string.IsNullOrEmpty(_string))
          return _string;

        switch (BhoResultType)
        {
          //"O3 - Toolbar: " & sName & " - " & sCLSID & " - " & sFile
          default:
            return _string = string.Format("{0}{1} - Toolbar: {2} - {3} - {4}", ResultType.FormatToString(), RegistryInformation.FormatViewToString(), ToolbarName, RegistryInformation.Name, ToolbarFileName);
        }
      }
    }

    public ToolbarResultType BhoResultType { get; private set; }
    public RegistryInformation RegistryInformation { get; private set; }
    //public RegistryHive RegistryHive { get; private set; }
    //public RegistryView RegistryView { get; private set; }
    //public string ClsidKey { get; private set; }
    public string ToolbarFileName { get; private set; }
    public string ToolbarName { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="resultType"></param>
    /// <param name="hive">RegistryHive</param>
    /// <param name="view">RegistryView</param>
    /// <param name="registryPath">Path of the Registry Key</param>
    /// <param name="clsidKey">Object CLSID</param>
    /// <param name="toolbarName"></param>
    /// <param name="toolbarFileName"></param>
    public ToolbarResult(ToolbarResultType resultType, RegistryHive hive, RegistryView view, string registryPath, string clsidKey, string toolbarName, string toolbarFileName)
    {
      ResultType = ScanResultType.InternetExplorerToolbar;
      BhoResultType = resultType;
      RegistryInformation = new RegistryInformation(hive, view, registryPath, clsidKey);
      //RegistryHive = hive;
      //RegistryView = view;
      //ClsidKey = clsidKey;
      ToolbarName = toolbarName;
      ToolbarFileName = toolbarFileName;
    }

    public ToolbarResult(RegistryHive hive, RegistryView view, string registryPath, string clsidKey, string name, string fileName)
      : this(ToolbarResultType.Default, hive, view, registryPath, clsidKey, name, fileName)
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