using System.ComponentModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Arithmetic")]
    public class Subtract : ArithmeticBasic
    {
        public override void Execute()
        {
            Output = new Mat();
            Cv2.Subtract(InPut1, InPut2, Output, Mask);
        }
    }
}
