using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using 极简浏览器.Api;

namespace 极简浏览器
{
    /// <summary>
    /// History.xaml 的交互逻辑
    /// </summary>
    public partial class History : Window
    {
        ObservableCollection<ConfigData> HistoryData;
        ObservableCollection<ConfigData> BookMarkData;
        public History()
        {
            InitializeComponent();
            HistoryData = ConfigHelper.GetConfig(FilePath.HistoryPath);
            HistoryDataGrid.ItemsSource = HistoryData;
            BookMarkData = ConfigHelper.GetConfig(FilePath.BookMarkPath);
            BookMarkDataGrid.ItemsSource = BookMarkData;
        }

        static void ShowFileError()
        {
            StandardApi.ShowNotifyIcon(Properties.Resources.File_Error);
        }

        #region History
        private void InitHistory()
        {
            HistoryDataGrid.ItemsSource = null;
            HistoryDataGrid.ItemsSource = HistoryData;
        }
        private void History_SelectAll_Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (ConfigData item in HistoryData)
            {
                item.IsChecked = !item.IsChecked;
            }
            InitHistory();
        }

        private void HistoryDelete(object sender, RoutedEventArgs e)
        {
            ObservableCollection<ConfigData> temp = new ObservableCollection<ConfigData>();
            foreach (ConfigData item in HistoryData)
            {
                if (item.IsChecked == true)
                {
                    temp.Add(item);
                }
            }
            foreach (ConfigData item in temp)
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
            foreach(ConfigData item in HistoryData)
            {
                if(item.IsChecked == true)
                {
                    NewInstance.StartNewInstance(item.SiteAddress);
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
            foreach (ConfigData item in BookMarkData)
            {
                item.IsChecked = !item.IsChecked;
            }
            InitBookMark();
        }

        private void BookMarkDelete(object sender, RoutedEventArgs e)
        {
            ObservableCollection<ConfigData> temp = new ObservableCollection<ConfigData>();
            foreach (ConfigData item in BookMarkData)
            {
                if (item.IsChecked == true)
                {
                    temp.Add(item);
                }
            }
            foreach (ConfigData item in temp)
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
            foreach (ConfigData item in BookMarkData)
            {
                if (item.IsChecked == true)
                {
                    NewInstance.StartNewInstance(item.SiteAddress);
                }
            }
        }
        #endregion

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ConfigHelper.SaveConfig(HistoryData, FilePath.HistoryPath);
            ConfigHelper.SaveConfig(BookMarkData, FilePath.BookMarkPath);
        }
    }
}