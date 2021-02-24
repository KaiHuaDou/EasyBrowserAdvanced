using System.Drawing;
using System.Windows.Forms;
using CefSharp;

namespace 极简浏览器.Api
{
    public static class StandardApi
    {
        public static string GetPageSource( )
        {
            TaskStringVisitor tsv = new TaskStringVisitor( );
            BrowserCore.GetBrowser( ).GetMainFrame( ).GetText(tsv);
            while (tsv.Task.IsCompleted == true)
                ;
            return tsv.Task.Result;
        }
        public static async void ViewPageSource( )
        {
            WebSource webSource;
            string x = await BrowserCore.GetBrowser( ).GetMainFrame( ).GetSourceAsync( );
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
