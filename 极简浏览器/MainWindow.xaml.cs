using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using CefSharp;
using 极简浏览器.Api;

namespace 极简浏览器;

/// <summary>
/// 主窗口的交互代码
/// 负责：ChromiumWebBrowser 相关、初始化
/// </summary>
public partial class MainWindow : Window, IDisposable
{
    private readonly int Id;
    public bool IsError { get; set; }
    public Argument Args { get; set; }
    private EasyBrowserCore Browser => Instance.Core[Id];

    public MainWindow( ) : this(0, new Argument( ))
    {
        Instance.Host[0] = this;
    }

    public MainWindow(int id, Argument args)
    {
        InitializeComponent( );
        Id = id;
        Args = args;
    }

    private void WindowLoaded(object o, RoutedEventArgs e)
    {
        Instance.Core[Id] = new EasyBrowserCore(Id)
        {
            Margin = new Thickness(0),
            MenuHandler = new MenuHandler(Id),
        };
        Browser.AddressChanged += PageLoading;
        Browser.FrameLoadEnd += PageLoaded;
        Browser.TitleChanged += BrowserTitleChanged;
        Browser.IsBrowserInitializedChanged += BrowserLoaded;
        Browser.LoadError += BrowserLoadError;
        MenuHandler.MainWindow = this;
        BrowserGrid.Children.Add(Browser);
        Dispatcher.BeginInvoke(( ) => Instance.Navigate(Id, App.Program.StartupUri));
    }

    private void BrowserLoaded(object o, DependencyPropertyChangedEventArgs e)
    {
        try
        {
            Cef.UIThreadTaskFactory.StartNew(( ) =>
            {
                Browser.GetBrowser( ).GetHost( ).RequestContext
                       .SetPreference("profile.default_content_setting_values.plugins", 1, out string error);
            });
        }
        catch (NullReferenceException) { }
        catch (ObjectDisposedException) { }
    }

    private void PageCheckUrl(object o, KeyEventArgs e)
    {
        if (e.Key is (Key.Enter or Key.Return) and not Key.ImeProcessed)
            PagePreload(o, new RoutedEventArgs( ));
    }

    private void PagePreload(object o, RoutedEventArgs e)
    {
        IsError = false;
        if (AddressBox.Text.ToUpperInvariant( ).Contains("EASY://"))
            Instance.PraseEasy(Id, AddressBox.Text);
        else if (Utils.AddressRegex.IsMatch(AddressBox.Text))
            Instance.Navigate(Id, AddressBox.Text);
        else
            Instance.Navigate(Id, App.Setting.Content[0].SearchEngine + AddressBox.Text);
    }

    private void PageLoading(object o, DependencyPropertyChangedEventArgs e)
    {
        loadLabel.Visibility = Visibility.Visible;
        LoadProgress.Visibility = Visibility.Visible;
        if (!IsError) AddressBox.Text = Browser.Address;
    }

    private void PageLoaded(object o, FrameLoadEndEventArgs e)
    {
        Dispatcher.BeginInvoke(( ) =>
        {
            LoadProgress.Visibility = Visibility.Collapsed;
            loadLabel.Visibility = Visibility.Collapsed;
            if (Args.IsPrivate || IsError) return;
            App.History.Content.Add(
                new Record
                {
                    Check = false,
                    Title = Browser.Title,
                    Url = Browser.Address,
                    Time = Utils.LocalTime
                }
            );
        });
    }

    private void BrowserTitleChanged(object o, DependencyPropertyChangedEventArgs e)
    {
        if (!IsError)
            Title = Browser.Title + " - 极简浏览器";
    }

    private void BrowserLoadError(object o, LoadErrorEventArgs e)
    {
        if (e.ErrorCode.ToString( ) is "Aborted" or "ConnectionClosed") return;
        Dispatcher.BeginInvoke(( ) =>
        {
            if (Browser.IsLoading) return;
            IsError = true;
            Instance.Navigate(Id, $@"{FilePath.ErrorPage}?code={e.ErrorCode}&text={e.ErrorText}&url={AddressBox.Text}");
        });
    }

    private async void BrowserZoom(object o, MouseWheelEventArgs e)
    {
        if (Keyboard.Modifiers != ModifierKeys.Control) return;
        switch (e.Delta)
        {
            case > 0: Browser.ZoomInCommand.Execute(null); break;
            case < 0: Browser.ZoomOutCommand.Execute(null); break;
        }
        double zoomLevel = Utils.ConvertZoomLevel(await Browser.GetBrowserHost( ).GetZoomLevelAsync( ));
        zoomLabel.Content = $"{zoomLevel}%";
        zoomLabel.Visibility = zoomLevel == 100 ? Visibility.Collapsed : Visibility.Visible;
        e.Handled = true;
    }

    private void GoShortcut(object o, KeyEventArgs e)
    {
        switch (Keyboard.Modifiers)
        {
            case ModifierKeys.Control:
                switch (e.Key)
                {
                    case Key.F: ShowSearchBox( ); break;
                    case Key.H: new History( ).Show( ); break;
                    case Key.I: new Setting( ).Show( ); break;
                    case Key.R: Instance.Refresh(Id); break;
                    case Key.N: Instance.New( ); break;
                    case Key.S: Instance.ViewSource(Id); break;
                    case Key.F5: Instance.Refresh(Id, true); break;
                }
                break;
            case ModifierKeys.Alt:
                switch (e.Key)
                {
                    case Key.Home: Instance.Navigate(Id, App.Setting.Content[0].MainPage); break;
                }
                break;
            default:
                switch (e.Key)
                {
                    case Key.F12: Browser.ShowDevTools( ); break;
                    case Key.F5: Instance.Refresh(Id); break;
                }
                break;
        }
    }

    private void WindowClosing(object o, CancelEventArgs e)
    {
        App.History.Save( );
        App.Bookmark.Save( );
        App.Setting.Save( );
        Browser.CloseDevTools( );
        Browser.GetBrowser( ).CloseBrowser(true);
    }

    public void Dispose( )
    {
        try { Browser.Dispose( ); }
        catch (ObjectDisposedException) { }
        GC.SuppressFinalize(this);
    }
}
