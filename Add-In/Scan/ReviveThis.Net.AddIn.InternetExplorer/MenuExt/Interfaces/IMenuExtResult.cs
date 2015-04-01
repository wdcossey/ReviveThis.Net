using Microsoft.Win32;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.InternetExplorer.MenuExt.Interfaces
{
  public interface IMenuExtResult : IDetectionResultItem
  {
    RegistryInformation RegistryInformation { get; }

    //RegistryHive RegistryHive { get; }

    //RegistryView RegistryView { get; }

    //string RegistryPath { get; }

    //string Name { get; }

    //string Data { get; }
  }
}