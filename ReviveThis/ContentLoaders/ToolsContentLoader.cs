using System;
using System.Linq;
using FirstFloor.ModernUI.Windows;

namespace ReviveThis.ContentLoaders
{
  public class ToolsContentLoader : DefaultContentLoader
  {
    protected override object LoadContent(Uri uri)
    {
      var content = (from c in ReviveThis.Entities.ReviveThisApplication.Default.AddIns.Tools
        where c.Guid.ToString().Equals(uri.OriginalString, StringComparison.OrdinalIgnoreCase)
        select c.Content).FirstOrDefault();

      return content ?? base.LoadContent(uri);
    }
  }
}