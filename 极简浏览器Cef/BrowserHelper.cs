using System;
using CefSharp;
using CefSharp.Wpf;
using Microsoft.Win32;

public class CefLifeSpanHandler : ILifeSpanHandler
{
    public CefLifeSpanHandler( )
    {

    }

    public bool DoClose(IWebBrowser browserControl, CefSharp.IBrowser browser)
    {
        if (browser.IsDisposed || browser.IsPopup)
        {
            return false;
        }

        return true;
    }

    public void OnAfterCreated(IWebBrowser browserControl, IBrowser browser)
    {

    }

    public void OnBeforeClose(IWebBrowser browserControl, IBrowser browser)
    {
    }


    public bool OnBeforePopup(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
    {
        ExtChromiumBrowser chromiumWebBrowser = (ExtChromiumBrowser) browserControl;
        chromiumWebBrowser.Dispatcher.Invoke(new Action(( ) =>
        {
            NewWindowEventArgs e = new NewWindowEventArgs(windowInfo, targetUrl);
            chromiumWebBrowser.OnNewWindow(e);
        }));

        newBrowser = null;
        return true;
    }
}
public class ExtChromiumBrowser : ChromiumWebBrowser
{
    public ExtChromiumBrowser( ) : base(null)
    {
        this.LifeSpanHandler = new CefLifeSpanHandler( );
        this.DownloadHandler = new DownloadHandler( );
    }

    public ExtChromiumBrowser(string url) : base(url)
    {
        this.LifeSpanHandler = new CefLifeSpanHandler( );
    }

    public event EventHandler<NewWindowEventArgs> StartNewWindow;
    public void OnNewWindow(NewWindowEventArgs e)
    {
        StartNewWindow?.Invoke(this, e);
    }
}
public class NewWindowEventArgs : EventArgs
{
    private IWindowInfo _windowInfo;
    public IWindowInfo WindowInfo
    {
        get { return _windowInfo; }
        set { value = _windowInfo; }
    }
    public string url { get; set; }
    public NewWindowEventArgs(IWindowInfo windowInfo, string url)
    {
        _windowInfo = windowInfo;
        this.url = url;
    }
}
public class DownloadHandler : IDownloadHandler
{
    public delegate void AlarmEventHandler(object sender, EventArgs e);
    public event EventHandler<DownloadItem> OnBeforeDownloadFired;
    public event EventHandler<DownloadItem> OnDownloadUpdatedFired;

    public void OnBeforeDownload(IWebBrowser browser, IBrowser browser2, DownloadItem downloadItem, IBeforeDownloadCallback callback)
    {
        OnBeforeDownloadFired?.Invoke(this, downloadItem);
        if (!callback.IsDisposed)
        {
            using (callback)
            {
                callback.Continue(downloadItem.SuggestedFileName, showDialog: true);
            }
        }
    }

    public void OnDownloadUpdated(IWebBrowser browser, IBrowser browser2, DownloadItem downloadItem, IDownloadItemCallback callback)
    {
        OnDownloadUpdatedFired?.Invoke(this, downloadItem);
    }
}

