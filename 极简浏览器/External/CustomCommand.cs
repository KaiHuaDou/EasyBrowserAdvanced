using System;
using System.Windows.Input;

namespace 极简浏览器.External;

public class CustomCommand(Action executeMethod) : ICommand
{
    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter) => true;

    public void RaiseCanExecuteChanged( ) { }

    public void Execute(object parameter) => executeMethod?.Invoke( );
}
