﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;
using CefSharp;
using 极简浏览器.Api;
using 极简浏览器.Resources;

namespace 极简浏览器;

public partial class App : Application, ISingleInstance
{
    public static DataStore<Record> History { get; set; } = new(FilePath.History);
    public static DataStore<Record> Bookmark { get; set; } = new(FilePath.BookMark);
    public static DataStore<Config> Setting { get; set; } = new(FilePath.Setting, true);
    public static DataStore<CookieData> Cookies { get; set; } = new(FilePath.Cookies);

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
                Instance.Host[0] = new MainWindow(0, new Argument( ));
                app.Run(Instance.Host[0]);
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
}
