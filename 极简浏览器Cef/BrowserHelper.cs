using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CefSharp;
using CefSharp.Wpf;
using Microsoft.Win32;
using 极简浏览器.Api;

namespace 极简浏览器
{
    public class CefLifeSpanHandler : ILifeSpanHandler
    {
        public CefLifeSpanHandler()
        {

        }

        public bool DoClose(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
            if (browser.IsDisposed || browser.IsPopup)
            {
                return false;
            }
            return true;
        }

        public void OnAfterCreated(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
        }

        public void OnBeforeClose(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
        }


        public bool OnBeforePopup(IWebBrowser chromiumWebBrowser,
            IBrowser browser, IFrame frame,
            string targetUrl, string targetFrameName,
            WindowOpenDisposition targetDisposition,
            bool userGesture, IPopupFeatures popupFeatures,
            IWindowInfo windowInfo, IBrowserSettings browserSettings,
            ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            ExtChromiumBrowser chromiumWebBrowser1 = (ExtChromiumBrowser)chromiumWebBrowser;
            chromiumWebBrowser1.Dispatcher.Invoke(new Action(() =>
            {
                NewWindowEventArgs e = new NewWindowEventArgs(windowInfo, targetUrl);
                chromiumWebBrowser1.OnNewWindow(e);
            }));
            newBrowser = null;
            return true;
        }
    }
    public class ExtChromiumBrowser : ChromiumWebBrowser
    {
        public ExtChromiumBrowser(int id) : base(null)
        {
            this.LifeSpanHandler = new CefLifeSpanHandler();
            this.DownloadHandler = new DownloadHandler();
            identity = id;
        }

        public ExtChromiumBrowser(int id, string url) : base(url)
        {
            this.LifeSpanHandler = new CefLifeSpanHandler();
            this.DownloadHandler = new DownloadHandler( );
            identity = id;
        }
        public static int identity;
        public void OnNewWindow(NewWindowEventArgs e)
        {
            if (Browser.Host[identity].singleBox.IsChecked != true)
                Browser.New(e.Url);
            else
                Browser.Navigate(identity, e.Url);
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
        public string Url { get; set; }
        public NewWindowEventArgs(IWindowInfo windowInfo, string url)
        {
            _windowInfo = windowInfo;
            this.Url = url;
        }
    }
    internal class DownloadHandler : IDownloadHandler
    {
        private readonly Action<bool, DownloadItem> _downloadCallBackEvent;

        public void OnBeforeDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem,
            IBeforeDownloadCallback callback)
        {
            if (callback.IsDisposed) return;

            _downloadCallBackEvent?.Invoke(false, downloadItem);
            var path = AskDownloadPath(downloadItem);
            if (path != null)
            {
                Download d = new Download(downloadItem, path);
                d.Show();
                downloadItem.IsInProgress = true;
            }
            //callback.Continue(path, false);
        }


        public void OnDownloadUpdated(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem,
            IDownloadItemCallback callback)
        {
            _downloadCallBackEvent?.Invoke(true, downloadItem);
        }


        private static string AskDownloadPath(DownloadItem item)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = item.SuggestedFileName;
            sfd.Title = "下载文件";
            bool? dr = sfd.ShowDialog();
            if (dr == true)
            {
                return sfd.FileName;
            }
            return null;
        }
    }

    public class MenuHandler : IContextMenuHandler
    {
        public MenuHandler(int id)
        {
            identity = id;
        }

        public static Window mainWindow { get; set; }
        public static int identity;
        void IContextMenuHandler.OnBeforeContextMenu(
            IWebBrowser browserControl,
            IBrowser browser, IFrame frame,
            IContextMenuParams parameters,
            IMenuModel model)
        {

        }

        bool IContextMenuHandler.OnContextMenuCommand(
            IWebBrowser browserControl,
            IBrowser browser,
            IFrame frame,
            IContextMenuParams parameters,
            CefMenuCommand commandId,
            CefEventFlags eventFlags)
        {
            return true;
        }

        void IContextMenuHandler.OnContextMenuDismissed(
            IWebBrowser browserControl,
            IBrowser browser,
            IFrame frame)
        {
            var chromiumWebBrowser = (ChromiumWebBrowser)browserControl;

            chromiumWebBrowser.Dispatcher.Invoke(() =>
            {
                chromiumWebBrowser.ContextMenu = null;
            });
        }

        bool IContextMenuHandler.RunContextMenu(
            IWebBrowser browserControl,
            IBrowser browser,
            IFrame frame,
            IContextMenuParams parameters,
            IMenuModel model,
            IRunContextMenuCallback callback)
        {
            var chromiumWebBrowser = (ChromiumWebBrowser)browserControl;
            chromiumWebBrowser.Dispatcher.Invoke(() =>
            {
                var menu = new ContextMenu
                {
                    IsOpen = true
                };
                RoutedEventHandler handler = null;
                handler = (s, e) =>
                {
                    menu.Closed -= handler;
                    if (!callback.IsDisposed)
                    {
                        callback.Cancel();
                    }
                };
                menu.Closed += handler;
                menu.Items.Add(new MenuItem
                {
                    Header = "前进",
                    Command = new CustomCommand(( ) => { Browser.GoForward(identity); })
                });
                menu.Items.Add(new MenuItem
                {
                    Header = "后退",
                    Command = new CustomCommand(( ) => { Browser.GoBack(identity); })
                });
                menu.Items.Add(new MenuItem
                {
                    Header = "刷新",
                    Command = new CustomCommand(()=> { Browser.Refresh(identity); })
                });
                menu.Items.Add(new MenuItem
                {
                    Header = "新窗口",
                    Command = new CustomCommand(() => { Browser.New(); })
                });
                menu.Items.Add(new MenuItem
                {
                    Header = "网页源代码",
                    Command = new CustomCommand(()=> { StdApi.ViewSource(identity); })
                });
                chromiumWebBrowser.ContextMenu = menu;

            });

            return true;
        }
        private static IEnumerable<Tuple<string, CefMenuCommand>> GetMenuItems(IMenuModel model)
        {
            var list = new List<Tuple<string, CefMenuCommand>>();
            for (var i = 0; i < model.Count; i++)
            {
                var header = model.GetLabelAt(i);
                var commandId = model.GetCommandIdAt(i);
                list.Add(new Tuple<string, CefMenuCommand>(header, commandId));
            }
            return list;
        }
    }
    public class CustomCommand : ICommand
    {
        Action _TargetExecuteMethod;
        Func<bool> _TargetCanExecuteMethod;
        public event EventHandler CanExecuteChanged = delegate { };

        public CustomCommand(Action executeMethod)
        {
            _TargetExecuteMethod = executeMethod;
        }

        bool ICommand.CanExecute(object parameter)
        {
            if (_TargetCanExecuteMethod != null)
            {
                return _TargetCanExecuteMethod();
            }
            if (_TargetExecuteMethod != null)
            {
                return true;
            }
            return false;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        void ICommand.Execute(object parameter)
        {
            _TargetExecuteMethod?.Invoke();
        }
    }
}