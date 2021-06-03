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
        /// </summary>
        [Category("Enter")] public double Rho { set; get; }

        /// <summary>
        /// >Angle resolution of the accumulator in radians
        /// </summary>
        [Category("Enter")] public double Theta { set; get; }

        /// <summary>
        /// The accumulator threshold parameter. Only those lines are returned that get enough votes ( &gt; threshold )
        /// </summary>
        [Category("Enter")] public int Threshold { set; get; }

        /// <summary>
        /// The minimum line length. Line segments shorter than that will be rejected. [By default this is 0]
        /// </summary>
        [Category("Enter")] public double MinLineLength { set; get; } = 0;

        /// <summary>
        /// The maximum allowed gap between points on the same line to link them. [By default this is 0]
        /// </summary>
        [Category("Enter")] public double MaxLineGap { set; get; } = 0;


        public override void Reload()
        {
            ExportResult=new LineSegmentPoint[0];
            base.Reload();
        }

        public override bool CanExecute()
        {
            return InPutMat != null;
        }

        /// <summary>
        /// The output lines. Each line is represented by a 4-element vector (x1, y1, x2, y2)
        /// </summary>
        public override void Execute()
        {
            ExportResult = new LineSegmentPoint[0];
            ExportResult = Cv2.HoughLinesP(InPutMat, Rho, Theta, Threshold, MinLineLength, MaxLineGap);
        }
    }
}
