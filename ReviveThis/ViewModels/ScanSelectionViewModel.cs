using System;
using System.Reflection;
using System.Windows.Input;
using System.Windows.Navigation;
using FirstFloor.ModernUI.Windows.Navigation;

namespace ReviveThis.ViewModels
{
  public class ScanSelectionViewModel
  {
    public ScanSelectionViewModel()
    {
        _canExecute = true;
    }

    private ICommand _clickCommand;
    public ICommand ScanAndLogCommand
    {
        get
        {
            return _clickCommand ?? (_clickCommand = new CommandHandler(ScanAndLogAction, _canExecute));
        }
    }

    public ICommand ScanOnlyCommand
    {
      get
      {
        return _clickCommand ?? (_clickCommand = new CommandHandler(ScanOnlyAction, _canExecute));
      }
    }

    private readonly bool _canExecute;

    public void ScanAndLogAction(object parameter)
    {
      
      //Messenger.Default.Send(new UpdateScanContent(typeof(ScanResults)));


      //var x = new FirstFloor.ModernUI.Windows.Controls.ModernDialog();
      //x.Title = "Title";
      //x.Buttons = new Button[] { x.YesButton, x.NoButton };
      //x.YesButton.Visibility = Visibility.Visible;

      //x.Content = "Message content";
      //x.ShowDialog();
    }

    public void ScanOnlyAction(object parameter)
    {
      //var x = new FirstFloor.ModernUI.Windows.Controls.ModernDialog();
      //x.Title = "Title";
      //x.Buttons = new Button[] { x.YesButton, x.NoButton };
      //x.YesButton.Visibility = Visibility.Visible;

      //x.Content = "Message content";
      //x.ShowDialog();
    }

  }

  public class CommandHandler : ICommand
  {
    private Action<object> _action;
    private bool _canExecute;

    public CommandHandler(Action<object> action, bool canExecute)
    {
      _action = action;
      _canExecute = canExecute;
    }

    public bool CanExecute(object parameter)
    {
      return _canExecute;
    }

    public event EventHandler CanExecuteChanged;

    public void Execute(object parameter)
    {
      _action(parameter);
    }
  }
}