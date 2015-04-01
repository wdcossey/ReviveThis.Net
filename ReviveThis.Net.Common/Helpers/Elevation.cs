using System.Security.Principal;

namespace ReviveThis.Helpers
{
  public static class Elevation
  {
    public static bool IsElevated
    {
      get
      {
        return new WindowsPrincipal
          (WindowsIdentity.GetCurrent()).IsInRole
          (WindowsBuiltInRole.Administrator);
      }
    }
  }
}