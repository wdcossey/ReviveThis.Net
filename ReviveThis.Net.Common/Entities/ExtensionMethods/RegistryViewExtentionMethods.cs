using Microsoft.Win32;
using ReviveThis.Structs;

namespace ReviveThis.Entities.ExtensionMethods
{
  public static class RegistryViewExtentionMethods
  {
    private const string X64_DEFAULT_VALUE = "-x64";

    public static string FormatViewToString(this RegistryView view, string x64Value = X64_DEFAULT_VALUE)
    {
      return view == RegistryView.Registry64 ? x64Value : string.Empty;
    }
     
    public static string FormatViewToString(this RegistryInformation information, string x64Value = X64_DEFAULT_VALUE)
    {
      return information.View.FormatViewToString(x64Value);
    }

  }
}