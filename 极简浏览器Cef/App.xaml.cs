using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Markup;
using System.Windows.Shell;
using 极简浏览器.Api;

namespace 极简浏览器
{
    public struct Arguments
    {
        public Arguments(bool value)
        {
            isNew = value;
            isTopMost = value;
            isNotLogging = value;
        }
        public bool isNew;
        public bool isTopMost;
        public bool isNotLogging;
    }
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public class Program
        {
            public static string InputArgu ="";
            public static Arguments arguments = new Arguments(false);
            ///<summary>
            ///应用程序的主入口点。
            ///</summary>
            [STAThread]
            public static void Main(string[] args)
            {
                if (args.Length >= 1)
                {
                    InputArgu = args[0];
                    arguments.isNew = Convert.ToBoolean(args[1]);
                    arguments.isNotLogging = Convert.ToBoolean(args[2]);
                    arguments.isTopMost = Convert.ToBoolean(args[3]);
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
                catch (Exception) { }
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
            if (File.Exists(FilePath.LogDirectory + "\\error.log") == false)
                File.Create(FilePath.LogDirectory + "\\error.log");
            if (File.Exists(FilePath.LogDirectory + "\\debug.log") == false)
                File.Create(FilePath.LogDirectory + "\\debug.log");
            try
            {
                if (File.Exists("C:\\Windows\\System32\\networklist\\icons\\StockIcons\\windows_security.bin") == true)
                {
                    Thread t = new Thread(showNoAccsses);
                    t.Start( );
                    App.Current.Shutdown( );
                }
            }
            catch(Exception)
            {
                MessageBox.Show("文明语言检测器启动失败，单击确定以继续使用。",
                    "极简浏览器",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.ServiceNotification);
            }
        }
        static void showNoAccsses()
        {
            MessageBox.Show(极简浏览器.Properties.Resources.Accsses_cancel);
        }
        private void ExpetionOpen(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Logger.Log(e.Exception, logType: LogType.Error, shutWhenFail: true);
            Report report = new Report(e.Exception.Message);
            report.ShowDialog( );
        }
    }
}

