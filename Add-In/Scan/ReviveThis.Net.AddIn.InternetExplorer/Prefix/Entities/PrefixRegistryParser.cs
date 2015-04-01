using System.Collections.Generic;
using Microsoft.Win32;
using ReviveThis.AddIn.InternetExplorer.Prefix.Enums;
using ReviveThis.Entities;

namespace ReviveThis.AddIn.InternetExplorer.Prefix.Entities
{
  public class PrefixRegistryParser: RegistryParser
  {
    public Dictionary<string, string[]> PrefixDictionary { get; private set; }

    public PrefixResultType PrefixResultType { get; private set; }

    public PrefixRegistryParser(RegistryHive hive, string subKey, IEnumerable<RegistryView> views, PrefixResultType resultType, Dictionary<string, string[]> dictionary) 
      : base(hive, subKey, views)
    {
      PrefixResultType = resultType;
      PrefixDictionary = dictionary;

    }
  }
}