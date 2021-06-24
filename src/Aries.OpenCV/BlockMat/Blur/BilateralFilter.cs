using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    /// <summary>
    /// 双边滤波
    /// </summary>
    [Category("Blur")]
    public class BilateralFilter : MatProcess
    {


        [Category("ARGUMENT")] public int D { set; get; } = 3;

        [Category("ARGUMENT")] public double SigmaColor { set; get; } = 3;

        [Category("ARGUMENT")] public double SigmaSpace { set; get; } = 3;

        [Category("ARGUMENT")] public BorderTypes BorderType { set; get; } = BorderTypes.Default;

        public override bool CanExecute()
        {
            return MatIn != null;
        }

        public override void Execute()
        {
            MatOut = new Mat();
            Cv2.BilateralFilter(MatIn, MatOut, D, SigmaColor, SigmaSpace, BorderType);
        }
    }
}
