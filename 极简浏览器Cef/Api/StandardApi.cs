using System.Drawing;
using System.Windows.Forms;

namespace 极简浏览器.Api
{
    public static class StandardApi
    {
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
