using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Shell;

namespace 极简浏览器.Api
{
    public enum LogType
    {
        Debug,
        Error,
        Other
    }
    public static class Logger
    {
        public static void Log(Exception e, LogType logType = LogType.Debug, bool shutWhenFail = false)
        {
            try
            {
                string LogPath = GenerateLogPath(logType);
                File.AppendAllText(LogPath,
                    e.Message + "|" + e.Source + "|"
                    + e.TargetSite + "|" + e.HelpLink + "|" + e.StackTrace);
            }
            catch (NullReferenceException)
            {
                if (shutWhenFail == true)
                    App.Current.Shutdown( );
            }
        }
        public static DialogResult Message(Exception e, bool shutWhenFail = false)
        {
            try
            {
                BrowserCore.GetInstance( ).TaskbarItemInfo.ProgressValue = 100;
                BrowserCore.GetInstance( ).TaskbarItemInfo.ProgressState = TaskbarItemProgressState.Error;
                BrowserCore.GetInstance( ).TaskbarItemInfo.Overlay = new BitmapImage(new Uri("pack://application:,,,/resource/Error.png"));
                string message, innermsg, endmsg;
                message = Properties.Resources.Excep_msg;
                innermsg = Properties.Resources.Excep_inmsg1 + e.Message + "\n" + e.Source + Properties.Resources.Excep_inmsg2 + e.TargetSite + Properties.Resources.Excep_inmsg3;
                endmsg = Properties.Resources.Excep_endmsg + e.HelpLink;
                DialogResult dr = new DialogResult( );
                dr = MessageBox.Show(
                    message + innermsg + endmsg, Properties.Resources.BrowserName,
                    MessageBoxButtons.AbortRetryIgnore,
                    MessageBoxIcon.Error);
                BrowserCore.GetInstance( ).TaskbarItemInfo.ProgressState = TaskbarItemProgressState.None;
                BrowserCore.GetInstance( ).TaskbarItemInfo.Overlay = null;
                return dr;
            }
            catch (Exception)
            {
                if(shutWhenFail == true)
                {
                    App.Current.Shutdown(1);
                }
                return DialogResult.None;
            }
        }
        private static string GenerateLogPath(LogType logType)
        {
            switch (logType)
            {
                case LogType.Debug: return FilePath.LogDirectory + "\\debug.log";
                case LogType.Error: return FilePath.LogDirectory + "\\error.log";
                case LogType.Other: return FilePath.LogDirectory + "\\other.log";
                default: return FilePath.LogDirectory + "\\LOG_EXCEPTION.LOG";
            }
        }
    }
}
