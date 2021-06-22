using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("ContourProperty")]
    public class ContourPermeter : ExportBlock<double>
    {
        [Category("ARGUMENT")] public bool Closed { set; get; }

        public override bool CanExecute()
        {
            return MatIn != null;
        }

        public override void Execute()
        {
            Result = Cv2.ArcLength(MatIn, Closed);
        }
    }
}
