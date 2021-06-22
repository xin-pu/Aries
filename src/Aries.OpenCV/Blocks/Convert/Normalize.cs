using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{

    [Category("Convert")]
    public class Normalize : MatProcessingBlock
    {
        [Category("DATAIN")] public Mat Mask { set; get; }

        [Category("ARGUMENT")] public double Alpha { set; get; } = 255;

        [Category("ARGUMENT")] public double Beta { set; get; } = 1;

        [Category("ARGUMENT")] public NormTypes NormType { set; get; } = NormTypes.MinMax;
        [Category("ARGUMENT")] public int DType { set; get; } = -1;


        [Category("CHOICE")] public bool EnableMask { set; get; }


        public override void Reload()
        {
            InPutMat = null;
            OutPutMat = null;
            Mask = null;
            base.Reload();

        }

        public override bool CanExecute()
        {
            return InPutMat != null &&
                   (!EnableMask || Mask != null);
        }

        public override void Execute()
        {
            OutPutMat = new Mat();
            Cv2.Normalize(InPutMat, OutPutMat, Alpha, Beta, NormType, DType, Mask);
        }
    }
}
