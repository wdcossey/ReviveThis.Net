using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using NavigatingCancelEventArgs = System.Windows.Navigation.NavigatingCancelEventArgs;

namespace ReviveThis
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {

    public App()
    {
      

    }
    protected override void OnNavigating(NavigatingCancelEventArgs e)
    {

      base.OnNavigating(e);
    }

  }
}
