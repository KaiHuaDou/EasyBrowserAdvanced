using System.Diagnostics;
using System.IO;

namespace 极简浏览器.Api
{
    public static class NewInstance
    {
        public static void StartNewInstance(string url)
        {
            Arguments argus = App.Program.arguments;
            string newInstanceArgs = argus.isNew + " " + argus.isNotLogging + " " + argus.isTopMost;
            ProcessStartInfo psi = new ProcessStartInfo(FilePath.AppRuntime, url + " " + newInstanceArgs);
            Process.Start(psi);
        }
    }
}