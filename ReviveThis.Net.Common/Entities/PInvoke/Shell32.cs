using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ReviveThis.Entities.PInvoke
{
  public static class Shell32
  {
    [DllImport("shell32.dll", CharSet = CharSet.Auto)]
    private static extern bool ShellExecuteEx(ref SHELLEXECUTEINFO lpExecInfo);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct SHELLEXECUTEINFO
    {
      public int cbSize;
      public uint fMask;
      public IntPtr hwnd;
      [MarshalAs(UnmanagedType.LPTStr)]
      public string lpVerb;
      [MarshalAs(UnmanagedType.LPTStr)]
      public string lpFile;
      [MarshalAs(UnmanagedType.LPTStr)]
      public string lpParameters;
      [MarshalAs(UnmanagedType.LPTStr)]
      public string lpDirectory;
      public ShowCommands nShow;
      public IntPtr hInstApp;
      public IntPtr lpIDList;
      [MarshalAs(UnmanagedType.LPTStr)]
      public string lpClass;
      public IntPtr hkeyClass;
      public uint dwHotKey;
      public IntPtr hIcon;
      public IntPtr hProcess;
    }

    public enum ShowCommands : int
    {
      SW_HIDE = 0,
      SW_SHOWNORMAL = 1,
      SW_NORMAL = 1,
      SW_SHOWMINIMIZED = 2,
      SW_SHOWMAXIMIZED = 3,
      SW_MAXIMIZE = 3,
      SW_SHOWNOACTIVATE = 4,
      SW_SHOW = 5,
      SW_MINIMIZE = 6,
      SW_SHOWMINNOACTIVE = 7,
      SW_SHOWNA = 8,
      SW_RESTORE = 9,
      SW_SHOWDEFAULT = 10,
      SW_FORCEMINIMIZE = 11,
      SW_MAX = 11
    }

    private const uint SEE_MASK_INVOKEIDLIST = 12;

    public static bool Start(ProcessStartInfo startInfo, uint mask)
    {
      var info = new SHELLEXECUTEINFO();

      info.cbSize = Marshal.SizeOf(info);
      info.lpVerb = startInfo.Verb;
      info.lpFile = startInfo.FileName;

      switch (startInfo.WindowStyle)
      {
          case ProcessWindowStyle.Hidden:
          info.nShow = ShowCommands.SW_HIDE;
          break;
          case ProcessWindowStyle.Minimized:
          info.nShow = ShowCommands.SW_MINIMIZE;
          break;
          case ProcessWindowStyle.Maximized:
          info.nShow = ShowCommands.SW_MAXIMIZE;
          break;
        default:
          info.nShow = ShowCommands.SW_SHOW;
          break;
      }

      info.lpParameters = startInfo.Arguments;
      info.lpDirectory = startInfo.WorkingDirectory;

      info.fMask = mask;
      return ShellExecuteEx(ref info);
    }

    public static bool Start(ProcessStartInfo startInfo)
    {
      return Start(startInfo, 0);
    }

    public static bool ShowFileProperties(string fileName)
    {
      return Start(
        new ProcessStartInfo(fileName)
        {
          Verb = "properties",
        }, SEE_MASK_INVOKEIDLIST);
      //var info = new SHELLEXECUTEINFO();
      //info.cbSize = Marshal.SizeOf(info);
      //info.lpVerb = "properties";
      //info.lpFile = fileName;
      //info.nShow = ShowCommands.SW_SHOW;
      //info.fMask = SEE_MASK_INVOKEIDLIST;
      //return ShellExecuteEx(ref info);
    }
  }
}