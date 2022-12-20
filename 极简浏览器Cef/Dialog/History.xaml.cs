using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        HashSet<Config> BookMarkData;
        public History()
        {
            InitializeComponent();
            HistoryData = Configer<Config>.Get(FilePath.History);
            HistoryDataGrid.ItemsSource = HistoryData;
            BookMarkData = Configer<Config>.Get(FilePath.BookMark);
            BookMarkDataGrid.ItemsSource = BookMarkData;
        }

        #region History
        private void InitHistory()
        {
            HistoryDataGrid.ItemsSource = null;
            HistoryDataGrid.ItemsSource = HistoryData;
        }
        private void History_SelectAll_Button_Click(object sender, RoutedEventArgs e)
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
        private void HistoryNewWindow(object sender, RoutedEventArgs e)
        {
            foreach(Config item in HistoryData)
            {
                if(item.IsChecked == true)
                {
                    Instance.New(item.SiteAddress);
                }
            }
        }
        #endregion
        #region BookMark
        private void InitBookMark()
        {
            BookMarkDataGrid.ItemsSource = null;
            BookMarkDataGrid.ItemsSource = BookMarkData;
        }
        private void BookMark_SelectAll_Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (Config item in BookMarkData)
            {
                item.IsChecked = !item.IsChecked;
            }
            InitBookMark();
        }

        private void BookMarkDelete(object sender, RoutedEventArgs e)
        {
            HashSet<Config> temp = new HashSet<Config>();
            foreach (Config item in BookMarkData)
            {
                if (item.IsChecked == true)
                {
                    temp.Add(item);
                }
            }
            foreach (Config item in temp)
            {
                BookMarkData.Remove(item);
            }
            InitBookMark();
        }
        private void BookMarkClear(object sender, RoutedEventArgs e)
        {
            BookMarkData.Clear();
            InitBookMark();
        }
        private void BookMarkNewWindow(object sender, RoutedEventArgs e)
        {
            foreach (Config item in BookMarkData)
            {
                if (item.IsChecked == true)
                {
                    Instance.New(item.SiteAddress);
                }
            }
        }
        #endregion

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Configer<Config>.Save(HistoryData, FilePath.History);
            Configer<Config>.Save(BookMarkData, FilePath.BookMark);
        }
    }
}