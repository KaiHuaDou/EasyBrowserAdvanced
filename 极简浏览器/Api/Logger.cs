using System;
using System.IO;
using System.Windows;

namespace 极简浏览器.Api;

public enum LogType
{
    Info,
    Warn,
    Error
}

public static class Logger
{
    public static string GenLog(Exception ex)
    {
        string log = "";
        log += $"{ex.Message}\n{ex.Source}\n{ex.TargetSite}\n{ex.StackTrace}\n\n";
        if (ex.InnerException is not null)
            log += GenLog(ex.InnerException);
        return log;
    }

    public static void Write(Exception ex, LogType logType = LogType.Info)
    {
        try
        {
            File.AppendAllText($@"{FilePath.Log}\{logType}.log", GenLog(ex));
        }
        catch (NullReferenceException)
        {
            if (logType == LogType.Error)
                Application.Current.Shutdown( );
        }
    }
}
