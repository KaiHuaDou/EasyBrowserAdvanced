using System.Windows;
using System.Windows.Input;
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
    private void MuteClick(object o, Args e)
        => _ = 0;

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

    private void SubmenuClick(object sender, Args e)
        => SubmenuPopup.IsOpen = !SubmenuPopup.IsOpen;

    private void ShowSearchBox(object o = null, Args e = null)
        => SearchPanel.Visibility = Visibility.Visible;

    private void SearchPanelClose(object o, Args e)
    {
        SearchPanel.Visibility = Visibility.Collapsed;
        Browser.StopFinding(true);
    }

    private void SearchPervious(object o, Args e)
    {
        if (string.IsNullOrEmpty(SearchBox.Text))
            return;
        Browser.Find(Id, SearchBox.Text, false, false, true);
    }

    private void SearchNext(object o, Args e)
    {
        if (string.IsNullOrEmpty(SearchBox.Text))
            return;
        Browser.Find(Id, SearchBox.Text, true, false, true);
    }

    private void SearchBoxInput(object o, KeyEventArgs e)
    {
        if (string.IsNullOrEmpty(SearchBox.Text))
            Browser.StopFinding(true);
        else if (e.Key is not Key.ImeProcessed)
            SearchNext(o, e);
    }
}
