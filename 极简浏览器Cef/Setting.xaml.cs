using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

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

        private void Go(object sender, System.Windows.Input.KeyEventArgs e)
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
                string AppStartupPath = Path.GetDirectoryName(Process.GetCurrentProcess( ).MainModule.FileName);
                File.WriteAllText(AppStartupPath + "\\DataBase\\Config.db", wb.Source.ToString( ));
                this.Close( );
            }
            catch (Exception)
            {
                NotifyIcon _NI = new NotifyIcon( );
                _NI.BalloonTipIcon = ToolTipIcon.Info;
                _NI.BalloonTipText = "请输入正确的地址！";
                _NI.BalloonTipTitle = "极简浏览器";
                _NI.Text = "请输入正确的地址！";
                _NI.Visible = true;
                _NI.Icon = new System.Drawing.Icon("favicon.ico");
                _NI.ShowBalloonTip(2000);
            }
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
