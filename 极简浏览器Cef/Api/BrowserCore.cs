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
            throw new MemberAccessException( );
        }
        public static ExtChromiumBrowser GetBrowser()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is MainWindow)
                {
                    return (((MainWindow)window).cwb) as ExtChromiumBrowser;
                }
            }
            throw new MemberAccessException( );
        }
        public static void Navigate(string url)
        {
            GetBrowser( ).Address = url;
        }

        public static void GoBack( )
        {
            if (GetBrowser( ).CanGoBack == true)
                GetBrowser( ).Back( );
        }

        public static void GoForward( )
        {
            if (GetBrowser( ).CanGoForward == true)
                GetBrowser( ).Forward( );
        }

        public static void Refresh( )
        {
            try{ GetBrowser( ).Reload( ); }
            catch (Exception) { }
        }
    }
}
