using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks.Processing
{
    [Category("Threshold")]
    public class Threshold : ProcessingBlock
    {
        /// <summary>
        /// threshold value.
        /// </summary>
        [Category("Enter")] public double Thresh { set; get; } = 122;

        /// <summary>
        /// maximum value to use with the THRESH_BINARY and THRESH_BINARY_INV thresholding types.
        /// </summary>
        [Category("Enter")] public double Maxval { set; get; } = 255;

        /// <summary>
        /// thresholding type (see the details below)
        /// </summary>
        [Category("Enter")] public ThresholdTypes ThresholdType { set; get; } = ThresholdTypes.Binary;


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
