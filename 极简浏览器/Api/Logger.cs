using System;
using System.IO;
using System.Windows;

namespace 极简浏览器.Api;

public enum LogType
{
    Debug,
    Warning,
    Error
}

public static class Logger
{
    public static void Log(Exception ex, LogType type = LogType.Debug)
    {
        try
        {
            File.AppendAllText(
                $@"{FilePath.Log}\{type}.log",
                $"\n\n{ex.Message}\n{ex.Source}\n{ex.TargetSite}\n{ex.HelpLink}\n{ex.StackTrace}");
        }
        catch (NullReferenceException)
        {
            if (type == LogType.Error)
                Application.Current.Shutdown( );
        }
    }
}
