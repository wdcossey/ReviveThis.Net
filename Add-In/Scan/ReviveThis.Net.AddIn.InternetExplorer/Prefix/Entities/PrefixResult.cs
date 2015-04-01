using System.IO;
using System.Threading.Tasks;
using Microsoft.Win32;
using ReviveThis.AddIn.InternetExplorer.Plugins.Enums;
using ReviveThis.AddIn.InternetExplorer.Prefix.Enums;
using ReviveThis.AddIn.InternetExplorer.Prefix.Interfaces;
using ReviveThis.Entities.ExtensionMethods;
using ReviveThis.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.InternetExplorer.Prefix.Entities
{
  public class PrefixResult : IPrefixResult
  {
    #region consts
    private const string FILE_MISSING = @"(file missing)";
    #endregion

    public ScanResultType ResultType { get; private set; }

    private string _string;

    public string LegacyString
    {
      get
      {
        if (!string.IsNullOrEmpty(_string))
          return _string;

        switch (PrefixResultType)
        {
            //"O13 - WWW. Prefix: " & sDummy
          case PrefixResultType.Prefix:
            return
              _string =
                string.Format("{0}{1} - {2} Prefix: {3}", ResultType.FormatToString(),
                  RegistryInformation.FormatViewToString(),
                  RegistryInformation.Name.ToUpper(),
                  RegistryInformation.Value);

            //"O13 - DefaultPrefix: " & sDummy
          case PrefixResultType.DefaultPrefix:
          default:
            return
              _string =
                string.Format("{0}{1} - DefaultPrefix: {2}", ResultType.FormatToString(),
                  RegistryInformation.FormatViewToString(),
                  RegistryInformation.Value);
        }
      }
    }

    public PrefixResultType PrefixResultType { get; private set; }
    public RegistryInformation RegistryInformation { get; private set; }

    public PrefixResult(PrefixResultType resultType, RegistryHive hive, RegistryView view, string registryPath, string registryName, object registryValue)
    {
      ResultType = ScanResultType.InternetExplorerPrefix;
      PrefixResultType = resultType;
      RegistryInformation = new RegistryInformation(hive, view, registryPath, registryName, registryValue);
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