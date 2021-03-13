using System.IO;
using System.Windows;
using Microsoft.Win32;
using 极简浏览器.Api;

namespace 极简浏览器
{
    /// <summary>
    /// Extensions.xaml 的交互逻辑
    /// </summary>
    public partial class Extensions : Window
    {
        public Extensions( )
        {
            InitializeComponent( );
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog( );
            bool? result = ofd.ShowDialog( );
            if(result == true)
            {
                string js = File.ReadAllText(ofd.FileName);
                BrowserCore.RunJavaSript(js);
            }
        }
    }
}
