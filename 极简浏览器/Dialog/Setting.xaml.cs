using System.IO;
using System.Windows;
using System.Windows.Controls;
using 极简浏览器.Api;
using 极简浏览器.Resources;

namespace 极简浏览器;

public partial class Setting : Window
{
    public Setting( )
    {
        InitializeComponent( );
        MainPageBox.Text = App.Setting.Content[0].MainPage;
        SearchEngineBox.Text = App.Setting.Content[0].SearchEngine;
        CheckUaBox.IsChecked = App.Setting.Content[0].CheatUA;
        UIThemeBox.SelectedIndex = (int) App.Setting.Content[0].UITheme;
    }

    private void OKClick(object o, RoutedEventArgs e)
    {

        App.Setting.Content.Clear( );
        App.Setting.Content.Add(new Config
        {
            MainPage = MainPageBox.Text,
            SearchEngine = SearchEngineBox.Text,
            CheatUA = (bool) CheckUaBox.IsChecked,
            UITheme = (UIThemes) UIThemeBox.SelectedIndex
        });
        App.Setting.Save( );
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

    private void UIThemeBoxSelectionChanged(object o, SelectionChangedEventArgs e)
    {
        if (UIThemeText is null || UIThemeBox is null) return;
        UIThemeText.Content = GuiText.ResourceManager.GetString($"UITheme.{UIThemeBox.SelectedValue}");
    }

    private void WindowLoaded(object o, RoutedEventArgs e)
        => UIThemeBoxSelectionChanged(null, null);
}
