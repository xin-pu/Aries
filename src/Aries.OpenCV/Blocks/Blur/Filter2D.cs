using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Blur")]
    public class Filter2D : ProcessingBlock
    {
        [Category("IN_MAT")] public InputArray Element { set; get; }

        [Category("Enter")] public MatType MatType { set; get; } = MatType.CV_32F;

        [Category("Enter")] public int AnchorPoint_X { set; get; } = -1;


        [Category("Enter")] public int AnchorPoint_Y { set; get; } = -1;

        [Category("Enter")] public double Delta { set; get; } = 0;


        [Category("Enter")] public BorderTypes BorderTypes { set; get; } = BorderTypes.Default;

        /// <summary>
        /// The anchor point. The default value Point(-1,-1) means that the anchor is at the kernel center
        /// </summary>
        private Point AnchorPoint => new Point(AnchorPoint_X, AnchorPoint_Y);

        public override bool CanExecute()
        {
            return InPutMat != null && Element != null;
        }

        public override void Execute()
        {
            OutPutMat = new Mat();
            Cv2.Filter2D(InPutMat, OutPutMat, MatType, Element, AnchorPoint, Delta, BorderTypes);
        }
    }
}
