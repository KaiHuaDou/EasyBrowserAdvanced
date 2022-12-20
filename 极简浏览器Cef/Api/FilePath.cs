using System.Diagnostics;
using System.IO;

namespace 极简浏览器.Api
{
    public static class FilePath
    {
        public static string App = Path.GetFullPath(Process.GetCurrentProcess().MainModule.FileName);
        public static string Runtime = Path.GetDirectoryName(Process.GetCurrentProcess( ).MainModule.FileName);
        public static string Datas = Runtime + "\\UserData";
        public static string History =  Runtime + "\\UserData\\History.xml";
        public static string BookMark = Runtime + "\\UserData\\BookMark.xml";
        public static string Config = Runtime + "\\UserData\\Config.ini";
        public static string Cookies = Runtime + "\\UserData\\Cookies.xml";
        public static string Logs = Runtime + "\\log";
        public static string Caches = Runtime + "\\GPUCache";
        public static string Startup = Runtime;
    }
}
