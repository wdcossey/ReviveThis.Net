using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using FirstFloor.ModernUI.Windows;
using ReviveThis.ViewModels;
using FragmentNavigationEventArgs = FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs;
using NavigatingCancelEventArgs = FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs;
using NavigationEventArgs = FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs;

namespace ReviveThis.Pages
{
  /// <summary>
  /// Interaction logic for pgScanResults.xaml
  /// </summary>
  public partial class DetectionResults : UserControl, IContent
  {
    public DetectionResults()
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
  }
}
