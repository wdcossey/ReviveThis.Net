using System;
using System.Linq;
using FirstFloor.ModernUI.Windows;
using ReviveThis.Pages;

namespace ReviveThis
{
  public class ReviveThisContentLoader
    : DefaultContentLoader
  {
    /// <summary>
    /// Loads the content from specified uri.
    /// </summary>
    /// <param name="uri">The content uri</param>
    /// <returns>The loaded content.</returns>
    protected override object LoadContent(Uri uri)
    {
      //var content = (from c in ReviveThis.Entities.ReviveThisApplication.Default.AddIns.Tools
      //               where c.Metadata.ContentUri == uri.OriginalString
      //               select c.Value).FirstOrDefault();
      var content = (from c in ReviveThis.Entities.ReviveThisApplication.Default.AddIns.Tools
                     where c.Guid.ToString().Equals(uri.OriginalString, StringComparison.OrdinalIgnoreCase) 
                     select c.Content).FirstOrDefault();

      if (content == null)
      {
        return base.LoadContent(uri);
        //throw new ArgumentException("Invalid uri: " + uri);
      }

      return content;

      //var result = base.LoadContent(uri);
      //return result;
    }
  }
}