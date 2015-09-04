using System.Threading.Tasks;
using Microsoft.Win32;
using ReviveThis.AddIn.InternetExplorer.BrowserHelperObject.Enums;
using ReviveThis.AddIn.InternetExplorer.BrowserHelperObject.Interfaces;
using ReviveThis.Entities.ExtensionMethods;
using ReviveThis.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.InternetExplorer.BrowserHelperObject.Entities
{
  public class BrowserHelperObjectResult : IBrowserHelperObjectResult
  {
    public ScanResultType ResultType { get; private set; }

    private string _string = null;

    public string LegacyString
    {
      get
      {
        if (!string.IsNullOrEmpty(_string))
          return _string;

        switch (BhoResultType)
        {
          //"O2 - BHO: " & sName & " - " & sCLSID & " - " & sFile
          default:
            return _string = string.Format("{0}{1} - BHO: {2} - {3} - {4}", ResultType.FormatToString(), RegistryInformation.FormatViewToString(), Name, ClsidKey, FileName);
        }

      }
    }

    public BrowserHelperObjectResultType BhoResultType { get; private set; }
    public RegistryInformation RegistryInformation { get; private set; }
    //public RegistryHive RegistryHive { get; private set; }
    //public RegistryView RegistryView { get; private set; }
    public string ClsidKey { get; private set; }
    public string FileName { get; private set; }
    public string Name { get; private set; }

    public BrowserHelperObjectResult(BrowserHelperObjectResultType resultType, RegistryHive hive, RegistryView view, string registryPath, string clsidKey, string name, string fileName)
    {
      ResultType = ScanResultType.BrowserHelperObject;
      BhoResultType = resultType;
      RegistryInformation = new RegistryInformation(hive, view, registryPath);
      //RegistryHive = hive;
      //RegistryView = view;
      ClsidKey = clsidKey;
      Name = name;
      FileName = fileName;
    }

    public bool CanRepair
    {
      get { return false; }
    }

    public bool IsChecked
    {
      get { return false; }
    }

    public async Task<IDetectionRepairResult> Repair()
    {
      //await Task.FromResult(0);
      return null;
    }
  }
}