using System.ComponentModel;
using System.Linq;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Draw")]
    public class DrawRotateRect : MatProcess
    {
        [Category("DATAIN")] public Mat[] Contours { set; get; }

        [Category("ARGUMENT")] public Scalar Scalar { set; get; }

        [Category("ARGUMENT")] public int Thickness { set; get; } = 1;
        [Category("ARGUMENT")] public int CenterRadius { set; get; } = 1;
        [Category("ARGUMENT")] public LineTypes LineType { set; get; } = LineTypes.Link8;


        public override bool CanCall()
        {
            return MatIn != null && Contours != null && Contours.Length > 0;
        }

        public override void Call()
        {
            var findSize = 2;
            MatOut = MatIn.Clone();
            foreach (var contour in Contours)
            {
                var rotatedRect = Cv2.MinAreaRect(contour);
                var points = rotatedRect.Points();
                var center = rotatedRect.Center;
                Cv2.Circle(MatOut, (Point) center, CenterRadius, Scalar, Thickness, LineType);
                Cv2.PutText(MatOut,
                    $" Width:{rotatedRect.Size.Width}",
                    (Point) center, HersheyFonts.HersheySimplex, findSize, Scalar,
                    Thickness, LineType);
                Cv2.PutText(MatOut,
                    $" Height:{rotatedRect.Size.Height}",
                    (Point) center + new Point(0, (findSize - 1) * 50), HersheyFonts.HersheySimplex, 2, Scalar,
                    Thickness, LineType);
                Enumerable.Range(0, 4).ToList().ForEach(i =>
                {
                    Cv2.Line(MatOut, (Point) points[i], (Point) points[(i + 1) % 4], Scalar, Thickness, LineType);
                });
            }
        }
    }
}