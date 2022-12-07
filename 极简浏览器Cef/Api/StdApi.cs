using System;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
        public static string GetSource
        {
            get
            {
                TaskStringVisitor task = new TaskStringVisitor();
                Browser.Core.GetMainFrame().GetText(task);
                while (task.Task.IsCompleted == true) ;
                return task.Task.Result;
            }
        }
        public static async void ViewSource()
        {
            string code = await Browser.Core.GetMainFrame().GetSourceAsync();
            new WebSource(code).Show();
        }
    }
}