using System.Diagnostics;

namespace 极简浏览器.Api
{
    public static class Instance
    {
        public static void New(string url)
        {
            Arguments argus = App.Program.arguments;
            string newInstanceArgs = argus.IsNew + " " + argus.IsStopLog + " " + argus.IsTopMost;
            ProcessStartInfo psi = new ProcessStartInfo(FilePath.Runtime, url + " " + newInstanceArgs);
            Process.Start(psi);
        }
    }
}