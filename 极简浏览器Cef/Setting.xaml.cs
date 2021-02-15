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
        }

        private void Go(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                try
                {
                    wb.Navigate(textBox.Text);
                }
                catch (Exception)
                {
                    wb.Navigate("http://" + textBox.Text);
                }

            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                File.WriteAllText(FilePath.ConfigPath, wb.Source.ToString( ));
                this.Close( );
            }
            catch (Exception){ }
        }

        private void Loading(object sender, RoutedEventArgs e)
        {
            try
            {
                wb.Navigate(textBox.Text);
            }
            catch (Exception)
            {
                wb.Navigate("http://" + textBox.Text);
            }

        }
    }
}
