using System;
using System.Windows;
using 极简浏览器.Api;

namespace 极简浏览器;

public partial class Report : Window
{
    public Report(Exception ex)
    {
        InitializeComponent( );
        messageBox.Text += Logger.GenLog(ex);
    }

    private void ShutdownClick(object o, RoutedEventArgs e)
        => Application.Current.Shutdown( );
}
