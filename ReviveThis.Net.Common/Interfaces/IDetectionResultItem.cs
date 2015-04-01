using ReviveThis.Enums;

namespace ReviveThis.Interfaces
{
  public interface IDetectionResultItem: IDetectionRepair
  {
    ScanResultType ResultType { get; }

    /// <summary>
    /// Formats the information to HiJackThis Legacy format.
    /// </summary>
    string LegacyString { get; }

    //ICollection<IScanItemContextMenu> ContextMenus { get; }

    //ListViewItem AsListViewItem { get; }
  }
}