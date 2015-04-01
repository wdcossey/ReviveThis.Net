using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace ReviveThis.ValueConverters
{
  public class StringsToHtmlValueConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (!(value is string[]))
        return string.Empty;

      const string HTTP_PATTERN = @"(?<Protocol>\w+):\/\/(?<Domain>[\w@][\w.:@]+)\/?[\w\.?=%&=\-@/$,#]*";

      return Regex.Replace(string.Join("\n", value as string[]), HTTP_PATTERN, string.Format("[url={0}]{0}[/url]", "$0"), RegexOptions.IgnoreCase | RegexOptions.Singleline);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      //throw new NotImplementedException();
      return null;
    }
  }
}