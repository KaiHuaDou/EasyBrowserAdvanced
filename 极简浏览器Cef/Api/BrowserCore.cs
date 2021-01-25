using System;
using System.Windows;
using CefSharp;

namespace 极简浏览器.Api
{
    public static class BrowserCore
    {
        public static MainWindow GetInstance( )
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is MainWindow)
                {
                    return window as MainWindow;
                }
            }
            throw new InvalidOperationException( );
        }
        public static void Navigate(string url)
        {
            GetInstance( ).cwb.Address = url;
        }

        public static void GoBack( )
        {
            if (GetInstance( ).cwb.CanGoBack == true)
                GetInstance( ).cwb.Back( );
        }

        public static void GoForward( )
        {
            if (GetInstance( ).cwb.CanGoForward == true)
                GetInstance( ).cwb.Forward( );
        }

        public static void Refresh( )
        {
            try{ GetInstance( ).cwb.Reload( ); }
            catch (Exception) { }
        }
    }
}
