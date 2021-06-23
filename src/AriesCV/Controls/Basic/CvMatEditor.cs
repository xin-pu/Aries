using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using GalaSoft.MvvmLight.Command;
using OpenCvSharp;

namespace AriesCV.Controls
{

    public class CvMatEditor : Label
    {
        static CvMatEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CvMatEditor),
                new FrameworkPropertyMetadata(typeof(CvMatEditor)));
        }

        public static readonly DependencyProperty MatProperty = DependencyProperty.Register(
            "Mat", typeof(Mat), typeof(CvMatEditor),
            new FrameworkPropertyMetadata(default(Mat), OnMatChanged));

        private static void OnMatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        public Mat Mat
        {
            get => (Mat) GetValue(MatProperty);
            set => SetValue(MatProperty, value);
        }


    }



}
