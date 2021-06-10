using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Logic")]
    public class CombineToMats : ProcessingBlock<Mat, Mat[]>
    {

        public Mat InPutMat2 { set; get; }

        public override void Reload()
        {
            InPutMat = null;
            OutPutMat = null;
            Status = BlockStatus.ToRun;
        }

        public override bool CanExecute()
        {
            return InPutMat != null;
        }

        public override void Execute()
        {   
            OutPutMat = new[] {InPutMat};
        }
    }
}
