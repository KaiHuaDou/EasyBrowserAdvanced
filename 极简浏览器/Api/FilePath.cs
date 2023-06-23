using System.Diagnostics;
using System.IO;

namespace 极简浏览器.Api;

public static class FilePath
{
    public static string App = Path.GetFullPath(Process.GetCurrentProcess( ).MainModule.FileName);
    public static string Runtime = Path.GetDirectoryName(Process.GetCurrentProcess( ).MainModule.FileName);
    public static string Data = $@"{Runtime}\Profile";
    public static string History = $@"{Data}\History.xml";
    public static string BookMark = $@"{Data}\BookMark.xml";
    public static string Setting = $@"{Data}\Setting.xml";
    public static string Cookies = $@"{Data}\Cookies.xml";
    public static string Log = $@"{Runtime}\Log";
    public static string Cache = $@"{Runtime}\Cache";
    public static string GPUCache = $@"{Runtime}\GPUCache";
}
