using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    public class CornerSubPix : ProcessingBlock<Mat, Point2f[]>
    {
        [Category("INPUT")] public Point2f[] Corners { set; get; }

        [Category("ARGUMENT")] public int WinSize_Width { set; get; } = 5;


        [Category("ARGUMENT")] public int WinSize_Height { set; get; } = 5;

        [Category("ARGUMENT")] public int ZeroZone_Width { set; get; } = -1;

        [Category("ARGUMENT")] public int ZeroZone_Height { set; get; } = -1;


        [Category("ARGUMENT")] public CriteriaTypes CriteriaType { set; get; } = CriteriaTypes.Eps;

        [Category("ARGUMENT")] public int MaxCount { set; get; } = 100;
        [Category("ARGUMENT")] public double Epsilon { set; get; } = -1;



        private Size winSize => new Size(WinSize_Width, WinSize_Height);
        private Size zeroZone => new Size(ZeroZone_Width, ZeroZone_Height);
        private TermCriteria TermCriteria => new TermCriteria(CriteriaType, MaxCount, Epsilon);

        public override void Reload()
        {
            throw new System.NotImplementedException();
        }

        public override bool CanExecute()
        {
            throw new System.NotImplementedException();
        }

        public override void Execute()
        {
            OutPutMat = Cv2.CornerSubPix(InPutMat, Corners, winSize, zeroZone, TermCriteria);
        }
    }
}
