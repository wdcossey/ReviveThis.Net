using System.Threading.Tasks;
using Microsoft.Win32;
using ReviveThis.AddIn.Registry.Regedit.Interfaces;
using ReviveThis.Entities.ExtensionMethods;
using ReviveThis.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.Registry.Regedit.Entities
{
  public class RegeditResult : IRegeditResult
  {
    public ScanResultType ResultType { get; private set; }

    private string _string;

    public string LegacyString
    {
      get
      {
        if (!string.IsNullOrEmpty(_string))
          return _string;

        //"O7 - HKLM\Software\Microsoft\Windows\CurrentVersion\Policies\System, DisableRegedit=1"
        return
          _string =
            string.Format("{0}{1} - {2}, {3}={4}", ResultType.FormatToString(),
              RegistryInformation.FormatViewToString(), RegistryInformation.Path,
              RegistryInformation.Name, RegistryInformation.Value);
      }
    }

    public RegistryInformation RegistryInformation { get; private set; }

    public RegeditResult()
    {
      ResultType = ScanResultType.RegeditDisabled;
    }

    public RegeditResult(RegistryHive hive, RegistryView view, string path, string name, object value)
      : this()
    {
      RegistryInformation = new RegistryInformation(hive, view, path, name, value);
    }

    public RegeditResult(RegistryInformation regInfo)
      : this()
    {
      RegistryInformation = regInfo;
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