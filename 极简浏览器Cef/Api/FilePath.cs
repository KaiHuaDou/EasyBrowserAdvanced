using System.Diagnostics;
using System.IO;

namespace 极简浏览器.Api
{
    public static class FilePath
    {
        public static string AppPath = Path.GetDirectoryName(Process.GetCurrentProcess( ).MainModule.FileName);
        public static string AppRuntime = Path.GetFullPath(Process.GetCurrentProcess( ).MainModule.FileName);
        public static string HistoryPath =  AppPath + "\\DataBase\\History.db";
        public static string DataBaseDirectory =  AppPath + "\\DataBase";
        public static string LogDirectory =  AppPath + "\\log";
        public static string BookMarkPath =  AppPath + "\\DataBase\\BookMark.db";
        public static string ConfigPath =  AppPath + "\\DataBase\\Config.db";
        public static string CacheDirectory = AppPath + "\\GPUCache";
        public static string AppStartupPath = AppPath;
    }
}
