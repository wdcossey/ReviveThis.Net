using System;
using System.Runtime.InteropServices;

namespace ReviveThis.Entities.PInvoke
{
  public static class AdvApi32
  {
    // Call InitiateShutdown
    [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool InitiateShutdown(string lpMachineName,
                                        string lpMessage,
                                        UInt32 dwGracePeriod,
                                        UInt32 dwShutdownFlags,
                                        UInt32 dwReason);

    //void CallInitiateShutdown()
    //{
    //  CheckBox[] chkFlags = { chkSHUTDOWN_FORCE_OTHERS, 
    //                                 chkSHUTDOWN_FORCE_SELF, 
    //                                 chkSHUTDOWN_GRACE_OVERRIDE, 
    //                                 chkSHUTDOWN_HYBRID, 
    //                                 chkSHUTDOWN_INSTALL_UPDATES, 
    //                                 chkSHUTDOWN_NOREBOOT, 
    //                                 chkSHUTDOWN_POWEROFF,
    //                                 chkSHUTDOWN_RESTART,
    //                                 chkSHUTDOWN_RESTARTAPPS };
    //  UInt32 flags = GetFlags(chkFlags);
    //  UInt32 reason = 0;
    //  if (HexToNum(txtReasonIS.Text, out reason))
    //  {
    //    UInt32 gracePeriod;
    //    if (HexToNum(txtGracePeriodIS.Text, out gracePeriod))
    //    {
    //      if (!InitiateShutdown(txtMachineNameIS.Text, txtMessageIS.Text, gracePeriod, flags, reason))
    //      {
    //        ShowLastError();
    //      }
    //    }
    //  }
    //}

    // Call InitiateSystemShutdownEx
    [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool InitiateSystemShutdownEx(
        string lpMachineName,
        string lpMessage,
        uint dwTimeout,
        bool bForceAppsClosed,
        bool bRebootAfterShutdown,
        UInt32 dwReason);
    //void CallInitiateSystemShutdownEx()
    //{
    //  UInt32 timeout = 0;
    //  if (HexToNum(txtTimeOutISSE.Text, out timeout))
    //  {
    //    UInt32 reason = 0;
    //    if (HexToNum(txtReasonISSE.Text, out reason))
    //    {
    //      if (!InitiateSystemShutdownEx(txtMachineNameISSE.Text, txtMessageISSE.Text, timeout,
    //          chkForceAppsClosed.Checked, chkRebootAfterShutdown.Checked, reason))
    //      {
    //        ShowLastError();
    //      }
    //    }
    //  }
    //}

    // Call AbortSystemShutdown
    [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool AbortSystemShutdown(string lpMachineName);
    //void CallAbortSystemShutdown()
    //{
    //  if (!AbortSystemShutdown(txtMachineNameASS.Text))
    //  {
    //    ShowLastError();
    //  }
    //}
  }
}