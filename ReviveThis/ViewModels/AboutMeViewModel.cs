using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using ReviveThis.Annotations;
using ReviveThis.Models;

namespace ReviveThis.ViewModels
{
  internal sealed class AboutMeViewModel : INotifyPropertyChanged
  {
    private readonly AboutModel _aboutModel;

    #region Construction
    /// Constructs the default instance of a AboutMeViewModel
    public AboutMeViewModel()
    {
      _aboutModel = new AboutModel();
    }

    #endregion

    public string Name
    {
      get { return _aboutModel.Name; }
    }

    public string CopyrightOwner
    {
      get { return _aboutModel.CopyrightOwner; }
    }

    public string CopyrightYear
    {
      get { return _aboutModel.CopyrightYear; }
    }

    public Version Version
    {
      get { return _aboutModel.Version; }
    }

    public ICommand NavigateTwitter
    {
      get { return new RelayCommand(o => System.Diagnostics.Process.Start("https://www.twitter.com/wdcossey")); }
    }

    public ICommand TogglePopup
    {
      get { return new RelayCommand(o => IsOpen = !IsOpen); }
    }

    private bool _isOpen;
    public bool IsOpen
    {
      get { return _isOpen; }
      set
      {
        if (_isOpen == value) return;
        _isOpen = value;
        OnPropertyChanged();
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChangedEventHandler handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}