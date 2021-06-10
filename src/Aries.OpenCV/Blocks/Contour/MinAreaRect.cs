using Aries.OpenCV.GraphModel;
using OpenCvSharp;
using System.ComponentModel;

namespace Aries.OpenCV.Blocks
{

    /// <summary>
    /// Output is Contour
    /// </summary>
    [Category("Contour")]
    public class MinAreaRect : MatProcessingBlock
    {

        public override bool CanExecute()
        {
            return InPutMat != null;
        }

        public override void Execute()
        {
            OutPutMat = new Mat();
            var box = Cv2.MinAreaRect(InPutMat);
            Cv2.BoxPoints(box, OutPutMat);
        }


    }
}
