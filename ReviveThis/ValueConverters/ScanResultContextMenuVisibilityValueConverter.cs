using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using ReviveThis.Interfaces;

namespace ReviveThis.ValueConverters
{
  public class ScanResultContextMenuVisibilityValueConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      var resultItem = value as IDetectionItemContextMenuCollection;

      if (resultItem == null || resultItem.MenuItems == null || !resultItem.MenuItems.Any())
        return Visibility.Hidden;

      return Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      //throw new NotImplementedException();
      return null;
    }
  }
}