using System;

namespace ReviveThis.Entities.ExtensionMethods
{
  public static class OperatingSystemExtensionMethods
  {
    public static string FormatToString(this OperatingSystem operatingSystem)
    {
      switch (operatingSystem.Platform)
      {
        case PlatformID.Win32S:
          return "Detected: Windows 3.x running Win32s";

        case PlatformID.Win32Windows:

          if (operatingSystem.Version.Major != 4)
          {
            return string.Format("Unknown Windows (Win9x {0}", operatingSystem.VersionString);
          }

          switch (operatingSystem.Version.Minor)
          {
            case 0: //Windows 95 [A/B/C]
              return string.Format("Windows 95 (Win9x {0})", operatingSystem.VersionString);
            case 10: //Windows 98 [Gold/SE]
              return string.Format("Windows 98 (Win9x {0})", operatingSystem.VersionString);
            case 90: //Windows Millennium Edition
              return string.Format("Windows ME (Win9x {0})", operatingSystem.VersionString);
            default: //WTF?
              return string.Format("Unknown Windows (Win9x {0})", operatingSystem.VersionString);
          }

        case PlatformID.Win32NT:

          switch (operatingSystem.Version.Major)
          {
            case 4: //Windows NT4
              return string.Format("Windows NT 4 {0} (WinNT {1})", operatingSystem.ServicePack, operatingSystem.VersionString);
            case 5:
              switch (operatingSystem.Version.Minor)
              {
                case 0: //Windows 2000
                  return string.Format("Windows 2000 {0} (Windows {1})", operatingSystem.ServicePack, operatingSystem.Version.FormatToString());
                case 1: //Windows XP
                  return string.Format("Windows XP {0} (Windows {1})", operatingSystem.ServicePack, operatingSystem.Version.FormatToString());
                case 2: //Windows 2003
                  return string.Format("Windows 2003 {0} (Windows {1})", operatingSystem.ServicePack, operatingSystem.Version.FormatToString());
                default: //WTF?
                  return string.Format("Unknown Windows (WinNT {0})", operatingSystem.Version.FormatToString());
              }
            case 6:
              switch (operatingSystem.Version.Minor)
              {
                case 0: //Windows Vista
                  return string.Format("Windows Vista {0} (Windows {1})", operatingSystem.ServicePack, operatingSystem.Version.FormatToString());
                case 1: //Windows 7
                  return string.Format("Windows 7 {0} (Windows {1})", operatingSystem.ServicePack, operatingSystem.Version.FormatToString());
                case 2: //Windows 8
                  return string.Format("Windows 8 {0} (Windows {1})", operatingSystem.ServicePack, operatingSystem.Version.FormatToString());
                case 3: //Windows 2008
                  return string.Format("Windows 2008 {0} (Windows {1})", operatingSystem.ServicePack, operatingSystem.Version.FormatToString());
                case 4: //Windows 2012
                  return string.Format("Windows 2012 {0} (Windows {1})", operatingSystem.ServicePack, operatingSystem.Version.FormatToString());
                default:
                  return string.Format("Unknown Windows {0} (Windows {1})", operatingSystem.ServicePack, operatingSystem.Version.FormatToString());
              }
            default: //WTF?
              return string.Format("Unknown Windows (WinNT {0})", operatingSystem.VersionString);
          }
        default:
          return null;
      }
    }
  }
}