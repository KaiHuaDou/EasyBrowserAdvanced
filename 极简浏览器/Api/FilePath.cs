using System;
using System.IO;

namespace 极简浏览器.Api;

public static class FilePath
{
    public static string App = Path.GetFullPath(Environment.ProcessPath);
    public static string Runtime = Path.GetDirectoryName(Environment.ProcessPath);

    public static string Profile = $@"{Runtime}\Profile";
    public static string Log = $@"{Runtime}\Log";
    public static string Cache = $@"{Runtime}\Cache";
    public static string GPUCache = $@"{Runtime}\GPUCache";

    public static string History = $@"{Profile}\History.xml";
    public static string BookMark = $@"{Profile}\BookMark.xml";
    public static string Setting = $@"{Profile}\Setting.xml";
    public static string Cookies = $@"{Profile}\Cookies.xml";
    public static string ErrorPage = $@"{Runtime}\Resources\Error.html";
}
