using System.IO;
using System.Threading;
using System.Windows;
using Microsoft.Win32;
using 极简浏览器.Api;

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

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            textBox.Text = StdApi.PageSource;
        }

        private void formatButton_Click(object sender, RoutedEventArgs e)
        {
            new Thread((html) =>
            {
                string result = Formatter.FormartHtml((string)html, true);
                Dispatcher.Invoke(() => { textBox.Text = result; });
            }).Start(textBox.Text);
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                DefaultExt = ".html",
                FileName = Browser.Title,
                AddExtension = true,
                Filter = "HTML 文件|.html"
            };
            sfd.ShowDialog();
            File.WriteAllText(sfd.FileName, textBox.Text);
        }
    }
}
