using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using GraphX.Common;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Draw")]
    public class Line : MatProcessingBlock
    {

        [Category("INPUT")] public LineSegmentPoint[] Lines { set; get; }

        [Category("ARGUMENT")] public double Color { set; get; } = 255;

        [Category("ARGUMENT")] public int Thickness { set; get; } = 1;

        [Category("ARGUMENT")] public LineTypes LineType { set; get; } = LineTypes.Link8;

        [Category("ARGUMENT")] public int Shift { set; get; } = 0;


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
