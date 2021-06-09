using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Contour")]
    public class ApproxPolyDP : MatProcessingBlock
    {

        [Category("Enter")] public double Epsilon { set; get; }
        [Category("Enter")] public bool Closed { set; get; } = true;


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
