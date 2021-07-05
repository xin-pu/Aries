using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using GraphX.Common;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Draw")]
    public class Circle : MatProcess
    {

        [Category("DATAIN")] public CircleSegment[] Circles { set; get; }

        [Category("ARGUMENT")] public Scalar Color { set; get; } = 255;

        [Category("ARGUMENT")] public int Thickness { set; get; } = 1;

        [Category("ARGUMENT")] public LineTypes LineType { set; get; } = LineTypes.Link8;

        [Category("ARGUMENT")] public int Shift { set; get; } = 0;


        public override bool CanCall()
        {
            return MatIn != null &&
                   Circles != null &&
                   Circles.Length > 0;
        }

        public override void Call()
        {
            MatOut = new Mat();
            MatIn.CopyTo(MatOut);
            Circles.ForEach(circle =>
            {
                Cv2.Circle(MatOut, (int) circle.Center.X, (int) circle.Center.Y, (int) circle.Radius,
                    Color, Thickness, LineType, Shift);
            });

        }
    }
}
