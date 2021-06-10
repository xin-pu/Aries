using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    /// <summary>
    ///  Converts image from one color space to another
    /// </summary>
    [Category("Transform")]
    public class CvtColor : MatProcessingBlock
    {

        /// <summary>
        /// The number of channels in the destination image;
        /// if the parameter is 0, the number of the channels will be derived automatically
        /// from src and the code
        /// </summary>
        [Category("ARGUMENT")] public int DstN { set; get; } = 0;

        /// <summary>
        /// ColorConversionCodes.BGR2HSV
        /// ColorConversionCodes.BGR2GRAY
        /// </summary>
        [Category("ARGUMENT")]
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
