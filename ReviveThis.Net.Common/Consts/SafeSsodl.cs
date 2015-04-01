namespace ReviveThis.Consts
{
  /// <summary>
  /// LOAD SSODL SAFELIST (ShellServiceObjectDelayLoad)
  /// O21
  /// </summary>

  public static class SafeSsodls
  {
    public static string[] Array =
    {
      //WebCheck: E:\WINDOWS\System32\webcheck.dll (WinAll)
      @"{E6FB5E20-DE35-11CF-9C87-00AA005127ED}",
      
      //SysTray: E:\WINDOWS\System32\stobject.dll (Win2k/XP)
      @"{35CEC8A3-2BE6-11D2-8773-92E220524153}",
  
      //PostBootReminder: E:\WINDOWS\system32\SHELL32.dll (WinXP)
      @"{7849596a-48ea-486e-8937-a2a3009f31a9}",
    
      //CDBurn: E:\WINDOWS\system32\SHELL32.dll (WinXP)
      @"{fbeb8a05-beee-4442-804e-409d6c4515e9}",
  
      //AUHook: C:\WINDOWS\SYSTEM\AUHOOK.DLL (WinME)
      @"{11566B38-955B-4549-930F-7B7482668782}",
    
      //Network.ConnectionTray: C:\WINNT\system32\NETSHELL.dll (Win2k)
      @"{7007ACCF-3202-11D1-AAD2-00805FC1270E}",

      //UPnPMonitor: C:\WINDOWS\SYSTEM\UPNPUI.DLL (WinME/XP)
      @"{e57ce738-33e8-4c51-8354-bb4de9d215d1}",
      
      //AUHook: C:\WINDOWS\SYSTEM\AUHOOK.DLL (WinME)
      @"{BCBCD383-3E06-11D3-91A9-00C04F68105C}",
  
      //0aMCPClient: C:\Program Files\StarDock\MCPCore.dll
      @"{F5DF91F9-15E9-416B-A7C3-7519B11ECBFC}",

      //WPDShServiceObj   WPDShServiceObj.dll Windows Portable Device Shell Service Object
      @"{AAA288BA-9A4C-45B0-95D7-94D524869DB5}",
  
      //IconPackager Repair  iprepair.dll    Stardock\Object Desktop\ ThemeManager
      @"{1799460C-0BC8-4865-B9DF-4A36CD703FF0}",
  
      //EnhancedDialog   enhdlginit.dll  EnhancedDialog by Stardock
      @"{6D972050-A934-44D7-AC67-7C9E0B264220}"
    };
  }
}