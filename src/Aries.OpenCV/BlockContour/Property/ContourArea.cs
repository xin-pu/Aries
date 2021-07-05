using System.ComponentModel;
using System.Linq;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockContour
{
    [Category("Property")]
    public class ContourArea : ContoursExport<double[]>
    {
        [Category("ARGUMENT")] public bool Oriented { set; get; }

        public override bool CanCall()
        {
            return ConsIn != null && ConsIn.Length > 0;
        }

        public override void Call()
        {
            Result = ConsIn.Select(c => Cv2.ContourArea(c, Oriented)).ToArray();
        }
    }
}
