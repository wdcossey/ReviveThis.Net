using System.Threading.Tasks;
using Microsoft.Win32;
using ReviveThis.AddIn.ControlPanel.Enums;
using ReviveThis.AddIn.ControlPanel.Interfaces;
using ReviveThis.Entities.ExtensionMethods;
using ReviveThis.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.ControlPanel.Entities
{
  public class ControlIniResult : IControlIniResult
  {
    public ScanResultType ResultType { get; private set; }

    private string _string = null;

    public string LegacyString
    {
      get
      {
        if (!string.IsNullOrEmpty(_string))
          return _string;

        switch (AutoRunResultType)
        {
          case ControlIniResultType.Registry:

            return _string = string.Format("{0}{1} - {2}: [{3}] {4}", ResultType.FormatToString(), RegistryInformation.FormatViewToString(), RegistryInformation.Path, RegistryInformation.Name, RegistryInformation.Value);
         
          case ControlIniResultType.File:
            //sHit = "O5 - control.ini: inetcpl.cpl=" & sDummy
            return _string = string.Format("{0} - {1}: {2}={3}", ResultType.FormatToString(), FileName, Name, Path);
         
          default:
            return "";
        }
      }
    }


    public ControlIniResultType AutoRunResultType { get; private set; }
    public RegistryInformation RegistryInformation { get; private set; }
    //public RegistryHive? RegistryHive { get; private set; }
    //public RegistryView? RegistryView { get; private set; }
    public string Path { get; private set; }
    public string FileName { get; set; }
    public string Name { get; private set; }

    public ControlIniResult()
    {
      FileName = null;
      ResultType = ScanResultType.ControlIni;
    }

    public ControlIniResult(RegistryHive hive, RegistryView view, string registryPath, string name, string fileName)
      : this()
    {
      AutoRunResultType = ControlIniResultType.Registry;

      RegistryInformation = new RegistryInformation(hive, view, registryPath, name, fileName);
      //RegistryHive = hive;
      //RegistryView = view;
      //Path = registryPath;
      //Name = name;
      //FileName = fileName;
    }

    public ControlIniResult(string fileName, string name, string path)
      : this()
    {
      AutoRunResultType = ControlIniResultType.File;
      Path = path;
      FileName = fileName;
      Name = name;
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