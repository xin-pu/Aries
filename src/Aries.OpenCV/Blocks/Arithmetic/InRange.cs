using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Arithmetic")]
    public class InRange : MatProcessingBlock
    {


        [Category("DATAIN")] public Scalar Lowerb { set; get; }

        [Category("DATAIN")] public Scalar Upperb { set; get; }


        public override bool CanExecute()
        {
            return InPutMat != null;
        }

        public override void Execute()
        {
            OutPutMat = new Mat();
            Cv2.InRange(InPutMat, Lowerb, Upperb, OutPutMat);
        }
    }
}
