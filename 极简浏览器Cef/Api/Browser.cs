using System;
using System.Windows;
using CefSharp;
using CefSharp.Wpf;

namespace 极简浏览器.Api
{
    public static class Browser
    {
        public static MainWindow HostWindow
        {
            get
            {
                foreach (Window window in Application.Current.Windows)
                    if (window is MainWindow)
                        return window as MainWindow;
                return null;
            }
        }
        public static ExtChromiumBrowser Core { get; set; }
        public static string Address { get { return Core.Address; } }
        public static string Title { get { return Core.Title; } }
        public static void Navigate(string url) { Core.Address = url; }
        public static void GoBack() { if (Core.CanGoBack) Core.Back(); }
        public static void GoForward() { if (Core.CanGoForward) Core.Forward(); }
        public static void Refresh()
        {
            try
            {
                if (Core.Address == HostWindow.UrlTextBox.Text)
                    Core.Reload();
                else Navigate(HostWindow.UrlTextBox.Text);
            }
            catch (Exception e) { Logger.Log(e); }
        }
        public static void Init()
        {
            var settings = new CefSettings();
            settings.Locale = "zh-CN";
            settings.AcceptLanguageList = "zh-CN";
            settings.CefCommandLineArgs["enable-media-stream"] = "1";
            settings.CefCommandLineArgs["no-proxy-server"] = "1";
            settings.CefCommandLineArgs["enable-system-flash"] = "1";
            settings.CefCommandLineArgs["log_severity"] = "disabled";
            settings.CefCommandLineArgs["ppapi-flash-path"] = "resource/pepflashplayer.dll";
            settings.CefCommandLineArgs["ppapi-flash-version"] = "99.0.0.999";
            Cef.Initialize(settings);
            Core = new ExtChromiumBrowser();
            CookieMgr.Get();
        }

        public static bool PraseEasy(string url)
        {
            switch (url.ToLower().Replace("easy://", ""))
            {
                case "about": new About().Show(); break;
                case "help": new Help().Show(); break;
                case "history": new History().Show(); break;
                case "bookmark": new History().Show(); break;
                case "setting": new Setting().Show(); break;
                case "websource": StdApi.ViewSource(); break;
                case "newtab": Instance.New(); break;
                default: return false;
            }
            return true;
        }
    }
}