using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Win32;
using ReviveThis.AddIn.AutoRun.AppInit.Enums;
using ReviveThis.AddIn.AutoRun.AppInit.Interfaces;
using ReviveThis.Entities.ExtensionMethods;
using ReviveThis.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.AutoRun.AppInit.Entities
{
  public class AppInitResult : IAppInitResult, IDetectionItemToolTip
  {
    public ScanResultType ResultType { get; private set; }

    private string _string;

    public string LegacyString
    {
      get
      {
        if (!string.IsNullOrEmpty(_string))
          return _string;

        switch (AppInitResultType)
        {
          case AppInitResultType.AppInitEnabled:
            return
              _string =
                string.Format("{0}{1} - LoadAppInit_DLLs is enabled.", ResultType.FormatToString(),
                  RegistryInformation.FormatViewToString());

          case AppInitResultType.CodeSigningDisabled:
            return _string = string.Format("{0}{1} - RequireSignedAppInit_DLLs is disabled.", ResultType.FormatToString(), RegistryInformation.FormatViewToString());

          case AppInitResultType.AppInitEntry:
          default:
            //sHit = "O20 - AppInit_DLLs: " & sFile
            return _string = string.Format("{0}{1} - AppInit_DLLs: {2}", ResultType.FormatToString(), RegistryInformation.FormatViewToString(), FileName);
        }
      }
    }

    public ICollection<IDetectionItemContextMenu> ContextMenus { get; private set; }

    public AppInitResultType AppInitResultType { get; private set; }
    public RegistryInformation RegistryInformation { get; private set; }
    //public RegistryHive RegistryHive { get; private set; }
    //public RegistryView RegistryView { get; private set; }
    //public string RegistryPath { get; private set; }
    public string FileName { get; set; }

    private AppInitResult(RegistryHive hive, RegistryView view, string registryPath, string registryName)
    {
      ResultType = ScanResultType.AppInitDlLs;

      RegistryInformation = new RegistryInformation(hive, view, registryPath, registryName);
      //RegistryHive = hive;
      //RegistryView = view;
      //RegistryPath = registryPath;
    }

    private AppInitResult(AppInitResultType resultType, RegistryHive hive, RegistryView view, string registryPath, string registryName, string fileName)
      : this(hive, view, registryPath, registryName)
    {
      AppInitResultType = resultType;
      FileName = fileName;
    }

    public AppInitResult(RegistryHive hive, RegistryView view, string registryPath, string registryName, string fileName)
      : this(AppInitResultType.AppInitEntry, hive, view, registryPath, registryName, fileName)
    {
      //
    }

    public AppInitResult(AppInitResultType resultType, RegistryHive hive, RegistryView view, string registryPath, string registryName)
      : this(resultType, hive, view, registryPath, registryName, null)
    {
      //
    }

/*    
    private IScanItemContextMenu[] _menuItems;

    public ICollection<IScanItemContextMenu> MenuItems
    {
      get
      {
        if (_menuItems != null && _menuItems.Any())
          return _menuItems;

        return _menuItems =
          new IScanItemContextMenu[] { new ScanItemContextMenu { Caption = "StartUp", Action = (sender, args) => MessageBox.Show(this.FileName) }, };
      }
    }
 */
    public string ToolTip
    {
      get
      {
        switch (AppInitResultType)
        {
          case AppInitResultType.AppInitEnabled:
            return
              _string =
                string.Format("{0}\n\nLoadAppInit_DLLs is enabled.", ResultType.FormatToString(true));

          case AppInitResultType.CodeSigningDisabled:
            return
              _string =
                string.Format("{0}\n\nRequireSignedAppInit_DLLs is disabled.", ResultType.FormatToString(true));

          case AppInitResultType.AppInitEntry:
          default:
            return
              string.Format("{0}\n\nFile : {1}", ResultType.FormatToString(true),
                FileName);
        }
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