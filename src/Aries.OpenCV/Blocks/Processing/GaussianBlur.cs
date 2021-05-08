using OpenCvSharp;

namespace Aries.OpenCV.Blocks.Processing
{
    public class GaussianBlur : ProcessingBlock
    {

        /// <summary>
        /// Gaussian kernel size. ksize.width and ksize.height can differ but they both must be positive and odd.
        /// Or, they can be zero’s and then they are computed from sigma* .
        /// </summary>
        public Size KSize { set; get; }

        /// <summary>
        /// Gaussian kernel standard deviation in X direction.
        /// </summary>
        public double SigmaX { set; get; }

        /// <summary>
        /// Gaussian kernel standard deviation in Y direction; if sigmaY is zero, it is set to be equal to sigmaX,
        /// if both sigmas are zeros, they are computed from ksize.width and ksize.height,
        /// respectively (see getGaussianKernel() for details); to fully control the result
        /// regardless of possible future modifications of all this semantics, it is recommended to specify all of ksize, sigmaX, and sigmaY.
        /// </summary>
        public double SigmaY { set; get; }

        public GaussianBlur()
        {
            KSize = new Size(3, 3);
            SigmaX = 1D;
            SigmaY = 0D;
        }

        public override bool CanExecute()
        {
            return true;
        }

        public override void Execute()
        {
            Cv2.GaussianBlur(InputMat, OutPutMat, KSize, SigmaX, SigmaY);
        }
    }
}
