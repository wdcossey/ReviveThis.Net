using System;
using System.Runtime.InteropServices;

namespace ReviveThis.Entities.PInvoke
{
  public static class User32
  {
    [Flags]
    public enum ShutDownReasonFlags : uint
    {
      //The following are the major reason flags. They indicate the general issue type.

      /// <summary>
      /// Application issue.
      /// SHTDN_REASON_MAJOR_APPLICATION
      /// </summary>
      MajorApplicationIssue = 0x00040000,

      /// <summary>
      /// Hardware issue.
      /// SHTDN_REASON_MAJOR_HARDWARE
      /// </summary>
      MajorHardwareIssue = 0x00010000,

      /// <summary>
      /// The InitiateSystemShutdown function was used instead of InitiateSystemShutdownEx.
      /// SHTDN_REASON_MAJOR_LEGACY_API
      /// </summary>
      MajorLegacyApi = 0x00070000,

      /// <summary>
      /// Operating system issue.
      /// SHTDN_REASON_MAJOR_OPERATINGSYSTEM
      /// </summary>
      MajorOperatingSystemIssue = 0x00020000, 

      /// <summary>
      /// Other issue.
      /// SHTDN_REASON_MAJOR_OTHER
      /// </summary>
      MajorOtherIssue = 0x00000000,

      /// <summary>
      /// Power failure.
      /// SHTDN_REASON_MAJOR_POWER
      /// </summary>
      MajorPowerFailure = 0x00060000,

      /// <summary>
      /// Software issue.
      /// SHTDN_REASON_MAJOR_SOFTWARE
      /// </summary>
      MajorSoftwareIssue = 0x00030000,

      /// <summary>
      /// System failure.
      /// SHTDN_REASON_MAJOR_SYSTEM
      /// </summary>
      MajorSystemFailure = 0x00050000, 

      //The following are the minor reason flags. They modify the specified major reason flag. You can use any minor reason in conjunction with any major reason, but some combinations do not make sense.

      /// <summary>
      /// Blue screen crash event.
      /// SHTDN_REASON_MINOR_BLUESCREEN
      /// </summary>
      MinorBlueScreen = 0x0000000F,

      /// <summary>
      /// Unplugged.
      /// SHTDN_REASON_MINOR_CORDUNPLUGGED
      /// </summary>
      MinorUnplugged = 0x0000000b,

      /// <summary>
      /// SHTDN_REASON_MINOR_DISK
      /// Disk.
      /// </summary>
      MinorDisk = 0x00000007, 

      /// <summary>
      /// Environment.
      /// SHTDN_REASON_MINOR_ENVIRONMENT
      /// </summary>
      MinorEnvironment = 0x0000000c,

      /// <summary>
      /// Driver.
      /// SHTDN_REASON_MINOR_HARDWARE_DRIVER
      /// </summary>

      MinorDriver = 0x0000000d,

      /// <summary>
      /// Hot fix.
      /// SHTDN_REASON_MINOR_HOTFIX
      /// </summary>
      MinorHotFix = 0x00000011, 

      /// <summary>
      /// Hot fix uninstallation.
      /// SHTDN_REASON_MINOR_HOTFIX_UNINSTALL
      /// </summary>

      MinorHotFixUninstall = 0x00000017,

      /// <summary>
      /// Unresponsive.
      /// SHTDN_REASON_MINOR_HUNG
      /// </summary>
      MinorUnresponsive = 0x00000005,

      /// <summary>
      /// Installation.
      /// SHTDN_REASON_MINOR_INSTALLATION
      /// </summary>
      MinorInstallation = 0x00000002,

      /// <summary>
      /// Maintenance.
      /// SHTDN_REASON_MINOR_MAINTENANCE
      /// </summary>

      MinorMaintenance = 0x00000001,

      /// <summary>
      /// MMC issue.
      /// SHTDN_REASON_MINOR_MMC
      /// </summary>

      MinorMmcIssue = 0x00000019, 

      /// <summary>
      /// Network connectivity.
      /// SHTDN_REASON_MINOR_NETWORK_CONNECTIVITY
      /// </summary>

      MinorNetworkConnectivity = 0x00000014, 

      /// <summary>
      /// Network card.
      /// SHTDN_REASON_MINOR_NETWORKCARD
      /// </summary>

      MinorNetworkCard = 0x00000009,

      /// <summary>
      /// Other issue.
      /// SHTDN_REASON_MINOR_OTHER
      /// </summary>

      MinorOtherIssue = 0x00000000,

      /// <summary>
      /// Other driver event.
      /// SHTDN_REASON_MINOR_OTHERDRIVER
      /// </summary>

      MinorOtherDriverEvent = 0x0000000e,

      /// <summary>
      /// Power supply.
      /// SHTDN_REASON_MINOR_POWER_SUPPLY
      /// </summary>

      MinorPowerSupply = 0x0000000a,

      /// <summary>
      /// Processor.
      /// SHTDN_REASON_MINOR_PROCESSOR
      /// </summary>

      MinorProcessor = 0x00000008,

      /// <summary>
      /// Reconfigure.
      /// SHTDN_REASON_MINOR_RECONFIG
      /// </summary>

      MinorReconfigure = 0x00000004,

      /// <summary>
      /// Security issue.
      /// SHTDN_REASON_MINOR_SECURITY
      /// </summary>

      MinorSecurityIssue = 0x00000013, 

      /// <summary>
      /// Security patch.
      /// SHTDN_REASON_MINOR_SECURITYFIX
      /// </summary>

      MinorSecurityPatch = 0x00000012, 

      /// <summary>
      /// Security patch uninstallation.
      /// SHTDN_REASON_MINOR_SECURITYFIX_UNINSTALL
      /// </summary>

      MinorSecurityPatchUninstall = 0x00000018, 

      /// <summary>
      /// Service pack.
      /// SHTDN_REASON_MINOR_SERVICEPACK
      /// </summary>
      MinorServicePack = 0x00000010, 

      /// <summary>
      /// Service pack uninstallation.
      /// SHTDN_REASON_MINOR_SERVICEPACK_UNINSTALL
      /// </summary>
      MinorServicePackUninstall = 0x00000016, 

      /// <summary>
      /// Terminal Services.
      /// SHTDN_REASON_MINOR_TERMSRV
      /// </summary>
      MinorTerminalServices = 0x00000020, 

      /// <summary>
      /// Unstable.
      /// SHTDN_REASON_MINOR_UNSTABLE
      /// </summary>
      MinorUnstable = 0x00000006, 

      /// <summary>
      /// Upgrade.
      /// SHTDN_REASON_MINOR_UPGRADE
      /// </summary>
      MinorUpgrade = 0x00000003,

      /// <summary>
      /// WMI issue.
      /// SHTDN_REASON_MINOR_WMI
      /// </summary>
      MinorWmiIssue = 0x00000015, 
    }

    //[Flags]
    public enum ExitWindowsFlags : uint
    {
      Logoff = 0x00000000, //EWX_LOGOFF
      Shutdown = 0x00000001, //EWX_SHUTDOWN
      Reboot = 0x00000002, //EWX_REBOOT
      Force = 0x00000004, //EWX_FORCE
      PowerOff = 0x00000008, //EWX_POWEROFF
      ForceIfHung = 0x00000010, //EWX_FORCEIFHUNG
      RestartApps = 0x00000040, //EWX_RESTARTAPPS
      HybridShutdown = 0x00400000, //EWX_HYBRID_SHUTDOWN
    }

    // Call ExitWindowEx
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool ExitWindowsEx(ExitWindowsFlags uFlags, ShutDownReasonFlags uReason);

    public static bool ExitWindows(ExitWindowsFlags flags, ShutDownReasonFlags reason = ShutDownReasonFlags.MajorOtherIssue)
    {
      return ExitWindowsEx(flags, reason);
    }
    //protected void CallExitWindowsEx()
    //{
    //  CheckBox[] chkFlags = { chkEWX_FORCE, 
    //                                 chkEWX_FORCEIFHUNG, 
    //                                 chkEWX_HYBRID_SHUTDOWN, 
    //                                 chkEWX_LOGOFF, 
    //                                 chkEWX_POWEROFF, 
    //                                 chkEWX_REBOOT, 
    //                                 chkEWX_RESTARTAPPS,
    //                                 chkEWX_SHUTDOWN };

    //  UInt32 dwFlags = GetFlags(chkFlags);
    //  UInt32 reason = 0;
    //  if (HexToNum(txtReasonEWE.Text, out reason))
    //  {
    //    if (!ExitWindowsEx(dwFlags, reason))
    //    {
    //      ShowLastError();
    //    }
    //  }
    //}
  }
}