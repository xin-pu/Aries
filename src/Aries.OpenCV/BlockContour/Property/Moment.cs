using System.ComponentModel;
using System.Linq;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockContour
{
    [Category("Property")]
    public class Moment : ContoursExport<Moments[]>
    {
        [Category("ARGUMENT")] public bool BinaryImage { set; get; } = false;

        public override bool CanExecute()
        {
            return ConsIn != null;
        }

        public override void Execute()
        {
            Result = new Moments[0];
            Result = ConsIn.Select(c => Cv2.Moments(c, BinaryImage)).ToArray();
        }
    }
}
