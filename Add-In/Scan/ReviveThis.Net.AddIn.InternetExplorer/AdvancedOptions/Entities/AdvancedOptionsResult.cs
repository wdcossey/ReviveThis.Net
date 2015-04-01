using System.Threading.Tasks;
using Microsoft.Win32;
using ReviveThis.AddIn.InternetExplorer.AdvancedOptions.Enums;
using ReviveThis.AddIn.InternetExplorer.AdvancedOptions.Interfaces;
using ReviveThis.Entities.ExtensionMethods;
using ReviveThis.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.InternetExplorer.AdvancedOptions.Entities
{
  public class AdvancedOptionsResult : IAdvancedOptionsResult
  {
    public ScanResultType ResultType { get; private set; }

    private string _string;

    public string LegacyString
    {
      get
      {
        if (!string.IsNullOrEmpty(_string))
          return _string;

        switch (AdvancedOptionsResultType)
        {
          //"O11 - Options group: [" & sKey & "] " & sName
          default:
            return _string = string.Format("{0}{1} - Options group: [{2}] {3}", ResultType.FormatToString(), RegistryInformation.FormatViewToString(), System.IO.Path.GetFileName(RegistryInformation.Path), RegistryInformation.Value);
        }
      }
    }

    public AdvancedOptionsResultType AdvancedOptionsResultType { get; private set; }
    public RegistryInformation RegistryInformation { get; private set; }
    //public RegistryHive RegistryHive { get; private set; }
    //public RegistryView RegistryView { get; private set; }
    //public string Path { get; private set; }
    //public string Name { get; private set; }
    //public object Value { get; private set; }

    public AdvancedOptionsResult(AdvancedOptionsResultType resultType, RegistryHive hive, RegistryView view, string path, string name, string value)
    {
      ResultType = ScanResultType.InternetExplorerAdvancedOptions;
      AdvancedOptionsResultType = resultType;
      RegistryInformation = new RegistryInformation(hive, view, path, name, value);
      //RegistryHive = hive;
      //RegistryView = view;
      //Path = path;
      //Name = name;
      //Value = value;
    }

    public AdvancedOptionsResult(RegistryHive hive, RegistryView view, string path, string name, string value)
      : this(AdvancedOptionsResultType.Default, hive, view, path, name, value)
    {
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