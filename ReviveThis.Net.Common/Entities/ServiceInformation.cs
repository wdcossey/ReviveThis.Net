using System.IO;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace ReviveThis.Entities
{
  public class ServiceInformation //: ServiceController
  {
    private ServiceController _service = null;

    public bool CanPauseAndContinue
    {
      get { return _service.CanPauseAndContinue; }
    }

    public bool CanShutdown
    {
      get { return _service.CanShutdown; }
    }

    public bool CanStop
    {
      get { return _service.CanStop; }
    }

    //public ServiceController[] DependentServices
    //{
    //  get { return _serviceController.DependentServices; }
    //}

    public string DisplayName
    {
      get { return _service.DisplayName; }
    }

    public string MachineName
    {
      get { return _service.MachineName; }
    }
    public SafeHandle ServiceHandle
    {
      get { return _service.ServiceHandle; }
    }
    public string ServiceName
    {
      get { return _service.ServiceName; }
    }

    //public ServiceController[] ServicesDependedOn
    //{
    //  get { return _service.ServicesDependedOn; }
    //}

    public ServiceType ServiceType
    {
      get { return _service.ServiceType; }
    }

    public ServiceControllerStatus Status
    {
      get { return _service.Status; }
    }

    private string _description = null;
    public string Description
    {
      get
      {
        if (!string.IsNullOrEmpty(_description))
          return _description;

        using (var regKey = Registry.LocalMachine.OpenSubKey(Path.Combine(@"SYSTEM\CurrentControlSet\Services", ServiceName)))
        {
          if (regKey == null)
            return _description = "<none>";

          var value = regKey.GetValue("Description") as string;
          return _description = !string.IsNullOrEmpty(value) && value.StartsWith("@") ? "..." : value;
        }

      }
    }

    private string[] ExtractFileName(string input)
    {
      const string pattern =
        @"^(?<path>\"".*?\"")(?<arguments>.*?)$|^(?<path>\%.*?\.{1}\w+)\s?(?<arguments>.*?)$|^(?<path>\w\:.*?\.{1}\w+)\s?(?<arguments>.*?)$";
      var match = Regex.Match(input, pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);

      return !match.Success ? new[] { input, string.Empty } : new[] { match.Groups["path"].Value, match.Groups["arguments"].Value };

    }

    private string _imagePath = null;
    public string ImagePath
    {
      get
      {
        if (!string.IsNullOrEmpty(_imagePath))
          return _imagePath;

        using (var regKey = Registry.LocalMachine.OpenSubKey(Path.Combine(@"SYSTEM\CurrentControlSet\Services", ServiceName)))
        {
          if (regKey == null)
            return _imagePath = "<none>";

          var value = regKey.GetValue("ImagePath") as string;
          return _imagePath = !string.IsNullOrEmpty(value) && value.StartsWith("@") ? "..." : value;
        }

      }
    }

    public ServiceInformation(ServiceController serviceController)
    {
      _service = serviceController;
    }
  }
}