﻿using System;
using System.IO;

namespace 极简浏览器.Api;

/// <summary>
/// 通用工具
/// </summary>
public static class Utils
{
    public static string AddressRegex = @"/$|.\.[A-Za-z\u4e00-\u9fa5]{1,5}($|/)|^[a-z]{2,6}://|^about:|^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}(:\d{1,5})?";

    public static string LocalTime => DateTime.Now.ToLocalTime( ).ToString( );

    public static void CreateIfNotExists(string filename)
        => new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite).Close( );

    public static string ZipStr(string str, int len)
    {
        return str.Length <= len ? str
            : str.Substring(0, len - 6) + "…" + str.Substring(str.Length - 6);
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
