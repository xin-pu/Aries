using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Threshold")]
    public class Threshold : MatProcessingBlock
    {
        /// <summary>
        /// threshold value.
        /// </summary>
        [Category("ARGUMENT")] public double Thresh { set; get; } = 122;

        /// <summary>
        /// maximum value to use with the THRESH_BINARY and THRESH_BINARY_INV thresholding types.
        /// </summary>
        [Category("ARGUMENT")] public double Maxval { set; get; } = 255;

        /// <summary>
        /// thresholding type (see the details below)
        /// </summary>
        [Category("ARGUMENT")] public ThresholdTypes ThresholdType { set; get; } = ThresholdTypes.Binary;


        public override bool CanExecute()
        {
            return InPutMat != null;
        }

        public override void Execute()
        {
            OutPutMat = new Mat();
            Cv2.Threshold(InPutMat, OutPutMat, Thresh, Maxval, ThresholdType);
        }
    }
}
