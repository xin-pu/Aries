using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockContour
{
    [Category("Logic")]
    public class ChoiceContours : ContoursExport<Mat>
    {
        [Category("Enter")] public uint Index { set; get; } = 0;

        public override bool CanCall()
        {
            return ConsIn != null && ConsIn.Length > 0;
        }

        public override void Call()
        {
            Result = new Mat();
            Result = ConsIn[Index].Clone();
        }
    }
}
