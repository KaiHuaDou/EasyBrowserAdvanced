using System;
using System.IO;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;
using CefSharp;
using SingleInstanceCore;
using 极简浏览器.Api;

namespace 极简浏览器;

public partial class App : Application, ISingleInstance
{
    public static DataStore<Record> History { get; set; }
    public static DataStore<Record> Bookmark { get; set; }
    public static DataStore<Config> Setting { get; set; }

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
            try
            {
                App app = new( );
                app.InitializeComponent( );
                if (args.Length >= 1)
                    startupUri = args[0];
                app.Run( );
            }
            catch (XamlParseException e)
            {
                Logger.Log(e, type: LogType.Error);
            }
        }
    }

    public void OnInstanceInvoked(string[] args) 
        => Instance.New(args.Length > 1 ? args[0] : "");

    protected override void OnStartup(StartupEventArgs e)
    {
        if (!this.InitializeAsFirstInstance("EasyBrowserAdvanced_3_4_7_2_Stable"))
            Current.Shutdown( );

        base.OnStartup(e);

        RuntimeFix( );
        Bookmark = new(FilePath.BookMark);
        History = new(FilePath.History);
        Setting = new(FilePath.Setting, false);
        Instance.Init( );
    }

    private static void RuntimeFix( )
    {
        Directory.CreateDirectory(FilePath.Data);
        Directory.CreateDirectory(FilePath.Log);
        Utils.CreateIfNotExists(FilePath.Log + "\\log.log");
        Utils.CreateIfNotExists(FilePath.Log + "\\error.log");
        Utils.CreateIfNotExists(FilePath.Log + "\\debug.log");
    }

    private void ExceptionOpen(object o, DispatcherUnhandledExceptionEventArgs e)
    {
        Logger.Log(e.Exception, type: LogType.Error);
        new Report(e.Exception.Message).ShowDialog( );
    }

    private void AppExit(object o, ExitEventArgs e)
    {
        History.Save( );
        Bookmark.Save( );
        Setting.Save( );
        Cef.Shutdown( );
        SingleInstance.Cleanup( );
    }
}
