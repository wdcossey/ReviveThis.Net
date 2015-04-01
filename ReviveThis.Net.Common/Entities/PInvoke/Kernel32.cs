using System;

namespace ReviveThis.Entities.PInvoke
{
  public static class Kernel32
  {
    [Flags]
    public enum MoveFileFlags
    {
      None = 0,
      ReplaceExisting = 1,
      CopyAllowed = 2,
      DelayUntilReboot = 4,
      WriteThrough = 8,
      CreateHardlink = 16,
      FailIfNotTrackable = 32,
    }

    [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "MoveFileEx")]
    internal static extern bool MoveFileExInternal(string lpExistingFileName, string lpNewFileName, MoveFileFlags dwFlags);

    public static bool MoveFileEx(string fileName, string newFileName, MoveFileFlags flags)
    {
      return MoveFileExInternal(fileName, null, MoveFileFlags.DelayUntilReboot);
    }
  }
}