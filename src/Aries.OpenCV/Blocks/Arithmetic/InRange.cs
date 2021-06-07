using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Arithmetic")]
    public class InRange : ProcessingBlock
    {


        [Category("IN_MAT")] public Scalar Lowerb { set; get; }

        [Category("IN_MAT")] public Scalar Upperb { set; get; }


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
