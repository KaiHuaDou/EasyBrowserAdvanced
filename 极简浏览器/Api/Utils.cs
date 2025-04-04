using System;
using System.IO;
using System.Text.RegularExpressions;

namespace 极简浏览器.Api;

/// <summary>
/// 通用工具
/// </summary>
public static class Utils
{
    public static Regex AddressRegex = new(@"^[a-z]+://|^about:|/$|.\.[A-Za-z一-龟]{1,5}($|/)|^\d+\.\d+\.\d+\.\d+(:\d+)?$|^\[[0-9a-fA-F:]+\]$", RegexOptions.IgnoreCase);

    public static string LocalTime => DateTime.Now.ToLocalTime( ).ToString( );

    public static void CreateIfNotExists(string filename)
        => new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite).Close( );

    public static string ZipStr(string str, int len)
    {
        return str.Length <= len ? str
            : string.Concat(str.AsSpan(0, len - 6), "…", str.AsSpan(str.Length - 6));
    }

    public static double ConvertZoomLevel(double zoomLevel)
    {
        return zoomLevel switch
        {
            > 0 => Math.Round((double) (zoomLevel * 100 + 100), 2),
            < 0 => Math.Round((double) (zoomLevel * 20 + 100)),
            _ => 100,
        };
    }
}
