using System.Windows;
using 极简浏览器.Api;

namespace 极简浏览器
{
    /// <summary>
    /// 使用 RoutedEventArgs 的非主要的事件处理程序
    /// </summary>
    public partial class MainWindow : Window
    {
        private void About_Click(object o, RoutedEventArgs e) { new About().Show(); }
        private void NewPage_Click(object o, RoutedEventArgs e) { Browser.New(); }
        private void GoBack_Click(object o, RoutedEventArgs e) { Browser.GoBack(Id); }
        private void GoForward_Click(object o, RoutedEventArgs e) { Browser.GoForward(Id); }
        private void Help_Click(object o, RoutedEventArgs e) { new Help().Show(); }
        private void Refresh_Click(object o, RoutedEventArgs e) { Browser.Refresh(Id); }
        private void SetBookMark_Click(object o, RoutedEventArgs e)
        {
            Configer<Config>.Add(
                new Config(false, Browser.Title(Id), Browser.Address(Id), StdApi.LocalTime),
                FilePath.BookMark);
        }
        private void Setting_Click(object o, RoutedEventArgs e) { new Setting().Show(); }
        private void ViewSource_Click(object o, RoutedEventArgs e) { Browser.ViewSource(Id); }
        private void History_Click(object o, RoutedEventArgs e) { new History().Show(); }
        private void DevTool_Click(object o, RoutedEventArgs e) { Browser.ShowDevTools(Id); }
        private void PrivateMode_Checked(object o, RoutedEventArgs e) { App.Program.Args.IsPrivate = (bool)privateBox.IsChecked; }
        private void Topmost_Checked(object o, RoutedEventArgs e)
        {
            this.Topmost = !this.Topmost;
            App.Program.Args.IsTopmost = this.Topmost;
        }
    }
}
