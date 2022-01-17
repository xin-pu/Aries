using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat.Effects
{
    public class Sharpen : MatProcess
    {
        public override bool CanCall()
        {
            return MatIn != null;
        }

        public override void Call()
        {
            MatOut = new Mat();
            var kernel = Mat.FromArray(new float[,] {{0, -1, 0}, {-1, 5, -1}, {0, -1, 0}});
            Cv2.Filter2D(MatIn, MatOut, MatIn.Type(), kernel, new Point(-1, -1));
        }
    }
}