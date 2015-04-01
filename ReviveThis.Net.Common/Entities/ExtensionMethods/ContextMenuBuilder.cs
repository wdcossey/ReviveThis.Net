using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ReviveThis.Interfaces;

namespace ReviveThis.Entities.ExtensionMethods
{
  public static class ContextMenuBuilder
  {

    /// <summary>
    /// Recursive Menu constructor.
    /// </summary>
    /// <param name="collection"></param>
    /// <returns></returns>
    private static ToolStripItem[] MenuBuilder(ICollection<IDetectionItemContextMenu> collection)
    {
      if (collection == null || !collection.Any())
      {
        return null;
      }

      var result = new Collection<ToolStripItem>();

      foreach (var menu in collection)
      {
        if (menu.Separator || string.IsNullOrEmpty(menu.Text))
        {
          result.Add(new ToolStripSeparator() /*{ Enabled = menu.Enabled }*/);
        }
        else
        {
          var newMenu = new ToolStripMenuItem(menu.Text, menu.Image, menu.OnClick) { Enabled = menu.Enabled };
          if (menu.MenuItems != null && menu.MenuItems.Any())
            newMenu.DropDown.Items.AddRange(MenuBuilder(menu.MenuItems));
          result.Add(newMenu);
        }
      }

      return result.ToArray();
    }

    public static void ShowContextMenu(this IDetectionItemContextMenuCollection sender, Control control, Point pos)
    {
      var menuItems = sender.MenuItems;

      if (menuItems == null || !menuItems.Any())
      {
        return;
      }

      var items = MenuBuilder(menuItems);

      if (items == null || !items.Any())
      {
        return;
      }

      var menu = new ContextMenuStrip();
      menu.Items.AddRange(items);
      menu.Show(control, pos);

      //new ContextMenu(items).Show(control, pos);
    }
  }
}