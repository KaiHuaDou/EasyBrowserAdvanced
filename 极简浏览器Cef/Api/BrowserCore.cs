using System;
using System.Windows;
using CefSharp;

namespace 极简浏览器.Api
{
    public static class BrowserCore
    {
        public static void RunJavaSript(string script)
        {
            CefBrowser.WebBrowser.ExecuteScriptAsync(script);
        }
        public static MainWindow CefInstance
        {
            get
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window is MainWindow)
                    {
                        return window as MainWindow;
                    }
                }
                return null;
            }
            private set { }
        }
        public static ExtChromiumBrowser CefBrowser
        {
            get
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window is MainWindow)
                    {
                        return (((MainWindow)window).cwb) as ExtChromiumBrowser;
                    }
                }
                return null;
            }
            private set { }
        }
        public static void Navigate(string url)
        {
            CefBrowser.Address = url;
        }

        public static void GoBack( )
        {
            if (CefBrowser.CanGoBack == true)
                CefBrowser.Back( );
        }

        public static void GoForward( )
        {
            if (CefBrowser.CanGoForward == true)
                CefBrowser.Forward( );
        }

        public static void Refresh( )
        {
            try{ CefBrowser.Reload( ); }
            catch (Exception e)
            {
                Logger.Log(e, logType: LogType.Debug, shutWhenFail: false);
            }
        }
    }
}
