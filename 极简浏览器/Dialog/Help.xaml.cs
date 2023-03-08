using System.Windows;

namespace 极简浏览器
{
    /// <summary>
    /// Help.xaml 的交互逻辑
    /// </summary>
    public partial class Help : Window
    {
        public Help( )
        {
            InitializeComponent( );
            this.SizeToContent = SizeToContent.WidthAndHeight;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close( );
        }
    }
}
