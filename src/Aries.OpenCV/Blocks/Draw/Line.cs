using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using GraphX.Common;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Draw")]
    public class Line : ProcessingBlock
    {

        [Category("IN_MAT")] public LineSegmentPoint[] Lines { set; get; }

        [Category("Enter")] public double Color { set; get; } = 255;

        [Category("Enter")] public int Thickness { set; get; } = 1;

        [Category("Enter")] public LineTypes LineType { set; get; } = LineTypes.Link8;

        [Category("Enter")] public int Shift { set; get; } = 0;


        public override bool CanExecute()
        {
            return InPutMat != null && Lines != null && Lines.Length > 0;
        }

        public override void Execute()
        {
            OutPutMat = new Mat();
            InPutMat.CopyTo(OutPutMat);
            Lines.ForEach(pointpair =>
            {
                Cv2.Line(OutPutMat, pointpair.P1, pointpair.P2, new Scalar(Color), Thickness, LineType, Shift);
            });

        }
    }
}
