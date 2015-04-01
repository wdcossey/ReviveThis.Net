using System;
using System.Security.Principal;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using ReviveThis.Helpers;

namespace ReviveThis
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : ModernWindow
  {
    public MainWindow()
    {
      InitializeComponent();

      this.Title = string.Format("{0}{1}", "ReviveThis.Net", Elevation.IsElevated ? string.Format(" ({0})", @"Administrator") : string.Empty);
    }

  }

}
