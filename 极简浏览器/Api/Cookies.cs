using System;
using System.Collections.ObjectModel;
using CefSharp;

namespace 极简浏览器.Api;

public static class CookieMgr
{
    private static string address;
    public static void Get( )
    {
        try
        {
            ObservableCollection<CookieData> cookies;
            cookies = DataMgr<CookieData>.Get(FilePath.Cookies);
            using ICookieManager manager = Cef.GetGlobalCookieManager( );
            foreach (CookieData item in cookies)
                manager.SetCookieAsync(item.Key, item.Value);
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
        ObservableCollection<CookieData> cookies;
        cookies = DataMgr<CookieData>.Get(FilePath.Cookies);
        try
        {
            foreach (CookieData item in cookies)
            {
                if (item.Value.Name == cookie.Name && item.Value.Domain == cookie.Domain)
                {
                    item.Value = cookie;
                    DataMgr<CookieData>.Save(cookies, FilePath.Cookies);
                    return;
                }
            }
        }
        catch (NullReferenceException) { }
        DataMgr<CookieData>.Add(new CookieData { Key = address, Value = cookie }, FilePath.Cookies);
    }
}

[Serializable]
public class CookieData
{
    public CookieData( ) { }
    public bool Check { get; set; }
    public string Key { get; set; }
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
