using System;
using System.Collections.Generic;
using CefSharp;
using CefSharp.Wpf;

namespace 极简浏览器.Api
{
    /// <summary>
    /// 访问浏览器与主窗口集合的唯一入口
    /// </summary>
    public static class Browser
    {
        public static Dictionary<int, MainWindow> Host = new Dictionary<int, MainWindow>( );
        public static Dictionary<int, EasyBrowserCore> Core = new Dictionary<int, EasyBrowserCore>( );
        public static string Address(int id) => Core[id].Address;
        public static string Title(int id) => Core[id].Title;
        public static void Navigate(int id, string url) => Core[id].Address = url;
        public static void GoBack(int id) { if (Core[id].CanGoBack) Core[id].Back( ); }
        public static void GoForward(int id) { if (Core[id].CanGoForward) Core[id].Forward( ); }
        public static void ShowDevTools(int id) => Core[id].ShowDevTools( );
        
        public static void Refresh(int id)
        {
            try
            {
                if (Core[id].Address == Host[id].UrlTextBox.Text)
                    Core[id].Reload( );
                else
                    Navigate(id, Host[id].UrlTextBox.Text);
            }
            catch (Exception e) { Logger.Log(e); }
        }

        public static void Init( )
        {
            var settings = new CefSettings( );
            settings.CefCommandLineArgs["enable-media-stream"] = "1";
            settings.CefCommandLineArgs["enable-system-flash"] = "1";
            settings.CefCommandLineArgs["log_severity"] = "disabled";
            settings.CefCommandLineArgs["ppapi-flash-path"] = "resource/pepflashplayer.dll";
            settings.CefCommandLineArgs["ppapi-flash-version"] = "99.0.0.999";
            Cef.Initialize(settings);
            CookieMgr.Get( );
        }

        public static void New(string url = "about:blank")
        {
            App.Program.startUrl = url;
            Host[Host.Count] = new MainWindow(Host.Count);
            Host[Host.Count - 1].Show( );
        }

        public static bool PraseEasy(int id, string url)
        {
            switch (url.ToLower( ).Replace("easy://", ""))
            {
                case "about": new About( ).Show( ); break;
                case "help": new Help( ).Show( ); break;
                case "history": new History( ).Show( ); break;
                case "bookmark": new History( ).Show( ); break;
                case "setting": new Setting( ).Show( ); break;
                case "websource": ViewSource(id); break;
                case "newtab": New( ); break;
                default: return false;
            }
            return true;
        }
        public static void ViewSource(int id) => new WebSource(id, PageSource(id)).Show( );

        public static string PageText(int id)
        {
            TaskStringVisitor tsv = new TaskStringVisitor( );
            Core[id].GetMainFrame( ).GetText(tsv);
            while (tsv.Task.IsCompleted) ;
            return tsv.Task.Result;
        }

        public static string PageSource(int id)
        {
            TaskStringVisitor tsv = new TaskStringVisitor( );
            Core[id].GetMainFrame( ).GetSource(tsv);
            while (tsv.Task.IsCompleted) ;
            return tsv.Task.Result;
        }
    }
}
