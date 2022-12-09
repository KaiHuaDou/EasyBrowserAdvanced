using System.Diagnostics;

namespace 极简浏览器.Api
{
    public static class Instance
    {
        public static void New(string url = "about:blank")
        {
            Argus argus = App.Program.argus;
            string newInstanceArgs = url + " " + argus.IsPrivate + " " + argus.IsTopmost;
            Process.Start(new ProcessStartInfo(FilePath.App, newInstanceArgs));
        }
    }
}