using System;
using System.IO;
using System.Windows;
using 极简浏览器.Api;

namespace 极简浏览器
{
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
                Close( );
            }
            catch (Exception ex)
            {
                Logger.Log(ex, type: LogType.Debug, shutWhenFail: false);
            }
        }

        private void ClearCache(object sender, RoutedEventArgs e)
        {
            foreach (string file in Directory.GetFiles(FilePath.Caches))
            {
                try { File.Delete(file); } catch { }
            }
        }

        private void ClearLog(object sender, RoutedEventArgs e)
        {
            foreach (string file in Directory.GetFiles(FilePath.Logs))
            {
                try { File.Delete(file); } catch { }
            }
        }
    }
}
