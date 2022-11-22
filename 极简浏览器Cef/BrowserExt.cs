using System;
using System.Windows;
using CefSharp;
using 极简浏览器.Api;

namespace 极简浏览器
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
        private void About_Click(object sender, RoutedEventArgs e)
        {
            About about = new About( );
            about.Show( );
        }
        private void AddNewPageButton_Click(object sender, RoutedEventArgs e)
        {
            NewInstance.StartNewInstance("about:blank");
        }
        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            BrowserCore.GoBack( );
        }
        private void GoForwardButton_Click(object sender, RoutedEventArgs e)
        {
            BrowserCore.GoForward( );
        }
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            Help help = new Help( );
            help.Show( );
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            BrowserCore.Refresh( );
        }
        private void SetBookMark_Click(object sender, RoutedEventArgs e)
        {
            ConfigHelper.AddConfig(
                new ConfigData(false, BrowserCore.CefBrowser.Title, 
                BrowserCore.CefBrowser.Address, StdApi.LocalTime),
                FilePath.BookMarkPath);
        }
        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            Setting setting = new Setting( );
            setting.Show( );
        }
        private void ViewSource_Click(object sender, RoutedEventArgs e)
        {
            StdApi.ViewPageSource( );
        }
        private void ViewHistory_Click(object sender, RoutedEventArgs e)
        {
            History history = new History( );
            history.Show( );
        }
        private void NoLogs_Click(object sender, RoutedEventArgs e)
        {
            App.Program.arguments.isNotLogging = (bool)NoLogs.IsChecked;
        }
        private void Topmost_Checked(object sender, RoutedEventArgs e)
        {
            this.Topmost = !this.Topmost;
        }

        private void DevToolsButton_Click(object sender, RoutedEventArgs e)
        {
            BrowserCore.CefBrowser.ShowDevTools();
        }

        private void ExtensionsButton_Click(object sender, RoutedEventArgs e)
        { }

        private void RunJSButton_Click(object sender, RoutedEventArgs e)
        {
            RunJavaScript rj = new RunJavaScript();
            rj.Show();
        }
    }
}
