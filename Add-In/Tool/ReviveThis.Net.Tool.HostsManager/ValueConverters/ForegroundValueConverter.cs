using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace ReviveThis.Net.Tool.HostsManager.ValueConverters
{
  public class ForegroundValueConverter: IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
      {
        return SystemColors.WindowTextColor;
      }

      if (value is string)
      {
        if (((string) value).TrimStart(new []{ '\0' , ' '}).StartsWith("#"))
        {
          //return new SolidColorBrush(Color.FromArgb(77, 0, 127, 0));
          return System.Windows.Application.Current.Resources["ItemTextDisabled"] as SolidColorBrush;
        }
        else
        {
          return System.Windows.Application.Current.Resources["ItemText"] as SolidColorBrush;
        }
      }

      return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return null;
    }
  }
}