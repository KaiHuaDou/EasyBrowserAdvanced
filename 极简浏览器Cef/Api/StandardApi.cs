using System;
using System.Drawing;
using System.Threading;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CefSharp;

namespace 极简浏览器.Api
{
    public static class StandardApi
    {
        public static ImageSource ConvertImage(Bitmap image)
        {
            Bitmap temp1 = image;
            IntPtr temp2 = temp1.GetHbitmap();
            return Imaging.CreateBitmapSourceFromHBitmap(temp2, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }
        public static string GetLocalTime()
        {
            return DateTime.Now.ToLocalTime().ToString();
        }
        public static string GetPageSource( )
        {
            TaskStringVisitor tsv = new TaskStringVisitor( );
            BrowserCore.CefBrowser.GetMainFrame( ).GetText(tsv);
            while (tsv.Task.IsCompleted == true)
                ;
            return tsv.Task.Result;
        }
        public static async void ViewPageSource( )
        {
            WebSource webSource;
            string x = await BrowserCore.CefBrowser.GetMainFrame( ).GetSourceAsync( );
            webSource = new WebSource(x);
            webSource.Show( );
        }
        public static void ShowNotifyIcon(string text, ToolTipIcon icon = ToolTipIcon.Info, int timeOut = 2000)
        {
            NotifyIcon _NI = new NotifyIcon( );
            _NI.BalloonTipIcon = icon;
            _NI.BalloonTipText = Properties.Resources.File_Error;
            _NI.BalloonTipTitle = Properties.Resources.BrowserName;
            _NI.Text = text;
            _NI.Visible = true;
            _NI.Icon = new Icon("favicon.ico");
            _NI.ShowBalloonTip(2000);
        }
    }
}
