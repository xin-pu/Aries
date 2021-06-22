using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Contour")]
    public class ApproxPolyDP : MatProcess
    {

        [Category("ARGUMENT")] public double Epsilon { set; get; }
        [Category("ARGUMENT")] public bool Closed { set; get; } = true;


        public override bool CanExecute()
        {
            return MatIn != null;
        }

        public override void Execute()
        {
            MatOut = new Mat();
            Cv2.ApproxPolyDP(MatIn, MatOut, Epsilon, Closed);
        }
    }
}
