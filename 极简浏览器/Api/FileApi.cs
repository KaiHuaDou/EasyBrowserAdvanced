using System.IO;

namespace 极简浏览器.Api
{
    public static class FileApi
    {
        public static string StartupPath
        {
            get
            {
                if (!string.IsNullOrEmpty(App.Program.startUrl))
                    return App.Program.startUrl;
                string path = File.ReadAllText(FilePath.Config);
                if (string.IsNullOrEmpty(path))
                {
                    File.WriteAllText(FilePath.Config, "about:blank");
                    return "about:blank";
                }
                return path;
            }
        }
    }
}