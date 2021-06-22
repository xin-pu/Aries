using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Blur")]
    public class GaussianBlur : MatProcessingBlock
    {

        /// <summary>
        /// Gaussian kernel size. ksize.width and ksize.height can differ but they both must be positive and odd.
        /// Or, they can be zero’s and then they are computed from sigma* .
        /// </summary>
        [Category("ARGUMENT")] public Size KSize { set; get; } = new Size(3, 3);


        /// <summary>
        /// Gaussian kernel standard deviation in X direction.
        /// </summary>
        [Category("ARGUMENT")]
        public double SigmaX { set; get; } = 1D;

        /// <summary>
        /// Gaussian kernel standard deviation in Y direction; if sigmaY is zero, it is set to be equal to sigmaX,
        /// if both sigmas are zeros, they are computed from ksize.width and ksize.height,
        /// respectively (see getGaussianKernel() for details); to fully control the result
        /// regardless of possible future modifications of all this semantics, it is recommended to specify all of ksize, sigmaX, and sigmaY.
        /// </summary>
        [Category("ARGUMENT")]
        public double SigmaY { set; get; } = 0D;


        

        public override bool CanExecute()
        {
            return MatIn != null;
        }

        public override void Execute()
        {
            MatOut = new Mat();
            Cv2.GaussianBlur(MatIn, MatOut, KSize, SigmaX, SigmaY);
        }
    }
}
