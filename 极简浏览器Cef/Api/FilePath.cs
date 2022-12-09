using System.Diagnostics;
using System.IO;

namespace 极简浏览器.Api
{
    public static class FilePath
    {
        public static string App = Path.GetFullPath(Process.GetCurrentProcess().MainModule.FileName);
        public static string Runtime = Path.GetDirectoryName(Process.GetCurrentProcess( ).MainModule.FileName);
        public static string Datas = Runtime + "\\DataBase";
        public static string History =  Runtime + "\\DataBase\\History.db";
        public static string BookMark = Runtime + "\\DataBase\\BookMark.db";
        public static string Config = Runtime + "\\DataBase\\Config.db";
        public static string Logs = Runtime + "\\log";
        public static string Caches = Runtime + "\\GPUCache";
        public static string Startup = Runtime;
    }
}
