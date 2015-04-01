using System.Linq;
using ReviveThis.Entities.Attributes;
using ReviveThis.Enums;

namespace ReviveThis.Entities.ExtensionMethods
{
  public static class ScanResultTypeExtensionMethods
  {
    public static string FormatToString(this ScanResultType scanResultType, bool includeHeader = false)
    {
      var member = typeof(ScanResultType).GetMember(scanResultType.ToString()).FirstOrDefault();
      
      if (member == null)
        return null;

      var attribute = member.GetCustomAttributes(typeof(ScanResultSectionAttribute), false).FirstOrDefault() as ScanResultSectionAttribute;

      return string.Format("{0}{1}", attribute == null ? "??" : attribute.SectionId, includeHeader ? string.Format(" - {0}", attribute == null ? "(undefined)" : attribute.Text) : string.Empty);
    }
  }
}