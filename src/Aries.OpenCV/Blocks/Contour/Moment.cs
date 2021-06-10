using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Contour")]
    public class Moment : ExportBlock<Moments>
    {
        [Category("ARGUMENT")] public bool BinaryImage { set; get; } = false;

        public override bool CanExecute()
        {
            return InPutMat != null;
        }

        public override void Execute()
        {
            ExportResult = new Moments();
            ExportResult = Cv2.Moments(InPutMat, BinaryImage);
        }
    }
}
