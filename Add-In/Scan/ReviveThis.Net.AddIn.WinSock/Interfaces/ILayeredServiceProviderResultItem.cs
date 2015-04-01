using ReviveThis.AddIn.WinSock.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.WinSock.Interfaces
{
  public interface ILayeredServiceProviderResultItem : IDetectionResultItem
  {
    RegistryInformation RegistryInformation { get; }

    LayeredServiceProviderType ProviderResultType { get; }

    string FileName { get; }
  }
}