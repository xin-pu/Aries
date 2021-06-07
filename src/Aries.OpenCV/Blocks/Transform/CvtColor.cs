using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Transform")]
    public class CvtColor : ProcessingBlock
    {


        [Category("Enter")] public int DstN { set; get; } = 0;

        [Category("Enter")]
        public ColorConversionCodes ColorConversion { set; get; }
            = ColorConversionCodes.BGR2HSV;

        public override bool CanExecute()
        {
            return InPutMat != null;
        }

        public override void Execute()
        {
            OutPutMat = new Mat();
            Cv2.CvtColor(InPutMat, OutPutMat, ColorConversion, DstN);
        }
    }
}
