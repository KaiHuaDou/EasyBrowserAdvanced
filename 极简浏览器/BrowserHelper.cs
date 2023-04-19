using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CefSharp;
using CefSharp.Wpf;
using Microsoft.Win32;
using 极简浏览器.Api;

namespace 极简浏览器;

/// <summary>
/// 荡来的浏览器辅助代码
/// 涉及：生命周期、右键菜单、新窗口、下载等
/// </summary>
public class CefLifeSpanHandler : ILifeSpanHandler
{
    public CefLifeSpanHandler( ) { }

    public bool DoClose(IWebBrowser chromiumWebBrowser, IBrowser browser) => !(browser.IsDisposed || browser.IsPopup);

    public void OnAfterCreated(IWebBrowser chromiumWebBrowser, IBrowser browser) { }
    public void OnBeforeClose(IWebBrowser chromiumWebBrowser, IBrowser browser) { }
    public bool OnBeforePopup(IWebBrowser chromiumWebBrowser,
        IBrowser browser, IFrame frame,
        string targetUrl, string targetFrameName,
        WindowOpenDisposition targetDisposition,
        bool userGesture, IPopupFeatures popupFeatures,
        IWindowInfo windowInfo, IBrowserSettings browserSettings,
        ref bool noJavascriptAccess, out IWebBrowser newBrowser)
    {
        EasyBrowserCore _browser = (EasyBrowserCore) chromiumWebBrowser;
        _browser.Dispatcher.Invoke(new Action(( ) =>
        {
            NewWindowEventArgs e = new(windowInfo, targetUrl);
            _browser.OnNewWindow(e);
        }));
        newBrowser = null;
        return true;
    }
}
public class EasyBrowserCore : ChromiumWebBrowser
{
    private readonly int Id;

    public EasyBrowserCore(int id) : base(null)
    {
        LifeSpanHandler = new CefLifeSpanHandler( );
        DownloadHandler = new DownloadHandler( );
        Id = id;
    }

    public EasyBrowserCore(int id, string url) : base(url)
    {
        LifeSpanHandler = new CefLifeSpanHandler( );
        DownloadHandler = new DownloadHandler( );
        Id = id;
    }

    public void OnNewWindow(NewWindowEventArgs e)
    {
        if (Instance.Host[Id].singleBox.IsChecked)
            Instance.Navigate(Id, e.Url);
        else Instance.New(e.Url);
    }
}
public class NewWindowEventArgs : EventArgs
{
    public IWindowInfo WindowInfo { get; set; }
    public string Url { get; set; }
    public NewWindowEventArgs(IWindowInfo windowInfo, string url)
    {
        WindowInfo = windowInfo;
        Url = url;
    }
}
internal sealed class DownloadHandler : IDownloadHandler
{
    private readonly Action<bool, DownloadItem> _downloadCallBackEvent;

    public void OnBeforeDownload(IWebBrowser webBrowser, IBrowser browser,
        DownloadItem item, IBeforeDownloadCallback callback)
    {
        if (callback.IsDisposed)
            return;
        _downloadCallBackEvent?.Invoke(false, item);
        string path = AskDownloadPath(item);
        if (path is null)
            return;
        new Download(item, path).Show( );
        item.IsInProgress = true;
    }

    public void OnDownloadUpdated(
        IWebBrowser webBrowser, IBrowser browser, DownloadItem item,
        IDownloadItemCallback callback)
        => _downloadCallBackEvent?.Invoke(true, item);

    private static string AskDownloadPath(DownloadItem item)
    {
        SaveFileDialog sfd = new( )
        {
            FileName = item.SuggestedFileName,
            Title = "下载文件 - 极简浏览器",
        };
        return sfd.ShowDialog( ) == true ? sfd.FileName : null;
    }
}

public class MenuHandler : IContextMenuHandler
{
    public MenuHandler(int id) => Id = id;

    public static Window MainWindow { get; set; }
    private readonly int Id;
    void IContextMenuHandler.OnBeforeContextMenu(
        IWebBrowser w,
        IBrowser b,
        IFrame f,
        IContextMenuParams p,
        IMenuModel m)
    { }

    bool IContextMenuHandler.OnContextMenuCommand(
        IWebBrowser w, IBrowser b, IFrame f, IContextMenuParams p,
        CefMenuCommand c, CefEventFlags e) => true;

    void IContextMenuHandler.OnContextMenuDismissed(
        IWebBrowser webBrowser,
        IBrowser browser,
        IFrame frame)
    {
        ChromiumWebBrowser core = (ChromiumWebBrowser) webBrowser;
        core.Dispatcher.Invoke(( ) => core.ContextMenu = null);
    }

    bool IContextMenuHandler.RunContextMenu(
        IWebBrowser webBrowser,
        IBrowser browser,
        IFrame frame,
        IContextMenuParams parameters,
        IMenuModel model,
        IRunContextMenuCallback callback)
    {
        ChromiumWebBrowser _browser = (ChromiumWebBrowser) webBrowser;
        _browser.Dispatcher.Invoke(( ) =>
        {
            ContextMenu menu = new( )
            {
                IsOpen = true,
                ItemsSource = new MenuItem[]
                {
                    new MenuItem { Header = "前进", Command = new CustomCommand(( ) => Instance.GoForward(Id)) },
                    new MenuItem { Header = "后退", Command = new CustomCommand(( ) => Instance.GoBack(Id)) },
                    new MenuItem { Header = "刷新", Command = new CustomCommand(( ) => Instance.Refresh(Id)) },
                    new MenuItem { Header = "新窗口", Command = new CustomCommand(( ) => Instance.New( )) },
                    new MenuItem { Header = "网页源代码", Command = new CustomCommand(( ) => Instance.ViewSource(Id)) },
                }
            };
            void handler(object o, RoutedEventArgs e)
            {
                menu.Closed -= handler;
                if (!callback.IsDisposed) callback.Cancel( );
            }
            menu.Closed += handler;
            _browser.ContextMenu = menu;
        });
        return true;
    }
}
public class CustomCommand : ICommand
{
    private readonly Action _TargetExecuteMethod;
    private readonly Func<bool> _TargetCanExecuteMethod;
    public event EventHandler CanExecuteChanged = delegate { };

    public CustomCommand(Action executeMethod)
        => _TargetExecuteMethod = executeMethod;

    bool ICommand.CanExecute(object o)
        => _TargetCanExecuteMethod != null && _TargetCanExecuteMethod( );

    public void RaiseCanExecuteChanged( )
        => CanExecuteChanged(this, EventArgs.Empty);

    void ICommand.Execute(object o)
        => _TargetExecuteMethod?.Invoke( );
}
