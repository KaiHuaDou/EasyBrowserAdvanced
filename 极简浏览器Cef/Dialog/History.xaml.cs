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
        public History()
        {
            InitializeComponent();
            HistoryData = ConfigHelper.GetConfig(FilePath.HistoryPath);
            HistoryDataGrid.ItemsSource = HistoryData;
        }

        static void ShowFileError()
        {
            StandardApi.ShowNotifyIcon(Properties.Resources.File_Error);
        }

        private void button_Click1(object sender, RoutedEventArgs e)
        {
            foreach (CheckBox cb in listBox1.Items)
            {
                if (cb.IsChecked == true)
                {
                    NewInstance.StartNewInstance((string)cb.Content);
                }
            }
        }

        private void WinLoaded(object sender, RoutedEventArgs e)
        {
            listBox1.ItemsSource = FileApi.ReadAll(FileType.BookMark);
        }

        private void button1_Click1(object sender, RoutedEventArgs e)
        {
            if (FileApi.Clear(FileType.BookMark) != true)
            {
                ShowFileError();
            }
            else
            {
                History his = new History( );
                his.Show();
                this.Close();
            }
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
        private void BookMark_SelectAll_Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (CheckBox cb in listBox1.Items)
            {
                cb.IsChecked = true;
            }
        }
        private void BookMarkDelete(object sender, RoutedEventArgs e)
        {
            List<CheckBox> lc = new List<CheckBox>( );
            FileApi.Clear(FileType.BookMark);
            foreach (CheckBox cb in listBox1.Items)
            {
                if (cb.IsChecked != true)
                {
                    FileApi.Write((string)cb.Content, FileType.BookMark);
                    lc.Add(cb);
                }
            }
            listBox1.ItemsSource = lc;
        }
        #endregion
    }
}