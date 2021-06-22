using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Draw")]
    public class CopyMakeBorder : MatProcessingBlock
    {
        [Category("DATAIN")] public Scalar Scalar { set; get; }

        [Category("ARGUMENT")] public int Top { set; get; } = 0;
        [Category("ARGUMENT")] public int Bottom { set; get; } = 0;
        [Category("ARGUMENT")] public int Left { set; get; } = 0;
        [Category("ARGUMENT")] public int Right { set; get; } = 0;
        [Category("ARGUMENT")] public BorderTypes BorderType { set; get; } = BorderTypes.Default;
  

        public override bool CanExecute()
        {
            return MatIn != null;
        }

        public override void Execute()
        {
            MatOut = new Mat();
            Cv2.CopyMakeBorder(MatIn, MatOut, Top, Bottom, Left, Right, BorderType, Scalar);
        }
    }
}
