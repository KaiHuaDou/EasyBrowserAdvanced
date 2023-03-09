using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using 极简浏览器.Resources;

namespace 极简浏览器
{
    public partial class About : Window
    {
        public About( )
        {
            InitializeComponent( );
        }

        private void CloseClick(object o, RoutedEventArgs e) => Close( );

        private void Circle(object o, MouseButtonEventArgs e)
        {
            RotateTransform rotate = new RotateTransform( );
            img.RenderTransform = rotate;
            img.RenderTransformOrigin = new Point(0.5, 0.5);
            Storyboard story = new Storyboard( );
            DoubleAnimation ani = new DoubleAnimation(0, 360, new Duration(TimeSpan.FromSeconds(0.5)));
            Storyboard.SetTarget(ani, img);
            Storyboard.SetTargetProperty(ani, new PropertyPath("RenderTransform.Angle"));
            ani.RepeatBehavior = new RepeatBehavior(10);
            story.Children.Add(ani);
            story.Begin( );
        }

        private void SetVersion(object o, RoutedEventArgs e)
        {
            verLabel.Content = Settings.Default.Attach + " "
                + Settings.Default.Version + " "
                + Settings.Default.Type;
        }
    }
}
