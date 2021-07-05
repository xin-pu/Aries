using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Property")]
    public class Type : MatExport<MatType>
    {
        public override bool CanCall()
        {
            return MatIn != null;
        }

        public override void Call()
        {
            Result = MatIn.Type();
        }

    }
}
