using System.ComponentModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Arithmetic")]
    public class BitwiseAnd : ArithmeticBasic
    {
        public override void Execute()
        {
            Output = new Mat();
            Cv2.BitwiseAnd(InPut1, InPut2, Output, Mask);
        }
    }

}
