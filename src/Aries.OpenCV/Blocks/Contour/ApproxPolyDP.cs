using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Contour")]
    public class ApproxPolyDP : MatProcessingBlock
    {

        [Category("ARGUMENT")] public double Epsilon { set; get; }
        [Category("ARGUMENT")] public bool Closed { set; get; } = true;


        public override bool CanExecute()
        {
            return InPutMat != null;
        }

        public override void Execute()
        {
            OutPutMat = new Mat();
            Cv2.ApproxPolyDP(InPutMat, OutPutMat, Epsilon, Closed);
        }
    }
}
