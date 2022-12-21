using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using CefSharp;
using 极简浏览器.Api;

namespace 极简浏览器
{
    /// <summary>
    /// 主窗口的交互代码
    /// 负责：ChromiumWebBrowser 的相关事宜、初始化
    /// </summary>
    public partial class MainWindow : Window
    {
        private int Id;
        private bool IsSuccess;

        public MainWindow()
        {
            this.Close( );
        }
        public MainWindow(int id)
        {
            InitializeComponent( );
            IsSuccess = false;
            Id = id;
        }
        private void BrowserLoaded(object o, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                Cef.UIThreadTaskFactory.StartNew(( ) =>
                {
                    string error = "";
                    var requestContext = Browser.Core[Id].GetBrowser( ).GetHost( ).RequestContext;
                    requestContext.SetPreference("profile.default_content_setting_values.plugins", 1, out error);
                });
            }
            catch (NullReferenceException) { }
        }
        private void WindowLoaded(object o, RoutedEventArgs e)
        {
            Browser.Core[Id] = new ExtChromiumBrowser(Id);
            Browser.Core[Id].Margin = new Thickness(0);
            Browser.Core[Id].AddressChanged += Nav_Loading;
            Browser.Core[Id].FrameLoadEnd += Nav_Loaded;
            Browser.Core[Id].TitleChanged += Cwb_TitleChanged;
            Browser.Core[Id].IsBrowserInitializedChanged += BrowserLoaded;
            Browser.Core[Id].LoadError += Cwb_LoadError;
            MenuHandler.mainWindow = this;
            Browser.Core[Id].MenuHandler = new MenuHandler(Id);
            Browser.Core[Id].DownloadHandler = new DownloadHandler( );
            CWBGrid.Children.Add(Browser.Core[Id]);
            Dispatcher.BeginInvoke((Action) (( ) =>
            {
                try
                {
                    Browser.Navigate(Id, FileApi.StartupPath);
                }
                catch (Exception ex)
                {
                    Logger.Log(ex, logType: LogType.Debug, shutWhenFail: true);
                }
            }));
        }
        private void Nav_KeyDown(object o, KeyEventArgs e)
        {
            if ((e.Key == Key.Enter || e.Key == Key.Return) && e.Key != Key.ImeProcessed)
            {
                Nav_ProcessUrl(o, new RoutedEventArgs( ));
            }
            if (Civilized.CheckCivilized(UrlTextBox.Text))
                Civilized.DeniedMsg( );
        }
        private void Nav_ProcessUrl(object o, RoutedEventArgs e)
        {
            if (UrlTextBox.Text.ToLower( ).Contains("easy://"))
                Browser.PraseEasy(Id, UrlTextBox.Text);
            else if (Regex.IsMatch(UrlTextBox.Text, @"\.[A-Za-z]{1,4}|(://)|^[A-Za-z]:\\|\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}(:\d{1,4})?|^about:"))
                Browser.Navigate(Id, UrlTextBox.Text);
            else
                Browser.Navigate(Id, "https://cn.bing.com/search?q=" + UrlTextBox.Text);
            IsSuccess = false;
        }
        private void Nav_Loading(object o, DependencyPropertyChangedEventArgs e)
        {
            loadLabel.Visibility = Visibility.Visible;
            civiLabel.Visibility = Visibility.Collapsed;
            LoadProgress.Visibility = Visibility.Visible;
            if (!Browser.Address(Id).Contains("Error.html?errorCode="))
                UrlTextBox.Text = Browser.Address(Id);
        }
        private void Nav_Loaded(object o, FrameLoadEndEventArgs e)
        {
            Dispatcher.BeginInvoke((Action) (( ) =>
            {
                IsSuccess = true;
                LoadProgress.Visibility = Visibility.Collapsed;
                loadLabel.Visibility = Visibility.Collapsed;
                CookieMgr.Set(Id);
                if (!App.Program.Args.IsPrivate)
                    Configer<Config>.Add(new Config(false, Browser.Core[Id].Title, Browser.Core[Id].Address, StdApi.LocalTime), FilePath.History);
                if (Civilized.CheckCivilized(Browser.PageText(Id)))
                    civiLabel.Visibility = Visibility.Visible;
            }));
        }
        private void Cwb_TitleChanged(object o, DependencyPropertyChangedEventArgs e)
        {
            this.Title = Browser.Title(Id) + " - 极简浏览器";
        }
        private void Cwb_LoadError(object o, LoadErrorEventArgs e)
        {
            Dispatcher.BeginInvoke((Action) (( ) =>
            {
                if (IsSuccess != true && e.ErrorCode.ToString( ) != "Aborted")
                    Browser.Navigate(Id, FilePath.Runtime + @"\resource\Error.html?errorCode=" + e.ErrorCode + "&errorText=" + e.ErrorText + "&url=" + UrlTextBox.Text);
            }));
        }
        private void Cwb_MouseWheel(object o, MouseWheelEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) != ModifierKeys.Control)
                return;
            if (e.Delta > 0)
                Browser.Core[Id].ZoomInCommand.Execute(null);
            else if (e.Delta < 0)
                Browser.Core[Id].ZoomOutCommand.Execute(null);
            zoomLabel.Content = ((int)(Browser.Core[Id].ZoomLevel * 100) + 100).ToString() + "%";
            if (zoomLabel.Content.ToString( ) != "100%")
                zoomLabel.Visibility = Visibility.Visible;
            else
                zoomLabel.Visibility = Visibility.Collapsed;
            e.Handled = true;
        }
        private void ShortcutProcess(object o, KeyEventArgs e)
        {
            if (e.Key == Key.F12)
                Browser.Core[Id].ShowDevTools( );
            if (Keyboard.Modifiers != ModifierKeys.Control)
                return;
            switch (e.Key)
            {
                case Key.H:
                    new History( ).Show( );
                    break;
                case Key.I:
                    new Setting( ).Show( );
                    break;
                case Key.R:
                    Browser.Refresh(Id);
                    break;
                case Key.N:
                    Browser.New( );
                    break;
            }
        }
        private void StatusMouseEnter(object o, MouseEventArgs e)
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
    }
}