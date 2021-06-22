using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Property")]
    public class Shape : ExportBlock<Size>
    {
        public override bool CanExecute()
        {
            return MatIn != null;
        }

        public override void Execute()
        {
            Result = MatIn.Size();
        }

    }

}
