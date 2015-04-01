using ReviveThis.AddIn.InternetExplorer.UrlSearchHooks.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.InternetExplorer.UrlSearchHooks.Interfaces
{
  public interface IUrlSearchHookResult : IDetectionResultItem
  {
    UrlSearchHookResultType UrlSearchHookResultType { get; }
    RegistryInformation RegistryInformation { get; }
    string Title { get; }
    string FileName { get; }
    bool FileExists { get; }
  }
}