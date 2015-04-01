using System.IO;

namespace ReviveThis.Entities.ExtensionMethods
{
  public static class FileAttributesExtesionMethods
  {
    public static string FormatToString(this FileAttributes fileAttributes)
    {
      var result = string.Empty;

      if ((fileAttributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
        result += "R";
      if ((fileAttributes & FileAttributes.Archive) == FileAttributes.Archive)
        result += "A";
      if ((fileAttributes & FileAttributes.Hidden) == FileAttributes.Hidden)
        result += "H";
      if ((fileAttributes & FileAttributes.System) == FileAttributes.System)
        result += "S";
      if ((fileAttributes & FileAttributes.Compressed) == FileAttributes.Compressed)
        result += "C";
      if ((fileAttributes & FileAttributes.NotContentIndexed) == FileAttributes.Compressed)
        result += "N";

      return result;
    }
  }
}