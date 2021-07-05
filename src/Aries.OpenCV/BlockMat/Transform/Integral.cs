using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{

    public class Integral : MatProcess
    {

        [Category("ARGUMENT")]
        public int Sdepth { set; get; } = -1;

        public override bool CanCall()
        {
            return MatIn != null;
        }

        public override void Call()
        {
            MatOut = new Mat();
            Cv2.Integral(MatIn, MatOut, Sdepth);
        }
    }
}
