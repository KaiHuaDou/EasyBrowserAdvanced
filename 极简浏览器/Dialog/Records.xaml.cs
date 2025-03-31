using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using 极简浏览器.Api;

namespace 极简浏览器;

public partial class Records : Window
{
    public Records( )
    {
        InitializeComponent( );
        try
        {
            CookieManager.Get( );
            HistoryView.ItemsSource = App.History.Content;
            BookmarkView.ItemsSource = App.Bookmark.Content;
            CookiesView.ItemsSource = CookieManager.Cookies;
        }
        catch (InvalidOperationException) { }
    }

    #region History
    private void HistoryInit( )
    {
        HistoryView.ItemsSource = null;
        HistoryView.ItemsSource = App.History.Content;
    }

    private void HistoryAll(object o, RoutedEventArgs e)
    {
        App.History.Content.ForEach(item => item.Check = !item.Check);
        HistoryInit( );
    }

    private void HistoryDelete(object o, RoutedEventArgs e)
    {
        App.History.Content.RemoveAll(item => item.Check);
        HistoryInit( );
    }

    private void HistoryClear(object o, RoutedEventArgs e)
    {
        App.History.Content.Clear( );
        HistoryInit( );
    }

    private void HistoryNew(object o, RoutedEventArgs e)
    {
        foreach (Record item in App.History.Content.Where(item => item.Check))
            Instance.New(item.Url);
    }
    #endregion

    #region Bookmark
    private void BookmarkInit( )
    {
        BookmarkView.ItemsSource = null;
        BookmarkView.ItemsSource = App.Bookmark.Content;
    }

    private void BookmarkAll(object o, RoutedEventArgs e)
    {
        App.Bookmark.Content.ForEach(item => item.Check = !item.Check);
        BookmarkInit( );
    }

    private void BookmarkDelete(object o, RoutedEventArgs e)
    {
        App.Bookmark.Content.RemoveAll(item => item.Check);
        BookmarkInit( );
    }

    private void BookmarkClear(object o, RoutedEventArgs e)
    {
        App.Bookmark.Content.Clear( );
        BookmarkInit( );
    }

    private void BookmarkNew(object o, RoutedEventArgs e)
    {
        foreach (Record item in App.Bookmark.Content.Where(item => item.Check))
            Instance.New(item.Url);
    }
    #endregion

    private void WindowClosing(object o, CancelEventArgs e)
    {
        App.History.Save( );
        App.Bookmark.Save( );
    }
}
