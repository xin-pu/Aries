﻿using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks.Processing
{
    [Category("Blur")]
    public class GaussianBlur : ProcessingBlock
    {
        [Category("Enter")] public int KSize_Width { set; get; }


        [Category("Enter")] public int KSize_Height { set; get; }


        /// <summary>
        /// Gaussian kernel standard deviation in X direction.
        /// </summary>
        [Category("Enter")]
        public double SigmaX { set; get; }

        /// <summary>
        /// Gaussian kernel standard deviation in Y direction; if sigmaY is zero, it is set to be equal to sigmaX,
        /// if both sigmas are zeros, they are computed from ksize.width and ksize.height,
        /// respectively (see getGaussianKernel() for details); to fully control the result
        /// regardless of possible future modifications of all this semantics, it is recommended to specify all of ksize, sigmaX, and sigmaY.
        /// </summary>
        [Category("Enter")]
        public double SigmaY { set; get; }

        /// <summary>
        /// Gaussian kernel size. ksize.width and ksize.height can differ but they both must be positive and odd.
        /// Or, they can be zero’s and then they are computed from sigma* .
        /// </summary>
        private Size KSize
        {
            get { return new Size(KSize_Width, KSize_Height); }
        }

        public GaussianBlur()
        {
            Name = "GaussianBlur";
            KSize_Width = KSize_Height = 3;
            SigmaX = 1D;
            SigmaY = 0D;
        }

        public override bool CanExecute()
        {
            return InPutMat != null;
        }

        public override void Execute()
        {
            OutPutMat = new Mat();
            Cv2.GaussianBlur(InPutMat, OutPutMat, KSize, SigmaX, SigmaY);
        }
    }
}
