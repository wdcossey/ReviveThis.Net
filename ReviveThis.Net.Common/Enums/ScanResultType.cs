using ReviveThis.Entities.Attributes;

namespace ReviveThis.Enums
{
  //R0 - Changed Registry value (MSIE)
  //R1 - Created Registry value
  //R2 - Created Registry key
  //R3 - Created extra value in regkey where only one should be

  //F0 - Changed inifile value (system.ini)
  //F1 - Created inifile value (win.ini)

  //N1 - Changed NS4.x homepage
  //N2 - Changed NS6 homepage
  //N3 - Changed NS7 homepage/searchpage
  //N4 - Changed Moz homepage/searchpage

  //O1 - Hosts file hijack
  //O2 - BHO
  //O3 - IE Toolbar
  //O4 - Regrun entry
  //O5 - Control.ini IE Options block
  //O6 - Policies IE Options/Control Panel block
  //O7 - Policies Regedit block
  //O8 - IE Context menuitem
  //O9 - IE Tools menuitem/button
  //O10 - Winsock hijack
  //O11 - IE Advanced Options group
  //O12 - IE Plugin
  //O13 - IE DefaultPrefix hijack
  //O14 - IERESET.INF hijack
  //O15 - Trusted Zone autoadd, e.g. free.aol.com
  //O16 - Downloaded Program Files
  //O17 - Domain hijacks in CurrentControlSet
  //O18 - Protocol & Filter enum
  //O19 - User style sheet hijack
  //O20 - AppInit_DLLs registry value + Winlogon Notify subkeys
  //O21 - ShellServiceObjectDelayLoad enumeration
  //O22 - SharedTaskScheduler enumeration

  public enum ScanResultType
  {

    #region R0, R1, R2, R3 - Registry Entries

    /// <summary>
    /// <para>R0<br/>
    /// Items are changed Registry values.</para>
    /// </summary>
    [ScanResultSection("R0", "Changed Registry value")]
    RegistryValueChanged,

    /// <summary>
    /// <para>R1<br/>
    /// Items are created Registry values.</para>
    /// </summary>
    [ScanResultSection("R1", "Created Registry value")]
    RegistryValueCreated,

    /// <summary>
    /// <para>R2<br/>
    /// Items are created Registry keys.</para>
    /// </summary>
    [ScanResultSection("R2", "Created Registry key")]
    RegistryKeyCreated,

    /// <summary>
    /// <para>R3<br/>
    /// Items are created extra Registry values where only one should exist.</para>
    /// </summary>
    [ScanResultSection("R3", "Multiple Registry values")]
    RegistryMultipleValues,

    #endregion

    #region F0, F1, F2, F3 - Ini File(s)

    /// <summary>
    /// <para>F0<br/>
    /// Changed inifile value.</para>
    /// </summary>
    [ScanResultSection("F0", "Changed .ini file value")]
    IniValueChanged,

    /// <summary>
    /// <para>F1<br/>
    /// Created inifile value.</para>
    /// </summary>
    [ScanResultSection("F1", "Created .ini file value")]
    IniValueCreated,

    /// <summary>
    /// <para>F2<br/>
    /// Changed inifile value, mapped to Registry.</para>
    /// </summary>
    [ScanResultSection("F2", "Changed .ini file value mapped to Registry")]
    IniValueChangedRegistry,

    /// <summary>
    /// <para>F3<br/>
    /// Created inifile value, mapped to Registry.</para>
    /// </summary>
    [ScanResultSection("F3", "Created .ini file value mapped to Registry")]
    IniValueCreatedRegistry,

    #endregion

    #region O1 - Hosts File

    /// <summary>
    /// <para>O1<br/>
    /// Hosts file as been hijacked</para>
    /// </summary>
    [ScanResultSection("O1", "Domain Hijacking")]
    HostsFileHiJack,
    #endregion

    #region O2 - Browser Helper Object(s)

    /// <summary>
    /// <para>O2<br/>
    /// Browser Helper Object</para>
    /// </summary>
    [ScanResultSection("O2", "Internet Explorer Browser Helper Object(s)")]
    BrowserHelperObject,
    #endregion

    #region O3 - Internet Explorer Toolbar(s)
    /// <summary>
    /// <para>O3<br/>
    /// Internet Explorer Toolbar</para>
    /// </summary>
    [ScanResultSection("O3", "Internet Explorer Toolbar(s)")]
    InternetExplorerToolbar,
    #endregion

    #region O4 - AutoRun
    /// <summary>
    /// <para>O4<br/>
    /// AutoRun value, either from the Registry or "StartUp" directory</para>
    /// </summary>
    [ScanResultSection("O4", "AutoRun / StartUp")]
    AutoRun,
    #endregion

    #region O5 - Control.ini IE Options block
    /// <summary>
    /// <para>O5<br/>
    /// Hidden Control Panel items, either from the Registry or "contro.ini"</para>
    /// </summary>
    [ScanResultSection("O5")]
    ControlIni,

    #endregion

    #region O6 - Policies IE Options/Control Panel block
    /// <summary>
    /// <para>O6<br/>
    /// Group Policy Settings</para>
    /// </summary>
    [ScanResultSection("O6", "Group Policy Settings")]
    GroupPolicySettings,

    #endregion

    #region O7 - Regedit Disabled
    /// <summary>
    /// <para>O7<br/>
    /// Registry ('regedit') Editing Disabled</para>
    /// </summary>
    [ScanResultSection("O7")]
    RegeditDisabled,
    #endregion

    #region O8 - Internet Explorer Context Menu
    /// <summary>
    /// <para>O8<br/>
    /// Internet Explorer Context Menu</para>
    /// </summary>
    [ScanResultSection("O8", "Internet Explorer Context Menu(s)")]
    InternetExplorerContextMenu,
    #endregion

    #region O9 - Internet Explorer Extentions
    /// <summary>
    /// <para>O9<br/>
    /// Internet Explorer Extension</para>
    /// </summary>
    [ScanResultSection("O9", "Internet Explorer Extention(s)")]
    InternetExplorerExtension,
    #endregion

    #region O10 - WinSock Layered Service Provider
    /// <summary>
    /// <para>O10<br/>
    /// WinSock Layered Service Provider</para>
    /// </summary>
    [ScanResultSection("O10", "Layered Service Provider(s)")]
    WinSockLayeredServiceProvider,
    #endregion

    #region O11 - Internet Explorer Advanced Options
    /// <summary>
    /// <para>O11<br/>
    /// Internet Explorer Advanced Options</para>
    /// </summary>
    [ScanResultSection("O11", "Internet Explorer Advanced Options")]
    InternetExplorerAdvancedOptions,
    #endregion

    #region O12 - Internet Explorer Plugin
    /// <summary>
    /// <para>O12<br/>
    /// Internet Explorer Plugin(s)</para>
    /// </summary>
    [ScanResultSection("O12", "Internet Explorer Plugin(s)")]
    InternetExplorerPlugin,
    #endregion

    #region O13 - Internet Explorer Prefix Hi-jacking
    /// <summary>
    /// <para>O13<br/>
    /// Internet Explorer Prefix Hi-jacking</para>
    /// </summary>
    [ScanResultSection("O13", "Internet Explorer Prefix")]
    InternetExplorerPrefix,
    #endregion



    #region O16 - Downloaded Program Files
    /// <summary>
    /// <para>O16<br/>
    /// Downloaded Program Files</para>
    /// </summary>
    [ScanResultSection("O16", "Downloaded Program Files")]
    DownloadedProgramFiles,
    #endregion



    #region O20 - AppInit_DLLs / Winlogon Notify
    /// <summary>
    /// <para>O20<br/>
    /// Windows® Explorer AppInit_DLLs</para>
    /// </summary>
    [ScanResultSection("O20", @"AppInit_DLLs")]
    AppInitDlLs,

    /// <summary>
    /// <para>O20<br/>
    /// Windows® Explorer Winlogon\Notify</para>
    /// </summary>
    [ScanResultSection("O20", @"Winlogon Notification Packages")]
    WinlogonNotify,
    #endregion











    /// <summary>
    /// <para>99999<br/>
    /// Custom Add-In's can use ScanResultType.CustomAddIn to create their own results section.</para>
    /// </summary>
    CustomAddIn = 99999,

  }
}