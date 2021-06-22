using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Logic")]
    public class ChoiceContours : ContoursExport<Mat>
    {
        [Category("Enter")] public uint Index { set; get; } = 0;

        public override void Reload()
        {
            CosIn = null;
            Result = null;
            Status = BlockStatus.ToRun;
        }

        public override bool CanExecute()
        {
            return CosIn != null && CosIn.Length > 0;
        }

        public override void Execute()
        {
            Result = new Mat();
            Result = CosIn[Index].Clone();
        }
    }
}
