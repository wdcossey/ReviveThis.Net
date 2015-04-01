using System.Collections.Generic;
using System.Linq;

namespace ReviveThis.Entities.Modules
{
  public static class Services
  {

    public static List<ServiceInformation> ParseServiceList()
    {
      return System.ServiceProcess.ServiceController.GetServices().OrderBy(o => o.DisplayName).Select(item => new ServiceInformation(item)).ToList();
    }
  }
}