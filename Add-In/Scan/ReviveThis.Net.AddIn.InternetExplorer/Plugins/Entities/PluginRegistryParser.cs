using System.Collections.Generic;
using Microsoft.Win32;
using ReviveThis.AddIn.InternetExplorer.Plugins.Enums;
using ReviveThis.Entities;

namespace ReviveThis.AddIn.InternetExplorer.Plugins.Entities
{
  public class PluginRegistryParser : RegistryParser
  {
    public PluginResultType ResultType { get; private set; }

    public PluginRegistryParser(RegistryHive hive, string subKey, IEnumerable<RegistryView> views, PluginResultType resultType)
      : base(hive, subKey, views)
    {
      ResultType = resultType;
    }
  }
}