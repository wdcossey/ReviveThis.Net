using Microsoft.Win32;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.AutoRun.WinlogonNotify.Interfaces
{
  public interface IWinlogonNotifyResult : IDetectionResultItem
  {

    RegistryInformation RegistryInformation { get; }

    string FileName { get; }
    bool FileExists { get; }
    bool RegistryInvalid { get; }
  }
}