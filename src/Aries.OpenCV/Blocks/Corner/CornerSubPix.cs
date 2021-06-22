using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    public class CornerSubPix : ProcessingBlock<Mat, Point2f[]>
    {
        [Category("DATAIN")] public Point2f[] Corners { set; get; }

        [Category("ARGUMENT")] public Size WinSize { set; get; } = new Size(5, 5);

        [Category("ARGUMENT")] public Size ZeroZone { set; get; } = new Size(-1, -1);


        [Category("ARGUMENT")] public CriteriaTypes CriteriaType { set; get; } = CriteriaTypes.Eps;

        [Category("ARGUMENT")] public int MaxCount { set; get; } = 100;
        [Category("ARGUMENT")] public double Epsilon { set; get; } = -1;


        private TermCriteria TermCriteria => new TermCriteria(CriteriaType, MaxCount, Epsilon);

        public override bool CanExecute()
        {
            return InPutMat != null;
        }

        public override void Execute()
        {
            OutPutMat = Cv2.CornerSubPix(InPutMat, Corners, WinSize, ZeroZone, TermCriteria);
        }
    }
}
