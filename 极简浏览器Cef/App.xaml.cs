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
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public static class Program
        {
            public static string inputUrl ="";
            public static Argus argus = new Argus(0);
            [STAThread]
            public static void Main(string[] args)
            {
                if (args.Length >= 1)
                {
                    inputUrl = args[0];
                    argus.IsPrivate = Convert.ToBoolean(args[1]);
                    argus.IsTopmost = Convert.ToBoolean(args[2]);
                }
                try
                {
                    RuntimeFix();
                    Browser.Init();
                    App app = new App();
                    app.InitializeComponent();
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
            new Thread(() =>
            {
                if (!Directory.Exists(FilePath.Datas))
                    Directory.CreateDirectory(FilePath.Datas);
                if (!Directory.Exists(FilePath.Logs))
                    Directory.CreateDirectory(FilePath.Logs);
                if (!File.Exists(FilePath.History))
                    File.Create(FilePath.History);
                if (!File.Exists(FilePath.BookMark))
                    File.Create(FilePath.BookMark);
                if (!File.Exists(FilePath.Config))
                    File.Create(FilePath.Config);
                if (!File.Exists(FilePath.Logs + "\\log.log"))
                    File.Create(FilePath.Logs + "\\log.log");
                if (!File.Exists(FilePath.Logs + "\\error.log"))
                    File.Create(FilePath.Logs + "\\error.log");
                if (!File.Exists(FilePath.Logs + "\\debug.log"))
                    File.Create(FilePath.Logs + "\\debug.log");
            }).Start();
            try
            {
                if (File.Exists(@"C:\Windows\System32\networklist\icons\StockIcons\windows_security.bin") == true)
                {
                    new Thread(showNoAccsses).Start( );
                    Current.Shutdown( );
                }
            }
            catch(Exception)
            {
                Current.Shutdown();
            }
        }
        static void showNoAccsses()
        {
            MessageBox.Show(极简浏览器.Properties.Resources.civiRefuse);
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