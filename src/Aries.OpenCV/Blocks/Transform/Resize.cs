using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Transform")]
    public class Resize : MatProcessingBlock
    {

        [Category("INPUT")] public Size Size { set; get; }
        [Category("Enter")] public double Fx { set; get; } = 0;
        [Category("Enter")] public double Fy { set; get; } = 0;

        [Category("Enter")] public InterpolationFlags InterpolationFlags { set; get; } = InterpolationFlags.Linear;


        public override bool CanExecute()
        {
            return InPutMat != null &&
                   (Size != new Size(0, 0) || Fx > 0 && Fy > 0);
        }

        public override void Execute()
        {
            OutPutMat = new Mat();
            Cv2.Resize(InPutMat, OutPutMat, Size, Fx, Fy, InterpolationFlags);
        }
    }
}
