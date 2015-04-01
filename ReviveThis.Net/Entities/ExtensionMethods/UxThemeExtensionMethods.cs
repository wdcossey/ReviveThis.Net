using System.Windows.Forms;
using ReviveThis.Entities.PInvoke;

namespace ReviveThis.Entities.ExtensionMethods
{
  public static class UxThemeExtensionMethods
  {
    public static int SetExplorerTheme(this ListView listView)
    {
      return UxTheme.SetExplorerTheme(listView.Handle);
    }

    public static int SetExplorerTheme(this TreeView listView)
    {
      return UxTheme.SetExplorerTheme(listView.Handle);
    }
  }

}