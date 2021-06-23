using OpenCvSharp;
using Aries.OpenCV.GraphModel;
using System.ComponentModel;
using System.Linq;

namespace Aries.OpenCV.Blocks
{

    /// <summary>
    /// Get Rect by Contours
    /// </summary>
    [Category("ContourProperty")]
    public class BoundingRect : ContoursExport<Rect[]>
    {

        public override bool CanExecute()
        {
            return ConsIn != null && ConsIn.Length != 0;
        }

        public override void Execute()
        {
            Result = ConsIn.Select(a => Cv2.BoundingRect(a)).ToArray();
        }

    }
}
