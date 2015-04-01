using Microsoft.Win32;
using ReviveThis.AddIn.AutoRun.AppInit.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.AutoRun.AppInit.Interfaces
{
  public interface IAppInitResult: IDetectionResultItem
  {
    AppInitResultType AppInitResultType { get; }

    RegistryInformation RegistryInformation { get; }
  
    //RegistryHive RegistryHive { get; }

    //RegistryView RegistryView { get; }

    //string RegistryPath { get; }

    string FileName { get; } 
  }
}