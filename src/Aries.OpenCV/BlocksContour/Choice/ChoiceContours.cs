using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlocksContour
{
    [Category("Logic")]
    public class ChoiceContours : ContoursExport<Mat>
    {
        [Category("Enter")] public uint Index { set; get; } = 0;

        public override void Reload()
        {
            ConsIn = null;
            Result = null;
            Status = BlockStatus.ToRun;
        }

        public override bool CanExecute()
        {
            return ConsIn != null && ConsIn.Length > 0;
        }

        public override void Execute()
        {
            Result = new Mat();
            Result = ConsIn[Index].Clone();
        }
    }
}
