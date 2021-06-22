using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Filter")]
    public class Laplacian : MatProcessingBlock
    {
        [Category("ARGUMENT")] public MatType MatType { set; get; }

        [Category("ARGUMENT")] public int KSize { set; get; } = 1;

        [Category("ARGUMENT")] public double Scale { set; get; } = 1;

        [Category("ARGUMENT")] public double Delta { set; get; } = 0;

        [Category("ARGUMENT")] public BorderTypes BorderType { set; get; } = BorderTypes.Default;

        public override bool CanExecute()
        {
            return MatIn != null;
        }

        public override void Execute()
        {
            MatOut = new Mat();
            Cv2.Laplacian(MatIn, MatOut, MatType, KSize, Scale, Delta, BorderType);
        }
    }
}
