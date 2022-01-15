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
        }
        public static ExtChromiumBrowser CefBrowser
        {
            get
            {
                return MainWindow.cwb as ExtChromiumBrowser;
            }
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
        public static bool PraseEasy(string easySite)
        {
            switch(easySite.ToLower().Replace("easy://",""))
            {
                case "about": StdApi.ShowWindow(new About()); break;
                case "extensions": StdApi.ShowWindow(new Extensions()); break;
                case "help": StdApi.ShowWindow(new Help()); break;
                case "history": StdApi.ShowWindow(new History()); break;
                case "bookmark": StdApi.ShowWindow(new History()); break;
                case "runjavascript": StdApi.ShowWindow(new RunJavaScript()); break;
                case "setting": StdApi.ShowWindow(new Setting()); break;
                case "websource": StdApi.ViewPageSource(); break;
                case "new-tab": NewInstance.StartNewInstance("about:blank"); break;
                default: return false;
            }
            return true;
        }
    }
}
