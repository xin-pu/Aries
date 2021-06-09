using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{

    [Category("Transform")]
    public class Transmit : MatProcessingBlock
    {

        public override bool CanExecute()
        {
            return InPutMat != null;
        }

        public override void Execute()
        {
            OutPutMat = new Mat();
            OutPutMat = InPutMat.Clone();
        }
    }
}
