using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows;
using Microsoft.Win32;
using 极简浏览器.Api;

namespace 极简浏览器;

public partial class WebSource : Window
{
    private int Id;
    private Thread formatter = null;

    public WebSource(int id)
    {
        InitializeComponent( );
        Id = id;
        RefreshSource(null, null);
    }

    private async void RefreshSource(object o, RoutedEventArgs e)
        => sourceBox.Text = await Instance.PageSourceAsync(Id);

    private void FormatSource(object o, RoutedEventArgs e)
    {
        formatter = new((source) =>
        {
            string result = Formatter.Format((string) source);
            Dispatcher.Invoke(( ) =>
            {
                sourceBox.Text = result;
                formatButton.IsEnabled = true;
            });
        });
        formatter.Start(sourceBox.Text);
        formatButton.IsEnabled = false;
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
        => formatter?.Abort( );
}
