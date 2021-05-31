using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks.Import
{
    [Category("Read")]
    public class ElementCreate :ImportBlock<InputArray>
    {
        [Category("Enter")] public int ElementWidth { set; get; } = 3;

        [Category("Enter")] public int ElementHeight { set; get; } = 3;

        [Category("Enter")] public MatType MatType { set; get; } = MatType.CV_8U;


       

        public override bool CanExecute()
        {
            return ElementWidth > 0 && ElementHeight > 0;
        }

        public override void Execute()
        {
            OutPut = new Mat(new Size(ElementWidth, ElementHeight), MatType);
        }
    }
}
