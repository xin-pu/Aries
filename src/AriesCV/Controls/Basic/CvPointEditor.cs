using System.Windows;
using System.Windows.Controls.Primitives;
using Point = OpenCvSharp.Point;

namespace AriesCV.Controls
{
  
    public class CvPointEditor : ButtonBase
    {
        static CvPointEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CvPointEditor),
                new FrameworkPropertyMetadata(typeof(CvPointEditor)));
        }

        public static readonly DependencyProperty PointProperty = DependencyProperty.Register(
            "Point", typeof(Point), typeof(CvPointEditor),
            new FrameworkPropertyMetadata(default(Point), OnPointChanged));

        private static void OnPointChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is Point point)
            {
                ((CvPointEditor) d).X = point.X;
                ((CvPointEditor) d).Y = point.Y;
                return;
            }

            ((CvPointEditor) d).X = 0;
            ((CvPointEditor) d).Y = 0;
        }

        public Point Point
        {
            get => (Point)GetValue(PointProperty);
            set => SetValue(PointProperty, value);
        }



        public static readonly DependencyProperty XProperty = DependencyProperty.Register(
            "X", typeof(int), typeof(CvPointEditor),
            new FrameworkPropertyMetadata(default(int), OnXChanged));



        private static void OnXChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cvPointEditor = (CvPointEditor) d;
            if (e.NewValue is int x)
            {
                cvPointEditor.Point = new Point(x, cvPointEditor.Y);
            }
        }

        public int X
        {
            get => (int) GetValue(XProperty);
            set => SetValue(XProperty, value);
        }

        public static readonly DependencyProperty YProperty = DependencyProperty.Register(
            "Y", typeof(int), typeof(CvPointEditor),
            new FrameworkPropertyMetadata(default(int), OnYChanged));

        private static void OnYChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cvPointEditor = (CvPointEditor) d;
            if (e.NewValue is int y)
            {
                cvPointEditor.Point = new Point(cvPointEditor.X, y);
            }
        }


        public int Y
        {
            get => (int) GetValue(YProperty);
            set => SetValue(YProperty, value);
        }


        
    }



}
