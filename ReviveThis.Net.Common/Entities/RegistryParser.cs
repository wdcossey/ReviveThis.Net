using System;
using System.Collections.Generic;
using Microsoft.Win32;

namespace ReviveThis.Entities
{
  public class RegistryParser
  {
    public RegistryHive RegistryHive { get; internal set; }

    private readonly List<RegistryView> _registryViews = new List<RegistryView>();

    public string SubKey { get; internal set; }

    public ICollection<RegistryView> RegistryViews
    {
      get
      {
        var result = _registryViews;
        if (!Environment.Is64BitOperatingSystem)
        {
          result.RemoveAll(r => r == RegistryView.Registry64);
        }
        return result;
      }
    }

    public RegistryParser(RegistryHive hive, string subKey)
    {
      RegistryHive = hive;
      SubKey = subKey;
    }

    public RegistryParser(RegistryHive hive, string subKey, RegistryView view)
      : this (hive, subKey)
    {
      _registryViews.Add(view);
    }

    public RegistryParser(RegistryHive hive, string subKey, IEnumerable<RegistryView> views)
      : this(hive, subKey)
    {
      _registryViews.AddRange(views);
    }
  }
}