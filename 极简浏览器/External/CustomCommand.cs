using System;
using System.Windows.Input;

namespace 极简浏览器.External;

public class CustomCommand : ICommand
{
    public event EventHandler CanExecuteChanged = delegate { };
    private readonly Action TargetExecuteMethod;
    private readonly Func<bool> TargetCanExecuteMethod;

    public CustomCommand(Action executeMethod)
        => TargetExecuteMethod = executeMethod;

    bool ICommand.CanExecute(object o)
        => TargetCanExecuteMethod != null && TargetCanExecuteMethod( );

    public void RaiseCanExecuteChanged( )
        => CanExecuteChanged(this, EventArgs.Empty);

    void ICommand.Execute(object o)
        => TargetExecuteMethod?.Invoke( );
}
