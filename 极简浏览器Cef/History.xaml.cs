using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        string AppStartupPath = Path.GetDirectoryName(Process.GetCurrentProcess( ).MainModule.FileName);
        public History( )
        {
            InitializeComponent( );
        }
        
        void ShowFileError()
        {
            System.Windows.Forms.NotifyIcon _NI = new System.Windows.Forms.NotifyIcon( );
            _NI.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            _NI.BalloonTipText = Properties.Resources.File_Error;
            _NI.BalloonTipTitle = Properties.Resources.BrowserName;
            _NI.Text = Properties.Resources.File_Error;
            _NI.Visible = true;
            _NI.Icon = new System.Drawing.Icon("favicon.ico");
            _NI.ShowBalloonTip(2000);
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            foreach (CheckBox cb in listBox.Items)
            {
                if (cb.IsChecked == true)
                {
                    NewInstance.StartNewInstance((string) cb.Content);
                }
            }
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
            listBox.ItemsSource = FileApi.ReadAll(FileType.History);
            listBox1.ItemsSource = FileApi.ReadAll(FileType.BookMark);
        }

        private void HistoryClear(object sender, RoutedEventArgs e)
        {
            if(FileApi.Clear(FileType.History) != true)
            {
                ShowFileError();
            }
            else
            {
                History his = new History( );
                his.Show( );
                this.Close( );
            }
        }
        private void button1_Click1(object sender, RoutedEventArgs e)
        {
            if(FileApi.Clear(FileType.BookMark) != true)
            {
                ShowFileError();
            }
            else
            {
                History his = new History( );
                his.Show( );
                this.Close( );
            }
        }

        private void HistoryDelete(object sender, RoutedEventArgs e)
        {
            List<CheckBox> lc = new List<CheckBox>( );
            FileApi.Clear(FileType.History);
            foreach (CheckBox cb in listBox.Items)
            {
                if (cb.IsChecked != true)
                {
                    FileApi.Write((string) cb.Content, FileType.History);
                    lc.Add(cb);
                }
            }
            listBox.ItemsSource = lc;
        }

        private void BookMarkDelete(object sender, RoutedEventArgs e)
        {
            List<CheckBox> lc = new List<CheckBox>( );
            FileApi.Clear(FileType.BookMark);
            foreach (CheckBox cb in listBox1.Items)
            {
                if (cb.IsChecked != true)
                {
                    FileApi.Write((string) cb.Content, FileType.BookMark);
                    lc.Add(cb);
                }
            }
            listBox1.ItemsSource = lc;
        }

        private void History_SelectAll_Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (CheckBox cb in listBox.Items)
            {
                cb.IsChecked = !cb.IsChecked;
            }
        }

        private void BookMark_SelectAll_Button_Click(object sender, RoutedEventArgs e)
        {
            foreach(CheckBox cb in listBox1.Items)
            {
                cb.IsChecked = !cb.IsChecked;
            }
        }
    }
}
