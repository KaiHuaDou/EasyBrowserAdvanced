using System;
using System.Collections.Generic;
using CefSharp;

namespace 极简浏览器.Api;

public static class CookieManager
{
    public static readonly HashSet<Cookie> Cookies = [];

    public static void Get( )
    {
        Cookies.Clear( );
        CookieVisitor visitor = new( );
        visitor.SendCookie += GetCookie;
        ICookieManager cookieManager = Cef.GetGlobalCookieManager( );
        cookieManager.VisitAllCookies(visitor);
    }

    private static void GetCookie(Cookie cookie)
        => Cookies.Add(cookie);
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
