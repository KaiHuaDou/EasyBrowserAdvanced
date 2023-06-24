using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;
using CefSharp;
using Microsoft.Shell;
using 极简浏览器.Api;
using 极简浏览器.Resources;

namespace 极简浏览器;

public partial class App : Application, ISingleInstanceApp
{
    public static DataStore<Record> History { get; set; }
    public static DataStore<Record> Bookmark { get; set; }
    public static DataStore<Config> Setting { get; set; }
    public static DataStore<CookieData> Cookies { get; set; }

    public static class Program
    {
        private static string startupUri;
        public static string StartupUri
        {
            get => string.IsNullOrWhiteSpace(startupUri) ? Setting.Content[0].MainPage : startupUri;
            set => startupUri = value;
        }

        [STAThread]
        public static void Main(string[] args)
        {
            if (!SingleInstance<App>.InitializeAsFirstInstance("EasyBrowserAdvanced_3_4_7_2_Stable"))
                return;
            try
            {
                App app = new( );
                app.InitializeComponent( );
                if (args.Length >= 1)
                    startupUri = args[0];
                Instance.Host[0] = new MainWindow(0, new Argument( ));
                app.Run(Instance.Host[0]);
                SingleInstance<App>.Cleanup( );
            }
            catch (XamlParseException e)
            {
                Logger.Log(e, type: LogType.Error);
            }
        }
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        RuntimeFix( );
        History = new(FilePath.History);
        Bookmark = new(FilePath.BookMark);
        Setting = new(FilePath.Setting, true);
        Cookies = new(FilePath.Cookies);
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
        Logger.Log(e.Exception, type: LogType.Error);
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

    public bool SignalExternalCommandLineArgs(IList<string> args)
    {
        Instance.New(args.Count > 1 ? args[0] : "");
        return true;
    }
}
