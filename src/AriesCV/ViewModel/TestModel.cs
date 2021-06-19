using System.Windows.Media;
using GalaSoft.MvvmLight;
using OpenCvSharp;

namespace AriesCV.ViewModel
{
    public class TestModel : ViewModelBase
    {
        public int A { set; get; } = 2;
        public Brush Color { set; get; } = Brushes.Aqua;
        public Point Point { set; get; } = new Point(3, 4);
    }
}
