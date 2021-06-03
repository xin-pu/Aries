using System.ComponentModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Arithmetic")]
    public class BitwiseOr : ArithmeticBasic
    {
        public override void Execute()
        {
            Output = new Mat();
            Cv2.BitwiseOr(InPut1, InPut2, Output, Mask);
        }
    }
}
