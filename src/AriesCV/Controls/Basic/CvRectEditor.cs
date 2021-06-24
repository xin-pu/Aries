
using System.Windows;
using System.Windows.Controls.Primitives;
using Rect = OpenCvSharp.Rect;

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

            ((CvRectEditor) d).Top = 0;
            ((CvRectEditor) d).Left = 0;
            ((CvRectEditor) d).RectWidth = 0;
            ((CvRectEditor) d).RectHeight = 0;
        }

        public Rect Rect
        {
            get => (Rect) GetValue(RectProperty);
            set => SetValue(RectProperty, value);
        }




        #region Top

        public static readonly DependencyProperty TopProperty = DependencyProperty.Register(
            "Top", typeof(int), typeof(CvRectEditor),
            new FrameworkPropertyMetadata(default(int), OnTopChanged));

        private static void OnTopChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cvRectEditor = (CvRectEditor) d;
            if (e.NewValue is int top)
            {
                cvRectEditor.Rect = new Rect(cvRectEditor.Left, top, cvRectEditor.RectWidth, cvRectEditor.RectHeight);
            }
        }

        public int Top
        {
            get => (int) GetValue(TopProperty);
            set => SetValue(TopProperty, value);
        }

        #endregion


        #region Left

        public static readonly DependencyProperty LeftProperty = DependencyProperty.Register(
            "Left", typeof(int), typeof(CvRectEditor),
            new FrameworkPropertyMetadata(default(int), OnLeftChanged));

        private static void OnLeftChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cvRectEditor = (CvRectEditor)d;
            if (e.NewValue is int left)
            {
                cvRectEditor.Rect = new Rect(left, cvRectEditor.Top, cvRectEditor.RectWidth, cvRectEditor.RectHeight);
            }

        }

        public int Left
        {
            get => (int) GetValue(LeftProperty);
            set => SetValue(LeftProperty, value);
        }

        #endregion


        #region Width

        public static readonly DependencyProperty RectWidthProperty = DependencyProperty.Register(
            "RectWidth", typeof(int), typeof(CvRectEditor),
            new FrameworkPropertyMetadata(default(int), OnRectWidthChanged));

        private static void OnRectWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cvRectEditor = (CvRectEditor) d;
            if (e.NewValue is int rectWidth)
            {
                cvRectEditor.Rect = new Rect(cvRectEditor.Left, cvRectEditor.Top, rectWidth, cvRectEditor.RectHeight);
            }

        }

        public int RectWidth
        {
            get => (int) GetValue(RectWidthProperty);
            set => SetValue(RectWidthProperty, value);
        }

        #endregion


        #region Height

        public static readonly DependencyProperty RectHeightProperty = DependencyProperty.Register(
            "RectHeight", typeof(int), typeof(CvRectEditor),
            new FrameworkPropertyMetadata(default(int), OnRectHeightChanged));

        private static void OnRectHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cvRectEditor = (CvRectEditor)d;
            if (e.NewValue is int rectHeight)
            {
                cvRectEditor.Rect = new Rect(cvRectEditor.Left, cvRectEditor.Top, cvRectEditor.RectWidth, rectHeight);
            }
        }

        public int RectHeight
        {
            get => (int) GetValue(RectHeightProperty);
            set => SetValue(RectHeightProperty, value);
        }

        #endregion



    }
}
