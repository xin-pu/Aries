using Aries.OpenCV.GraphModel;
using OpenCvSharp;
using System.ComponentModel;

namespace Aries.OpenCV.Blocks
{


    [Category("Contour")]
    public class MinEnclosingCircle : ProcessingBlock<Mat, CircleSegment>
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
            Point2f point;
            float radius;
            Cv2.MinEnclosingCircle(InPutMat, out point, out radius);
            OutPutMat = new CircleSegment(point, radius);
        }


    }
}
