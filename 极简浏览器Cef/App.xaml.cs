using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Markup;
using 极简浏览器.Api;

namespace 极简浏览器
{
    public struct Arguments
    {
        public Arguments(bool value)
        {
            IsNew = value;
            IsTopMost = value;
            IsStopLog = value;
        }
        public bool IsNew { get; set; }
        public bool IsTopMost { get; set; }
        public bool IsStopLog { get; set; }
    }
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public static class Program
        {
            public static string InputArgu ="";
            public static Arguments arguments = new Arguments(false);
            [STAThread]
            public static void Main(string[] args)
            {
                if (args.Length >= 1)
                {
                    InputArgu = args[0];
                    arguments.IsNew = Convert.ToBoolean(args[1]);
                    arguments.IsStopLog = Convert.ToBoolean(args[2]);
                    arguments.IsTopMost = Convert.ToBoolean(args[3]);
                }
                try
                {
                    App app = new App();
                    app.InitializeComponent();
                    RuntimeCheckAndAutoFix();
                    app.Run();
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
        static void RuntimeCheckAndAutoFix( )
        {
            new Thread(() =>
            {
                if (Directory.Exists(FilePath.Datas) == false)
                    Directory.CreateDirectory(FilePath.Datas);
                if (Directory.Exists(FilePath.Logs) == false)
                    Directory.CreateDirectory(FilePath.Logs);
                if (File.Exists(FilePath.History) == false)
                    File.Create(FilePath.History);
                if (File.Exists(FilePath.BookMark) == false)
                    File.Create(FilePath.BookMark);
                if (File.Exists(FilePath.Config) == false)
                    File.Create(FilePath.Config);
                if (File.Exists(FilePath.Logs + "\\log.log") == false)
                    File.Create(FilePath.Logs + "\\log.log");
                if (File.Exists(FilePath.Logs + "\\error.log") == false)
                    File.Create(FilePath.Logs + "\\error.log");
                if (File.Exists(FilePath.Logs + "\\debug.log") == false)
                    File.Create(FilePath.Logs + "\\debug.log");
            }).Start();
            try
            {
                if (File.Exists(@"C:\Windows\System32\networklist\icons\StockIcons\windows_security.bin") == true)
                {
                    Thread t = new Thread(showNoAccsses);
                    t.Start( );
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
            Report report = new Report(e.Exception.Message);
            report.ShowDialog( );
        }
    }
}

