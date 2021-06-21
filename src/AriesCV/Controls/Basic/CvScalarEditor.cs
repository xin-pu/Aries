using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using OpenCvSharp;

namespace AriesCV.Controls
{

    public class CvScalarEditor : ButtonBase
    {
        static CvScalarEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CvScalarEditor),
                new FrameworkPropertyMetadata(typeof(CvScalarEditor)));

        }

        public static readonly DependencyProperty ScalarProperty = DependencyProperty.Register(
            "Rect", typeof(Scalar), typeof(CvScalarEditor),
            new FrameworkPropertyMetadata(new Scalar(0, 0, 0, 255), OnScalarChanged));


        public Scalar Scalar
        {
            get => (Scalar) GetValue(ScalarProperty);
            set => SetValue(ScalarProperty, value);
        }

        private static void OnScalarChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is Scalar scalar)
            {
                var cvScalarEditor = (CvScalarEditor) d;
                cvScalarEditor.Val0 = scalar.Val0;
                cvScalarEditor.Val1 = scalar.Val1;
                cvScalarEditor.Val2 = scalar.Val2;
                cvScalarEditor.Val3 = scalar.Val3;
                cvScalarEditor.Brush = new SolidColorBrush
                (Color.FromArgb(
                    (byte) scalar.Val3,
                    (byte) scalar.Val2,
                    (byte) scalar.Val1,
                    (byte) scalar.Val0));
            }
        }

        public static readonly DependencyProperty BrushProperty = DependencyProperty.Register(
            "Brush", typeof(Brush), typeof(CvScalarEditor),
            new FrameworkPropertyMetadata(default(Brush), OnScalarChanged));


        public Brush Brush
        {
            get => (Brush) GetValue(BrushProperty);
            set => SetValue(BrushProperty, value);
        }


        public static readonly DependencyProperty Val0Property = DependencyProperty.Register(
            "Val0", typeof(double), typeof(CvScalarEditor),
            new FrameworkPropertyMetadata(default(double), OnVal0Changed));


        public double Val0
        {
            get => (double) GetValue(Val0Property);
            set => SetValue(Val0Property, value);
        }




        public static readonly DependencyProperty Val1Property = DependencyProperty.Register(
            "Val1", typeof(double), typeof(CvScalarEditor),
            new FrameworkPropertyMetadata(default(double), OnVal1Changed));


        public double Val1
        {
            get => (double) GetValue(Val1Property);
            set => SetValue(Val1Property, value);
        }



        public static readonly DependencyProperty Val2Property = DependencyProperty.Register(
            "Val2", typeof(double), typeof(CvScalarEditor),
            new FrameworkPropertyMetadata(default(double), OnVal2Changed));


        public double Val2
        {
            get => (double) GetValue(Val2Property);
            set => SetValue(Val2Property, value);
        }

        public static readonly DependencyProperty Val3Property = DependencyProperty.Register(
            "Val3", typeof(double), typeof(CvScalarEditor),
            new FrameworkPropertyMetadata(default(double), OnVal3Changed));



        public double Val3
        {
            get => (double) GetValue(Val3Property);
            set => SetValue(Val3Property, value);
        }


        private static void OnVal0Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cvRectEditor = (CvScalarEditor) d;
            if (e.NewValue is double val0)
            {
                cvRectEditor.Scalar = new Scalar(val0, cvRectEditor.Val1, cvRectEditor.Val2, cvRectEditor.Val3);
                Update(cvRectEditor);
            }
        }

        private static void OnVal1Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cvRectEditor = (CvScalarEditor) d;
            if (e.NewValue is double val1)
            {
                cvRectEditor.Scalar = new Scalar(cvRectEditor.Val0, val1, cvRectEditor.Val2, cvRectEditor.Val3);
                Update(cvRectEditor);
            }
        }

        private static void OnVal2Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cvRectEditor = (CvScalarEditor) d;
            if (e.NewValue is double val2)
            {
                cvRectEditor.Scalar = new Scalar(cvRectEditor.Val0, cvRectEditor.Val1, val2, cvRectEditor.Val3);
                Update(cvRectEditor);
            }
        }

        private static void OnVal3Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cvRectEditor = (CvScalarEditor) d;
            if (e.NewValue is double val3)
            {
                cvRectEditor.Scalar = new Scalar(cvRectEditor.Val0, cvRectEditor.Val1, cvRectEditor.Val2, val3);
                Update(cvRectEditor);
            }
        }

        private static void Update(CvScalarEditor cvScalar)
        {
            var scalar = cvScalar.Scalar;
            cvScalar.Brush = new SolidColorBrush
            (Color.FromArgb(
                (byte) scalar.Val3,
                (byte) scalar.Val2,
                (byte) scalar.Val1,
                (byte) scalar.Val0));
        }


    }



}
