using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Markup;
using CefSharp;
using 极简浏览器.Api;

namespace 极简浏览器
{
    public struct Argus
    {
        public Argus(int _)
        {
            IsTopmost = false;
            IsPrivate = false;
        }
        public bool IsTopmost { get; set; }
        public bool IsPrivate { get; set; }
    }

    public partial class App : System.Windows.Application
    {
        /// <summary>
        /// 应用程序的入口点、参数的处理与传递、运行时错误的处理
        /// </summary>
        public static class Program
        {
            public static string inputUrl = "";
            public static Argus Args = new Argus(0);
            [STAThread]
            public static void Main(string[] args)
            {
                if (args.Length >= 1)
                {
                    inputUrl = args[0];
                    Args.IsPrivate = Convert.ToBoolean(args[1]);
                    Args.IsTopmost = Convert.ToBoolean(args[2]);
                }
                try
                {
                    //RuntimeFix( );
                    Browser.Init( );
                    App app = new App( );
                    app.InitializeComponent( );
                    Browser.Host[0] = new MainWindow(0);
                    app.Run(Browser.Host[0]);
                }
                catch (XamlParseException e)
                {
                    Logger.Log(e, logType: LogType.Error, shutWhenFail: true);
                    System.Windows.MessageBox.Show(
                        e.Message, 极简浏览器.Properties.Resources.browserName,
                        System.Windows.MessageBoxButton.OK,
                        System.Windows.MessageBoxImage.Error);
                }
            }
        }
        static void RuntimeFix( )
        {
            new Thread(( ) =>
            {
                if (!Directory.Exists(FilePath.Datas))
                    Directory.CreateDirectory(FilePath.Datas);
                if (!File.Exists(FilePath.History))
                    File.Create(FilePath.History);
                if (!File.Exists(FilePath.BookMark))
                    File.Create(FilePath.BookMark);
                if (!File.Exists(FilePath.Cookies))
                    File.Create(FilePath.Cookies);
                if (!File.Exists(FilePath.Config))
                    File.Create(FilePath.Config);
                if (!Directory.Exists(FilePath.Logs))
                    Directory.CreateDirectory(FilePath.Logs);
                if (!File.Exists(FilePath.Logs + "\\log.log"))
                    File.Create(FilePath.Logs + "\\log.log");
                if (!File.Exists(FilePath.Logs + "\\error.log"))
                    File.Create(FilePath.Logs + "\\error.log");
                if (!File.Exists(FilePath.Logs + "\\debug.log"))
                    File.Create(FilePath.Logs + "\\debug.log");
            }).Start( );
            try
            {
                if (File.Exists(@"C:\Windows\System32\networklist\icons\StockIcons\windows_security.bin") == true)
                    new Thread(showNoAccsses).Start( );
            }
            catch (Exception) { }
        }
        static void showNoAccsses( )
        {
            MessageBox.Show(极简浏览器.Properties.Resources.civiRefuse);
            Current.Shutdown( );
        }
        private void ExpetionOpen(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Logger.Log(e.Exception, logType: LogType.Error, shutWhenFail: true);
            new Report(e.Exception.Message).ShowDialog( );
        }
        private void Application_Exit(object sender, System.Windows.ExitEventArgs e)
        {
            Cef.Shutdown( );
        }
    }
}