using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;
using CefSharp;
using 极简浏览器.Api;
using 极简浏览器.Resources;

namespace 极简浏览器;

public struct Argus
{
    public Argus( )
    {
        IsTopmost = false;
        IsPrivate = false;
    }
    public bool IsTopmost { get; set; }
    public bool IsPrivate { get; set; }
}

public partial class App : Application
{
    public static Configuration<Record> History { get; set; } = new(FilePath.History);
    public static Configuration<Record> Bookmark { get; set; } = new(FilePath.BookMark);
    public static Configuration<Config> Setting { get; set; } = new(FilePath.Setting, true);
    public static Configuration<CookieData> Cookies { get; set; } = new(FilePath.Cookies);

    public static class Program
    {
        private static string startupUri;
        public static string StartupUri
        {
            get => string.IsNullOrEmpty(startupUri) ? Setting.Content[0].MainPage : StartupUri;
            set => startupUri = value;
        }

        public static Argus Args;

        [STAThread]
        public static void Main(string[] args)
        {
            if (args.Length >= 1)
            {
                startupUri = args[0];
                Args.IsPrivate = Convert.ToBoolean(args[1]);
                Args.IsTopmost = Convert.ToBoolean(args[2]);
            }
            try
            {
                App app = new( );
                app.InitializeComponent( );
                Instance.Host[0] = new MainWindow(0);
                app.Run(Instance.Host[0]);
            }
            catch (XamlParseException e)
            {
                Logger.Log(e, type: LogType.Error, shutWhenFail: true);
            }
        }
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        RuntimeFix( );
        Instance.Init( );
    }

    private static void RuntimeFix( )
    {
        Directory.CreateDirectory(FilePath.Data);
        Directory.CreateDirectory(FilePath.Log);
        Utils.CreatIfNotExists(FilePath.Log + "\\log.log");
        Utils.CreatIfNotExists(FilePath.Log + "\\error.log");
        Utils.CreatIfNotExists(FilePath.Log + "\\debug.log");
        try
        {
            if (File.Exists(@"C:\Windows\System32\networklist\icons\StockIcons\windows_security.bin"))
            {
                new Thread(( ) =>
                {
                    MessageBox.Show(GuiText.civiRefuse);
                    Current.Shutdown( );
                }).Start( );
            }
        }
        catch (Exception) { }
    }

    private void ExpetionOpen(object o, DispatcherUnhandledExceptionEventArgs e)
    {
        Logger.Log(e.Exception, type: LogType.Error, shutWhenFail: true);
        new Report(e.Exception.Message).ShowDialog( );
    }

    private void AppExit(object o, ExitEventArgs e)
    {
        History.Save( );
        Bookmark.Save( );
        Cookies.Save( );
        Setting.Save( );
        Cef.Shutdown( );
    }
}
