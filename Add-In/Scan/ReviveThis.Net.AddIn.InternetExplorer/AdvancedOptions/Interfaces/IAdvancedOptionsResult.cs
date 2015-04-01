using Microsoft.Win32;
using ReviveThis.AddIn.InternetExplorer.AdvancedOptions.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.InternetExplorer.AdvancedOptions.Interfaces
{
  public interface IAdvancedOptionsResult : IDetectionResultItem
  {
    AdvancedOptionsResultType AdvancedOptionsResultType { get; }

    RegistryInformation RegistryInformation { get; }
    //RegistryHive RegistryHive { get; }

    //RegistryView RegistryView { get; }

    //string Path { get; }

    //string Name { get; }

    //object Value { get; }

  }
}