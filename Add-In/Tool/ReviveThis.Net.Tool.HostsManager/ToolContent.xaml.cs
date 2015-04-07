using System.Collections.ObjectModel;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ReviveThis.Entities;
using ReviveThis.Entities.Modules;
using ReviveThis.Interfaces;

namespace ReviveThis.Net.Tool.HostsManager
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
