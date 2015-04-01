using ReviveThis.AddIn.InternetExplorer.Prefix.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.InternetExplorer.Prefix.Interfaces
{
  public interface IPrefixResult : IDetectionResultItem
  {
    PrefixResultType PrefixResultType { get; }

    RegistryInformation RegistryInformation { get; }

  }
}