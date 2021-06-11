using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Convert")]
    public class ConvertScaleAbs : MatProcessingBlock
    {

        [Category("ARGUMENT")] public double Alpha { set; get; } = 1D;

        [Category("ARGUMENT")] public double Beta { set; get; } = 0D;

        public override bool CanExecute()
        {
            return InPutMat != null;
        }

        public override void Execute()
        {
            OutPutMat = new Mat();
            Cv2.ConvertScaleAbs(InPutMat, OutPutMat, Alpha, Beta);
        }
    }
}
