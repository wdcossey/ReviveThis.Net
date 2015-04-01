using System;
using FirstFloor.ModernUI.Windows;

namespace ReviveThis.ContentLoaders
{
  public class AboutContentLoader
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