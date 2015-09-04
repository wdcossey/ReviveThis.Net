using System;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;
using ReviveThis.AddIn.UefiBinary.LenovoServiceEngine.Enums;
using ReviveThis.Entities.ExtensionMethods;
using ReviveThis.Enums;
using ReviveThis.Interfaces;
using ReviveThis.Structs;

namespace ReviveThis.AddIn.UefiBinary.LenovoServiceEngine.Entities
{
  public class LenovoServiceEngineResult : IDetectionResultItem, ICustomAddInSection, IDetectionItemToolTip
  {
    private readonly LenovoServiceEngineTypes _type;
    private readonly string _text;
    private string _fileOrDirectory;
    private const string SECTION_ID = "LSE";
    private const string SECTION_DESCRIPTION = "Lenovo Service Engine";

    public LenovoServiceEngineResult(LenovoServiceEngineTypes type, string fileOrDirectory, string text)
    {
      _type = type;
      _fileOrDirectory = fileOrDirectory;
      _text = text;
    }

    public LenovoServiceEngineTypes Type
    {
      get { return _type; }
    }

    public string FileOrDirectory
    {
      get { return _fileOrDirectory; }
    }

    public ScanResultType ResultType
    {
      get { return ScanResultType.CustomAddIn; }
    }

    public string LegacyString
    {
      get { return string.Format("{0} - {1}.", SECTION_ID, _text); }
    }

    public CustomAddInSection CustomAddInSection
    {
      get { return new CustomAddInSection(SECTION_ID, SECTION_DESCRIPTION); }
    }

    public bool CanRepair
    {
      get { return true; }
    }

    public bool IsChecked
    {
      get { return true; }
    }

    public async Task<IDetectionRepairResult> Repair()
    {
      try
      {
        switch (Type)
        {
          case LenovoServiceEngineTypes.Binary:
          case LenovoServiceEngineTypes.Log:
          case LenovoServiceEngineTypes.Directory:

            if (Type == LenovoServiceEngineTypes.Directory && Directory.Exists(FileOrDirectory))
            {
              Directory.Delete(FileOrDirectory, true);
            }
            else if (File.Exists(FileOrDirectory))
            {
              File.Delete(FileOrDirectory);
            }

            return new LenovoServiceEngineRepairResult(RepairResultType.Successful,
              string.Format("\"{0}\"\n\n was deleted.", FileOrDirectory));

          case LenovoServiceEngineTypes.Service:
            var serviceList = ServiceController.GetServices().Where(w => w.ServiceName == FileOrDirectory);
            foreach (var service in serviceList)
            {


              if (service.Status != ServiceControllerStatus.Stopped)
              {
                if (!service.CanStop)
                  throw new Exception(
                    string.Format("Unable to stop the Service: \"{0}\"\n\nService does not allow this.",
                      service.DisplayName ?? service.ServiceName));

                service.Stop();

                service.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 10));
              }

              using (var serviceInstaller = new ServiceInstaller())
              {
                var installContext =
                  new InstallContext(Path.Combine(Path.GetTempPath(), "ReviveThis_LSEService_Removal.log"), null);
                serviceInstaller.Context = installContext;
                serviceInstaller.ServiceName = service.ServiceName;
                serviceInstaller.Uninstall(null);
              }

              return new LenovoServiceEngineRepairResult(RepairResultType.Successful,
                string.Format("\"{0}\"\n\n was stopped and removed.", service.DisplayName ?? service.ServiceName));
            }
            break;
        }

        throw new NotImplementedException();

      }
      catch (Exception ex)
      {
        return new LenovoServiceEngineRepairResult(RepairResultType.Failed,
          string.Format("Error: \"{0}\".", ex.Message));
      }
    }

    public string ToolTip
    {
      get
      {

        switch (Type)
        {
          case LenovoServiceEngineTypes.Service:
            return string.Format("{0} - {1}\n\nSystem Service: {2}", SECTION_ID, SECTION_DESCRIPTION, _text);

          default:
          case LenovoServiceEngineTypes.Binary:
          case LenovoServiceEngineTypes.Log:
          case LenovoServiceEngineTypes.Directory:
            return string.Format("{0} - {1}\n\nPath: {2}", SECTION_ID, SECTION_DESCRIPTION, FileOrDirectory);
        }
      }
    }

    //return
      //string.Format("{0}\n\nPath\t: {1}\nName\t: {2}\nValue\t: {3}", ResultType.FormatToString(true),
      //      RegistryInfo.Path, RegistryInfo.Name, RegistryInfo.Value);
    }


}