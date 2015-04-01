using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using ReviveThis.Annotations;
using ReviveThis.Entities;
using ReviveThis.Interfaces;
using ReviveThis.Models;

namespace ReviveThis.ViewModels
{
  internal sealed class AddInViewModel : INotifyPropertyChanged
  {
    private CollectionView _items;

    public CollectionView Items
    {
      get { return _items; }
      protected set
      {
        _items = value;
        OnPropertyChanged();
      }
    }

    private AddInInformation _selectedAddIn;
// ReSharper disable once ConvertToAutoProperty
    public AddInInformation SelectedItem
    {
      get { return _selectedAddIn; }
      set 
      { 
        _selectedAddIn = value;
        //OnPropertyChanged("SelectedDescription");
      }
    }

    #region Construction

    public AddInViewModel()
    {
      _items =
        (CollectionView)
          CollectionViewSource.GetDefaultView(
            ReviveThisApplication.Default.AddIns.Items
              .Select(s => new AddInInformation(s))
              .OrderBy(ob => ob.FileName));

      if (_items == null)
        return;

      Items = _items;

      var groupDescription = new PropertyGroupDescription("FileName");

      if (_items.GroupDescriptions != null)
        _items.GroupDescriptions.Add(groupDescription);

      var first = _items.Cast<AddInInformation>().FirstOrDefault();
      if (first != null)
        SelectedItem = first;
    }

    #endregion

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      var handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }
  }

  

}