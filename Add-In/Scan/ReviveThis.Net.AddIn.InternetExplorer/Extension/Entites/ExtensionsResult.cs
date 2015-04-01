using System.Threading.Tasks;
using Microsoft.Win32;
using ReviveThis.AddIn.InternetExplorer.Extension.Enums;
using ReviveThis.AddIn.InternetExplorer.Extension.Interfaces;
using ReviveThis.Entities.ExtensionMethods;
using ReviveThis.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.InternetExplorer.Extension.Entites
{
  public class ExtensionsResult : IExtensionsResult
  {
    #region consts
    private const string FILE_MISSING = @" (file missing)";
    private const string NO_NAME = @"(no name)";
    #endregion

    public ScanResultType ResultType { get; private set; }

    private string _string = null;

    public string LegacyString
    {
      get
      {
        if (!string.IsNullOrEmpty(_string))
          return _string;

        switch (ExtensionsResultType)
        {
            case ExtensionsResultType.ToolMenu:
            //"O9 - Extra 'Tools' menuitem: " & sData & " - " & sCLSID & " - " & sFile '& " (HKLM)"
            return _string = string.Format("{0}{1} - Extra 'Tools' menuitem: {2} - {3} - {4}{5}", ResultType.FormatToString(), RegistryInformation.FormatViewToString(), Caption ?? NO_NAME, ClsidKey, FileName, !FileExists ? FILE_MISSING : null);

            case ExtensionsResultType.Button:
            default:
            //"O9 - Extra button: " & sData & " - " & sCLSID & " - " & sFile '& " (HKLM)"
            return _string = string.Format("{0}{1} - Extra button: {2} - {3} - {4}{5}", ResultType.FormatToString(), RegistryInformation.FormatViewToString(), Caption ?? NO_NAME, ClsidKey, FileName, !FileExists ? FILE_MISSING : null);

        }
      }
    }

    public ExtensionsResultType ExtensionsResultType { get; private set; }
    public RegistryInformation RegistryInformation { get; private set; }
    //public RegistryHive RegistryHive { get; private set; }
    //public RegistryView RegistryView { get; private set; }
    //public string RegistryPath { get; private set; }
    public string ClsidKey { get; private set; }
    public string Caption { get; private set; }
    public string FileName { get; private set; }
    public bool FileExists { get; private set; }

    //public ExtensionsResult()
    //{
      
    //}

    public ExtensionsResult(ExtensionsResultType resultType, RegistryHive hive, RegistryView view, string registryPath, string registryName, string clsidKey, string caption, string fileName, bool fileExists)
    {
      ResultType = ScanResultType.InternetExplorerExtension;

      ExtensionsResultType = resultType;

      RegistryInformation = new RegistryInformation(hive, view, registryPath, registryName);
      //RegistryHive = hive;
      //RegistryView = view;
      //RegistryPath = registryPath;

      ClsidKey = clsidKey;

      Caption = caption;
      FileName = fileName;
      FileExists = fileExists;

    }

    public bool CanRepair
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