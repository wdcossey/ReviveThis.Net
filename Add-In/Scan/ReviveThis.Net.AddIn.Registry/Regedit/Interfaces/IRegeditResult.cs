using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.Registry.Regedit.Interfaces
{
  public interface IRegeditResult : IDetectionResultItem
  {
    RegistryInformation RegistryInformation { get; }
  }
}