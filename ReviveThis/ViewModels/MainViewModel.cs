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

      grpScan.Links.Add(new Link
      {
        DisplayName = "Selection",
        Source = new Uri("/Pages/DetectionSelection.xaml", UriKind.RelativeOrAbsolute)
      });

      grpScan.Links.Add(new Link
      {
        DisplayName = "Results",
        Source = new Uri("/Pages/DetectionResults.xaml", UriKind.RelativeOrAbsolute)
      });

      menuCollection.Add(grpScan);

      //menuCollection.Add(
      //  new LinkGroup
      //  {
      //    DisplayName = "Scan",
      //    Links = new LinkCollection(new List<Link>()
      //    {
      //      new Link
      //      {
      //        DisplayName = "Selection",
      //        Source = new Uri("/Pages/DetectionSelection.xaml", UriKind.RelativeOrAbsolute)
      //      },
      //      new Link
      //      {
      //        DisplayName = "Results",
      //        Source = new Uri("/Pages/DetectionResults.xaml", UriKind.RelativeOrAbsolute)
      //      },
      //      new Link
      //      {
      //        DisplayName = "Analysis",
      //        Source = new Uri("/Pages/pgScan.xaml", UriKind.RelativeOrAbsolute)
      //      },
      //      new Link
      //      {
      //        DisplayName = "Log",
      //        Source = new Uri("/Pages/pgScan.xaml", UriKind.RelativeOrAbsolute)
      //      },
      //    })
      //  });

      var grpTools = new LinkGroup
      {
        DisplayName = "Tools",
      };

      foreach (var toolAddIn in ReviveThisApplication.Default.AddIns.Tools)
      {
        grpTools.Links.Add(new Link { DisplayName = toolAddIn.Name, Source = new Uri(toolAddIn.Guid.ToString(), UriKind.RelativeOrAbsolute) });
      }
      
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

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChangedEventHandler handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}