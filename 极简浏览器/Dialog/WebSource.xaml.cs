using System.IO;
using System.Threading;
using System.Windows;
using Microsoft.Win32;
using 极简浏览器.Api;

namespace 极简浏览器;

public partial class WebSource : Window
{
    public int Id;

    public WebSource(int id, string text)
    {
        InitializeComponent( );
        textBox.Text = text;
        Id = id;
    }

    private void RefreshSource(object o, RoutedEventArgs e) => textBox.Text = Browser.PageSource(Id);

    private void FormatSource(object o, RoutedEventArgs e)
    {
        new Thread((html) =>
        {
            string result = Formatter.FormartHtml((string) html, true);
            Dispatcher.Invoke(( ) =>
            {
                try { textBox.Text = result; } catch { }
            });
        }).Start(textBox.Text);
    }

    private void SaveSource(object o, RoutedEventArgs e)
    {
        SaveFileDialog dialog = new( )
        {
            DefaultExt = ".html",
            FileName = Browser.Title(Id),
            AddExtension = true,
            Filter = "HTML 文件|.html"
        };
        if (dialog.ShowDialog( ) == true)
            File.WriteAllText(dialog.FileName, textBox.Text);
    }
}
