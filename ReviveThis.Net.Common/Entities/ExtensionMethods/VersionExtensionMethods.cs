using System;

namespace ReviveThis.Entities.ExtensionMethods
{
  public static class VersionExtensionMethods
  {
    public static string FormatToString(this Version version, bool includeRevision = false)
    {
      return version == null ? null : string.Format("{0}.{1}.{2}{3}", version.Major, version.Minor, version.Build, includeRevision ? string.Format(".{0}", version.Revision) : string.Empty);
    }
  }
}