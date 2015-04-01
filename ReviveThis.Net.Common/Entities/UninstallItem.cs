using System;
using System.Linq;
using Microsoft.Win32;

namespace ReviveThis.Entities
{
  public class UninstallItem
  {
    /// <summary>
    /// 
    /// </summary>
    public RegistryHive RegistryHive { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public RegistryView RegistryView { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public string RegistryPath { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public string DisplayName { get; private set; }

    private readonly string _fancyName = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    public string FancyName
    {
      get
      {
        if (!string.IsNullOrEmpty(_fancyName))
          return _fancyName;

        var fancyString = string.Empty;
        if (RegistryView == RegistryView.Registry32)
          fancyString += " [WoW64]";
        else if (RegistryHive == RegistryHive.CurrentUser)
        {
          fancyString += string.Format(" [{0}]", Environment.UserName);
        }

        return string.Format("{0}{1}", DisplayName, fancyString);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    public string UninstallString { get; private set; }

    public UninstallItem(RegistryKey registryKey, string registryPath, string displayName, string uninstallString)
    {

      var hiveString = string.Empty;

      var splitString = registryKey.Name.Split('\\');
      if (splitString.Any())
      {
        hiveString = splitString[0].ToUpper();
        switch (hiveString)
        {
          case "HKEY_CLASSES_ROOT":
            RegistryHive = RegistryHive.ClassesRoot;
            break;
          case "HKEY_CURRENT_CONFIG":
            RegistryHive = RegistryHive.CurrentConfig;
            break;
          case "HKEY_CURRENT_USER":
            RegistryHive = RegistryHive.CurrentUser;
            break;
          case "HKEY_DYN_DATA":
            RegistryHive = RegistryHive.DynData;
            break;
          case "HKEY_LOCAL_MACHINE":
            RegistryHive = RegistryHive.LocalMachine;
            break;
          case "HKEY_PERFORMANCE_DATA":
            RegistryHive = RegistryHive.PerformanceData;
            break;
          case "HKEY_USERS":
            RegistryHive = RegistryHive.Users;
            break;
        }
      }

      RegistryView = registryKey.View;
      RegistryPath = registryPath.Replace(string.Format("{0}\\", hiveString), string.Empty);
      DisplayName = displayName;
      UninstallString = uninstallString;
    }

  }
}