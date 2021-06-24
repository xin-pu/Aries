using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Arithmetic")]
    public class Multiply : MatArithmetic
    {
        public override void Execute()
        {
            MatOut = new Mat();
            Cv2.Multiply(MatIn1, MatIn2, MatOut);
        }
    }
}
