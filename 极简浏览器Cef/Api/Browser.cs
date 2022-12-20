using System;
using System.Collections.Generic;
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
        public static List<ExtChromiumBrowser> Core = new List<ExtChromiumBrowser>( );
        public static string Address(int id) { return Core[id].Address; }
        public static string Title(int id) { return Core[id].Title; }
        public static void Navigate(int id, string url) { Core[id].Address = url; }
        public static void GoBack(int id) { if (Core[id].CanGoBack) Core[id].Back( ); }
        public static void GoForward(int id) { if (Core[id].CanGoForward) Core[id].Forward( ); }
        public static void Refresh(int id)
        {
            try
            {
                if (Core[id].Address == HostWindow.UrlTextBox.Text)
                    Core[id].Reload( );
                else
                    Navigate(id, HostWindow.UrlTextBox.Text);
            }
            catch (Exception e) { Logger.Log(e); }
        }
        public static void Init( )
        {
            var settings = new CefSettings( );
            settings.Locale = "zh-CN";
            settings.AcceptLanguageList = "zh-CN";
            settings.CefCommandLineArgs["enable-media-stream"] = "1";
            settings.CefCommandLineArgs["no-proxy-server"] = "1";
            settings.CefCommandLineArgs["enable-system-flash"] = "1";
            settings.CefCommandLineArgs["log_severity"] = "disabled";
            settings.CefCommandLineArgs["ppapi-flash-path"] = "resource/pepflashplayer.dll";
            settings.CefCommandLineArgs["ppapi-flash-version"] = "99.0.0.999";
            Cef.Initialize(settings);
            CookieMgr.Get( );
        }

        public static bool PraseEasy(int id, string url)
        {
            switch (url.ToLower().Replace("easy://", ""))
            {
                case "about": new About().Show(); break;
                case "help": new Help().Show(); break;
                case "history": new History().Show(); break;
                case "bookmark": new History().Show(); break;
                case "setting": new Setting().Show(); break;
                case "websource": StdApi.ViewSource(id); break;
                case "newtab": Instance.New(); break;
                default: return false;
            }
            return true;
        }
    }
}