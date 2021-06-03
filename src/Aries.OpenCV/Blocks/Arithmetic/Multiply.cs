using System.ComponentModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Arithmetic")]
    public class Multiply : ArithmeticBasic
    {
        public override void Execute()
        {
            Output = new Mat();
            Cv2.Multiply(InPut1, InPut2, Output);
        }
    }
}
