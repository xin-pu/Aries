using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks.Processing
{
    [Serializable]
    [XmlInclude(typeof(BlockVertex))]
    public class Blur : ProcessingBlock
    {

        /// <summary>
        /// The smoothing kernel size
        /// </summary>
        [Category("Enter")]
        public Size KSize { set; get; }

        /// <summary>
        /// The anchor point. The default value Point(-1,-1) means that the anchor is at the kernel center
        /// </summary>
        [Category("Enter")]
        public Point AnchorPoint { set; get; }

        /// <summary>
        /// The border mode used to extrapolate pixels outside of the image
        /// </summary>
        [Category("Enter")]
        public BorderTypes BorderTypes { set; get; }

        public Blur()
        {
            Name = "BLUR";
            KSize = new Size(3, 3);
            AnchorPoint = new Point(-1, -1);
            BorderTypes = BorderTypes.Default;
        }

        public override bool CanExecute()
        {
            return InPutMat != null;
        }

        public override void Execute()
        {
            OutPutMat = new Mat();
            KSize = new Size(3, 3);
            AnchorPoint = new Point(-1, -1);
            BorderTypes = BorderTypes.Default;
            Cv2.Blur(InPutMat, OutPutMat, KSize, AnchorPoint, BorderTypes);
        }


    }

}
