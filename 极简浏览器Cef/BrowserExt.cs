using System;
using System.Windows;
using CefSharp;
using 极简浏览器.Api;

namespace 极简浏览器
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private void About_Click(object o, RoutedEventArgs e) { new About().Show(); }
        private void NewPage_Click(object o, RoutedEventArgs e) { Instance.New(); }
        private void GoBack_Click(object o, RoutedEventArgs e) { Browser.GoBack(identity); }
        private void GoForward_Click(object o, RoutedEventArgs e) { Browser.GoForward(identity); }
        private void Help_Click(object o, RoutedEventArgs e) { new Help().Show(); }
        private void Refresh_Click(object o, RoutedEventArgs e) { Browser.Refresh(identity); }
        private void SetBookMark_Click(object o, RoutedEventArgs e)
        {
            Configer<Config>.Add(
                new Config(false, Browser.Title(identity), Browser.Address(identity), StdApi.LocalTime),
                FilePath.BookMark);
        }
        private void Setting_Click(object o, RoutedEventArgs e) { new Setting().Show(); }
        private void ViewSource_Click(object o, RoutedEventArgs e) { StdApi.ViewSource(identity); }
        private void History_Click(object o, RoutedEventArgs e) { new History().Show(); }
        private void DevTool_Click(object o, RoutedEventArgs e) { Browser.Core[identity].ShowDevTools(); }
        private void privateBox_Click(object o, RoutedEventArgs e) { App.Program.argus.IsPrivate = (bool)privateBox.IsChecked; }
        private void Topmost_Checked(object o, RoutedEventArgs e)
        {
            this.Topmost = !this.Topmost;
            App.Program.argus.IsTopmost = this.Topmost;
        }
    }
}
