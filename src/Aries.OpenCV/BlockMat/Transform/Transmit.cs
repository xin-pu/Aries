using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{

    [Category("Transform")]
    public class Transmit : MatProcess
    {

        public override bool CanCall()
        {
            return MatIn != null;
        }

        public override void Call()
        {
            MatOut = new Mat();
            MatOut = MatIn.Clone();
        }
    }
}
