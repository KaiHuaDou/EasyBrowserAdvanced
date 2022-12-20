using System.Collections.Generic;
using System.Windows;
using 极简浏览器.Api;

namespace 极简浏览器
{
    /// <summary>
    /// History.xaml 的交互逻辑
    /// </summary>
    public partial class History : Window
    {
        HashSet<Config> HistoryData;
        HashSet<Config> BookmarkData;
        HashSet<CookieData> CookiesData;
        public History()
        {
            InitializeComponent();
            HistoryData = Configer<Config>.Get(FilePath.History);
            HistoryDataGrid.ItemsSource = HistoryData;
            BookmarkData = Configer<Config>.Get(FilePath.BookMark);
            BookmarkDataGrid.ItemsSource = BookmarkData;
            CookiesData = Configer<CookieData>.Get(FilePath.Cookies);
            CookiesDataGrid.ItemsSource = CookiesData;
        }

        #region History
        private void InitHistory()
        {
            HistoryDataGrid.ItemsSource = null;
            HistoryDataGrid.ItemsSource = HistoryData;
        }
        private void HistoryAll(object sender, RoutedEventArgs e)
        {
            foreach (Config item in HistoryData)
            {
                item.IsChecked = !item.IsChecked;
            }
            InitHistory();
        }

        private void HistoryDelete(object sender, RoutedEventArgs e)
        {
            HashSet<Config> temp = new HashSet<Config>();
            foreach (Config item in HistoryData)
            {
                if (item.IsChecked == true)
                {
                    temp.Add(item);
                }
            }
            foreach (Config item in temp)
            {
                HistoryData.Remove(item);
            }
            InitHistory();
        }
        private void HistoryClear(object sender, RoutedEventArgs e)
        {
            HistoryData.Clear();
            InitHistory();
        }
        private void HistoryNew(object sender, RoutedEventArgs e)
        {
            foreach(Config item in HistoryData)
            {
                if(item.IsChecked == true)
                {
                    Browser.New(item.SiteAddress);
                }
            }
        }
        #endregion
        #region BookMark
        private void InitBookmark()
        {
            BookmarkDataGrid.ItemsSource = null;
            BookmarkDataGrid.ItemsSource = BookmarkData;
        }
        private void BookmarkAll(object sender, RoutedEventArgs e)
        {
            foreach (Config item in BookmarkData)
            {
                item.IsChecked = !item.IsChecked;
            }
            InitBookmark();
        }

        private void BookmarkDelete(object sender, RoutedEventArgs e)
        {
            HashSet<Config> temp = new HashSet<Config>();
            foreach (Config item in BookmarkData)
            {
                if (item.IsChecked == true)
                {
                    temp.Add(item);
                }
            }
            foreach (Config item in temp)
            {
                BookmarkData.Remove(item);
            }
            InitBookmark();
        }
        private void BookmarkClear(object sender, RoutedEventArgs e)
        {
            BookmarkData.Clear();
            InitBookmark();
        }
        private void BookmarkNew(object sender, RoutedEventArgs e)
        {
            foreach (Config item in BookmarkData)
            {
                if (item.IsChecked == true)
                {
                    Browser.New(item.SiteAddress);
                }
            }
        }
        #endregion
        #region Cookies
        private void InitCookies()
        {
            CookiesDataGrid.ItemsSource = null;
            CookiesDataGrid.ItemsSource = CookiesData;
        }
        private void CookiesAll(object sender, RoutedEventArgs e)
        {
            foreach (CookieData item in CookiesData)
            {
                item.IsChecked = !item.IsChecked;
            }
            InitCookies();
        }

        private void CookiesDelete(object sender, RoutedEventArgs e)
        {
            HashSet<CookieData> temp = new HashSet<CookieData>();
            foreach (CookieData item in CookiesData)
            {
                if (item.IsChecked == true)
                {
                    temp.Add(item);
                }
            }
            foreach (CookieData item in temp)
            {
                CookiesData.Remove(item);
            }
            InitCookies();
        }
        private void CookiesClear(object sender, RoutedEventArgs e)
        {
            CookiesData.Clear();
            InitCookies();
        }
        #endregion

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Configer<Config>.Save(HistoryData, FilePath.History);
            Configer<Config>.Save(BookmarkData, FilePath.BookMark);
            Configer<CookieData>.Save(CookiesData, FilePath.Cookies);
        }
    }
}