using System.Diagnostics;

namespace ReviveThis.Entities.ExtensionMethods
{
  public static class FileVersionInfoExtensionMethods
  {
    public static string FormatToString(this FileVersionInfo fileVersionInfo)
    {
      return fileVersionInfo == null ? null : string.Format("{0}.{1}.{2}.{3}", fileVersionInfo.FileMajorPart, fileVersionInfo.FileMinorPart, fileVersionInfo.FileBuildPart, fileVersionInfo.FilePrivatePart );
    }
  }
}