using System.Windows;

namespace 极简浏览器
{
    /// <summary>
    /// 报错的模块
    /// </summary>
    public partial class Report : Window
    {
        string message;
        public Report(string msg)
        {
            InitializeComponent( );
            message = msg;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown( );
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textBox.Text = message;
        }
    }
}
