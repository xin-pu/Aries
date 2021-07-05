using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Logic")]
    public class ChoiceMats : ProcessBlock<Mat[], Mat>
    {

        [Category("ARGUMENT")] public uint Index { set; get; } = 0;


        public override bool CanCall()
        {
            return TIn != null && TIn.Length > 0;
        }

        public override void Call()
        {
            TOut = new Mat();
            TOut = TIn[Index].Clone();
        }

    }

}
