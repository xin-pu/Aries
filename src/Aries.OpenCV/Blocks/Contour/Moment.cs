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
            return MatIn != null;
        }

        public override void Execute()
        {
            Result = new Moments();
            Result = Cv2.Moments(MatIn, BinaryImage);
        }
    }
}
