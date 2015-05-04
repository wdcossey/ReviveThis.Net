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
      var result = base.LoadContent(uri);
      return result;
    }
  }
}