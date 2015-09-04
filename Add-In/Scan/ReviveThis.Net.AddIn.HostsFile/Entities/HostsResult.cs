using System.Threading.Tasks;
using ReviveThis.AddIn.HostsFile.Enums;
using ReviveThis.AddIn.HostsFile.Interfaces;
using ReviveThis.Entities.ExtensionMethods;
using ReviveThis.Enums;
using ReviveThis.Interfaces;

namespace ReviveThis.AddIn.HostsFile.Entities
{
  public class HostsResult : IHostsResult
  {
    public ScanResultType ResultType { get; private set; }

    private string _string = null;

    public string LegacyString
    {
      get
      {
        if (!string.IsNullOrEmpty(_string))
          return _string;

        switch (HostsFileResultType)
        {
          case HostsFileResultType.NotFound:
            return _string = string.Format(
              "{0} - Hosts file could not be found", ResultType.FormatToString());
          case HostsFileResultType.InvalidLocation:
            //"O1 - Hosts file is located at: " & sHostsFile
            return _string = string.Format(
              "{0} - Hosts file is located at: {1}", ResultType.FormatToString(), Line);
          case HostsFileResultType.InvalidFormat:
            //"O1 - Hosts file is located at: " & sHostsFile
            return _string = string.Format(
              "{0} - Hosts file has invalid (UNIX) linebreaks", ResultType.FormatToString());
          default:
            //"O1 - Hosts: " & sLine
            return _string = string.Format(
              "{0} - Hosts: {1}", ResultType.FormatToString(), Line.Replace("\t", " ").Replace("  ", " "));
        }
      }
    }

    public HostsFileResultType HostsFileResultType { get; private set; }
    public string Line { get; private set; }

    public HostsResult(HostsFileResultType resultType, string line)
    {
      ResultType = ScanResultType.HostsFileHiJack;
      HostsFileResultType = resultType;
      Line = line;
    }

    public HostsResult(HostsFileResultType resultType)
      :this(resultType, null)
    {
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