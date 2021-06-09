using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{

    [Category("Blur")]
    public class Blur : MatProcessingBlock
    {
        [Category("Enter")] public int KSize_Width { set; get; } = 3;


        [Category("Enter")] public int KSize_Height { set; get; } = 3;

        [Category("Enter")] public int AnchorPoint_X { set; get; } = -1;


        [Category("Enter")] public int AnchorPoint_Y { set; get; } = -1;

        /// <summary>
        /// The border mode used to extrapolate pixels outside of the image
        /// </summary>
        [Category("Enter")]
        public BorderTypes BorderTypes { set; get; } = BorderTypes.Default;

        /// <summary>
        /// The smoothing kernel size
        /// </summary>
        private Size KSize => new Size(KSize_Width, KSize_Height);

        /// <summary>
        /// The anchor point. The default value Point(-1,-1) means that the anchor is at the kernel center
        /// </summary>
        private Point AnchorPoint => new Point(AnchorPoint_X, AnchorPoint_Y);


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
