using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Contour")]
    public class HoughCircle : ExportBlock<CircleSegment[]>
    {
        [Category("Enter")]
        public HoughModes HoughMode { set; get; } = HoughModes.Gradient;

        [Category("Enter")]
        public double DP { set; get; }

        [Category("Enter")]
        public double MinDIst { set; get; }

        [Category("Enter")]
        public double Param1 { set; get; } = 100;

        [Category("Enter")]
        public double Param2 { set; get; } = 100;

        [Category("Enter")]
        public int MinRadius { set; get; } = 0;

        [Category("Enter")]
        public int MaxRadius { set; get; } = 0;

        public override void Reload()
        {
            ExportResult = null;
            base.Reload();
        }

        public override bool CanExecute()
        {
            return InPutMat != null;
        }

        public override void Execute()
        {
            ExportResult = new CircleSegment[0];
            ExportResult = Cv2.HoughCircles(InPutMat, HoughMode, DP, MinDIst,
                Param1, Param2, MinRadius, MaxRadius);
        }
    }
}
