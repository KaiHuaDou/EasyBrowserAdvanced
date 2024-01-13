using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Controls;
using CefSharp;
using CefSharp.DevTools;
using CefSharp.DevTools.Page;
using CefSharp.Wpf;
using 极简浏览器.External;

namespace 极简浏览器.Api;

/// <summary>
/// 访问浏览器与主窗口集合的唯一入口
/// </summary>
public static class Instance
{
    public static Dictionary<int, MainWindow> Host = new( );
    public static Dictionary<int, EasyBrowserCore> Core = new( );

    public static void Navigate(int id, string url) => Core[id].Address = url;
    public static void GoBack(int id) { if (Core[id].CanGoBack) Core[id].Back( ); }
    public static void GoForward(int id) { if (Core[id].CanGoForward) Core[id].Forward( ); }
    public static void ViewSource(int id) => new WebSource(id).Show( );

    public static void Refresh(int id, bool force = false)
    {
        try
        {
            if (Core[id].Address == Host[id].AddressBox.Text)
                Core[id].Reload(force);
            else
                Navigate(id, Host[id].AddressBox.Text);
        }
        catch (InvalidOperationException) { }
    }

    public static void New(string url = "")
    {
        App.Program.StartupUri = url;
        int id = Host.Count;
        Host[id] = new MainWindow(id, new Argument( ));
        Host[id].Show( );
    }

    public static async Task<byte[]> Capture(int id)
    {
        using DevToolsClient devToolsClient = Core[id].GetDevToolsClient( );
        CaptureScreenshotResponse result = await devToolsClient.Page.CaptureScreenshotAsync( );
        return result.Data;
    }

    public static void Init( )
    {
        Cef.EnableHighDPISupport( );
        CefSettings settings = new( )
        {
            Locale = CultureInfo.CurrentCulture.Name,
            LogSeverity = LogSeverity.Disable,
            CachePath = $@"{FilePath.Runtime}\Cache",
            UserDataPath = $@"{FilePath.Runtime}\Profile",
            UserAgent = App.Setting.Content[0].CheatUA ? Config.UACheated : Config.UANormal
        };
        settings.CefCommandLineArgs["disable-gpu"] = App.Setting.Content[0].DisableGPU ? "1" : "0";
        settings.CefCommandLineArgs["disable-gpu-compositing"] = App.Setting.Content[0].DisableGPU ? "1" : "0";
        settings.CefCommandLineArgs["enable-system-flash"] = "1";
        settings.CefCommandLineArgs["ppapi-flash-path"] = "Resources/pepflashplayer.dll";
        settings.CefCommandLineArgs["ppapi-flash-version"] = "99.9.9.999";
        Cef.Initialize(settings, true);
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

    public static async Task<string> PageSourceAsync(int id)
        => await Core[id].GetMainFrame( ).GetSourceAsync( );

    public static MenuItem[] ContextMenu(int Id)
        => new MenuItem[]
        {
            new( ) { Header = "前进", Command = new CustomCommand(( ) => GoForward(Id)) },
            new( ) { Header = "后退", Command = new CustomCommand(( ) => GoBack(Id)) },
            new( ) { Header = "刷新", Command = new CustomCommand(( ) => Refresh(Id)) },
            new( ) { Header = "新窗口", Command = new CustomCommand(( ) => New( )) },
            new( ) { Header = "网页源代码", Command = new CustomCommand(( ) => ViewSource(Id)) },
        };
}
