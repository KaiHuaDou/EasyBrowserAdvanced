using System;
using CefSharp;

namespace 极简浏览器.Api;

public static class CookieMgr
{
    private static string address;
    public static void Get( )
    {
        try
        {
            using ICookieManager manager = Cef.GetGlobalCookieManager( );
            foreach (CookieData item in App.Cookies.Content)
                manager.SetCookieAsync(item.Url, item.Value);
        }
        catch (NullReferenceException) { }
    }

    public static void Set(int id)
    {
        CookieVisitor visitor = new( );
        visitor.SendCookie += AddCookie;
        ICookieManager cookieManager = Instance.Core[id].GetCookieManager( );
        address = Instance.Address(id);
        cookieManager.VisitAllCookies(visitor);
    }

    private static void AddCookie(Cookie cookie)
    {
        try
        {
            foreach (CookieData item in App.Cookies.Content)
            {
                if (item.Value.Name == cookie.Name && item.Value.Domain == cookie.Domain)
                {
                    App.Cookies.Content.Add(item);
                    return;
                }
            }
        }
        catch (NullReferenceException) { }
        App.Cookies.Content.Add(new CookieData { Url = address, Value = cookie });
    }
}

public class CookieData
{
    public bool Check { get; set; }
    public string Url { get; set; }
    public Cookie Value { get; set; }
}

public class CookieVisitor : ICookieVisitor
{
    public event Action<Cookie> SendCookie;

    public void Dispose( ) => GC.SuppressFinalize(this);

    public bool Visit(Cookie cookie, int count, int total, ref bool deleteCookie)
    {
        deleteCookie = false;
        SendCookie?.Invoke(cookie);
        return true;
    }
}
