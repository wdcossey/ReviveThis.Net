using System;
using System.Threading.Tasks;
using Microsoft.Win32;
using ReviveThis.AddIn.InternetExplorer.MenuExt.Interfaces;
using ReviveThis.Entities.ExtensionMethods;
using ReviveThis.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.InternetExplorer.MenuExt.Entities
{
  public class MenuExtResult : IMenuExtResult
  {
    public ScanResultType ResultType { get; private set; }

    private string _string = null;

    public string LegacyString
    {
      get
      {
        if (!string.IsNullOrEmpty(_string))
          return _string;

        //"O8 - Extra context menu item: " & sName & " - " & sData
        return
          //_string =
          //  string.Format("{0}{1} - Extra context menu item: {2} - {3}", ResultType.FormatToString(),
          //    RegistryView == RegistryView.Registry64 ? "-x64" : string.Empty, Name, Data);
        
        _string =
            string.Format("{0}{1} - Extra context menu item: {2} - {3}", ResultType.FormatToString(),
              RegistryInformation.FormatViewToString(), RegistryInformation.Name, RegistryInformation.Value);
      }
    }

    public RegistryInformation RegistryInformation { get; private set; }
    //public RegistryHive RegistryHive { get; private set; }
    //public RegistryView RegistryView { get; private set; }
    //public string RegistryPath { get; private set; }
    //public string Name { get; private set; }
    //public string Data { get; private set; }

    public MenuExtResult(RegistryHive hive, RegistryView view, string registryPath, string name, string data)
    {
      ResultType = ScanResultType.InternetExplorerContextMenu;
      RegistryInformation = new RegistryInformation(hive, view, registryPath, name, data);
      //RegistryHive = hive;
      //RegistryView = view;
      //RegistryPath = registryPath;
      //Name = name;
      //Data = data;
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