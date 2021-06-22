using Aries.OpenCV.GraphModel;
using OpenCvSharp;
using System.ComponentModel;

namespace Aries.OpenCV.Blocks
{

    /// <summary>
    /// Output is Contour
    /// </summary>
    [Category("ContourProperty")]
    public class MinAreaRect : MatProcess
    {

        public override bool CanExecute()
        {
            return MatIn != null;
        }

        public override void Execute()
        {
            MatOut = new Mat();
            var box = Cv2.MinAreaRect(MatIn);
            Cv2.BoxPoints(box, MatOut);
        }


    }
}
