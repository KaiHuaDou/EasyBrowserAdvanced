using System.Windows;

namespace 极简浏览器;

public partial class Report : Window
{
    public Report(string message)
    {
        InitializeComponent( );
        msgBox.Text = message;
    }

    private void ShutdownClick(object o, RoutedEventArgs e)
        => Application.Current.Shutdown( );
}
