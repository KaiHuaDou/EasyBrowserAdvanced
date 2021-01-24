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
        static string AppPath = Path.GetDirectoryName(Process.GetCurrentProcess( ).MainModule.FileName);
        static string HistoryPath =  AppPath + "\\DataBase\\History.db";
        static string BookMarkPath =  AppPath + "\\DataBase\\BookMark.db";

        public static bool Write(string url, FileType fileType)
        {
            try
            {
                if(fileType == FileType.History)
                    File.AppendAllText(HistoryPath, url + '\n');
                else
                    File.AppendAllText(BookMarkPath, url + '\n');
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }
        public static bool Clear(FileType fileType)
        {
            try
            {
                if(fileType == FileType.History)
                    File.WriteAllText(HistoryPath, "");
                else
                    File.WriteAllText(BookMarkPath, "");
            }
            catch(Exception)
            {
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
                fs = new FileStream(HistoryPath, FileMode.OpenOrCreate, FileAccess.Read);
            }
            if(fileType == FileType.BookMark)
            {
                fs = new FileStream(BookMarkPath, FileMode.OpenOrCreate, FileAccess.Read);
            }
            StreamReader sr = new StreamReader(fs);
            while (sr.EndOfStream != true)
            {
                checkBox = new CheckBox( );
                checkBox.Content = sr.ReadLine( );
                if ((string)checkBox.Content == "")
                    continue;
                list.Add(checkBox);
            }
            return list;
        }
    }
}