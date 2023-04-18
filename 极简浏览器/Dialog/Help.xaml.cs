using System.Windows;

namespace 极简浏览器;

public partial class Help : Window
{
    public Help( )
    {
        InitializeComponent( );
    }

    private void CloseWindow(object sender, RoutedEventArgs e)
        => Close( );
}
