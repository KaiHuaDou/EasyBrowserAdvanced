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
        private readonly int Id;
        private bool IsPageOk = false;

        public MainWindow( ) => Close( );

        public MainWindow(int id)
        {
            InitializeComponent( );
            Id = id;
        }

        private void BrowserLoaded(object o, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                Cef.UIThreadTaskFactory.StartNew(( ) =>
                {
                    var requestContext = Browser.Core[Id].GetBrowser( ).GetHost( ).RequestContext;
                    requestContext.SetPreference("profile.default_content_setting_values.plugins", 1, out string error);
                });
            }
            catch (NullReferenceException) { }
        }

        private void WindowLoaded(object o, RoutedEventArgs e)
        {
            Browser.Core[Id] = new EasyBrowserCore(Id)
            {
                Margin = new Thickness(0),
                MenuHandler = new MenuHandler(Id),
                DownloadHandler = new DownloadHandler( )
            };
            Browser.Core[Id].AddressChanged += PageLoading;
            Browser.Core[Id].FrameLoadEnd += PageLoaded;
            Browser.Core[Id].TitleChanged += BrowserTitleChanged;
            Browser.Core[Id].IsBrowserInitializedChanged += BrowserLoaded;
            Browser.Core[Id].LoadError += BrowserLoadError;
            MenuHandler.MainWindow = this;
            CWBGrid.Children.Add(Browser.Core[Id]);
            Dispatcher.BeginInvoke((Action) (( ) => Browser.Navigate(Id, FileApi.StartupPath)));
        }
        private void PageCheckUrl(object o, KeyEventArgs e)
        {
            if ((e.Key == Key.Enter || e.Key == Key.Return) && e.Key != Key.ImeProcessed)
                PagePreload(o, new RoutedEventArgs( ));
            if (Civilized.CheckCivilized(UrlTextBox.Text))
                Civilized.DeniedMsg( );
        }
        private void PagePreload(object o, RoutedEventArgs e)
        {
            if (UrlTextBox.Text.ToLower( ).Contains("easy://"))
                Browser.PraseEasy(Id, UrlTextBox.Text);
            else if (Regex.IsMatch(UrlTextBox.Text, @"\.[A-Za-z]{1,4}|(://)|^[A-Za-z]:\\|\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}(:\d{1,4})?|^about:"))
                Browser.Navigate(Id, UrlTextBox.Text);
            else Browser.Navigate(Id, "https://cn.bing.com/search?q=" + UrlTextBox.Text);
            IsPageOk = false;
        }
        private void PageLoading(object o, DependencyPropertyChangedEventArgs e)
        {
            loadLabel.Visibility = Visibility.Visible;
            civiLabel.Visibility = Visibility.Collapsed;
            LoadProgress.Visibility = Visibility.Visible;
            if (!Browser.Address(Id).Contains("Error.html?errorCode="))
                UrlTextBox.Text = Browser.Address(Id);
        }
        private void PageLoaded(object o, FrameLoadEndEventArgs e)
        {
            Dispatcher.BeginInvoke((Action) (( ) =>
            {
                IsPageOk = true;
                LoadProgress.Visibility = Visibility.Collapsed;
                loadLabel.Visibility = Visibility.Collapsed;
                CookieMgr.Set(Id);
                if (!App.Program.Args.IsPrivate)
                {
                    DataMgr<Config>.Add(new Config
                    {
                        Check = false,
                        Title = Browser.Core[Id].Title,
                        Url = Browser.Core[Id].Address,
                        Time = StdApi.LocalTime
                    }, FilePath.History);
                }
                if (Civilized.CheckCivilized(Browser.PageText(Id)))
                    civiLabel.Visibility = Visibility.Visible;
            }));
        }
        private void BrowserTitleChanged(object o, DependencyPropertyChangedEventArgs e)
            => this.Title = Browser.Title(Id) + " - 极简浏览器";
        private void BrowserLoadError(object o, LoadErrorEventArgs e)
        {
            Dispatcher.BeginInvoke((Action) (( ) =>
            {
                if (IsPageOk != true && e.ErrorCode.ToString( ) != "Aborted")
                    Browser.Navigate(Id, FilePath.Runtime + @"\resource\Error.html?errorCode=" + e.ErrorCode + "&errorText=" + e.ErrorText + "&url=" + UrlTextBox.Text);
            }));
        }
        private void BrowserZooming(object o, MouseWheelEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) != ModifierKeys.Control) return;
            if (e.Delta > 0) Browser.Core[Id].ZoomInCommand.Execute(null);
            else if (e.Delta < 0) Browser.Core[Id].ZoomOutCommand.Execute(null);
            zoomLabel.Content = ((int) (Browser.Core[Id].ZoomLevel * 100) + 100).ToString( ) + "%";
            if (zoomLabel.Content.ToString( ) != "100%")
                zoomLabel.Visibility = Visibility.Visible;
            else zoomLabel.Visibility = Visibility.Collapsed;
            e.Handled = true;
        }
        private void ShortcutProcess(object o, KeyEventArgs e)
        {
            if (e.Key == Key.F12) Browser.Core[Id].ShowDevTools( );
            if (Keyboard.Modifiers != ModifierKeys.Control) return;
            switch (e.Key)
            {
                case Key.H: new History( ).Show( ); break;
                case Key.I: new Setting( ).Show( ); break;
                case Key.R: Browser.Refresh(Id); break;
                case Key.N: Browser.New( ); break;
                case Key.S: Browser.ViewSource(Id); break;
            }
        }
    }
}
