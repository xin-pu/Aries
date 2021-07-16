using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Contour")]
    public class DrawContours : MatProcess
    {
        [Category("DATAIN")] public Mat[] Contours { set; get; }

        [Category("ARGUMENT")] public Scalar Scalar { set; get; }

        [Category("ARGUMENT")] public int ContourIndex { set; get; } = -1;

        [Category("ARGUMENT")] public int Thickness { set; get; } = 1;

        [Category("ARGUMENT")] public LineTypes LineType { set; get; } = LineTypes.Link8;


        public override bool CanCall()
        {
            return MatIn != null && Contours != null && Contours.Length > 0;
        }

        public override void Call()
        {
            MatOut = new Mat();
            Cv2.CopyTo(MatIn, MatOut);
            Cv2.DrawContours(MatOut, Contours, ContourIndex, Scalar, Thickness, LineType);
        }
    }
}