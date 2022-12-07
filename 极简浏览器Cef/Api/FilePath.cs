using System.Diagnostics;
using System.IO;

namespace 极简浏览器.Api
{
    public static class FilePath
    {
        public static string App = Path.GetDirectoryName(Process.GetCurrentProcess( ).MainModule.FileName);
        public static string Runtime = Path.GetFullPath(Process.GetCurrentProcess( ).MainModule.FileName);
        public static string Datas = App + "\\DataBase";
        public static string History =  App + "\\DataBase\\History.db";
        public static string BookMark = App + "\\DataBase\\BookMark.db";
        public static string Config = App + "\\DataBase\\Config.db";
        public static string Logs = App + "\\log";
        public static string Caches = App + "\\GPUCache";
        public static string Startup = App;
    }
}
