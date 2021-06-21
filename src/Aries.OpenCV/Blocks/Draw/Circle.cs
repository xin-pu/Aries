using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using GraphX.Common;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Draw")]
    public class Circle : MatProcessingBlock
    {

        [Category("DATAIN")] public CircleSegment[] Circles { set; get; }

        [Category("ARGUMENT")] public double Color { set; get; } = 255;

        [Category("ARGUMENT")] public int Thickness { set; get; } = 1;

        [Category("ARGUMENT")] public LineTypes LineType { set; get; } = LineTypes.Link8;

        [Category("ARGUMENT")] public int Shift { set; get; } = 0;

        public override void Reload()
        {
            Circles = null;
            base.Reload();
        }

        public override bool CanExecute()
        {
            return InPutMat != null && 
                   Circles != null &&
                   Circles.Length > 0;
        }

        public override void Execute()
        {
            OutPutMat=new Mat();
            InPutMat.CopyTo(OutPutMat);
            Circles.ForEach(circle =>
            {
                Cv2.Circle(OutPutMat, (int) circle.Center.X, (int) circle.Center.Y, (int) circle.Radius,
                    new Scalar(Color), Thickness, LineType, Shift);
            });

        }
    }
}
