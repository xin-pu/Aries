using System.ComponentModel;
using OpenCvSharp;

namespace Aries.OpenCV.GraphModel
{
    public abstract class ProcessingBlock : BlockVertex
    {
        protected ProcessingBlock()
        {
            BlockType = BlockType.Processing;
        }

        [Category("IN_MAT")] public Mat InPutMat { set; get; }

        [Category("OUT_MAT")] public Mat OutPutMat { set; get; }


        public override void Reload()
        {
            InPutMat = null;
            OutPutMat = null;
            Status = BlockStatus.ToRun;
        }

    }

}
