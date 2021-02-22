using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using System.Collections.Generic;

namespace 极简浏览器.Api
{
    public enum FileType
    {
        History = 0,
        BookMark = 1,
    }
    public static class FileApi
    {

        public static bool Write(string title, string url, FileType fileType)
        {
            try
            {
                if (fileType == FileType.History)
                    File.AppendAllText(FilePath.HistoryPath, string.Format("{0,-50}{1,-50}{2,-20}\n", title, url, DateTime.Now.ToString( )));
                else
                    File.AppendAllText(FilePath.BookMarkPath, string.Format("{0,-50}{1,-50}{2,-20}\n", title, url, DateTime.Now.ToString( )));
            }
            catch(Exception e)
            {
                Logger.Log(e, logType: LogType.Debug, shutWhenFail: false);
                return false;
            }
            return true;
        }
        public static bool Write(string text, FileType fileType)
        {
            try
            {
                if (fileType == FileType.History)
                    File.AppendAllText(FilePath.HistoryPath, text + "\n");
                else
                    File.AppendAllText(FilePath.BookMarkPath, text + "\n");
            }
            catch (Exception e)
            {
                Logger.Log(e, logType: LogType.Debug, shutWhenFail: false);
                return false;
            }
            return true;
        }
        public static bool Clear(FileType fileType)
        {
            try
            {
                if(fileType == FileType.History)
                    File.WriteAllText(FilePath.HistoryPath, "");
                else
                    File.WriteAllText(FilePath.BookMarkPath, "");
            }
            catch(Exception e)
            {
                Logger.Log(e ,logType: LogType.Debug, shutWhenFail: false);
                return false;
            }
            return true;
        }
        public static List<CheckBox> ReadAll(FileType fileType)
        {
            List<CheckBox> list = new List<CheckBox>( );
            CheckBox checkBox = new CheckBox();
            FileStream fs = null;
            if(fileType == FileType.History)
            {
                fs = new FileStream(FilePath.HistoryPath, FileMode.OpenOrCreate, FileAccess.Read);
            }
            if(fileType == FileType.BookMark)
            {
                fs = new FileStream(FilePath.BookMarkPath, FileMode.OpenOrCreate, FileAccess.Read);
            }
            StreamReader sr = new StreamReader(fs);
            while (sr.EndOfStream != true)
            {
                checkBox = new CheckBox( );
                checkBox.Content = sr.ReadLine( );
                if (string.IsNullOrEmpty((string)checkBox.Content) == true)
                    continue;
                list.Add(checkBox);
            }
            return list;
        }
        public static string GetStartupPath(string path, string isnew)
        {
            string result = "";
            if (App.Program.InputArgu != "")
            {
                result = App.Program.InputArgu;
            }
            else if (path != null && path != "" && path != ".")
            {
                result = path;
            }
            else if (!(isnew == "false"))
            {
                string pathFile = File.ReadAllText(FilePath.ConfigPath);
                if (string.IsNullOrEmpty(pathFile))
                {
                    File.WriteAllText(FilePath.ConfigPath, "about:blank");
                    result = "about:blank";
                }
                else
                {
                    result = pathFile;
                }
            }
            return result;
        }
    }
}