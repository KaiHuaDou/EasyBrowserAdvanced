using System.IO;
using System.Threading;
using System.Windows;
using Microsoft.Win32;
using 极简浏览器.Api;

namespace 极简浏览器;

public partial class WebSource : Window
{
    private int Id;

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
        new Thread((html) =>
        {
            string result = Formatter.FormartHtml((string) html, true);
            Dispatcher.Invoke(( ) =>
            {
                try { sourceBox.Text = result; } catch { }
            });
        }).Start(sourceBox.Text);
    }

    private void SaveSource(object o, RoutedEventArgs e)
    {
        SaveFileDialog dialog = new( )
        {
            DefaultExt = ".html",
            FileName = Instance.Title(Id),
            AddExtension = true,
            Filter = "HTML 文件|.html"
        };
        if (dialog.ShowDialog( ) == true)
            File.WriteAllText(dialog.FileName, sourceBox.Text);
    }
}
