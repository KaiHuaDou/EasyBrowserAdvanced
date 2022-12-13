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
        public static string PageText
        {
            get
            {
                TaskStringVisitor tsv = new TaskStringVisitor();
                Browser.Core.GetMainFrame().GetText(tsv);
                while (tsv.Task.IsCompleted) ;
                return tsv.Task.Result;
            }
        }
        public static string PageSource
        {
            get
            {
                TaskStringVisitor tsv = new TaskStringVisitor();
                Browser.Core.GetMainFrame().GetSource(tsv);
                while (tsv.Task.IsCompleted) ;
                return tsv.Task.Result;
            }
        }
        public static async void ViewSource()
        {
            new WebSource(PageSource).Show();
        }

        public static string ZipStr(string str, int len)
        {
            if (str.Length <= len) return str;
            return str.Substring(0, len - 6) + "…" + str.Substring(str.Length - 6);
        }
    }
}