using System;
using CefSharp;

namespace 极简浏览器.Api
{
    public static class StdApi
    {
        public static string LocalTime
        {
            get
            {
                return DateTime.Now.ToLocalTime().ToString();
            }
        }
        public static string PageText(int id)
        {
            TaskStringVisitor tsv = new TaskStringVisitor();
            Browser.Core[id].GetMainFrame().GetText(tsv);
            while (tsv.Task.IsCompleted) ;
            return tsv.Task.Result;
        }
        public static string PageSource(int id)
        {
            TaskStringVisitor tsv = new TaskStringVisitor();
            Browser.Core[id].GetMainFrame().GetSource(tsv);
            while (tsv.Task.IsCompleted) ;
            return tsv.Task.Result;
        }
        public static void ViewSource(int id)
        {
            new WebSource(id, PageSource(id)).Show();
        }

        public static string ZipStr(string str, int len)
        {
            if (str.Length <= len) return str;
            return str.Substring(0, len - 6) + "…" + str.Substring(str.Length - 6);
        }
    }
}