using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Contour")]
    public class ContourPermeter : ExportBlock<double>
    {
        [Category("Enter")] public bool Closed { set; get; }

        public override bool CanExecute()
        {
            return InPutMat != null;
        }

        public override void Execute()
        {
            ExportResult = Cv2.ArcLength(InPutMat, Closed);
        }
    }
}
