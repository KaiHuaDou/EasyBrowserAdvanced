using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using CefSharp;
using CefSharp.Wpf;

namespace 极简浏览器.Api;

/// <summary>
/// 访问浏览器与主窗口集合的唯一入口
/// </summary>
public static class Instance
{
    public static Dictionary<int, MainWindow> Host = new( );
    public static Dictionary<int, EasyBrowserCore> Core = new( );

    public static string Address(int id) => Core[id].Address;
    public static string Title(int id) => Core[id].Title;
    public static void Navigate(int id, string url) => Core[id].Address = url;
    public static void GoBack(int id) { if (Core[id].CanGoBack) Core[id].Back( ); }
    public static void GoForward(int id) { if (Core[id].CanGoForward) Core[id].Forward( ); }
    public static void ShowDevTools(int id) => Core[id].ShowDevTools( );
    public static void ViewSource(int id) => new WebSource(id).Show( );

    public static void Refresh(int id)
    {
        try
        {
            if (Core[id].Address == Host[id].UrlTextBox.Text)
                Core[id].Reload( );
            else
                Navigate(id, Host[id].UrlTextBox.Text);
        }
        catch (Exception e) { Logger.Log(e); }
    }

    public static void Init( )
    {
        Cef.EnableHighDPISupport( );
        CefSettings settings = new( )
        {
            Locale = CultureInfo.CurrentCulture.Name,
            LogSeverity = LogSeverity.Disable,
            CachePath = $@"{FilePath.Runtime}\Cache",
            UserDataPath = $@"{FilePath.Runtime}\Profile"
        };
        if (App.Setting.Content[0].CheatUA)
            settings.UserAgent = Config.UaCheated;
        settings.CefCommandLineArgs["enable-media-stream"] = "1";
        settings.CefCommandLineArgs["enable-system-flash"] = "1";
        settings.CefCommandLineArgs["ppapi-flash-path"] = "Resources/pepflashplayer.dll";
        settings.CefCommandLineArgs["ppapi-flash-version"] = "99.9.9.999";
        Cef.Initialize(settings);
        CookieMgr.Get( );
    }

    public static void New(string url = App.DEFAULT)
    {
        App.Program.StartupUri =
            url == App.DEFAULT || string.IsNullOrWhiteSpace(url) ?
            App.Setting.Content[0].MainPage : url;
        int id = Host.Count;
        Host[id] = new MainWindow(id);
        Host[id].Show( );
    }

    public static void PraseEasy(int id, string url)
    {
        switch (url.ToUpperInvariant( ).Replace("EASY://", ""))
        {
            case "about": new About( ).Show( ); break;
            case "help": new Help( ).Show( ); break;
            case "history": new History( ).Show( ); break;
            case "bookmark": new History( ).Show( ); break;
            case "setting": new Setting( ).Show( ); break;
            case "websource": ViewSource(id); break;
            case "newtab": New( ); break;
            default: Navigate(id, "about:blank"); break;
        }
    }

    public static async Task<string> PageTextAsync(int id)
        => await Core[id].GetMainFrame( ).GetTextAsync( );

    public static async Task<string> PageSourceAsync(int id)
        => await Core[id].GetMainFrame( ).GetSourceAsync( );
}
