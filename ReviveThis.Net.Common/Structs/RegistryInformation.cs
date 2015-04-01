using Microsoft.Win32;

namespace ReviveThis.Structs
{
  public struct RegistryInformation
  {
    public RegistryHive Hive { get; set; }

    public RegistryView View { get; set; }

    public string Path { get; set; }

    public string Name { get; set; }

    public object Value { get; set; }

    public RegistryInformation(RegistryHive hive, RegistryView view, string path, string name = null, object value = null)
      : this()
    {
      Hive = hive;
      View = view;
      Path = path;
      Name = name;
      Value = value;
    }

  }
}