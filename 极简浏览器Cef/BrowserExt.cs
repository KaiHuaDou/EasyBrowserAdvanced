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
            Instance.New("about:blank");
        }
        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            Browser.GoBack( );
        }
        private void GoForwardButton_Click(object sender, RoutedEventArgs e)
        {
            Browser.GoForward( );
        }
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            Help help = new Help( );
            help.Show( );
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Browser.Refresh( );
        }
        private void SetBookMark_Click(object sender, RoutedEventArgs e)
        {
            Configer.AddConfig(
                new Config(false, Browser.Core.Title, 
                Browser.Core.Address, StdApi.LocalTime),
                FilePath.BookMark);
        }
        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            Setting setting = new Setting( );
            setting.Show( );
        }
        private void ViewSource_Click(object sender, RoutedEventArgs e)
        {
            StdApi.ViewSource( );
        }
        private void ViewHistory_Click(object sender, RoutedEventArgs e)
        {
            History history = new History( );
            history.Show( );
        }
        private void NoLogs_Click(object sender, RoutedEventArgs e)
        {
            App.Program.arguments.IsStopLog = (bool)NoLogs.IsChecked;
        }
        private void Topmost_Checked(object sender, RoutedEventArgs e)
        {
            this.Topmost = !this.Topmost;
        }

        private void DevToolsButton_Click(object sender, RoutedEventArgs e)
        {
            Browser.Core.ShowDevTools();
        }
    }
}
