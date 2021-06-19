using System.Windows;
using System.Windows.Controls;
using Point = OpenCvSharp.Point;

namespace AriesCV.Controls
{
    public class PointEditor : Control
    {

        public Point Point
        {
            get { return (Point)GetValue(PointProperty); }
            set
            {
                SetValue(PointProperty, value);
                X = value.X;
                Y = value.Y;
            }
        }

        public static readonly DependencyProperty PointProperty =
            DependencyProperty.Register("Point", typeof(Point), typeof(PointEditor), new PropertyMetadata(null));

        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(double), typeof(PointEditor), new PropertyMetadata(null));

        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(double), typeof(PointEditor), new PropertyMetadata(null));
    }
}
