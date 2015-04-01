using ReviveThis.AddIn.ControlPanel.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.ControlPanel.Interfaces
{
  public interface IControlIniResult : IDetectionResultItem
  {
    ControlIniResultType AutoRunResultType { get; }

    RegistryInformation RegistryInformation { get; }
    //RegistryHive? RegistryHive { get; }

    //RegistryView? RegistryView { get; }

    string Path { get; }

    string FileName { get; }

    string Name { get; }

  }
}