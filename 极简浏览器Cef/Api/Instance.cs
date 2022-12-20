using System.Diagnostics;

namespace 极简浏览器.Api
{
    public static class Instance
    {
        public static void New(string url = "about:blank")
        {
            string newInstanceArgs = url + " " + App.Program.argus.IsPrivate + " " + App.Program.argus.IsTopmost;
            App.Program.windows.Add(new MainWindow(App.Program.windows.Count));
            App.Program.windows[App.Program.windows.Count - 1].Show( );
            //Process.Start(new ProcessStartInfo(FilePath.App, newInstanceArgs));
        }
    }
}