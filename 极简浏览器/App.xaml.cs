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
                SingleInstance.Cleanup( );
            }
            catch (XamlParseException e)
            {
                Logger.Write(e, logType: LogType.Error);
            }
        }
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        if (!this.InitializeAsFirstInstance("EasyBrowserAdvanced_3_4_7_2_Stable"))
            Current.Shutdown( );

        base.OnStartup(e);
        RuntimeFix( );

        Bookmark = new(FilePath.BookMark);
        History = new(FilePath.History);
        Setting = new(FilePath.Setting, false);

        //Resources.MergedDictionaries.Add(new ResourceDictionary( )
        //{
        //    Source = new Uri(Setting.Content[0].UITheme switch
        //    {
        //        0 => "pack://application:,,,/PresentationFramework.Aero,Version=4.0.0.0,Culture=neutral,PublicKeyToken=31bf3856ad364e35,ProcessorArchitecture=MSIL;component/themes/Aero.NormalColor.xaml",
        //        1 => "pack://application:,,,/PresentationFramework.Aero2,Version=4.0.0.0,Culture=neutral,PublicKeyToken=31bf3856ad364e35,ProcessorArchitecture=MSIL;component/themes/Aero2.NormalColor.xaml",
        //        2 => "pack://application:,,,/PresentationFramework.Luna,Version=4.0.0.0,Culture=neutral,PublicKeyToken=31bf3856ad364e35,ProcessorArchitecture=MSIL;component/themes/Luna.NormalColor.xaml",
        //        3 => "pack://application:,,,/PresentationFramework.Luna,Version=4.0.0.0,Culture=neutral,PublicKeyToken=31bf3856ad364e35,ProcessorArchitecture=MSIL;component/themes/Luna.Homestead.xaml",
        //        4 => "pack://application:,,,/PresentationFramework.Luna,Version=4.0.0.0,Culture=neutral,PublicKeyToken=31bf3856ad364e35,ProcessorArchitecture=MSIL;component/themes/Luna.Metallic.xaml",
        //        5 => "pack://application:,,,/PresentationFramework.Luna,Version=4.0.0.0,Culture=neutral,PublicKeyToken=31bf3856ad364e35,ProcessorArchitecture=MSIL;component/themes/Royale.NormalColor.xaml",
        //        6 => "pack://application:,,,/PresentationFramework.Classic,Version=4.0.0.0,Culture=neutral,PublicKeyToken=31bf3856ad364e35,ProcessorArchitecture=MSIL;component/themes/Classic.xaml",
        //        _ => "pack://application:,,,/PresentationFramework.Aero,Version=4.0.0.0,Culture=neutral,PublicKeyToken=31bf3856ad364e35,ProcessorArchitecture=MSIL;component/themes/Aero.NormalColor.xaml",
        //    })
        //});

        Instance.Init( );
    }

    private static void RuntimeFix( )
    {
        Directory.CreateDirectory(FilePath.Profile);
        Directory.CreateDirectory(FilePath.Log);
        Utils.CreateIfNotExists(FilePath.Log + "\\info.log");
        Utils.CreateIfNotExists(FilePath.Log + "\\error.log");
        Utils.CreateIfNotExists(FilePath.Log + "\\warn.log");
    }

    private void ExceptionOpen(object o, DispatcherUnhandledExceptionEventArgs e)
    {
        Logger.Write(e.Exception, LogType.Error);
        new Report(e.Exception).ShowDialog( );
    }

    private void AppExit(object o, ExitEventArgs e)
    {
        Setting.Save( );
        Bookmark.Save( );
        History.Save( );
        Cef.Shutdown( );
    }

    public void OnInstanceInvoked(string[] args)
        => Instance.New(args.Length > 1 ? args[1] : "");
}
