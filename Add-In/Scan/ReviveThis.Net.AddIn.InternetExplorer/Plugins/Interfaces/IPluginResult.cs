using ReviveThis.AddIn.InternetExplorer.Plugins.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.InternetExplorer.Plugins.Interfaces
{
  public interface IPluginResult : IDetectionResultItem
  {
    PluginResultType PluginResultType { get; }

    RegistryInformation RegistryInformation { get; }

    string Title { get; }

    string FileName { get; }

    bool FileExists { get; }

  }
}