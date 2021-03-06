using System.Diagnostics;
using System.IO;

namespace 极简浏览器.Api
{
    public static class NewInstance
    {
        public static string AppPath = Path.GetDirectoryName(Process.GetCurrentProcess( ).MainModule.FileName);
        public static void StartNewInstance(string url, bool isNotLog)
        {
            ProcessStartInfo psi = new ProcessStartInfo(AppPath + "\\极简浏览器.exe", url + " false " + isNotLog);
            Process.Start(psi);
        }
    }
}