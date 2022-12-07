using System;
using System.IO;
using System.Windows;
using CefSharp;
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

        private async void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            textBox.Text = await Browser.Core.GetMainFrame( ).GetSourceAsync( );
        }

        private void formatButton_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                string result = Formatter.FormartHtml(textBox.Text, true);
                Dispatcher.BeginInvoke((Action)(() =>
                {
                    textBox.Text = result;
                }));
            }));
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                DefaultExt = ".html",
                FileName = Browser.Core.Title,
                AddExtension = true,
                Filter = "HTML 文件|.html"
            };
            sfd.ShowDialog();
            File.WriteAllText(sfd.FileName, textBox.Text);
        }
    }
}
