using System.Windows;
using 极简浏览器.Api;

namespace 极简浏览器;

/// <summary>
/// 使用 RoutedEventArgs 的非主要的事件处理程序
/// </summary>
public partial class MainWindow : Window
{
    private void AboutClick(object o, RoutedEventArgs e)
        => new About( ).Show( );

    private void NewPageClick(object o, RoutedEventArgs e)
        => Browser.New( );

    private void GoBackClick(object o, RoutedEventArgs e)
        => Browser.GoBack(Id);

    private void GoForwardClick(object o, RoutedEventArgs e)
        => Browser.GoForward(Id);

    private void HelpClick(object o, RoutedEventArgs e)
        => new Help( ).Show( );

    private void RefreshClick(object o, RoutedEventArgs e)
        => Browser.Refresh(Id);

    private void SetBookMarkClick(object o, RoutedEventArgs e)
    {
        DataMgr<Config>.Add(
            new Config
            {
                Check = false,
                Title = Browser.Title(Id),
                Url = Browser.Address(Id),
                Time = StdApi.LocalTime
            }, FilePath.BookMark
        );
    }

    private void SettingClick(object o, RoutedEventArgs e)
        => new Setting( ).Show( );

    private void ViewSourceClick(object o, RoutedEventArgs e)
        => Browser.ViewSource(Id);

    private void HistoryClick(object o, RoutedEventArgs e)
        => new History( ).Show( );

    private void DevToolClick(object o, RoutedEventArgs e)
        => Browser.ShowDevTools(Id);

    private void PrivateModeChecked(object o, RoutedEventArgs e)
        => App.Program.Args.IsPrivate = privateBox.IsChecked;

    private void TopmostChecked(object o, RoutedEventArgs e)
    {
        Topmost = !Topmost;
        App.Program.Args.IsTopmost = Topmost;
    }
}
