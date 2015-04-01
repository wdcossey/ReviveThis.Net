using System;
using System.Windows;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using ReviveThis.Pages;

namespace ReviveThis
{
  public class ReviveThisLinkNavigator
    : ILinkNavigator
  {
    public void Navigate(Uri uri, FrameworkElement source, string parameter = null)
    {
      throw new NotImplementedException();
    }

    public CommandDictionary Commands { get; set; }
  }
}