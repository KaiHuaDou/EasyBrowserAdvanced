using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using 极简浏览器.Api;

namespace 极简浏览器;

public partial class History : Window
{
    private ObservableCollection<Config> HistoryData;
    private ObservableCollection<Config> BookmarkData;
    private ObservableCollection<CookieData> CookiesData;
    public History( )
    {
        InitializeComponent( );
        HistoryData = DataMgr<Config>.Get(FilePath.History);
        BookmarkData = DataMgr<Config>.Get(FilePath.BookMark);
        CookiesData = DataMgr<CookieData>.Get(FilePath.Cookies);
        HistoryView.ItemsSource = HistoryData;
        BookmarkView.ItemsSource = BookmarkData;
        CookiesView.ItemsSource = CookiesData;
    }

    #region History
    private void HistoryInit( )
    {
        HistoryView.ItemsSource = null;
        HistoryView.ItemsSource = HistoryData;
    }

    private void HistoryAll(object o, RoutedEventArgs e)
    {
        foreach (Config item in HistoryData)
            item.Check = !item.Check;
        HistoryInit( );
    }

    private void HistoryDelete(object o, RoutedEventArgs e)
    {
        HashSet<Config> temp = new( );
        foreach (Config item in HistoryData.Where(item => item.Check))
            temp.Add(item);
        foreach (Config item in temp)
            HistoryData.Remove(item);
        HistoryInit( );
    }

    private void HistoryClear(object o, RoutedEventArgs e)
    {
        HistoryData.Clear( );
        HistoryInit( );
    }

    private void HistoryNew(object o, RoutedEventArgs e)
    {
        foreach (Config item in HistoryData.Where(item => item.Check))
            Browser.New(item.Url);
    }
    #endregion

    #region BookMark
    private void BookmarkInit( )
    {
        BookmarkView.ItemsSource = null;
        BookmarkView.ItemsSource = BookmarkData;
    }

    private void BookmarkAll(object o, RoutedEventArgs e)
    {
        foreach (Config item in BookmarkData)
            item.Check = !item.Check;
        BookmarkInit( );
    }

    private void BookmarkDelete(object o, RoutedEventArgs e)
    {
        HashSet<Config> temp = new( );
        foreach (Config item in BookmarkData.Where(item => item.Check))
            temp.Add(item);
        foreach (Config item in temp)
            BookmarkData.Remove(item);
        BookmarkInit( );
    }

    private void BookmarkClear(object o, RoutedEventArgs e)
    {
        BookmarkData.Clear( );
        BookmarkInit( );
    }

    private void BookmarkNew(object o, RoutedEventArgs e)
    {
        foreach (Config item in BookmarkData.Where(item => item.Check))
            Browser.New(item.Url);
    }
    #endregion

    #region Cookies
    private void CookiesInit( )
    {
        CookiesView.ItemsSource = null;
        CookiesView.ItemsSource = CookiesData;
    }

    private void CookiesAll(object o, RoutedEventArgs e)
    {
        foreach (CookieData item in CookiesData)
            item.Check = !item.Check;
        CookiesInit( );
    }

    private void CookiesDelete(object o, RoutedEventArgs e)
    {
        HashSet<CookieData> temp = new( );
        foreach (CookieData item in CookiesData.Where(item => item.Check))
            temp.Add(item);
        foreach (CookieData item in temp)
            CookiesData.Remove(item);
        CookiesInit( );
    }

    private void CookiesClear(object o, RoutedEventArgs e)
    {
        CookiesData.Clear( );
        CookiesInit( );
    }
    #endregion

    private void WindowClosing(object o, CancelEventArgs e)
    {
        DataMgr<Config>.Save(HistoryData, FilePath.History);
        DataMgr<Config>.Save(BookmarkData, FilePath.BookMark);
        DataMgr<CookieData>.Save(CookiesData, FilePath.Cookies);
    }
}
