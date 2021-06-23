using System.ComponentModel;
using System.Linq;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("ContourProperty")]
    public class ContourPermeter : ContoursExport<double[]>
    {
        [Category("ARGUMENT")] public bool Closed { set; get; }

        public override bool CanExecute()
        {
            return ConsIn != null && ConsIn.Length > 1;
        }

        public override void Execute()
        {
            Result = ConsIn.Select(c => Cv2.ArcLength(c, Closed)).ToArray();
        }
    }
}
