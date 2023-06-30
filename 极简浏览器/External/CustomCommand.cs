using System;
using System.Windows.Input;

namespace 极简浏览器.External;

public class CustomCommand : ICommand
{
    public event EventHandler CanExecuteChanged = delegate { };
    private readonly Action _TargetExecuteMethod;
    private readonly Func<bool> _TargetCanExecuteMethod;

    public CustomCommand(Action executeMethod)
        => _TargetExecuteMethod = executeMethod;

    bool ICommand.CanExecute(object o)
        => _TargetCanExecuteMethod != null && _TargetCanExecuteMethod( );

    public void RaiseCanExecuteChanged( )
        => CanExecuteChanged(this, EventArgs.Empty);

    void ICommand.Execute(object o)
        => _TargetExecuteMethod?.Invoke( );
}
