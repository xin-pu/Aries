using Aries.OpenCV.GraphModel;
using OpenCvSharp;
using System.ComponentModel;

namespace Aries.OpenCV.Blocks
{
    [Category("Morphology")]
    public class MorphologyEx : ProcessingBlock
    {
        [Category("IN_MAT")]
        public InputArray Shape { set; get; }

        [Category("Enter")]
        public MorphTypes MorphType { set; get; } = MorphTypes.Open;

        public override bool CanExecute()
        {
            return InPutMat != null && Shape != null;
        }

        public override void Execute()
        {
            OutPutMat = new Mat();
            Cv2.MorphologyEx(InPutMat, OutPutMat, MorphType, Shape, new Point?(), 1, BorderTypes.Constant, new Scalar?());
        }
    }
}