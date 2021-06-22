using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Contour")]
    public class HoughLineP : ExportBlock<LineSegmentPoint[]>
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
        /// The minimum line length. Line segments shorter than that will be rejected. [By default this is 0]
        /// 最小直线长度，有默认值0，表示最低线段的长度，比这个设定参数短的线段就不能被显现出来。
        /// </summary>
        [Category("ARGUMENT")] public double MinLineLength { set; get; } = 0;

        /// <summary>
        /// The maximum allowed gap between points on the same line to link them. [By default this is 0]
        /// 最大间隔，有默认值0，允许将同一行点与点之间连接起来的最大的距离。
        /// </summary>
        [Category("ARGUMENT")] public double MaxLineGap { set; get; } = 0;


        public override void Reload()
        {
            Result = null;
            base.Reload();
        }

        public override bool CanExecute()
        {
            return MatIn != null;
        }

        /// <summary>
        /// The output lines. Each line is represented by a 4-element vector (x1, y1, x2, y2)
        /// </summary>
        public override void Execute()
        {
            Result = new LineSegmentPoint[0];
            Result = Cv2.HoughLinesP(MatIn, Rho, Theta, Threshold, MinLineLength, MaxLineGap);
        }
    }
}
