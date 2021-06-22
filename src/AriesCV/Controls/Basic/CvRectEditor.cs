using System.Windows;
using System.Windows.Controls.Primitives;

namespace AriesCV.Controls
{

    public class CvRectEditor : ButtonBase
    {
        static CvRectEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CvRectEditor),
                new FrameworkPropertyMetadata(typeof(CvRectEditor)));
        }


        public static readonly DependencyProperty RectProperty = DependencyProperty.Register(
            "Rect", typeof(Rect), typeof(CvRectEditor),
            new FrameworkPropertyMetadata(default(Rect), OnRectChanged));

        private static void OnRectChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is Rect rect)
            {
                ((CvRectEditor) d).Top = rect.Top;
                ((CvRectEditor) d).Left = rect.Left;
                ((CvRectEditor) d).RectWidth = rect.Width;
                ((CvRectEditor) d).RectHeight = rect.Height;
                return;
            }

            ((CvRectEditor)d).Top = 0;
            ((CvRectEditor)d).Left = 0;
            ((CvRectEditor)d).RectWidth = 0;
            ((CvRectEditor)d).RectHeight = 0;
        }

        public Rect Rect
        {
            get => (Rect) GetValue(RectProperty);
            set => SetValue(RectProperty, value);
        }




        #region Top

        public static readonly DependencyProperty TopProperty = DependencyProperty.Register(
            "Top", typeof(double), typeof(CvRectEditor),
            new FrameworkPropertyMetadata(default(double), OnTopChanged));

        private static void OnTopChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cvRectEditor = (CvRectEditor) d;
            if (e.NewValue is double top)
            {
                cvRectEditor.Rect = new Rect(cvRectEditor.Left, top, cvRectEditor.Width, cvRectEditor.RectHeight);
            }
        }

        public double Top
        {
            get => (double) GetValue(TopProperty);
            set => SetValue(TopProperty, value);
        }

        #endregion


        #region Left

        public static readonly DependencyProperty LeftProperty = DependencyProperty.Register(
            "Left", typeof(double), typeof(CvRectEditor),
            new FrameworkPropertyMetadata(default(double), OnLeftChanged));

        private static void OnLeftChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cvRectEditor = (CvRectEditor)d;
            if (e.NewValue is double left)
            {
                cvRectEditor.Rect = new Rect(left, cvRectEditor.Top, cvRectEditor.Width, cvRectEditor.RectHeight);
            }

        }

        public double Left
        {
            get => (double) GetValue(LeftProperty);
            set => SetValue(LeftProperty, value);
        }

        #endregion


        #region Width

        public static readonly DependencyProperty RectWidthProperty = DependencyProperty.Register(
            "RectWidth", typeof(double), typeof(CvRectEditor),
            new FrameworkPropertyMetadata(default(double), OnRectWidthChanged));

        private static void OnRectWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cvRectEditor = (CvRectEditor) d;
            if (e.NewValue is double rectWidth)
            {
                cvRectEditor.Rect = new Rect(cvRectEditor.Left, cvRectEditor.Top, rectWidth, cvRectEditor.RectHeight);
            }

        }

        public double RectWidth
        {
            get => (double) GetValue(RectWidthProperty);
            set => SetValue(RectWidthProperty, value);
        }

        #endregion


        #region Height

        public static readonly DependencyProperty RectHeightProperty = DependencyProperty.Register(
            "RectHeight", typeof(double), typeof(CvRectEditor),
            new FrameworkPropertyMetadata(default(double), OnRectHeightChanged));

        private static void OnRectHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cvRectEditor = (CvRectEditor)d;
            if (e.NewValue is double rectHeight)
            {
                cvRectEditor.Rect = new Rect(cvRectEditor.Left, cvRectEditor.Top, cvRectEditor.RectWidth, rectHeight);
            }
        }

        public double RectHeight
        {
            get => (double) GetValue(RectHeightProperty);
            set => SetValue(RectHeightProperty, value);
        }

        #endregion



    }
}
