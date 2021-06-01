using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Morphology")]
    public class Erode : ProcessingBlock
    {

        [Category("IN_MAT")] public InputArray Element { set; get; }

        public override bool CanExecute()
        {
            return InPutMat != null && Element != null;
        }

        public override void Execute()
        {
            OutPutMat = new Mat();
            Cv2.Erode(InPutMat, OutPutMat, Element);
        }
    }
}
