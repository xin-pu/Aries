using Aries.OpenCV.GraphModel;
using OpenCvSharp;
using System.ComponentModel;
using System.Linq;

namespace Aries.OpenCV.BlocksContour
{

    /// <summary>
    /// Output is Contour
    /// </summary>
    [Category("Property")]
    public class MinAreaRect : ContoursExport<RotatedRect[]>
    {

        public override bool CanExecute()
        {
            return ConsIn != null && ConsIn.Length > 0;
        }

        public override void Execute()
        {
            Result = ConsIn.Select(con => Cv2.MinAreaRect(con)).ToArray();
        }


    }
}
