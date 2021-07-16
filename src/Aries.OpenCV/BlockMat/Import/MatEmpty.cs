using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Initial")]
    public class MatEmpty : MatProcess
    {
        [Category("ARGUMENT")] public Scalar Color { set; get; } = new Scalar(255);


        public override bool CanCall()
        {
            return MatIn != null;
        }

        public override void Call()
        {
            MatOut = new Mat(MatIn.Size(), MatIn.Type(), Color);
        }
    }
}