using Microsoft.Win32;
using ReviveThis.Structs;

namespace ReviveThis.Interfaces
{
  public interface IRegistryResultItem : IDetectionResultItem
  {
    RegistryInformation RegistryInformation { get; }

    //RegistryHive RegistryHive { get; }

    //RegistryView RegistryView { get; }

    //string KeyName { get; }

    //string ValueName { get; }

    //object Value { get; }

    string FileName { get; }
    string Text { get; }
  }
}