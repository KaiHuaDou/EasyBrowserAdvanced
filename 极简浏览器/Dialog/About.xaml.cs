using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using 极简浏览器.Resources;

namespace 极简浏览器
{
    /// <summary>
    /// About.xaml 的交互逻辑
    /// </summary>
    public partial class About : Window
    {
        public About( )
        {
            InitializeComponent( );
        }

        private void Button_Click(object sender, RoutedEventArgs e) => Close( );

        private void Circle(object sender, MouseButtonEventArgs e)
        {
            RotateTransform rotate = new RotateTransform( );
            image.RenderTransform = rotate;
            image.RenderTransformOrigin = new Point(0.5, 0.5);
            Storyboard story = new Storyboard( );
            DoubleAnimation da = new DoubleAnimation(0, 360, new Duration(TimeSpan.FromSeconds(0.5)));
            Storyboard.SetTarget(da, image);
            Storyboard.SetTargetProperty(da, new PropertyPath("RenderTransform.Angle"));
            da.RepeatBehavior = new RepeatBehavior(10);
            story.Children.Add(da);
            story.Begin( );
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            label3.Content = Settings.Default.Attach + " "
                + Settings.Default.Version + " "
                + Settings.Default.Type;
        }
    }
}
