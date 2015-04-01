using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.AutoRun.AutoRun.Interfaces
{
  public interface IAutoRunRegistryResult : IDetectionResultItem, IAutoRunResult
  {
    RegistryInformation RegistryInfo { get; }

    string RealFileName { get; }

    string Parameters { get; }

  }
}