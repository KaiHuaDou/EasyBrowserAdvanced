using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows;
using Microsoft.Win32;
using 极简浏览器.Api;

namespace 极简浏览器;

public partial class WebSource : Window, IDisposable
{
    private int Id;
    private CancellationTokenSource cancellation = new( );

    public WebSource(int id)
    {
        InitializeComponent( );
        Id = id;
        RefreshSource(null, null);
    }

    private async void RefreshSource(object o, RoutedEventArgs e)
        => sourceBox.Text = await Instance.PageSourceAsync(Id);

    private async void FormatSource(object o, RoutedEventArgs e)
    {
        formatButton.IsEnabled = false;
        string result = await Formatter.FormatAsync(sourceBox.Text, cancellation.Token);
        sourceBox.Text = result;
        formatButton.IsEnabled = true;
    }

    private void SaveSource(object o, RoutedEventArgs e)
    {
        SaveFileDialog dialog = new( )
        {
            DefaultExt = ".html",
            FileName = Instance.Core[Id].Title,
            AddExtension = true,
            Filter = "HTML 文件|.html"
        };
        if (dialog.ShowDialog( ) == true)
            File.WriteAllText(dialog.FileName, sourceBox.Text);
    }

    private void WindowClosing(object o, CancelEventArgs e)
        => cancellation.Cancel( );

    public void Dispose( )
    {
        cancellation.Dispose( );
        GC.SuppressFinalize(this);
    }
}
