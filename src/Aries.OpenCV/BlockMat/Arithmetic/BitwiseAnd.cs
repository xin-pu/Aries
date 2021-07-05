using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Arithmetic")]
    public class BitwiseAnd : MatArithmetic
    {
        public override void Call()
        {
            MatOut = new Mat();
            Cv2.BitwiseAnd(MatIn1, MatIn2, MatOut, Mask);
        }
    }

}
