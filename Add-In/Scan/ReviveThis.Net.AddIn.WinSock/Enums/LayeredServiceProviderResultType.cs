namespace ReviveThis.AddIn.WinSock.Enums
{
  public enum LayeredServiceProviderType
  {
    /// <summary>
    /// "Current_NameSpace_Catalog"/"Current_Protocol_Catalog" in "HKEY_LOCAL_MACHINE\System\CurrentControlSet\Services\WinSock2\Parameters" is empty.
    /// </summary>
    EmptyEntry,

    /// <summary>
    /// Can't find Registry Key "HKEY_LOCAL_MACHINE\System\CurrentControlSet\Services\WinSock2\Parameters\%Current_NameSpace_Catalog/Current_Protocol_Catalog%" .
    /// </summary>
    RegistryKeyNotFound,

    /// <summary>
    /// Can't access registry key "HKEY_LOCAL_MACHINE\System\CurrentControlSet\Services\WinSock2\Parameters\%Current_NameSpace_Catalog/Current_Protocol_Catalog%"
    /// </summary>
    RegistryAccessError,

    /// <summary>
    /// "Num_Catalog_Entries" value is null or less than 0
    /// </summary>
    NoRegistryEntries,

    /// <summary>
    /// Missing Chain Gap entry
    /// </summary>
    MissingChainGap,

    /// <summary>
    /// "LibraryPath" value in the Registry does not exist or is invalid.
    /// </summary>
    MissingLibrary,

    /// <summary>
    /// "LibraryPath" file does not exist or is invalid.
    /// </summary>
    MissingProvider,

    /// <summary>
    /// HiJacked Internet access detected, WebHancer "webhdll.dll".
    /// </summary>
    HijackedWebHancer,

    /// <summary>
    /// HiJacked Internet access detected, New.Net "newdot".
    /// </summary>
    HijackedNewDotNet,

    /// <summary>
    /// HiJacked Internet access detected, CommonName "cnmib.dll".
    /// </summary>
    HijackedCommonName,

    /// <summary>
    /// "LibraryPath" file not found on the White-List.
    /// </summary>
    UnknownFile,
  }
}