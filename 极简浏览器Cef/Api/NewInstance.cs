using System.Diagnostics;
using System.IO;

namespace 极简浏览器.Api
{
    public static class NewInstance
    {
        public static void StartNewInstance(string url, bool isNotLog)
        {
            ProcessStartInfo psi = new ProcessStartInfo(FilePath.AppRuntime, url + " false " + isNotLog);
            Process.Start(psi);
        }
    }
}