using System.Diagnostics;

namespace 极简浏览器.Api
{
    public static class Instance
    {
        public static void New(string url = "about:blank")
        {
            string newInstanceArgs = url + " " + App.Program.argus.IsPrivate + " " + App.Program.argus.IsTopmost;
            //new MainWindow( ).Show( );
            Process.Start(new ProcessStartInfo(FilePath.App, newInstanceArgs));
        }
    }
}