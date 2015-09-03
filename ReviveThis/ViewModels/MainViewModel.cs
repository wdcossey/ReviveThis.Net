using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Documents;
using FirstFloor.ModernUI.Presentation;
using ReviveThis.Entities;
using ReviveThis.Helpers;

namespace ReviveThis.ViewModels
{
  public class MainViewModel: INotifyPropertyChanged
  {

    public MainViewModel()
    {
      if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
      {
        LoadMenus();
      }
    }

    private async void LoadMenus()
    {
      var menuCollection = new LinkGroupCollection();

      var grpScan = new LinkGroup
      {
        DisplayName = "Scan",
      };

      //grpScan.Links.Add(new Link
      //{
      //  DisplayName = "Selection",
      //  Source = new Uri("/Pages/DetectionSelection.xaml", UriKind.RelativeOrAbsolute)
      //});

      grpScan.Links.Add(new Link
      {
        DisplayName = "Results",
        Source = new Uri("/Pages/DetectionResults.xaml", UriKind.RelativeOrAbsolute)
      });

      menuCollection.Add(grpScan);

      var grpTools = new LinkGroup
      {
        DisplayName = "Tools"
      };

      grpTools.Links.Add(new Link
      {
        DisplayName = "tools",
        Source = new Uri("/Pages/Tools.xaml", UriKind.RelativeOrAbsolute)
      });

      menuCollection.Add(grpTools);

      var grpSettings = new LinkGroup
      {
        DisplayName = "Settings",
        GroupKey = "settings",
      };

      grpSettings.Links.Add(new Link
      {
        DisplayName = "Add-in's",
        Source = new Uri("/Pages/AddInManager.xaml", UriKind.RelativeOrAbsolute)
      });

      menuCollection.Add(grpSettings);

      var grpAbout = 
        new LinkGroup
        {
          DisplayName = "About",
          GroupKey = "about",
        };

      grpAbout.Links.Add(
        new Link
        {
          DisplayName = "Credits",
          Source = new Uri("/Pages/About.xaml", UriKind.RelativeOrAbsolute)
        });

      menuCollection.Add(grpAbout);


      MenuCollection = menuCollection;
    }

    private LinkGroupCollection _menuCollection;

    public LinkGroupCollection MenuCollection
    {
      get { return _menuCollection; }
      private set
      {
        _menuCollection = value;
        OnPropertyChanged();
      }
    }

    public string Title
    {
      get
      {
        return $"{"ReviveThis.Net"}{(Elevation.IsElevated ? $" ({@"Administrator"})" : string.Empty)}";
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChangedEventHandler handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}