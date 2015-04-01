using System;
using System.Globalization;
using System.Windows.Data;
using ReviveThis.Interfaces;

namespace ReviveThis.ValueConverters
{
  public class ScanResultToolTipValueConverter: IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      var scanItem = value as IDetectionItemToolTip;

      if (scanItem == null)
      {
        var resultItem = value as IDetectionResultItem;
        if (resultItem == null)
          return null;

        return resultItem.LegacyString;
      }

      //throw new NotImplementedException();
      return scanItem.ToolTip;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      //throw new NotImplementedException();
      return null;
    }
  }
}