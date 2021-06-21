using GalaSoft.MvvmLight;
using OpenCvSharp;

namespace AriesCV.ViewModel
{
    public class TestModel : ViewModelBase
    {
        public int A { set; get; } = 2;


        public Point Point { set; get; } = new Point(101, 102);

        public Scalar Scalar { set; get; } = new Scalar(3, 127, 245, 255);
    }
}
