using System.Threading.Tasks;
using ReviveThis.AddIn.WinSock.Enums;
using ReviveThis.AddIn.WinSock.Interfaces;
using ReviveThis.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.WinSock.Entities
{
  public class LayeredServiceProviderResult : ILayeredServiceProviderResultItem
  {
    public ScanResultType ResultType { get; private set; }

    private string _legacyString;
    public string LegacyString
    {
      get
      {
        if (!string.IsNullOrEmpty(_legacyString))
          return _legacyString;

        return _legacyString = this.FormatToString();
      }
    }

    public RegistryInformation RegistryInformation { get; private set; }
    public LayeredServiceProviderType ProviderResultType { get; private set; }
    public string FileName { get; private set; }

    public LayeredServiceProviderResult(LayeredServiceProviderType type, RegistryInformation regInfo, string fileName = null)
    {
      ResultType = ScanResultType.WinSockLayeredServiceProvider;
      ProviderResultType = type;
      RegistryInformation = regInfo;
      FileName = fileName;
    }

    public bool CanRepair
    {
      get { return false; }
    }

    public bool IsChecked
    {
      get { return true; }
    }

    public async Task<IDetectionRepairResult> Repair()
    {
      //await Task.FromResult(0);
      return null;
    }
  }
}