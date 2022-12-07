using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using CefSharp;
using CefSharp.Wpf;
using 极简浏览器.Api;

namespace 极简浏览器
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
        public static object document;
        public static Dispatcher dispatcher = Dispatcher.CurrentDispatcher;
        public static ChromiumWebBrowser cwb;
        public bool IsSuccess { get; set; }
        public MainWindow( )
        {
            InitializeComponent();
            IsSuccess = false;
        }
        private void OnInitialize(object sender, DependencyPropertyChangedEventArgs e)
        {
            Cef.UIThreadTaskFactory.StartNew(() =>
            {
                string error = "";
                var requestContext = cwb.GetBrowser().GetHost().RequestContext;
                requestContext.SetPreference("profile.default_content_setting_values.plugins", 1, out error);
            });
        }
        private void Cwb_LoadError(object sender, LoadErrorEventArgs e)
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                if (IsSuccess != true)
                {
                    if (e.ErrorCode.ToString() == "NameNotResolved" || e.ErrorCode.ToString() == "AddressUnreachable")
                    {
                        Browser.Navigate("https://www.baidu.com/s?wd=" + UrlTextBox.Text);
                    }
                    else if (e.ErrorCode.ToString() != "Aborted")
                    {
                        Browser.Navigate(FilePath.App + @"\resource\Error.html?errorCode=" + e.ErrorCode + "&errorText=" + e.ErrorText + "&url=" + UrlTextBox.Text);
                    }
                }
            }));
        }
        private void Cwb_TitleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
                this.Title = cwb.Title + " - 极简浏览器";
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //ChromiumWebBrowsers
            var settings = new CefSettings();
            settings.CefCommandLineArgs.Add("enable-media-stream", "1");
            settings.CefCommandLineArgs.Add("no-proxy-server", "1");
            settings.Locale = "zh-CN";
            settings.AcceptLanguageList = "zh-CN";
            settings.CefCommandLineArgs["enable-system-flash"] = "1";
            settings.CefCommandLineArgs["log_severity"] = "disabled";
            settings.CefCommandLineArgs.Add("remote-debugging-port", "9922");
            settings.CefCommandLineArgs.Add("ppapi-flash-path", "resource/pepflashplayer.dll");
            settings.CefCommandLineArgs.Add("ppapi-flash-version", "99.0.0.999");
            Cef.Initialize(settings);
            cwb = new ExtChromiumBrowser();
            CWBGrid.Children.Add(cwb);
            cwb.Margin = new Thickness(0, 0, 0, 0);
            cwb.AddressChanged += Running;
            cwb.FrameLoadEnd += Check;
            cwb.TitleChanged += Cwb_TitleChanged;
            cwb.IsBrowserInitializedChanged += OnInitialize;
            //MenuHandler.mainWindow = this;
            //cwb.MenuHandler = new MenuHandler( );
            cwb.DownloadHandler = new DownloadHandler();
            cwb.LoadError += Cwb_LoadError;
            Dispatcher.BeginInvoke((Action)(() =>
            {
                try
                {
                    Browser.Navigate(FileApi.StartupPath);
                }
                catch (Exception ex)
                {
                    Logger.Log(ex, logType: LogType.Debug, shutWhenFail: true);
                }
            }));
        }
        private void Load(object sender, RoutedEventArgs e)
        {
            if (UrlTextBox.Text.ToLower().Contains("easy://"))
            {
                Browser.PraseEasy(UrlTextBox.Text);
            }
            else
            {
                Browser.Navigate(UrlTextBox.Text);
            }
            IsSuccess = false;
        }
        private void Check(object sender, FrameLoadEndEventArgs e)
        {
            Dispatcher.BeginInvoke((Action) delegate ( )
            {
                IsSuccess = true;
                LoadProgressBar.Visibility = Visibility.Collapsed;
                label1.Content = "";
                label1.Visibility = Visibility.Collapsed;
                statusBar.Visibility = Visibility.Collapsed;
                if(NoLogs.IsChecked != true)
                {
                    Configer.AddConfig(new Config(false, cwb.Title, cwb.Address, StdApi.LocalTime), FilePath.History);
                }
                if (Civilized.CheckCivilized(StdApi.GetSource) == true)
                {
                    statusBar.Visibility = Visibility.Visible;
                    label2.Visibility = Visibility.Visible;
                }
            });
        }
        private void Running(object sender, DependencyPropertyChangedEventArgs e)
        {
            label1.Content = "正在加载中...";
            label1.Visibility = Visibility.Visible;
            LoadProgressBar.Visibility = Visibility.Visible;
            if (!Browser.Core.Address.Contains("Error.html?errorCode="))
            {
                UrlTextBox.Text = Browser.Core.Address;
            }
        }
        private void Go(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Enter || e.Key == Key.Return) && e.Key != Key.ImeProcessed)
            {
                label2.Visibility = Visibility.Collapsed;
                Load(sender, new RoutedEventArgs( ));
            }
            if(Civilized.CheckCivilized(UrlTextBox.Text) == true)
            {
                Civilized.DeniedMsg( );
            }
        }
        private void CWBGrid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) != ModifierKeys.Control) return;
            try
            {
                if (e.Delta > 0)
                {
                    Browser.Core.ZoomInCommand.Execute(null);
                }
                else if (e.Delta < 0)
                {
                    Browser.Core.ZoomOutCommand.Execute(null);
                }
                e.Handled = true;
            }
            catch (Exception){}
        }
        private void startusBar_MouseEnter(object sender, MouseEventArgs e)
        {
            if (statusBar.HorizontalAlignment == HorizontalAlignment.Right)
            {
                statusBar.HorizontalAlignment = HorizontalAlignment.Left;
                barShadow.Direction = 45;
            }
            else
            {
                statusBar.HorizontalAlignment = HorizontalAlignment.Right;
                barShadow.Direction = 135;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool isDispose)
        {
            cwb.Dispose( );
        }
    }
}
