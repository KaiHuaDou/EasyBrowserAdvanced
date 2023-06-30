using System.Windows;
using CefSharp;
using 极简浏览器.Api;
using Args = System.Windows.RoutedEventArgs;

namespace 极简浏览器;

/// <summary>
/// 事件处理程序
/// </summary>
public partial class MainWindow : Window
{
    private void AboutClick(object o, Args e) => new About( ).Show( );
    private void DevToolsClick(object o, Args e) => Instance.Core[Id].ShowDevTools( );
    private void GoBackClick(object o, Args e) => Instance.GoBack(Id);
    private void GoForwardClick(object o, Args e) => Instance.GoForward(Id);
    private void HelpClick(object o, Args e) => new Help( ).Show( );
    private void HistoryClick(object o, Args e) => new History( ).Show( );
    private void NewPageClick(object o, Args e) => Instance.New( );
    private void RefreshClick(object o, Args e) => Instance.Refresh(Id);
    private void SettingClick(object o, Args e) => new Setting( ).Show( );
    private void TopmostClick(object o, Args e) => Topmost = !Topmost;
    private void ViewSourceClick(object o, Args e) => Instance.ViewSource(Id);

    private void SetBookMarkClick(object o, Args e)
    {
        App.Bookmark.Content.Add(
            new Record
            {
                Check = false,
                Title = Instance.Core[Id].Title,
                Url = Instance.Core[Id].Address,
                Time = Utils.LocalTime
            }
        );
    }
}
