using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Arithmetic")]
    public class BitwiseXor : MatArithmetic
    {
        public override void Call()
        {
            MatOut = new Mat();
            Cv2.BitwiseXor(MatIn1, MatIn2, MatOut, Mask);
        }
    }
}
