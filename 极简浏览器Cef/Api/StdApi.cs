using System;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CefSharp;

namespace 极简浏览器.Api
{
    public static class StdApi
    {
        public static string LocalTime
        {
            get
            {
                return DateTime.Now.ToLocalTime().ToString();
            }
        }
        public static string CefPageSource
        {
            get
            {
                TaskStringVisitor tsv = new TaskStringVisitor( );
                BrowserCore.CefBrowser.GetMainFrame().GetText(tsv);
                while (tsv.Task.IsCompleted == true)
                    ;
                return tsv.Task.Result;
            }
        }
        public static async void ViewPageSource( )
        {
            WebSource webSource;
            string x = await BrowserCore.CefBrowser.GetMainFrame( ).GetSourceAsync( );
            webSource = new WebSource(x);
            webSource.Show( );
        }
        public static void ShowWindow(Window window)
        {
            window.Show();
        }
    }
}
