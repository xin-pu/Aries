using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Transform")]
    public class Resize : MatProcessingBlock
    {

        [Category("DATAIN")] public Size Size { set; get; }

        [Category("ARGUMENT")] public double Fx { set; get; } = 0;
        [Category("ARGUMENT")] public double Fy { set; get; } = 0;
        [Category("ARGUMENT")] public InterpolationFlags InterpolationFlags { set; get; } = InterpolationFlags.Linear;

        public override void Reload()
        {
            Size = new Size(0, 0);
            base.Reload();
        }

        public override bool CanExecute()
        {
            return MatIn != null &&
                   (Size != new Size(0, 0) || Fx > 0 && Fy > 0);
        }

        public override void Execute()
        {
            MatOut = new Mat();
            Cv2.Resize(MatIn, MatOut, Size, Fx, Fy, InterpolationFlags);
        }
    }
}
