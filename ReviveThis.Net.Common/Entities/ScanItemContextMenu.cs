using System;
using System.Collections.Generic;
using System.Drawing;
using ReviveThis.Interfaces;

namespace ReviveThis.Entities
{
  public class ScanItemContextMenu : IDetectionItemContextMenu
  {

    public string Text { get; set; }

    public bool Enabled { get; set; }

    public Bitmap Image { get; set; }

    public EventHandler OnClick { get; set; }

    public bool Separator { get; set; }

    /// <summary>
    /// Sub-Menu Item(s).
    /// </summary>
    public ICollection<IDetectionItemContextMenu> MenuItems { get; set; }


    public ScanItemContextMenu()
    {
      Enabled = true;
    }

    public ScanItemContextMenu(bool separator = false)
      : this()
    {
      Separator = separator;
    }

    private ScanItemContextMenu(Bitmap image)
      : this()
    {
      Image = image;
    }

    public ScanItemContextMenu(string text, Bitmap image)
      : this(image)
    {
      Text = text;
    }

    public ScanItemContextMenu(string text)
      : this(text, image: null)
    {

    }

    public ScanItemContextMenu(string text, EventHandler onClick, Bitmap image)
      : this(text, image)
    {
      OnClick = onClick;
    }

    public ScanItemContextMenu(string text, EventHandler onClick)
      : this(text, onClick, image: null)
    {

    }

    public ScanItemContextMenu(string text, EventHandler onClick, Bitmap image,
      ICollection<IDetectionItemContextMenu> menuItems)
      : this(text, onClick, image)
    {
      MenuItems = menuItems;
    }

    public ScanItemContextMenu(string text, EventHandler onClick, ICollection<IDetectionItemContextMenu> menuItems)
      : this(text, onClick, null, menuItems)
    {

    }
  }
}