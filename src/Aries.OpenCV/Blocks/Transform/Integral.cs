using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{

    public class Integral : MatProcess
    {

        [Category("ARGUMENT")]
        public int Sdepth { set; get; } = -1;

        public override bool CanExecute()
        {
            return MatIn != null;
        }

        public override void Execute()
        {
            MatOut = new Mat();
            Cv2.Integral(MatIn, MatOut, Sdepth);
        }
    }
}
