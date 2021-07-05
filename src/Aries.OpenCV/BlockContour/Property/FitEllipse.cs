using Aries.OpenCV.GraphModel;
using OpenCvSharp;
using System.ComponentModel;
using System.Linq;

namespace Aries.OpenCV.BlockContour
{
    [Category("Property")]
    public class FitEllipse : ContoursExport<RotatedRect[]>
    {


        public override bool CanCall()
        {
            return ConsIn != null;
        }

        public override void Call()
        {
            Result = ConsIn.Select(a => Cv2.FitEllipse(a)).ToArray();
        }


    }
}
