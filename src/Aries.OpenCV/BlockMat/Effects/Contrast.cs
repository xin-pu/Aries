using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat.Effects
{
    /// <summary>
    ///     对比度增强
    /// </summary>
    public class Contrast : MatProcess
    {
        [Category("ARGUMENT")] public double Alpha { set; get; } = 1.2;

        [Category("ARGUMENT")] public double Beta { set; get; } = 30;

        public override bool CanCall()
        {
            return MatIn != null;
        }

        public override void Call()
        {
            MatOut = new Mat();


            MatOut = MatIn.Multiply(Alpha).Add(Beta);
        }
    }
}