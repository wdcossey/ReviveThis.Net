using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
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

    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);

      // bootstrap MEF composition
      //var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
      //var container = new CompositionContainer(catalog);

      //var contentLoader = container.GetExport<MefContentLoader>().Value;
      //this.Resources.Add("MefContentLoader", contentLoader);
    }
  }
}
