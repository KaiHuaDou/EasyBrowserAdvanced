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
    /// <summary>
    /// 荡来的浏览器辅助代码
    /// 涉及：生命周期、右键菜单、新窗口、下载等
    /// </summary>
    public class CefLifeSpanHandler : ILifeSpanHandler
    {
        public CefLifeSpanHandler( ) { }

        public bool DoClose(IWebBrowser webBrowser, IBrowser browser)
        {
            if (browser.IsDisposed || browser.IsPopup)
            {
                return false;
            }
            return true;
        }

        public void OnAfterCreated(IWebBrowser webBrowser, IBrowser browser) { }
        public void OnBeforeClose(IWebBrowser webBrowser, IBrowser browser) { }
        public bool OnBeforePopup(IWebBrowser webBrowser,
            IBrowser browser, IFrame frame,
            string url, string frameName,
            WindowOpenDisposition disposition,
            bool gesture, IPopupFeatures features,
            IWindowInfo info, IBrowserSettings settings,
            ref bool noJs, out IWebBrowser newBrowser)
        {
            ExtChromiumBrowser _browser = (ExtChromiumBrowser)webBrowser;
            _browser.Dispatcher.Invoke(new Action(() =>
            {
                NewWindowEventArgs e = new NewWindowEventArgs(info, url);
                _browser.OnNewWindow(e);
            }));
            newBrowser = null;
            return true;
        }
    }
    public class ExtChromiumBrowser : ChromiumWebBrowser
    {
        public static int Id;

        public ExtChromiumBrowser(int id) : base(null)
        {
            this.LifeSpanHandler = new CefLifeSpanHandler();
            this.DownloadHandler = new DownloadHandler();
            Id = id;
        }

        public ExtChromiumBrowser(int id, string url) : base(url)
        {
            this.LifeSpanHandler = new CefLifeSpanHandler();
            this.DownloadHandler = new DownloadHandler( );
            Id = id;
        }
        public void OnNewWindow(NewWindowEventArgs e)
        {
            if (Browser.Host[Id].singleBox.IsChecked != true)
                Browser.New(e.Url);
            else
                Browser.Navigate(Id, e.Url);
        }
    }
    public class NewWindowEventArgs : EventArgs
    {
        public IWindowInfo WindowInfo { get; set; }
        public string Url { get; set; }
        public NewWindowEventArgs(IWindowInfo windowInfo, string url)
        {
            WindowInfo = windowInfo;
            this.Url = url;
        }
    }
    internal class DownloadHandler : IDownloadHandler
    {
        private readonly Action<bool, DownloadItem> _downloadCallBackEvent;

        public void OnBeforeDownload(IWebBrowser webBrowser, IBrowser browser, 
            DownloadItem item, IBeforeDownloadCallback callback)
        {
            if (callback.IsDisposed) return;

            _downloadCallBackEvent?.Invoke(false, item);
            var path = AskDownloadPath(item);
            if (path != null)
            {
                new Download(item, path).Show();
                item.IsInProgress = true;
            }
        }

        public void OnDownloadUpdated(IWebBrowser webBrowser, IBrowser browser,
            DownloadItem item, IDownloadItemCallback callback)
        {
            _downloadCallBackEvent?.Invoke(true, item);
        }

        private static string AskDownloadPath(DownloadItem item)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = item.SuggestedFileName;
            sfd.Title = "下载文件 - 极简浏览器";
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
            Id = id;
        }

        public static Window mainWindow { get; set; }
        public static int Id;
        void IContextMenuHandler.OnBeforeContextMenu(
            IWebBrowser webBrowser,
            IBrowser browser, IFrame frame,
            IContextMenuParams paras,
            IMenuModel model)
        { }

        bool IContextMenuHandler.OnContextMenuCommand(
            IWebBrowser webBrowser,
            IBrowser browser,
            IFrame frame,
            IContextMenuParams paras,
            CefMenuCommand command,
            CefEventFlags eventFlags)
        { return true; }

        void IContextMenuHandler.OnContextMenuDismissed(
            IWebBrowser webBrowser,
            IBrowser browser,
            IFrame frame)
        {
            var chromiumWebBrowser = (ChromiumWebBrowser)webBrowser;
            chromiumWebBrowser.Dispatcher.Invoke(() =>
            {
                chromiumWebBrowser.ContextMenu = null;
            });
        }

        bool IContextMenuHandler.RunContextMenu(
            IWebBrowser webBrowser,
            IBrowser browser,
            IFrame frame,
            IContextMenuParams parameters,
            IMenuModel model,
            IRunContextMenuCallback callback)
        {
            var _browser = (ChromiumWebBrowser)webBrowser;
            _browser.Dispatcher.Invoke(() =>
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
                    Command = new CustomCommand(( ) => { Browser.GoForward(Id); })
                });
                menu.Items.Add(new MenuItem
                {
                    Header = "后退",
                    Command = new CustomCommand(( ) => { Browser.GoBack(Id); })
                });
                menu.Items.Add(new MenuItem
                {
                    Header = "刷新",
                    Command = new CustomCommand(()=> { Browser.Refresh(Id); })
                });
                menu.Items.Add(new MenuItem
                {
                    Header = "新窗口",
                    Command = new CustomCommand(() => { Browser.New(); })
                });
                menu.Items.Add(new MenuItem
                {
                    Header = "网页源代码",
                    Command = new CustomCommand(()=> { Browser.ViewSource(Id); })
                });
                _browser.ContextMenu = menu;
            });
            return true;
        }
        private static IEnumerable<Tuple<string, CefMenuCommand>> GetMenuItems(IMenuModel model)
        {
            var list = new List<Tuple<string, CefMenuCommand>>();
            for (var i = 0; i < model.Count; i++)
            {
                var header = model.GetLabelAt(i);
                var command = model.GetCommandIdAt(i);
                list.Add(new Tuple<string, CefMenuCommand>(header, command));
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

        bool ICommand.CanExecute(object o)
        {
            if (_TargetCanExecuteMethod != null)
                return _TargetCanExecuteMethod();
            if (_TargetExecuteMethod != null)
                return true;
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