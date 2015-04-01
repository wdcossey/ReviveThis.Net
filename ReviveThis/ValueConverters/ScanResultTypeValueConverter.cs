using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using ReviveThis.Entities.Attributes;
using ReviveThis.Enums;
using ReviveThis.Interfaces;

namespace ReviveThis.ValueConverters
{
  public class ScanResultTypeValueConverter: IValueConverter
  {
    private readonly bool _parameter;

    public ScanResultTypeValueConverter()
    {
      
    }

    public ScanResultTypeValueConverter(bool parameter)
    {
      _parameter = parameter;
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      var scanItem = value as IDetectionResultItem;

      if (scanItem == null)
        return null;

      var includeHeader = _parameter;

      if (parameter is string)
      {
        includeHeader = bool.Parse(parameter as string);
      }

      var scanResultType = scanItem.ResultType;

      var member = typeof(ScanResultType).GetMember(scanResultType.ToString()).FirstOrDefault();

      if (member == null)
        return null;

      if (scanResultType == ScanResultType.CustomAddIn && scanItem is ICustomAddInSection)
      {
        var customSection = scanItem as ICustomAddInSection;

        return string.Format("{0}{1}",
          customSection.CustomAddInSection.Id,
          includeHeader
            ? string.Format(" - {0}",
              string.IsNullOrWhiteSpace(customSection.CustomAddInSection.Text)
                ? "(undefined)"
                : customSection.CustomAddInSection.Text)
            : string.Empty);
      }

      var attribute =
        member.GetCustomAttributes(typeof (ScanResultSectionAttribute), false).FirstOrDefault() as
          ScanResultSectionAttribute;

      return string.Format("{0}{1}", attribute == null ? "??" : attribute.SectionId,
        includeHeader ? string.Format(" - {0}", attribute == null ? "(undefined)" : attribute.Text) : string.Empty);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      //throw new NotImplementedException();
      return null;
    }
  }
}