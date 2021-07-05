using System.ComponentModel;
using Aries.OpenCV.GraphModel;

namespace Aries.OpenCV.BlockMat
{
    [Category("Property")]
    public class Depth : MatExport<int>
    {
        public override bool CanCall()
        {
            return MatIn != null;
        }

        public override void Call()
        {
            Result = MatIn.Depth();
        }

    }
}
