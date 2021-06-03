using System.ComponentModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Arithmetic")]
    public class AddWeighted : ArithmeticBasic
    {

        [Category("Enter")] public double Alpha { set; get; } = 0.5;

        [Category("Enter")] public double Beta { set; get; } = 0.5;

        [Category("Enter")] public double Gamma { set; get; } = 0;

        /// <summary>
        /// dst = alpha cdot img1 + beta cdot img2 + gamma
        /// </summary>
        public override void Execute()
        {
            Output = new Mat();
            Cv2.AddWeighted(InPut1, Alpha, InPut2, Beta, Gamma, Output);
        }
    }
}
