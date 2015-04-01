using Microsoft.Win32;
using ReviveThis.AddIn.InternetExplorer.BrowserHelperObject.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.InternetExplorer.BrowserHelperObject.Interfaces
{
  public interface IBrowserHelperObjectResult : IDetectionResultItem
  {
    BrowserHelperObjectResultType BhoResultType { get; }

    RegistryInformation RegistryInformation { get; }
    //RegistryHive RegistryHive { get; }

    //RegistryView RegistryView { get; }

    string ClsidKey { get; }

    string FileName { get; }

    string Name { get; }

  }
}