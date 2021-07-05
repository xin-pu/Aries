using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Arithmetic")]
    public class InRange : MatProcess
    {

        [Category("ARGUMENT")] public Scalar Lowerb { set; get; }

        [Category("ARGUMENT")] public Scalar Upperb { set; get; }


        public override bool CanCall()
        {
            return MatIn != null;
        }

        public override void Call()
        {
            MatOut = new Mat();
            Cv2.InRange(MatIn, Lowerb, Upperb, MatOut);
        }
    }
}
