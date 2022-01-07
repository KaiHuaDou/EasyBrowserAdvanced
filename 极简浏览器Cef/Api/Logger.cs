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
                if(logType != LogType.Error && App.Program.arguments.isNotLogging)
                {
                    goto skip;
                }
                string LogPath = GenerateLogPath(logType);
                File.AppendAllText(LogPath,
                    "---------------" + e.Message + "\n" + e.Source + "\n"
                    + e.TargetSite + "\n" + e.HelpLink + "\n" + e.StackTrace);
            }
            catch (NullReferenceException)
            {
                if (shutWhenFail == true)
                    App.Current.Shutdown( );
            }
            skip:
            return;
        }
        private static string GenerateLogPath(LogType logType)
        {
            switch (logType)
            {
                case LogType.Debug: return FilePath.LogDirectory + "\\debug.log";
                case LogType.Error: return FilePath.LogDirectory + "\\error.log";
                case LogType.Other: return FilePath.LogDirectory + "\\other.log";
            }
            return null;
        }
    }
}
