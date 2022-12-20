using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using CefSharp;
using CefSharp.Wpf;
using 极简浏览器.Api;

namespace 极简浏览器
{
    public partial class MainWindow : Window
    {
        public static object document;
        public static Dispatcher dispatcher = Dispatcher.CurrentDispatcher;
        public static ChromiumWebBrowser cwb;

        public bool IsSuccess { get; set; }

        public MainWindow()
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cwb = Browser.Core;
            CWBGrid.Children.Add(cwb);
            cwb.Margin = new Thickness(0);
            cwb.AddressChanged += Running;
            cwb.FrameLoadEnd += Check;
            cwb.TitleChanged += Cwb_TitleChanged;
            cwb.IsBrowserInitializedChanged += OnInitialize;
            MenuHandler.mainWindow = this;
            cwb.MenuHandler = new MenuHandler();
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

        private void Go(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Enter || e.Key == Key.Return) && e.Key != Key.ImeProcessed)
            {
                Load(sender, new RoutedEventArgs());
            }
            if (Civilized.CheckCivilized(UrlTextBox.Text)) Civilized.DeniedMsg();
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            if (UrlTextBox.Text.ToLower().Contains("easy://"))
                Browser.PraseEasy(UrlTextBox.Text);
            else if (Regex.IsMatch(UrlTextBox.Text, @"\.[A-Za-z]{1,4}|(://)|^[A-Za-z]:\\|\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}(:\d{1,4})?|^about:"))
                Browser.Navigate(UrlTextBox.Text);
            else Browser.Navigate("https://cn.bing.com/search?q=" + UrlTextBox.Text);
            IsSuccess = false;
        }

        private void Running(object sender, DependencyPropertyChangedEventArgs e)
        {
            loadLabel.Visibility = Visibility.Visible;
            civiLabel.Visibility = Visibility.Collapsed;
            LoadProgress.Visibility = Visibility.Visible;
            if (!Browser.Address.Contains("Error.html?errorCode="))
                UrlTextBox.Text = Browser.Address;
        }

        private void Check(object sender, FrameLoadEndEventArgs e)
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                IsSuccess = true;
                LoadProgress.Visibility = Visibility.Collapsed;
                loadLabel.Visibility = Visibility.Collapsed;
                CookieMgr.Set();
                if (!App.Program.argus.IsPrivate)
                    Configer<Config>.Add(new Config(false, cwb.Title, cwb.Address, StdApi.LocalTime), FilePath.History);
                if (Civilized.CheckCivilized(StdApi.PageText))
                    civiLabel.Visibility = Visibility.Visible;
            }));
        }

        private void Cwb_TitleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.Title = Browser.Title + " - 极简浏览器";
        }

        private void Cwb_LoadError(object sender, LoadErrorEventArgs e)
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                if (IsSuccess != true && e.ErrorCode.ToString() != "Aborted")
                    Browser.Navigate(FilePath.Runtime + @"\resource\Error.html?errorCode=" + e.ErrorCode + "&errorText=" + e.ErrorText + "&url=" + UrlTextBox.Text);
            }));
        }

        private void CWBGrid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) != ModifierKeys.Control) return;
            if (e.Delta > 0)
                Browser.Core.ZoomInCommand.Execute(null);
            else if (e.Delta < 0)
                Browser.Core.ZoomOutCommand.Execute(null);
            e.Handled = true;
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
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            cwb.Dispose();
            GC.SuppressFinalize(cwb);
            GC.SuppressFinalize(this);
        }
    }
}