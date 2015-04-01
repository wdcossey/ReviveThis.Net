using ReviveThis.AddIn.HostsFile.Enums;
using ReviveThis.Interfaces;

namespace ReviveThis.AddIn.HostsFile.Interfaces
{
  public interface IHostsResult : IDetectionResultItem
  {
    HostsFileResultType HostsFileResultType { get; }
    string Line { get; }
  }
}