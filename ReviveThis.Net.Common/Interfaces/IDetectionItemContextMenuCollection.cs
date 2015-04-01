using System.Collections.Generic;

namespace ReviveThis.Interfaces
{
  public interface IDetectionItemContextMenuCollection
  {
    ICollection<IDetectionItemContextMenu> MenuItems { get; } 
  }
}