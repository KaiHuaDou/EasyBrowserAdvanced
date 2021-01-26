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
        public CefLifeSpanHandler( )
        {

        }

        public bool DoClose(IWebBrowser chromiumWebBrowser, CefSharp.IBrowser browser)
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


        public bool OnBeforePopup(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            ExtChromiumBrowser chromiumWebBrowser1 = (ExtChromiumBrowser) chromiumWebBrowser;
            chromiumWebBrowser1.Dispatcher.Invoke(new Action(( ) =>
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
        public string Url { get; set; }
        public NewWindowEventArgs(IWindowInfo windowInfo, string url)
        {
            _windowInfo = windowInfo;
            this.Url = url;
        }
    }
    public class DownloadHandler : IDownloadHandler
    {
        public event EventHandler<DownloadItem> OnBeforeDownloadFired;
        public event EventHandler<DownloadItem> OnDownloadUpdatedFired;

        public void OnBeforeDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IBeforeDownloadCallback callback)
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

        public void OnDownloadUpdated(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
        {
            OnDownloadUpdatedFired?.Invoke(this, downloadItem);
        }
    }
    public class MenuHandler : IContextMenuHandler
    {
        public static Window mainWindow { get; set; }
        void IContextMenuHandler.OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
        {

        }

        bool IContextMenuHandler.OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
        {
            return true;
        }

        void IContextMenuHandler.OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame)
        {
            var chromiumWebBrowser = (ChromiumWebBrowser) browserControl;

            chromiumWebBrowser.Dispatcher.Invoke(( ) =>
            {
                chromiumWebBrowser.ContextMenu = null;
            });
        }

        bool IContextMenuHandler.RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
        {
           var chromiumWebBrowser = (ChromiumWebBrowser) browserControl;
            chromiumWebBrowser.Dispatcher.Invoke(( ) =>
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
                        callback.Cancel( );
                    }
                };
                menu.Closed += handler;
                menu.Items.Add(new MenuItem
                {
                    Header = "刷新",
                    Command = new CustomCommand(RefeshPage)
                });
                menu.Items.Add(new MenuItem
                {
                    Header = "最小化",
                    Command = new CustomCommand(MinWindow)
                });
                menu.Items.Add(new MenuItem
                {
                    Header = "关闭",
                    Command = new CustomCommand(CloseWindow)
                });
                chromiumWebBrowser.ContextMenu = menu;

            });

            return true;
        }
        private void CloseWindow( )
        {
            mainWindow.Dispatcher.Invoke(
                new Action(
                        delegate
                        {
                            Application.Current.Shutdown( );
                        }
                    ));
        }

        private void MinWindow( )
        {
            mainWindow.Dispatcher.Invoke(
                new Action(
                        delegate
                        {
                            mainWindow.WindowState = WindowState.Minimized;
                        }
                    ));
        }
        private void RefeshPage( )
        {
            mainWindow.Dispatcher.Invoke(
                new Action(
                        delegate
                        {
                            BrowserCore.Refresh( );
                        }
                    ));
        }

        private static IEnumerable<Tuple<string, CefMenuCommand>> GetMenuItems(IMenuModel model)
        {
            var list = new List<Tuple<string, CefMenuCommand>>( );
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
                return _TargetCanExecuteMethod( );
            }
            if (_TargetExecuteMethod != null)
            {
                return true;
            }
            return false;
        }

        public void RaiseCanExecuteChanged( )
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        void ICommand.Execute(object parameter)
        {
            _TargetExecuteMethod?.Invoke( );
        }
    }
}