using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    /// <summary>
    ///  Converts image from one color space to another
    /// </summary>
    [Category("Transform")]
    public class CvtColor : MatProcess
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

        public override bool CanCall()
        {
            return MatIn != null;
        }

        public override void Call()
        {
            MatOut = new Mat();
            Cv2.CvtColor(MatIn, MatOut, ColorConversion, DstN);
        }
    }
}
