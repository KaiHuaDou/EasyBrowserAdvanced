using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using CefSharp;
using CefSharp.Wpf;
using 极简浏览器.Api;

namespace 极简浏览器
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        string Url = "";
        public static object document;
        public ChromiumWebBrowser cwb = new ExtChromiumBrowser( );
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

            //ChromiumWebBrowsers
            CWBGrid.Children.Add(cwb);
            cwb.Margin = new Thickness(0, 0, 0, 0);
            cwb.AddressChanged += Running;
            cwb.FrameLoadEnd += Check;
            cwb.TitleChanged += Cwb_TitleChanged;
            MenuHandler.mainWindow = this;
            cwb.MenuHandler = new MenuHandler( );
            cwb.DownloadHandler = new DownloadHandler( );
            //CefSettings settings = new CefSettings( );
            //settings.Locale = "zh-CN";
            //cwb.BrowserSettings = (IBrowserSettings)settings;
        }

        private void Cwb_TitleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.Title = cwb.Title + " - 极简浏览器";
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke((Action)delegate ( )
            {
                try
                {
                    BrowserCore.Navigate(FileApi.GetStartupPath(Url, Isnew));
                }
                catch (Exception ex)
                {
                    Logger.Log(ex, logType: LogType.Debug, shutWhenFail: true);
                    this.Close( );
                }
            });
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
                FileApi.Write(cwb.Title, cwb.Address, FileType.History);
                if (CivilizedLanguage.CheckIfNotCivilized(StandardApi.GetPageSource( )) == true)
                {
                    label2.Visibility = Visibility.Visible;
                }
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
            UrlTextBox.Text = BrowserCore.GetBrowser( ).Address;
        }

        private void StatusBar_ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            if (startusBar.Visibility == Visibility.Visible)
            {
                startusBar.Visibility = Visibility.Collapsed;
                OptionMenu.Visibility = Visibility.Collapsed;
                BrowserCore.GetInstance( ).CWBGrid.Margin = new Thickness(0, 37, 0, 0);
                HidestartusBar.Header = "显示状态栏";
            }
            else
            {
                startusBar.Visibility = Visibility.Visible;
                OptionMenu.Visibility = Visibility.Visible;
                BrowserCore.GetInstance( ).CWBGrid.Margin = new Thickness(0, 37, 0, 35);
                HidestartusBar.Header = "隐藏状态栏";
            }
        }

        private void Go(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Enter || e.Key == Key.Return) && e.Key != Key.ImeProcessed)
            {
                label2.Visibility = Visibility.Collapsed;
                Load(sender, new RoutedEventArgs( ));
            }
            if(CivilizedLanguage.CheckIfNotCivilized(UrlTextBox.Text) == true)
            {
                CivilizedLanguage.ShowDeniedMessage( );
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool IsDispose)
        {
            cwb.Dispose( );
        }

        private void Topmost_Checked(object sender, RoutedEventArgs e)
        {
            this.Topmost = !this.Topmost;
        }
    }
}
