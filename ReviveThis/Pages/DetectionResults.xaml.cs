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
    private readonly DetectionResultsViewModel _dataContext;

    public DetectionResults()
    {
      InitializeComponent();
      _dataContext = (DataContext as DetectionResultsViewModel);
    }

    public void OnFragmentNavigation(FragmentNavigationEventArgs e)
    {
      var parameters = e.Fragment
        .Split(new [] {'&'}, StringSplitOptions.RemoveEmptyEntries)
        .Select(s => s.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries)).ToDictionary(s => s[0], s => s[1]);

      if (parameters.ContainsKey("mode"))
      {

        _dataContext.StartScan.Execute(Convert.ToBoolean(Convert.ToInt32(parameters["mode"])));
      }

      //throw new NotImplementedException();
    }

    public void OnNavigatedFrom(NavigationEventArgs e)
    {
      //throw new NotImplementedException();
    }

    public void OnNavigatedTo(NavigationEventArgs e)
    {
      
      //throw new NotImplementedException();
    }

    public void OnNavigatingFrom(NavigatingCancelEventArgs e)
    {
      //throw new NotImplementedException();
    }
  }
}
