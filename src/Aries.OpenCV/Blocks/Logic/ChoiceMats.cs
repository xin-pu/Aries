using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Logic")]
    public class ChoiceMats : ProcessBlock<Mat[], Mat>
    {

        [Category("Enter")] public uint Index { set; get; } = 0;

        public override void Reload()
        {
            TIn = null;
            TOut = null;
            Status = BlockStatus.ToRun;
        }

        public override bool CanExecute()
        {
            return TIn != null && TIn.Length > 0;
        }

        public override void Execute()
        {
            TOut = new Mat();
            TOut = TIn[Index].Clone();
        }

    }

}
