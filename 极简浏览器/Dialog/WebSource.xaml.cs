using System.IO;
using System.Windows;
using Microsoft.Win32;
using 极简浏览器.Api;

namespace 极简浏览器;

public partial class WebSource : Window
{
    private int Id;
    private bool isLoading;

    public WebSource(int id)
    {
        InitializeComponent( );
        Id = id;
    }

    private void RefreshSource(object o, RoutedEventArgs e)
        => LoadSource( );
    private void WindowLoaded(object o, RoutedEventArgs e)
        => LoadSource( );

    private async void LoadSource( )
    {
        if (isLoading)
            return;
        isLoading = true;
        sourceBox.Text = await Instance.PageSourceAsync(Id);
        isLoading = false;
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
}
