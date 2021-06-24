using Aries.OpenCV.GraphModel;
using OpenCvSharp;
using System.ComponentModel;
using System.Linq;

namespace Aries.OpenCV.BlocksContour
{
    [Category("Property")]
    public class FitEllipse : ContoursExport<RotatedRect[]>
    {

        public override void Reload()
        {
            ConsIn = null;
            Status = BlockStatus.ToRun;
        }

        public override bool CanExecute()
        {
            return ConsIn != null;
        }

        public override void Execute()
        {
            Result = ConsIn.Select(a => Cv2.FitEllipse(a)).ToArray();
        }


    }
}
