using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using ReviveThis.Annotations;
using ReviveThis.Entities;
using ReviveThis.Models;

namespace ReviveThis.ViewModels
{
  public class ToolsViewModel : INotifyPropertyChanged
  {
    private Uri _selectedLink;
    

    #region Members

    #endregion

    #region Properties

    public LinkCollection Links
    {
      get
      {
        return
          new LinkCollection(
            ReviveThisApplication.Default.AddIns.Tools.Select(
              s => new Link {DisplayName = s.Name, Source = new Uri(s.Guid.ToString(), UriKind.RelativeOrAbsolute)}));
      }
    }

    //#region DefaultContent
    //private Uri _defaultContent;

    //public Uri DefaultContent
    //{
    //  get { return _defaultContent; }
    //  set
    //  {
    //    _defaultContent = value;
    //    OnPropertyChanged();
    //  }
    //}
    //#endregion


    public Uri SelectedLink
    {
      get
      {
        if (_selectedLink != null)
          return _selectedLink;

        //var first = Links.FirstOrDefault();

        //if (first == null) 
        //  return null;

        //_selectedLink = first.Source;
        return new Uri("/Content/ToolsDefault.xaml", UriKind.RelativeOrAbsolute); //_selectedLink;
      }
      set
      {
        _selectedLink = value;
        OnPropertyChanged();
      }
    }

    #endregion

    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChangedEventHandler handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}