using System;

namespace 极简浏览器.Api;

/// <summary>
/// 标准（杂项）Api
/// </summary>
public static class StdApi
{
    public static string LocalTime => DateTime.Now.ToLocalTime( ).ToString( );

    public static string ZipStr(string str, int len)
    {
        return str.Length <= len ? str
            : str.Substring(0, len - 6) + "…" + str.Substring(str.Length - 6);
    }
}
