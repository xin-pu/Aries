using System.Linq;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat.Integrate
{
    public abstract class MatIntegrate<T2> : MatProcess
    {
        public Scalar PenColor => Scalar.OrangeRed;

        public override bool CanCall()
        {
            return MatIn != null;
        }

        public override void Call()
        {
            var result = Process(MatIn.Clone());
            var drawMat = new Mat();
            Cv2.CvtColor(MatIn, drawMat, ColorConversionCodes.GRAY2BGR);
            MatOut = Draw(drawMat, result);
        }

        public abstract T2 Process(Mat matin);

        public abstract Mat Draw(Mat matin, T2 result);


        #region DrawFunction

        public Mat DrawPoint(Mat mat, Point point, Scalar color, int size = 20, int thickness = 3)
        {
            mat.Line(point + new Point(-size, 0), point + new Point(size, 0), color, thickness);
            mat.Line(point + new Point(0, size), point + new Point(0, -size), color, thickness);
            return mat;
        }

        public Mat DrawRotatedRect(Mat mat, RotatedRect rect, Scalar color, int size = 10, int thickness = 3)
        {
            var points = Cv2.BoxPoints(rect).ToList();
            var cons = points.Select(a => a.ToPoint());
            Cv2.DrawContours(mat, new[] {cons}, -1, color);

            points.Add(rect.Center.ToPoint());
            points.ForEach(p => Cv2.Circle(mat, p.ToPoint(), size, color, -1));
            return mat;
        }

        #endregion
    }
}