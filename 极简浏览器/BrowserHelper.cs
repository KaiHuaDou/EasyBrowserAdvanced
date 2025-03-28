using System;
using System.Windows;
using System.Windows.Controls;
using CefSharp;
using CefSharp.Wpf;
using Microsoft.Win32;
using 极简浏览器.Api;

namespace 极简浏览器;

/// <summary>
/// 浏览器辅助代码
/// 涉及：生命周期、右键菜单、新窗口、下载等
/// </summary>

public class EasyBrowserCore : ChromiumWebBrowser
{
    private readonly int Id;

    public EasyBrowserCore(int id, string url = null) : base(url)
    {
        LifeSpanHandler = new LifeSpanHandler( );
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

public class NewWindowEventArgs(IWindowInfo windowInfo, string url) : EventArgs
{
    public IWindowInfo WindowInfo { get; set; } = windowInfo;
    public string Url { get; set; } = url;
}

public class LifeSpanHandler : ILifeSpanHandler
{
    public LifeSpanHandler( ) { }

    public bool DoClose(IWebBrowser chromiumWebBrowser, IBrowser browser)
        => !(browser.IsDisposed || browser.IsPopup);

    public void OnAfterCreated(IWebBrowser chromiumWebBrowser, IBrowser browser) { }

    public void OnBeforeClose(IWebBrowser chromiumWebBrowser, IBrowser browser) { }

    public bool OnBeforePopup(
        IWebBrowser chromiumWebBrowser,
        IBrowser browser,
        IFrame frame,
        string targetUrl,
        string targetFrameName,
        WindowOpenDisposition targetDisposition,
        bool userGesture,
        IPopupFeatures popupFeatures,
        IWindowInfo windowInfo,
        IBrowserSettings browserSettings,
        ref bool noJavascriptAccess,
        out IWebBrowser newBrowser)
    {
        EasyBrowserCore _core = (EasyBrowserCore) chromiumWebBrowser;
        _core.Dispatcher.Invoke(( ) => _core.OnNewWindow(new NewWindowEventArgs(windowInfo, targetUrl)));
        newBrowser = null;
        return true;
    }
}

public class DownloadHandler : IDownloadHandler
{
    private readonly Action<bool, DownloadItem> downloadCallBackEvent;

    public void OnBeforeDownload(
        IWebBrowser chromiumWebBrowser, IBrowser browser,
        DownloadItem downloadItem, IBeforeDownloadCallback callback)
    {
        if (callback.IsDisposed) return;
        downloadCallBackEvent?.Invoke(false, downloadItem);
        string path = AskDownloadPath(downloadItem);
        if (path is null) return;
        new Download(downloadItem, path).Show( );
        downloadItem.IsInProgress = true;
    }

    public void OnDownloadUpdated(
        IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem,
        IDownloadItemCallback callback)
        => downloadCallBackEvent?.Invoke(true, downloadItem);

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

public class MenuHandler(int id) : IContextMenuHandler
{
    public static Window MainWindow { get; set; }

    void IContextMenuHandler.OnContextMenuDismissed(
        IWebBrowser webBrowser,
        IBrowser browser,
        IFrame frame)
    {
        ChromiumWebBrowser core = (ChromiumWebBrowser) webBrowser;
        core.Dispatcher.Invoke(( ) => core.ContextMenu = null);
    }

    bool IContextMenuHandler.RunContextMenu(
        IWebBrowser webBrowser, IBrowser browser, IFrame frame,
        IContextMenuParams parameters, IMenuModel model,
        IRunContextMenuCallback callback)
    {
        ChromiumWebBrowser _core = (ChromiumWebBrowser) webBrowser;
        _core.Dispatcher.Invoke(( ) =>
        {
            ContextMenu menu = new( )
            {
                IsOpen = true,
                ItemsSource = Instance.ContextMenu(id)
            };
            void handler(object o, RoutedEventArgs e)
            {
                menu.Closed -= handler;
                if (!callback.IsDisposed) callback.Cancel( );
            }
            menu.Closed += handler;
            _core.ContextMenu = menu;
        });
        return true;
    }

    public void OnBeforeContextMenu(
        IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame,
        IContextMenuParams parameters, IMenuModel model) => _ = true;
    public bool OnContextMenuCommand(
        IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IContextMenuParams parameters,
        CefMenuCommand commandId, CefEventFlags eventFlags) => true;
}
