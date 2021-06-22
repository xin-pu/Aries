using System.Windows;
using System.Windows.Controls.Primitives;
using Size = OpenCvSharp.Size;

namespace AriesCV.Controls
{

    public class CvSizeEditor : ButtonBase
    {
        static CvSizeEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CvSizeEditor),
                new FrameworkPropertyMetadata(typeof(CvSizeEditor)));
        }




        public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(
            "Size", typeof(Size), typeof(CvSizeEditor),
            new FrameworkPropertyMetadata(default(Size), OnSizeChanged));

        private static void OnSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is Size size)
            {
                ((CvSizeEditor) d).SizeWidth = size.Width;
                ((CvSizeEditor) d).SizeHeight = size.Height;
                return;
            }

            ((CvSizeEditor) d).SizeWidth = 0;
            ((CvSizeEditor) d).SizeHeight = 0;
        }

        public Size Size
        {
            get => (Size) GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        public static readonly DependencyProperty SizeWidthProperty = DependencyProperty.Register(
            "Width", typeof(int), typeof(CvSizeEditor),
            new FrameworkPropertyMetadata(default(int), OnSizeWidthChanged));


        private static void OnSizeWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cvSizeEditor = (CvSizeEditor) d;
            if (e.NewValue is int sizeWidth)
            {
                cvSizeEditor.Size = new Size(sizeWidth, cvSizeEditor.Height);
            }
        }

        public int SizeWidth
        {
            get => (int) GetValue(SizeWidthProperty);
            set => SetValue(SizeWidthProperty, value);
        }




        public static readonly DependencyProperty SizeHeightProperty = DependencyProperty.Register(
            "Height", typeof(int), typeof(CvSizeEditor),
            new FrameworkPropertyMetadata(default(int), OnSizeHeightChanged));

        private static void OnSizeHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cvSizeEditor = (CvSizeEditor) d;
            if (e.NewValue is int sizeHeight)
            {
                cvSizeEditor.Size = new Size(cvSizeEditor.SizeWidth, sizeHeight);
            }
        }


        public int SizeHeight
        {
            get => (int) GetValue(SizeHeightProperty);
            set => SetValue(SizeHeightProperty, value);
        }
    }
}
