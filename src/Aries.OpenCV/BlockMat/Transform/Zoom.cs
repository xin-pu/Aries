using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Transform")]
    public class Zoom : MatProcess
    {
        [Category("ARGUMENT")] public int Ratio { set; get; }

        [Category("ARGUMENT")] public bool ZoomInOut { set; get; }

        [Category("ARGUMENT")] public double Fx { set; get; } = 0;
        [Category("ARGUMENT")] public double Fy { set; get; } = 0;
        [Category("ARGUMENT")] public InterpolationFlags InterpolationFlags { set; get; } = InterpolationFlags.Linear;


        public override bool CanCall()
        {
            return MatIn != null &&
                   (Ratio > 0 || Fx > 0 && Fy > 0);
        }

        public override void Call()
        {
            MatOut = new Mat();
            var size = MatIn.Size();
            var newsize = ZoomInOut
                ? new Size(size.Width * Ratio, size.Height * Ratio)
                : new Size(size.Width / Ratio, size.Height / Ratio);
            Cv2.Resize(MatIn, MatOut, newsize, Fx, Fy, InterpolationFlags);
        }
    }
}