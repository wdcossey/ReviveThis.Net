using Microsoft.Win32;
using ReviveThis.AddIn.InternetExplorer.Extension.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.InternetExplorer.Extension.Interfaces
{
  public interface IExtensionsResult : IDetectionResultItem
  {
    ExtensionsResultType ExtensionsResultType { get; }

    RegistryInformation RegistryInformation { get; }

    //RegistryHive RegistryHive { get; }

    //RegistryView RegistryView { get; }

    //string RegistryPath { get; }

    string ClsidKey { get; }

    string Caption { get; }

    string FileName { get; }

    bool FileExists { get; }
  }
}