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
            return MatIn != null;
        }

        public override void Execute()
        {
            MatOut = new Mat();
            MatOut = MatIn.Clone();
        }
    }
}
