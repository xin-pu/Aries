using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Hough")]
    public class HoughLine : MatExport<LineSegmentPolar[]>
    {

        /// <summary>
        /// Distance resolution of the accumulator in pixels
        /// 生成极坐标时候的像素扫描步长，一般取值为 1
        /// </summary>
        [Category("ARGUMENT")]
        public double Rho { set; get; } = 1;

        /// <summary>
        /// Angle resolution of the accumulator in radians
        /// 生成极坐标时候的角度步长，一般取值CV_PI/180，即表示一度
        /// </summary>
        [Category("ARGUMENT")]
        public double Theta { set; get; } = Cv2.PI / 180F;

        /// <summary>
        /// The accumulator threshold parameter. Only those lines are returned that get enough votes ( &gt; threshold )
        /// 阈值，只有获得足够交点的极坐标点才被看成是直线
        /// </summary>
        [Category("ARGUMENT")]
        public int Threshold { set; get; } = 30;

        /// <summary>
        /// For the multi-scale Hough transform it is the divisor for the distance resolution rho.
        /// [By default this is 0]
        /// </summary>
        [Category("ARGUMENT")]
        public double Srn { set; get; } = 0;

        /// <summary>
        /// For the multi-scale Hough transform it is the divisor for the distance resolution theta.
        /// [By default this is 0]
        /// </summary>
        [Category("ARGUMENT")]
        public double Stn { set; get; } = 0;




        public override bool CanCall()
        {
            return MatIn != null;
        }

        /// <summary>
        /// The output lines. Each line is represented by a 4-element vector (x1, y1, x2, y2)
        /// </summary>
        public override void Call()
        {
            Result = new LineSegmentPolar[0];
            Result = Cv2.HoughLines(MatIn, Rho, Theta, Threshold, Srn, Stn);
        }
    }
}
