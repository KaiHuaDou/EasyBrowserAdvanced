using System.ComponentModel;
using System.IO;
using System.Windows;
using 极简浏览器.Api;

namespace 极简浏览器;

public partial class Setting : Window
{
    public Setting( )
    {
        InitializeComponent( );
        MainPageBox.Text = App.Setting.Content[0].MainPage;
        SearchEngineBox.Text = App.Setting.Content[0].SearchEngine;
        CheckUaBox.IsChecked = App.Setting.Content[0].CheatUA;
    }

    private void OKClick(object o, RoutedEventArgs e)
    {

        App.Setting.Content.Clear( );
        App.Setting.Content.Add(new Config
        {
            MainPage = MainPageBox.Text,
            SearchEngine = SearchEngineBox.Text,
            CheatUA = (bool) CheckUaBox.IsChecked
        });
        Close( );
    }

    private void ClearCache(object o, RoutedEventArgs e)
    {
        foreach (string file in Directory.GetFiles(FilePath.Cache))
        { try { File.Delete(file); } catch { } }
        foreach (string file in Directory.GetFiles(FilePath.GPUCache))
        { try { File.Delete(file); } catch { } }
    }

    private void ClearLog(object o, RoutedEventArgs e)
    {
        foreach (string file in Directory.GetFiles(FilePath.Log))
        { try { File.Delete(file); } catch { } }
    }

    private void WindowClosing(object o, CancelEventArgs e)
        => App.Setting.Save( );
}
