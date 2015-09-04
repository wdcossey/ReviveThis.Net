using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Win32;
using ReviveThis.Entities.ExtensionMethods;
using ReviveThis.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.Entities
{
  public class RegistryResultItem : IRegistryResultItem
  {
    public ScanResultType ResultType { get; private set; }

    private string _string = null;

    public string LegacyString
    {
      get
      {
        if (!string.IsNullOrEmpty(_string))
          return _string;

        switch (ResultType)
        {
          case ScanResultType.RegistryMultipleValues:
            _string = string.Format(
              "{0}{1} - URLSearchHook: {2} - {3} - {4}",
              ResultType.FormatToString(),
              RegistryInformation.FormatViewToString(),
              Text,
              RegistryInformation.Value,
              FileName);
            break;
          default:
            var valueName = RegistryInformation.Name;
            var value = RegistryInformation.Value as string;
            _string = string.Format(
              "{0}{1} - {2},{3}{4}",
              ResultType.FormatToString(),
              RegistryInformation.FormatViewToString(),
              RegistryInformation.Path,
              string.IsNullOrEmpty(valueName) ? "(Default)" : valueName,
              string.Format(" = {0}", value));
            break;
        }


        return _string;
      }
    }

    public ICollection<IDetectionItemContextMenu> ContextMenus { get; private set; }

    //public ListViewItem AsListViewItem { get; private set; }
    //public RegistryHive RegistryHive { get; private set; }
    //public RegistryView RegistryView { get; private set; }
    //public string KeyName { get; private set; }
    //public string ValueName { get; private set; }
    //public object Value { get; private set; }
    public RegistryInformation RegistryInformation { get; private set; }
    public string FileName { get; private set; }
    public string Text { get; private set; }

    public RegistryResultItem(ScanResultType resultType, RegistryHive hive, RegistryView view, string registryPath,
      string valueName = null, string value = null)
    {
      ResultType = resultType;
      RegistryInformation = new RegistryInformation(hive, view, registryPath, valueName, value);
      //RegistryHive = hive;
      //RegistryView = view;
      //KeyName = registryPath;
      //ValueName = valueName;
      //Value = value;
    }

    public RegistryResultItem(ScanResultType resultType, RegistryHive hive, RegistryView view, string registryPath,
      string text, string fileName,
      string valueName = null, string value = null)
      :this(resultType, hive, view, registryPath, valueName, value)
    {
      ResultType = resultType;
      //RegistryHive = hive;
      //RegistryView = view;
      //KeyName = registryPath;
      //ValueName = valueName;
      //Value = value;
      Text = text;
      FileName = fileName;
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