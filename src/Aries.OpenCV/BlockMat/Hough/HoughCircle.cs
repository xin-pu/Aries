using Aries.OpenCV.GraphModel;
using OpenCvSharp;
using System.ComponentModel;

namespace Aries.OpenCV.BlockMat
{
    [Category("Hough")]
    public class HoughCircle : MatExport<CircleSegment[]>
    {
        [Category("ARGUMENT")] public HoughModes HoughMode { set; get; } = HoughModes.Gradient;

        /// <summary>
        /// The inverse ratio of the accumulator resolution to the image resolution.
        /// 累加器分辨率与图像分辨率的反比。
        /// </summary>
        [Category("ARGUMENT")]
        public double DP { set; get; } = 1;

        /// <summary>
        /// 被检测圆的中心之间的最小距离。
        /// </summary>
        [Category("ARGUMENT")]
        public double MinDIst { set; get; } = 20;

        /// <summary>
        /// The first method-specific parameter. [By default this is 100]
        /// </summary>
        [Category("ARGUMENT")]
        public double Param1 { set; get; } = 100;

        /// <summary>
        /// The second method-specific parameter. [By default this is 100]
        /// </summary>
        [Category("ARGUMENT")]
        public double Param2 { set; get; } = 100;

        /// <summary>
        /// Minimum circle radius. [By default this is 0]
        /// </summary>
        [Category("ARGUMENT")]
        public int MinRadius { set; get; } = 0;

        /// <summary>
        /// Maximum circle radius. [By default this is 0]
        /// </summary>
        [Category("ARGUMENT")]
        public int MaxRadius { set; get; } = 0;



        public override bool CanCall()
        {
            return DP >= 1 && MatIn != null;
        }

        public override void Call()
        {
            Result = new CircleSegment[0];
            Result = Cv2.HoughCircles(MatIn, HoughMode, DP, MinDIst,
                Param1, Param2, MinRadius, MaxRadius);
        }
    }
}
