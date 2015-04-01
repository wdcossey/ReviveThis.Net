using System;
using System.Runtime.InteropServices;

namespace ReviveThis.Entities.PInvoke
{
  public static class UxTheme
  {
    [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);

    // The constructor:
    public static int SetExplorerTheme(IntPtr hwnd)
    {
      return (Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 6)
        ? SetWindowTheme(hwnd, "Explorer", null)
        : -1;
    }
  }
}