using Aries.OpenCV.GraphModel;
using OpenCvSharp;
using System.ComponentModel;

namespace Aries.OpenCV.Blocks
{
    [Category("Contour")]
    public class FitEllipse : ProcessingBlock<Mat, RotatedRect>
    {

        public override void Reload()
        {
            InPutMat = null;
            Status = BlockStatus.ToRun;
        }

        public override bool CanExecute()
        {
            return InPutMat != null;
        }

        public override void Execute()
        {
            OutPutMat = Cv2.FitEllipse(InPutMat);
        }


    }
}
