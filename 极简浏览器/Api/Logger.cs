using System;
using System.IO;
using System.Windows;

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
        public static void Log(Exception ex, LogType type = LogType.Debug, bool shutWhenFail = false)
        {
            try
            {
                if (App.Program.Args.IsPrivate && type != LogType.Error)
                    return;
                File.AppendAllText(GenLogPath(type),
                    $"---------------\n{ex.Message}\n{ex.Source}\n{ex.TargetSite}\n{ex.HelpLink}\n{ex.StackTrace}");
            }
            catch (NullReferenceException)
            {
                if (shutWhenFail) Application.Current.Shutdown( );
            }
        }

        private static string GenLogPath(LogType type)
            => $@"{FilePath.Logs}\{type}.log";
    }
}
