using System.Windows;
using 极简浏览器.Api;
using CefSharp;
using System.Threading;
using System;
using System.Windows.Threading;
using System.Windows.Media;

namespace 极简浏览器
{
    /// <summary>
    /// WebSource.xaml 的交互逻辑
    /// </summary>
    public partial class WebSource : Window
    {
        public WebSource(string text)
        {
            InitializeComponent( );
            textBox.Text = text;
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            textBox.Text = await BrowserCore.CefBrowser.GetMainFrame( ).GetSourceAsync( );
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            textBox.Text = Formatter.Format(textBox.Text);
        }
    }
}
