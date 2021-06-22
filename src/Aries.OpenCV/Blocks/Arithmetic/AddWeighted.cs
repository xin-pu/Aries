using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Arithmetic")]
    public class AddWeighted : MatArithmetic
    {

        [Category("ARGUMENT")] public double Alpha { set; get; } = 0.5;

        [Category("ARGUMENT")] public double Beta { set; get; } = 0.5;

        [Category("ARGUMENT")] public double Gamma { set; get; } = 0;

        /// <summary>
        /// dst = alpha cdot img1 + beta cdot img2 + gamma
        /// </summary>
        public override void Execute()
        {
            MatOut = new Mat();
            Cv2.AddWeighted(MatIn1, Alpha, MatIn2, Beta, Gamma, MatOut);
        }
    }
}
