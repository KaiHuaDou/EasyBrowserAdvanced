using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using System.Windows.Shell;
using 极简浏览器.Api;

namespace 极简浏览器
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : System.Windows.Application
    {
        JumpList jumplist = new JumpList( );
        public class Program
        {
            public static string InputArgu ="";
            public static string Isnew { get; private set; }
            public static bool isNotLog { get; private set; }
            ///<summary>
            ///应用程序的主入口点。
            ///</summary>
            [STAThread]
            public static void Main(string[] args)
            {
                if (args.Length >= 1)
                {
                    InputArgu = args[0];
                    Isnew = args[1];
                    isNotLog = Convert.ToBoolean(args[2]);
                }
                try
                {
                     App app = new App( );
                     app.InitializeComponent( );
                     RuntimeCheckAndAutoFix( );
                     app.Run( );
                }
                catch (XamlParseException e)
                {
                    Logger.Log(e, logType: LogType.Error, shutWhenFail: true);
                    System.Windows.MessageBox.Show(e.Message, 极简浏览器.Properties.Resources.BrowserName, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error, System.Windows.MessageBoxResult.OK, System.Windows.MessageBoxOptions.ServiceNotification);
                }
            }
        }

        static void RuntimeCheckAndAutoFix( )
        {
            if (Directory.Exists(FilePath.DataBaseDirectory) == false)
                Directory.CreateDirectory(FilePath.DataBaseDirectory);
            if (Directory.Exists(FilePath.LogDirectory) == false)
                Directory.CreateDirectory(FilePath.LogDirectory);
            if (File.Exists(FilePath.HistoryPath) == false)
                File.Create(FilePath.HistoryPath);
            if (File.Exists(FilePath.BookMarkPath))
            if (File.Exists(FilePath.ConfigPath) == false)
                File.Create(FilePath.ConfigPath);
            if (File.Exists(FilePath.LogDirectory + "\\log.log") == false)
                File.Create(FilePath.LogDirectory + "\\log.log");
            if(File.Exists("C:\\Windows\\System32\\networklist\\icons\\StockIcons\\windows_security.bin") == true)
            {
                Thread t = new Thread(showNoAccsses);
                t.Start( );
                App.Current.Shutdown( );
            }
        }
        static void showNoAccsses()
        {
            MessageBox.Show(极简浏览器.Properties.Resources.Accsses_cancel);
        }
        private void ExpetionOpen(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Logger.Log(e.Exception, logType: LogType.Error, shutWhenFail: true);
            DialogResult dr = new DialogResult( );
            dr = Logger.Message(e.Exception, true);
            if (dr == DialogResult.Abort)
                App.Current.Shutdown(1);
            if (dr == DialogResult.Retry)
                e.Handled = false;
            if (dr == DialogResult.Ignore)
                return;
            return;
        }

        private void Application_Startup(object sender, System.Windows.StartupEventArgs e)
        {
            JumpList.SetJumpList(App.Current, jumplist);
            JumpTask jumptask = new JumpTask( );
            jumptask.CustomCategory = "任务";
            jumptask.Title = "新建窗口";
            jumptask.ApplicationPath = FilePath.AppRuntime;
            jumptask.IconResourcePath = FilePath.AppRuntime;
            jumptask.Arguments = "about:blank false";
            jumplist.JumpItems.Add(jumptask);
            jumplist.Apply( );
        }
    }
}

