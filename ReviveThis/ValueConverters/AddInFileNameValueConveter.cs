using System;
using System.Globalization;
using System.Windows.Data;

namespace ReviveThis.ValueConverters
{
  public class AddInFileNameValueConveter: IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return value.GetType().Assembly.ManifestModule.ScopeName; ;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      //throw new NotImplementedException();
      return null;
    }
  }
}