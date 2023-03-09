using System.Windows;

namespace 极简浏览器
{
    /// <summary>
    /// 报错的模块
    /// </summary>
    public partial class Report : Window
    {
        public Report(string msg)
        {
            InitializeComponent( );
            textBox.Text = msg;
        }

        private void ShutdownClick(object sender, RoutedEventArgs e) => Application.Current.Shutdown( );
    }
}
