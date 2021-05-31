using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks.Processing
{
    [Serializable]
    [XmlInclude(typeof(BlockVertex))]
    public class Threshold : ProcessingBlock
    {
        /// <summary>
        /// threshold value.
        /// </summary>
        [Category("Enter")] public double Thresh { set; get; }

        /// <summary>
        /// maximum value to use with the THRESH_BINARY and THRESH_BINARY_INV thresholding types.
        /// </summary>
        [Category("Enter")] public double Maxval { set; get; }

        /// <summary>
        /// thresholding type (see the details below)
        /// </summary>
        [Category("Enter")] public ThresholdTypes ThresholdType { set; get; }


        public Threshold()
        {
            Thresh = 122;
            Maxval=255;
            ThresholdType = ThresholdTypes.Binary;
        }

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
