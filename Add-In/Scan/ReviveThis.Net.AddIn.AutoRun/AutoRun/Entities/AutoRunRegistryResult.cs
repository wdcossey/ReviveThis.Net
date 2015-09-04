using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Win32;
using ReviveThis.AddIn.AutoRun.AutoRun.Interfaces;
using ReviveThis.Entities.ExtensionMethods;
using ReviveThis.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.AutoRun.AutoRun.Entities
{
  public class AutoRunRegistryResult : IAutoRunRegistryResult, IDetectionItemToolTip
  {
    public ScanResultType ResultType { get; private set; }

    private string _string;

    public string LegacyString
    {
      get
      {
        if (!string.IsNullOrEmpty(_string))
          return _string;

        //sHit = sHit & "[" & sName & "] " & sData
        return
          _string =
            string.Format("{0}{1} - {2}: [{3}] {4}", ResultType.FormatToString(),
              RegistryInfo.FormatViewToString(), RegistryInfo.Path, RegistryInfo.Name, RegistryInfo.Value);
      }
    }

    private IDetectionItemContextMenu[] _menuItems;

    public ICollection<IDetectionItemContextMenu> MenuItems
    {
      get
      {
        if (_menuItems != null && _menuItems.Any())
          return _menuItems;

        return null;
        //return _menuItems =
        //  new IScanItemContextMenu[] { new ScanItemContextMenu { Text = "Autorun", OnClick = (sender, args) => MessageBox.Show(this.FileName) }, };
      }
    }

    //private string _fileName;
    //public string FileName
    //{
    //  get { return _fileName; }
    //  set
    //  {
    //    _fileName = value;
    //    //RealFileName = _fileName.ExtractFileName();
    //  }
    //}

    public RegistryInformation RegistryInfo { get; private set; }
    public string RealFileName { get; private set; }
    public string Parameters { get; private set; }

    public AutoRunRegistryResult()
    {
      ResultType = ScanResultType.AutoRun;
    }

    public AutoRunRegistryResult(RegistryHive hive, RegistryView view, string path, string name, string fileName = null)
      : this()
    {
      RegistryInfo = new RegistryInformation(hive, view, path, name, fileName);
      //FileName = fileName;
    }

    //public AutoRunRegistryResult(RegistryInformation registryInfo, string fileName = null)
    //  : this()
    //{
    //  RegistryInfo = registryInfo;
    //  //FileName = fileName;
    //}


    public string ToolTip
    {
      get
      {
        return
          string.Format("{0}\n\nPath\t: {1}\nName\t: {2}\nValue\t: {3}", ResultType.FormatToString(true),
          RegistryInfo.Path, RegistryInfo.Name, RegistryInfo.Value);
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