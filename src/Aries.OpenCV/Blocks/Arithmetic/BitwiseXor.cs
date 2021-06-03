using System.ComponentModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Arithmetic")]
    public class BitwiseXor : ArithmeticBasic
    {
        public override void Execute()
        {
            Output = new Mat();
            Cv2.BitwiseXor(InPut1, InPut2, Output, Mask);
        }
    }
}
