using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Transform")]
    public class Resize : MatProcess
    {

        [Category("DATAIN")] public Size Size { set; get; }

        [Category("ARGUMENT")] public double Fx { set; get; } = 0;
        [Category("ARGUMENT")] public double Fy { set; get; } = 0;
        [Category("ARGUMENT")] public InterpolationFlags InterpolationFlags { set; get; } = InterpolationFlags.Linear;


        public override bool CanCall()
        {
            return MatIn != null &&
                   (Size != new Size(0, 0) || Fx > 0 && Fy > 0);
        }

        public override void Call()
        {
            MatOut = new Mat();
            Cv2.Resize(MatIn, MatOut, Size, Fx, Fy, InterpolationFlags);
        }
    }
}
