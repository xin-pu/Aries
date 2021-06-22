using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Arithmetic")]
    public class Add : MatArithmetic
    {

        public override void Execute()
        {
            MatOut = new Mat();
            Cv2.Add(MatIn1, MatIn2, MatOut, Mask);

        }
    }
}
