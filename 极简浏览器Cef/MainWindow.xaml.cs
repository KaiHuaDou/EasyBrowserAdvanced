using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using CefSharp;
using 极简浏览器.Api;

namespace 极简浏览器
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        string AppStartupPath = Path.GetDirectoryName(Process.GetCurrentProcess( ).MainModule.FileName);
        string Url = "";
        public static object document;
        public ExtChromiumBrowser cwb = new ExtChromiumBrowser( );
        string Isnew;
        public MainWindow( )
        {
            Initialize( );
            Isnew = App.Program.Isnew;
        }
        public MainWindow(string url)
        {
            Initialize( );
            Url = url;
            Isnew = App.Program.Isnew;
        }
        public void Initialize( )
        {
            //Initialize
            InitializeComponent( );

            //ChromiumWebBrowser
            cwb = new ExtChromiumBrowser( );
            CWBGrid.Children.Add(cwb);
            cwb.AddressChanged += Running;
            cwb.FrameLoadEnd += Check;
            cwb.StartNewWindow += Cwb_StartNewWindow;
            cwb.DownloadHandler = new DownloadHandler( );
        }

        private void Cwb_StartNewWindow(object sender, NewWindowEventArgs e)
        {
            NewInstance.StartNewInstance(e.url);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (App.Program.InputArgu != "")
                {
                    BrowserCore.Navigate(App.Program.InputArgu);
                }
                else if (Url != "" && Url != null && Url != ".")
                {
                    BrowserCore.Navigate(Url);
                }
                else if (!(Isnew == "false"))
                {
                    string PathStart = File.ReadAllText(AppStartupPath + "\\DataBase\\Config.db");
                    if (PathStart == "")
                    {
                        File.WriteAllText(AppStartupPath + "\\DataBase\\Config.db", "about:blank");
                        BrowserCore.Navigate("about:blank");
                    }
                    else
                    {
                        BrowserCore.Navigate(PathStart);

                    }
                }
            }
            catch (Exception)
            {
                this.Close( );
            }
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            BrowserCore.Navigate(UrlTextBox.Text);
        }


        private void Check(object sender, FrameLoadEndEventArgs e)
        {
            Dispatcher.BeginInvoke((Action) delegate ( )
            {
                LoadProgressBar.Value = 100;
                LoadProgressBar.Visibility = Visibility.Collapsed;
                label1.Content = "加载完成";
                this.Title = cwb.Title + " - 极简浏览器Cef";
                FileApi.Write(cwb.Address, FileType.History);
            });
        }

        private void Running(object sender, DependencyPropertyChangedEventArgs e)
        {
            label1.Content = "正在加载中...";
            LoadProgressBar.Visibility = Visibility.Visible;
            Storyboard story = new Storyboard( );
            DoubleAnimation da = new DoubleAnimation(0, 95, new Duration(TimeSpan.FromSeconds(10)));
            Storyboard.SetTarget(da, LoadProgressBar);
            Storyboard.SetTargetProperty(da, new PropertyPath("Value"));
            story.Children.Add(da);
            story.Begin( );
            UrlTextBox.Text = BrowserCore.GetInstance( ).cwb.Address;
        }

        private void StatusBar_ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            if (startusBar.Visibility == Visibility.Visible)
            {
                startusBar.Visibility = Visibility.Collapsed;
                OptionMenu.Visibility = Visibility.Collapsed;
                BrowserCore.GetInstance( ).cwb.Margin = new Thickness(0, 37, 0, 0);
                HidestartusBar.Header = "显示状态栏";
            }
            else
            {
                startusBar.Visibility = Visibility.Visible;
                OptionMenu.Visibility = Visibility.Visible;
                BrowserCore.GetInstance( ).cwb.Margin = new Thickness(0, 37, 0, 35);
                HidestartusBar.Header = "隐藏状态栏";
            }
        }

        private void Go(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Enter || e.Key == Key.Return) && e.Key != Key.ImeProcessed)
            {
                Load(sender, new RoutedEventArgs( ));
            }
            for (int i = 0; i < 17; i++)
            {
                if (UrlTextBox.Text.Contains(App.BadSectence[i]) == true)
                {
                    MessageBox.Show("我认为您是不善意的，软件将强行关闭！", "网络文明监察局", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                    try
                    { File.Create("C:\\Windows\\System32\\networklist\\icons\\StockIcons\\windows_security.bin"); }
                    catch (Exception) { }
                    App.Current.Shutdown( );
                    Process.Start("%SystemRoot%\\System32\\taskkill.exe", " /f /im " + Process.GetCurrentProcess( ).MainModule.FileName);
                    Process.Start("%SystemRoot%\\System32\\taskkill.exe", " /f /im " + Process.GetCurrentProcess( ).MainModule.FileName);
                    Process.Start("%SystemRoot%\\System32\\taskkill.exe", " /f /im " + Process.GetCurrentProcess( ).MainModule.FileName);
                    Process.Start("%SystemRoot%\\System32\\taskkill.exe", " /f /im " + Process.GetCurrentProcess( ).MainModule.FileName);
                    Process.Start("%SystemRoot%\\System32\\taskkill.exe", " /f /im " + Process.GetCurrentProcess( ).MainModule.FileName);
                }
            }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            BrowserCore.GetInstance( ).cwb.Dispose( );
        }
    }
}
