using OpenCvSharp;
using Aries.OpenCV.GraphModel;
using System.ComponentModel;

namespace Aries.OpenCV.Blocks
{

    [Category("Contour")]
    public class BoundingRect : ProcessingBlock<Mat, Rect>
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
            OutPutMat = Cv2.BoundingRect(InPutMat);
        }


    }
}
