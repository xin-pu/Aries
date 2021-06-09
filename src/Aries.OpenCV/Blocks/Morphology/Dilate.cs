using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Morphology")]
    public class Dilate : MatProcessingBlock
    {

        [Category("INPUT")] public InputArray Element { set; get; }

        public override bool CanExecute()
        {
            return InPutMat != null && Element != null;
        }

        public override void Execute()
        {
            OutPutMat = new Mat();
            Cv2.Dilate(InPutMat, OutPutMat, Element);
        }
    }



}