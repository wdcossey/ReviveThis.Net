using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using ReviveThis.Interfaces;

namespace ReviveThis.ValueConverters
{
  public class ContextMenuValueConverter: IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      var resultItem = value as IDetectionItemContextMenuCollection;

      if (resultItem == null || resultItem.MenuItems == null || !resultItem.MenuItems.Any())
      {
        return null;
      }

      return MenuBuilder(resultItem.MenuItems);
    }

    private ICollection<object> MenuBuilder(IEnumerable<IDetectionItemContextMenu> collection)
    {
      var result = new List<object>();

      foreach (var menu in collection.ToList())
      {
        if (menu.Separator || string.IsNullOrEmpty(menu.Text))
        {
          result.Add(new Separator());
          //result.Add(new ToolStripSeparator() /*{ Enabled = menu.Enabled }*/);
        }
        else
        {
          var newMenu = new MenuItem
          {
            Header = menu.Text,
            IsEnabled = menu.Enabled
          };

          newMenu.Click += (sender, args) =>
          {
            if (menu.OnClick != null)
              menu.OnClick.Invoke(sender, args);
          };

          if (menu.MenuItems != null && menu.MenuItems.Any())
          {
            foreach (var subMenu in MenuBuilder(menu.MenuItems))
            {
              newMenu.Items.Add(subMenu);
            }
          }

          result.Add(newMenu);
        }
      }

      return result;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      //throw new NotImplementedException();
      return null;
    }
  }
}