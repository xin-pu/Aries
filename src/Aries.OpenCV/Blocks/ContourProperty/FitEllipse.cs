using Aries.OpenCV.GraphModel;
using OpenCvSharp;
using System.ComponentModel;
using System.Linq;

namespace Aries.OpenCV.Blocks
{
    [Category("ContourProperty")]
    public class FitEllipse : ContoursExport<RotatedRect[]>
    {

        public override void Reload()
        {
            CosIn = null;
            Status = BlockStatus.ToRun;
        }

        public override bool CanExecute()
        {
            return CosIn != null;
        }

        public override void Execute()
        {
            Result = CosIn.Select(a => Cv2.FitEllipse(a)).ToArray();
        }


    }
}
