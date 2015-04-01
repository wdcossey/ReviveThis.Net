//using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReviveThis.Entities.ExtensionMethods;
using ReviveThis.Enums;
using ReviveThis.Interfaces;

namespace ReviveThis.Entities
{
  public class IniResultItem : IIniResultItem
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
            //case 
          default:
            //var sHit = "F1 - " & CStr(vRule(0)) & ": " & CStr(vRule(2)) & "=" & sValue
            _string = string.Format(
              "{0} - {1}: {2}={3}",
              ScanResultTypeExtensionMethods.FormatToString(ResultType),
              FileName,
              ValueName,
              Value);
            break;
        }

        return _string;
      }
    }

    public ICollection<IDetectionItemContextMenu> ContextMenus { get; private set; }

    //public ListViewItem AsListViewItem { get; private set; }
    public string FileName { get; private set; }
    public string SecionName { get; private set; }
    public string ValueName { get; private set; }
    public object Value { get; private set; }
    public object DefaultValue { get; private set; }

    //public IniResultItem(ScanResultType resultType, RegistryHive hive, RegistryView view, string keyName,
    //  string valueName = null, string value = null)
    //{
    //  ResultType = resultType;
    //  RegistryHive = hive;
    //  RegistryView = view;
    //  KeyName = keyName;
    //  ValueName = valueName;
    //  Value = value;
    //}

    public IniResultItem(ScanResultType resultType, string fileName, string sectionName,
      string valueName = null, string value = null, string defaultValue = null)
    {
      ResultType = resultType;
      SecionName = sectionName;
      ValueName = valueName;
      Value = value;
      DefaultValue = defaultValue;
      FileName = fileName;
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