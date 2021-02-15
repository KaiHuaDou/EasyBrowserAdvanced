using System.Diagnostics;
using System.IO;

namespace 极简浏览器.Api
{
    public static class FilePath
    {
        public static string AppPath = Path.GetDirectoryName(Process.GetCurrentProcess( ).MainModule.FileName);
        public static string HistoryPath =  AppPath + "\\DataBase\\History.db";
        public static string BookMarkPath =  AppPath + "\\DataBase\\BookMark.db";
        public static string AppStartupPath = Path.GetDirectoryName(Process.GetCurrentProcess( ).MainModule.FileName);
    }
}
