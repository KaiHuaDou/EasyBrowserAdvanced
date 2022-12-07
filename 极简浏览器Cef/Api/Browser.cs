using System;
using System.Windows;
using CefSharp;

namespace 极简浏览器.Api
{
    public static class Browser
    {
        public static MainWindow HostWindow
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
        public static ExtChromiumBrowser Core
        {
            get
            {
                return MainWindow.cwb as ExtChromiumBrowser;
            }
        }
        public static void Navigate(string url)
        {
            Core.Address = url;
        }

        public static void GoBack( )
        {
            if (Core.CanGoBack == true)
                Core.Back( );
        }

        public static void GoForward( )
        {
            if (Core.CanGoForward == true)
                Core.Forward( );
        }

        public static void Refresh( )
        {
            try { Core.Reload(); }
            catch (Exception e)
            {
                Logger.Log(e, logType: LogType.Debug, shutWhenFail: false);
            }
        }
        public static bool PraseEasy(string easySite)
        {
            switch(easySite.ToLower().Replace("easy://",""))
            {
                case "about": new About().Show(); break;
                case "help": new Help().Show(); break;
                case "history": new History().Show(); break;
                case "bookmark": new History().Show(); break;
                case "setting": new Setting().Show(); break;
                case "websource": StdApi.ViewSource(); break;
                case "newtab": Instance.New("about:blank"); break;
                default: return false;
            }
            return true;
        }
    }
}
