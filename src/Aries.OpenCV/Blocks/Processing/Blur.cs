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
        [Category("Enter")] public int KSize_Width { set; get; }


        [Category("Enter")] public int KSize_Height { set; get; }

        [Category("Enter")] public int AnchorPoint_X { set; get; }


        [Category("Enter")] public int AnchorPoint_Y { set; get; }

        /// <summary>
        /// The border mode used to extrapolate pixels outside of the image
        /// </summary>
        [Category("Enter")]
        public BorderTypes BorderTypes { set; get; }

        /// <summary>
        /// The smoothing kernel size
        /// </summary>
        private Size KSize
        {
            get { return new Size(KSize_Width, KSize_Height); }
        }

        /// <summary>
        /// The anchor point. The default value Point(-1,-1) means that the anchor is at the kernel center
        /// </summary>
        private Point AnchorPoint
        {
            get { return new Point(AnchorPoint_X, AnchorPoint_Y); }
        }

        public Blur()
        {
            Name = "BLUR";
            KSize_Width = KSize_Height = 3;
            AnchorPoint_X = AnchorPoint_Y = -1;
            BorderTypes = BorderTypes.Default;
        }

        public override bool CanExecute()
        {
            return InPutMat != null;
        }

        public override void Execute()
        {
            OutPutMat = new Mat();
            Cv2.Blur(InPutMat, OutPutMat, KSize, AnchorPoint, BorderTypes);
        }


    }

}
