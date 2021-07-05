using System.ComponentModel;
using System.Linq;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockContour
{

    /// <summary>
    /// Get Rect by Contours
    /// </summary>
    [Category("Property")]
    public class BoundingRect : ContoursExport<Rect[]>
    {

        public override bool CanCall()
        {
            return ConsIn != null && ConsIn.Length != 0;
        }

        public override void Call()
        {
            Result = ConsIn.Select(a => Cv2.BoundingRect(a)).ToArray();
        }

    }
}
