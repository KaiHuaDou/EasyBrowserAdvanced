using System;
using System.Windows;
using CefSharp;
using 极简浏览器.Api;

namespace 极简浏览器
{
    partial class MainWindow : IDisposable
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
            FileApi.Write(BrowserCore.GetBrowser().Title, BrowserCore.GetBrowser( ).Address, FileType.BookMark);
        }
        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            Setting setting = new Setting( );
            setting.Show( );
        }
        private void ViewBookMark_Click(object sender, RoutedEventArgs e)
        {
            History history = new History( );
            history.Show( );
        }
        private void ViewSource_Click(object sender, RoutedEventArgs e)
        {
            StandardApi.ViewPageSource( );
        }
        private void ViewHistory_Click(object sender, RoutedEventArgs e)
        {
            History history = new History( );
            history.Show( );
        }
        private void NoLogs_Click(object sender, RoutedEventArgs e)
        {
            App.Program.arguments.isNotLogging = NoLogs.IsChecked;
        }
    }
}
