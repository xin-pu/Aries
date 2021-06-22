using OpenCvSharp;
using Aries.OpenCV.GraphModel;
using System.ComponentModel;

namespace Aries.OpenCV.Blocks
{
    [Category("Contour")]
    public class ContourConvexHull : MatProcessingBlock
    {

        [Category("ARGUMENT")] public bool Clockwise { set; get; } = false;
        
        /// <summary>
        /// Return Point or Return Contour Index of Point
        /// </summary>
        [Category("ARGUMENT")] 
        public bool ReturnPoints { set; get; } = true;

        public override bool CanExecute()
        {
            return MatIn != null;
        }

        public override void Execute()
        {
            MatOut = new Mat();
            Cv2.ConvexHull(MatIn, MatOut, Clockwise, ReturnPoints);
        }
    }
}
