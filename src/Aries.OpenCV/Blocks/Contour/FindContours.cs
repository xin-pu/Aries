using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Contour")]
    public class FindContours : ProcessingBlock
    {

        [Category("Enter")] public RetrievalModes RetrievalMode { set; get; } = RetrievalModes.List;

        [Category("Enter")]
        public ContourApproximationModes ContourApproximationMode { set; get; } = ContourApproximationModes.ApproxNone;

        public Point Point { set; get; }

        public override bool CanExecute()
        {
            return InPutMat != null;
        }

        public override void Execute()
        {
            OutPutMat=new Mat();
            var COn = new Mat[0];
            Cv2.FindContours(InPutMat, out COn, OutPutMat, RetrievalMode, ContourApproximationMode, Point);
        }
    }
}
