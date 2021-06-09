using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Contour")]
    public class FindContours : ProcessingBlock<Mat, Mat[]>
    {

        [Category("OUTPUT")] public Mat Hierarchy { set; get; }

        [Category("Enter")] public RetrievalModes RetrievalMode { set; get; } = RetrievalModes.List;

        [Category("Enter")]
        public ContourApproximationModes ContourApproximationMode { set; get; } = ContourApproximationModes.ApproxNone;

        [Category("Enter")] public Point Offset { set; get; }

        public override void Reload()
        {
            InPutMat = null;
            OutPutMat = null;
            Status = BlockStatus.ToRun;
        }

        public override bool CanExecute()
        {
            return InPutMat != null;
        }

        public override void Execute()
        {
            Hierarchy = new Mat();
            Mat[] outMats;
            Cv2.FindContours(InPutMat, out outMats, Hierarchy, RetrievalMode, ContourApproximationMode, Offset);
            OutPutMat = outMats;
        }
    }
}
