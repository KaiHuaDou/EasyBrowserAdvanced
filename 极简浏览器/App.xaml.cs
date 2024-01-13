using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;
using CefSharp;
using Microsoft.Shell;
using 极简浏览器.Api;

namespace 极简浏览器;

public partial class App : Application, ISingleInstanceApp
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
            if (!SingleInstance<App>.InitializeAsFirstInstance("EasyBrowserAdvanced_3_4_7_2_Stable"))
                return;
            try
            {
                App app = new( );
                app.InitializeComponent( );
                if (args.Length >= 1)
                    startupUri = args[0];
                app.Run( );
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

        Bookmark = new(FilePath.BookMark);
        History = new(FilePath.History);
        Setting = new(FilePath.Setting, false);

        Resources.MergedDictionaries.Add(new ResourceDictionary( )
        {
            Source = new Uri(Setting.Content[0].UITheme switch
            {
                0 => "pack://application:,,,/PresentationFramework.Aero,Version=4.0.0.0,Culture=neutral,PublicKeyToken=31bf3856ad364e35,ProcessorArchitecture=MSIL;component/themes/Aero.NormalColor.xaml",
                1 => "pack://application:,,,/PresentationFramework.Aero2,Version=4.0.0.0,Culture=neutral,PublicKeyToken=31bf3856ad364e35,ProcessorArchitecture=MSIL;component/themes/Aero2.NormalColor.xaml",
                2 => "pack://application:,,,/PresentationFramework.Luna,Version=4.0.0.0,Culture=neutral,PublicKeyToken=31bf3856ad364e35,ProcessorArchitecture=MSIL;component/themes/Luna.NormalColor.xaml",
                3 => "pack://application:,,,/PresentationFramework.Luna,Version=4.0.0.0,Culture=neutral,PublicKeyToken=31bf3856ad364e35,ProcessorArchitecture=MSIL;component/themes/Luna.Homestead.xaml",
                4 => "pack://application:,,,/PresentationFramework.Luna,Version=4.0.0.0,Culture=neutral,PublicKeyToken=31bf3856ad364e35,ProcessorArchitecture=MSIL;component/themes/Luna.Metallic.xaml",
                5 => "pack://application:,,,/PresentationFramework.Luna,Version=4.0.0.0,Culture=neutral,PublicKeyToken=31bf3856ad364e35,ProcessorArchitecture=MSIL;component/themes/Royale.NormalColor.xaml",
                6 => "pack://application:,,,/PresentationFramework.Classic,Version=4.0.0.0,Culture=neutral,PublicKeyToken=31bf3856ad364e35,ProcessorArchitecture=MSIL;component/themes/Classic.xaml",
                _ => "pack://application:,,,/PresentationFramework.Aero,Version=4.0.0.0,Culture=neutral,PublicKeyToken=31bf3856ad364e35,ProcessorArchitecture=MSIL;component/themes/Aero.NormalColor.xaml",
            })
        });

        Instance.Init( );
    }

    private static void RuntimeFix( )
    {
        Directory.CreateDirectory(FilePath.Profile);
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
    }

    public bool SignalExternalCommandLineArgs(IList<string> args)
    {
        Instance.New(args.Count > 1 ? args[1] : "");
        return true;
    }
}
