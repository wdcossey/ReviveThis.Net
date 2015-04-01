using Microsoft.Win32;
using ReviveThis.AddIn.InternetExplorer.Toolbar.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.InternetExplorer.Toolbar.Interfaces
{
  public interface IToolbarResult : IDetectionResultItem
  {
    ToolbarResultType BhoResultType { get; }

    RegistryInformation RegistryInformation { get; }

    //RegistryHive RegistryHive { get; }

    //RegistryView RegistryView { get; }

    //string ClsidKey { get; }
    string ToolbarName { get; }

    string ToolbarFileName { get; }


  }
}