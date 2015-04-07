using System;
using System.Windows.Controls;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;

namespace ReviveThis.Net.Tool.UninstallManager
{
  /// <summary>
  /// Interaction logic for Content.xaml
  /// </summary>
  public partial class ToolContent : UserControl, IContent, IDisposable
  {
    public ToolContent()
    {
      InitializeComponent();
    }

    public void OnFragmentNavigation(FragmentNavigationEventArgs e)
    {
    }

    public void OnNavigatedFrom(NavigationEventArgs e)
    {
    }

    public void OnNavigatedTo(NavigationEventArgs e)
    {
    }

    public void OnNavigatingFrom(NavigatingCancelEventArgs e)
    {
    }

    public void Dispose()
    {
      //
    }
  }
}
