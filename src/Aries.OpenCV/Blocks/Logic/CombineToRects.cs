using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Logic")]
    public class CombineToRects : ProcessingBlock<Rect, Rect[]>
    {

        public override void Reload()
        {
            InPutMat = new Rect(0, 0, 0, 0);
            OutPutMat = null;
            Status = BlockStatus.ToRun;
        }

        public override bool CanExecute()
        {
            return InPutMat != new Rect(0, 0, 0, 0);
        }

        public override void Execute()
        {
            OutPutMat = new[] { InPutMat };
        }
    }
}
