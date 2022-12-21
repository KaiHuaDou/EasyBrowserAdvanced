using System;
using System.IO;

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
        public static void Log(Exception ex, LogType logType = LogType.Debug, bool shutWhenFail = false)
        {
            try
            {
                if(logType != LogType.Error && App.Program.Args.IsPrivate)
                {
                    goto skip;
                }
                string LogPath = genLogPath(logType);
                File.AppendAllText(LogPath,
                    "---------------" + ex.Message + "\n" + ex.Source + "\n"
                    + ex.TargetSite + "\n" + ex.HelpLink + "\n" + ex.StackTrace);
            }
            catch (NullReferenceException)
            {
                if (shutWhenFail == true)
                    App.Current.Shutdown( );
            }
            skip:
            return;
        }
        private static string genLogPath(LogType logType)
        {
            switch (logType)
            {
                case LogType.Debug: return FilePath.Logs + "\\debug.log";
                case LogType.Error: return FilePath.Logs + "\\error.log";
                case LogType.Other: return FilePath.Logs + "\\other.log";
            }
            return null;
        }
    }
}
