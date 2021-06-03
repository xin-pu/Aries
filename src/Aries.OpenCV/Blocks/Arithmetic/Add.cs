using System.ComponentModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Arithmetic")]
    public class Add : ArithmeticBasic
    {
     
        public override void Execute()
        {
            Output = new Mat();
            Cv2.Add(InPut1, InPut2, Output);

        }
    }
}
