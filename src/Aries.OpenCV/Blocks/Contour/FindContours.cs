using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Contour")]
    public class FindContours : ExportBlock<Size>
    {
        public override bool CanExecute()
        {
            return true;
        }

        public override void Execute()
        {
         
        }
    }
}
