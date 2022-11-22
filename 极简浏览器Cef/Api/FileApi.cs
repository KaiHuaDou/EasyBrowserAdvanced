using System.IO;

namespace 极简浏览器.Api
{
    public static class FileApi
    {
        public static string StartupPath
        {
            get
            {
                string result = "";
                if (string.IsNullOrEmpty(App.Program.InputArgu) == false)
                    result = App.Program.InputArgu;
                else
                {
                    string pathFile = File.ReadAllText(FilePath.ConfigPath);
                    if (string.IsNullOrEmpty(pathFile))
                    {
                        File.WriteAllText(FilePath.ConfigPath, "about:blank");
                        result = "about:blank";
                    }
                    else
                        result = pathFile;
                }
                return result;
            }
        }
    }
}