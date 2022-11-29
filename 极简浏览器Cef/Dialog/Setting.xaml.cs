using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using 极简浏览器.Api;

namespace 极简浏览器
{
    /// <summary>
    /// Setting.xaml 的交互逻辑
    /// </summary>
    public partial class Setting : Window
    {
        public Setting( )
        {
            InitializeComponent( );
            MainPageBox.Text = File.ReadAllText(FilePath.ConfigPath);
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                File.WriteAllText(FilePath.ConfigPath, MainPageBox.Text);
                this.Close( );
            }
            catch (Exception ex)
            {
                Logger.Log(ex, logType: LogType.Debug, shutWhenFail: false);
            }
        }

        private void CacheButton_Click(object sender, RoutedEventArgs e)
        {
            string[] files = Directory.GetFiles(FilePath.CacheDirectory);
            foreach(string x in files)
            {
                try
                {
                    File.Delete(x);
                }
                catch (Exception) { }
            }
        }

        private void LogButton_Click(object sender, RoutedEventArgs e)
        {
            string[] files = Directory.GetFiles(FilePath.LogDirectory);
            foreach (string x in files)
            {
                try
                {
                    File.Delete(x);
                }
                catch (Exception) { }
            }
        }
    }
}
