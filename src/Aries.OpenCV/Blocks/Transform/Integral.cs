using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{

    public class Integral : MatProcessingBlock
    {

        [Category("ARGUMENT")]
        public int Sdepth { set; get; } = -1;

        public override bool CanExecute()
        {
            return InPutMat != null;
        }

        public override void Execute()
        {
            OutPutMat = new Mat();
            Cv2.Integral(InPutMat, OutPutMat, Sdepth);
        }
    }
}
