using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Contour")]
    public class DrawContours : ProcessingBlock<Mat, Mat>
    {
        [Category("INPUT")] public Mat[] Contours { set; get; }

        [Category("INPUT")] public Scalar Scalar { set; get; }

        [Category("Enter")] public int ContourIndex { set; get; } = -1;

        [Category("Enter")] public int Thickness { set; get; } = 1;

        [Category("Enter")] public LineTypes LineType { set; get; } = LineTypes.Link8;

        public override void Reload()
        {
            InPutMat = null;
            OutPutMat = null;
            Status = BlockStatus.ToRun;
        }

        public override bool CanExecute()
        {
            return InPutMat != null && Contours != null && Contours.Length > 0;
        }

        public override void Execute()
        {
            OutPutMat = new Mat();
            Cv2.CopyTo(InPutMat, OutPutMat);
            Cv2.DrawContours(OutPutMat, Contours, ContourIndex, Scalar, Thickness, LineType);
        }
    }
}
