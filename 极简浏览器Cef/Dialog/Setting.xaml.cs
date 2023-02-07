using System;
using System.IO;
using System.Windows;
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
            MainPageBox.Text = File.ReadAllText(FilePath.Config);
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                File.WriteAllText(FilePath.Config, MainPageBox.Text);
                this.Close( );
            }
            catch (Exception ex)
            {
                Logger.Log(ex, logType: LogType.Debug, shutWhenFail: false);
            }
        }

        private void CacheButton_Click(object sender, RoutedEventArgs e)
        {
            string[] files = Directory.GetFiles(FilePath.Caches);
            foreach (string x in files)
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
            string[] files = Directory.GetFiles(FilePath.Logs);
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
