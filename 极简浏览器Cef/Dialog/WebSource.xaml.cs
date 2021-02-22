using System.Windows;
using 极简浏览器.Api;
using CefSharp;
using System.Threading;
using System;
using System.Windows.Threading;

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
            textBox.Text = await BrowserCore.GetBrowser( ).GetMainFrame( ).GetSourceAsync( );
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string result;
            //Thread thread = new Thread(delegate ( )
            //{
                result = HtmlFormatter.Format(textBox.Text);
            //});
            //thread.Start( );
            textBox.Text = result;
        }
    }
}
