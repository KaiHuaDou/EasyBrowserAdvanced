using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using CefSharp;
using 极简浏览器.Api;

namespace 极简浏览器;

/// <summary>
/// 主窗口的交互代码
/// 负责：ChromiumWebBrowser 的相关事宜、初始化
/// </summary>
public partial class MainWindow : Window
{
    private readonly int Id;
    private bool IsPageOk;
    public Argument Args { get; set; }

    public MainWindow( ) => Close( );

    public MainWindow(int id, Argument args)
    {
        InitializeComponent( );
        Id = id;
        Args = args;
    }

    private void BrowserLoaded(object o, DependencyPropertyChangedEventArgs e)
    {
        Cef.UIThreadTaskFactory.StartNew(( ) =>
        {
            try
            {
                IRequestContext requestContext = Instance.Core[Id].GetBrowser( ).GetHost( ).RequestContext;
                requestContext.SetPreference("profile.default_content_setting_values.plugins", 1, out string error);
            }
            catch (NullReferenceException) { }
            catch (ObjectDisposedException) { }
        });
    }

    private void WindowLoaded(object o, RoutedEventArgs e)
    {
        Instance.Core[Id] = new EasyBrowserCore(Id)
        {
            Margin = new Thickness(0),
            MenuHandler = new MenuHandler(Id),
            DownloadHandler = new DownloadHandler( )
        };
        Instance.Core[Id].AddressChanged += PageLoading;
        Instance.Core[Id].FrameLoadEnd += PageLoaded;
        Instance.Core[Id].TitleChanged += BrowserTitleChanged;
        Instance.Core[Id].IsBrowserInitializedChanged += BrowserLoaded;
        Instance.Core[Id].LoadError += BrowserLoadError;
        MenuHandler.MainWindow = this;
        BrowserGrid.Children.Add(Instance.Core[Id]);
        Dispatcher.BeginInvoke(( ) => Instance.Navigate(Id, App.Program.StartupUri));
    }
    private void PageCheckUrl(object o, KeyEventArgs e)
    {
        if (e.Key is (Key.Enter or Key.Return) and not Key.ImeProcessed)
            PagePreload(o, new RoutedEventArgs( ));
    }
    private void PagePreload(object o, RoutedEventArgs e)
    {
        if (UrlTextBox.Text.ToUpperInvariant( ).Contains("EASY://"))
            Instance.PraseEasy(Id, UrlTextBox.Text);
        else if (Regex.IsMatch(UrlTextBox.Text, @"\.[A-Za-z]{1,4}|(://)|^[A-Za-z]:\\|\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}(:\d{1,4})?|^about:"))
            Instance.Navigate(Id, UrlTextBox.Text);
        else
            Instance.Navigate(Id, App.Setting.Content[0].SearchEngine + UrlTextBox.Text);
        IsPageOk = false;
    }
    private void PageLoading(object o, DependencyPropertyChangedEventArgs e)
    {
        loadLabel.Visibility = Visibility.Visible;
        LoadProgress.Visibility = Visibility.Visible;
        if (!Instance.Core[Id].Address.Contains("Error.html?errorCode="))
            UrlTextBox.Text = Instance.Core[Id].Address;
    }
    private void PageLoaded(object o, FrameLoadEndEventArgs e)
    {
        void Loaded( )
        {
            IsPageOk = true;
            LoadProgress.Visibility = Visibility.Collapsed;
            loadLabel.Visibility = Visibility.Collapsed;
            CookieMgr.Set(Id);
            if (!Args.IsPrivate)
            {
                App.History.Content.Add(
                    new Record
                    {
                        Check = false,
                        Title = Instance.Core[Id].Title,
                        Url = Instance.Core[Id].Address,
                        Time = Utils.LocalTime
                    }
                );
            }
        }
        Dispatcher.BeginInvoke(Loaded);
    }
    private void BrowserTitleChanged(object o, DependencyPropertyChangedEventArgs e)
        => Title = Instance.Core[Id].Title + " - 极简浏览器";

    private void BrowserLoadError(object o, LoadErrorEventArgs e)
    {
        void LoadError( )
        {
            if (IsPageOk || e.ErrorCode.ToString( ) == "Aborted") return;
            Instance.Navigate(Id,
                $@"{FilePath.Runtime}\Resources\Error.html?errorCode={e.ErrorCode}&errorText={e.ErrorText}&url={UrlTextBox.Text}");
        }
        Dispatcher.BeginInvoke(LoadError);
    }
    private void BrowserZooming(object o, MouseWheelEventArgs e)
    {
        if ((Keyboard.Modifiers & ModifierKeys.Control) != ModifierKeys.Control) return;
        if (e.Delta > 0)
            Instance.Core[Id].ZoomInCommand.Execute(null);
        else if (e.Delta < 0)
            Instance.Core[Id].ZoomOutCommand.Execute(null);
        zoomLabel.Content = Instance.Core[Id].GetBrowserHost( ).GetZoomLevel( ).ToString( ) + "%";
        zoomLabel.Visibility = zoomLabel.Content.ToString( ) == "100%" ? Visibility.Collapsed : Visibility.Visible;
        e.Handled = true;
    }
    private void ShortcutProcess(object o, KeyEventArgs e)
    {
        if (e.Key == Key.F12) Instance.Core[Id].ShowDevTools( );
        if (Keyboard.Modifiers != ModifierKeys.Control) return;
        switch (e.Key)
        {
            case Key.H: new History( ).Show( ); break;
            case Key.I: new Setting( ).Show( ); break;
            case Key.R: Instance.Refresh(Id); break;
            case Key.N: Instance.New( ); break;
            case Key.S: Instance.ViewSource(Id); break;
        }
    }

    private void WindowClosing(object o, CancelEventArgs e)
    {
        App.History.Save( );
        App.Bookmark.Save( );
        App.Cookies.Save( );
        App.Setting.Save( );
        Instance.Core[Id].CloseDevTools( );
        Instance.Core[Id].GetBrowser( ).CloseBrowser(true);
        Instance.Core[Id].Dispose( );
    }
}
