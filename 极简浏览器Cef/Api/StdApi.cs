using System;
using CefSharp;

namespace 极简浏览器.Api
{
    /// <summary>
    /// 标准（杂项，其实很少）Api
    /// </summary>
    public static class StdApi
    {
        public static string LocalTime
        {
            get
            {
                return DateTime.Now.ToLocalTime().ToString();
            }
        }
        public static string ZipStr(string str, int len)
        {
            if (str.Length <= len) return str;
            return str.Substring(0, len - 6) + "…" + str.Substring(str.Length - 6);
        }
    }
}