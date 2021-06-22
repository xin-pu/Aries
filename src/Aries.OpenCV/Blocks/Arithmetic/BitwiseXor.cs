using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Arithmetic")]
    public class BitwiseXor : ArithmeticBasic
    {
        public override void Execute()
        {
            MatOut = new Mat();
            Cv2.BitwiseXor(MatIn1, MatIn2, MatOut, Mask);
        }
    }
}
