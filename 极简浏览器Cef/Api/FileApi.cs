using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;

namespace 极简浏览器.Api
{
    public enum FileType
    {
        History = 0,
        BookMark = 1,
    }
    public static class FileApi
    {
        public static string ParseFileType(FileType fileType)
        {
            if(fileType == FileType.History)
            {
                return FilePath.HistoryPath;
            }
            if(fileType == FileType.BookMark)
            {
                return FilePath.BookMarkPath;
            }
            return null;
        }
        public static string ParseContent(string title, string url)
        {
            return string.Format("{0,-50}{1,-50}{2,-20}\n", title, url, DateTime.Now.ToString( ));
        }
        public static bool Write(string title, string url, FileType fileType)
        {
            try
            {
                if (App.Program.arguments.isNotLogging == true)
                    goto skip;
                if (url == "about:blank")
                    title = "新标签页";
                File.AppendAllText(ParseFileType(fileType), ParseContent(title, url));
            }
            catch(Exception e)
            {
                Logger.Log(e, logType: LogType.Debug, shutWhenFail: false);
                return false;
            }
            skip:
            return true;
        }
        public static bool Write(string text, FileType fileType)
        {
            try
            {
                if (App.Program.arguments.isNotLogging == true)
                    goto skip;
                File.AppendAllText(ParseFileType(fileType), text + "\n");
            }
            catch (Exception e)
            {
                Logger.Log(e, logType: LogType.Debug, shutWhenFail: false);
                return false;
            }
            skip:
            return true;
        }
        public static bool Clear(FileType fileType)
        {
            try
            {
                File.WriteAllText(ParseFileType(fileType), "");
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
            FileStream fs = new FileStream(ParseFileType(fileType), FileMode.OpenOrCreate, FileAccess.Read);
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
        public static string GetStartupPath(bool isnew)
        {
            string result = "";
            if (App.Program.InputArgu != "")
            {
                result = App.Program.InputArgu;
            }
            else
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
        public static string GetLastData(FileType fileType)
        {
            string lastData = "";
            FileStream fs = new FileStream(ParseFileType(fileType), FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            while(sr.EndOfStream != true)
            {
                lastData = sr.ReadLine( );
            }
            return lastData;
        }
    }
}