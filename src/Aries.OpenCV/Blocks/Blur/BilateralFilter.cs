using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    /// <summary>
    /// 双边滤波
    /// </summary>
    [Category("Blur")]
    public class BilateralFilter : ProcessingBlock
    {


        [Category("Enter")] public int D { set; get; } = 3;

        [Category("Enter")] public double SigmaColor { set; get; } = 3;

        [Category("Enter")] public double SigmaSpace { set; get; } = 3;

        [Category("Enter")] public BorderTypes BorderType { set; get; } = BorderTypes.Default;

        public override bool CanExecute()
        {
            return InPutMat != null;
        }

        public override void Execute()
        {
            OutPutMat = new Mat();
            Cv2.BilateralFilter(InPutMat, OutPutMat, D, SigmaColor, SigmaSpace, BorderType);
        }
    }
}
