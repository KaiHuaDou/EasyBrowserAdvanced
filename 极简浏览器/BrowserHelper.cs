using System;
using System.Linq;
using CefSharp;
using CefSharp.Wpf;
using Microsoft.Win32;
using 极简浏览器.Api;

namespace 极简浏览器;

/// <summary>
/// 浏览器辅助代码
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

public class LifeSpanHandler : CefSharp.Handler.LifeSpanHandler
{
    protected override bool OnBeforePopup(
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
        EasyBrowserCore core = (EasyBrowserCore) chromiumWebBrowser;
        core.Dispatcher.Invoke(( ) => core.OnNewWindow(new NewWindowEventArgs(windowInfo, targetUrl)));
        newBrowser = null;
        return true;
    }
}

public class DownloadHandler : IDownloadHandler
{
    private readonly Action<bool, DownloadItem> downloadCallBackEvent;

    public bool OnBeforeDownload(
        IWebBrowser chromiumWebBrowser,
        IBrowser browser,
        DownloadItem downloadItem,
        IBeforeDownloadCallback callback)
    {
        if (callback.IsDisposed)
            return false;
        downloadCallBackEvent?.Invoke(false, downloadItem);
        string path = AskDownloadPath(downloadItem);
        if (path is null)
            return false;
        new Download(downloadItem, path).Show( );
        downloadItem.IsInProgress = true;
        return true;
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
            AddExtension = true,
            DefaultExt = item.SuggestedFileName.Split('.').Last( ),
            Title = "下载文件 - 极简浏览器",
        };
        return sfd.ShowDialog( ) == true ? sfd.FileName : null;
    }

    public bool CanDownload(
        IWebBrowser chromiumWebBrowser,
        IBrowser browser,
        string url,
        string requestMethod)
        => true;
}
