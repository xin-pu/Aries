using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks.Logic
{
    [Category("Logic")]
    public class ChoiceFromMats : ProcessingBlock<Mat[], Mat>
    {

        [Category("Enter")] public uint Index { set; get; } = 0;

        public override void Reload()
        {
            InPutMat = null;
            OutPutMat = null;
            Status = BlockStatus.ToRun;
        }

        public override bool CanExecute()
        {
            return InPutMat != null && InPutMat.Length > 0;
        }

        public override void Execute()
        {
            OutPutMat = new Mat();
            OutPutMat = InPutMat[Index];
        }
    }

}
