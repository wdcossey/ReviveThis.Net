using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using ReviveThis.Entities;
using ReviveThis.Entities.Modules;
using ReviveThis.Net.Tool.HostsManager.Annotations;


namespace ReviveThis.Net.Tool.HostsManager.ViewModels
{
  public class HostsManagerViewModel : INotifyPropertyChanged
  {
    private HostsFileReader _hostsFile;

    public HostsManagerViewModel()
    {
      if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
      {
        LoadHostsFile();
      }
    }

    private async void LoadHostsFile()
    {
      HostsFileReader = await Hosts.ListHostsFile();
    }

    public HostsFileReader HostsFileReader
    {
      get { return _hostsFile; }
      set
      {
        _hostsFile = value;
        OnPropertyChanged();
        OnPropertyChanged("HostsContent");
      }
    }

    public ObservableCollection<string> HostsContent
    {
      get
      {
        return _hostsFile == null ? null : new ObservableCollection<string>(_hostsFile.Lines);
      }
      set
      {
        if (_hostsFile == null)
          return;

        _hostsFile.Lines = value.ToArray();
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChangedEventHandler handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}