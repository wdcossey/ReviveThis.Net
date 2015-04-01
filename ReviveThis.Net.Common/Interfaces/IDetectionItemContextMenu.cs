using System;
using System.Collections.Generic;
using System.Drawing;

namespace ReviveThis.Interfaces
{
  public interface IDetectionItemContextMenu
  {

    string Text { get; set; }

    bool Enabled { get; set; }

    bool Separator { get; set; }

    Bitmap Image { get; set; }

    EventHandler OnClick { get; set; }

    ICollection<IDetectionItemContextMenu> MenuItems { get; set; }
  }
}