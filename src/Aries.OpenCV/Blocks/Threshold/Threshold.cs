using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Threshold")]
    public class Threshold : MatProcess
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
            return MatIn != null;
        }

        public override void Execute()
        {
            MatOut = new Mat();
            Cv2.Threshold(MatIn, MatOut, Thresh, Maxval, ThresholdType);
        }
    }
}
