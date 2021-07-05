using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Convert")]
    public class ConvertScaleAbs : MatProcess
    {

        [Category("ARGUMENT")] public double Alpha { set; get; } = 1D;

        [Category("ARGUMENT")] public double Beta { set; get; } = 0D;

        public override bool CanCall()
        {
            return MatIn != null;
        }

        public override void Call()
        {
            MatOut = new Mat();
            Cv2.ConvertScaleAbs(MatIn, MatOut, Alpha, Beta);
        }
    }
}
