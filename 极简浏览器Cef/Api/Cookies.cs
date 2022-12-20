using System;
using System.Collections.Generic;
using CefSharp;

namespace 极简浏览器.Api
{
    public static class CookieMgr
    {
        private static string curUrl;
        public static void Get()
        {
            try
            {
                HashSet<CookieData> cookies;
                cookies = Configer<CookieData>.Get(FilePath.Cookies);
                var manager = Cef.GetGlobalCookieManager();
                foreach (CookieData item in cookies)
                    manager.SetCookieAsync(item.Key, item.Value);
            }
            catch (NullReferenceException) { }
        }

        public static void Set(int id)
        {
            CookieVisitor visitor = new CookieVisitor();
            visitor.SendCookie += visitor_SendCookie;
            ICookieManager cookieManager = Browser.Core[id].GetCookieManager();
            curUrl = Browser.Address(id);
            cookieManager.VisitAllCookies(visitor);
        }

        private static void visitor_SendCookie(Cookie obj)
        {
            HashSet<CookieData> cookies;
            cookies = Configer<CookieData>.Get(FilePath.Cookies);
            try
            {
                foreach (CookieData item in cookies)
                {
                    if (item.Value.Name == obj.Name && item.Value.Domain == obj.Domain)
                    {
                        item.Value.Value = obj.Value;
                        item.Value.SameSite = obj.SameSite;
                        item.Value.Secure = obj.Secure;
                        item.Value.Priority = obj.Priority;
                        item.Value.Path = obj.Path;
                        item.Value.LastAccess = obj.LastAccess;
                        item.Value.Expires = obj.Expires;
                        item.Value.Creation = obj.Creation;
                        item.Value.HttpOnly = obj.HttpOnly;
                        Configer<CookieData>.Save(cookies, FilePath.Cookies);
                        return;
                    }
                }
            }
            catch (NullReferenceException) { }
            Configer<CookieData>.
                    Add(new CookieData(curUrl, obj), FilePath.Cookies);
        }
    }
    public class CookieData
    {
        public CookieData() { }
        public CookieData(string k, Cookie v)
        {
            Key = k;
            Value = v;
        }
        public bool IsChecked;
        public string Key;
        public Cookie Value;
    }
    public class CookieVisitor : ICookieVisitor
    {
        public event Action<Cookie> SendCookie;
        public void Dispose() { }
        public bool Visit(Cookie cookie, int count, int total, ref bool deleteCookie)
        {
            deleteCookie = false;
            SendCookie?.Invoke(cookie);
            return true;
        }
    }
}