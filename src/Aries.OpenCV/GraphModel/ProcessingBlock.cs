using System.ComponentModel;
using OpenCvSharp;

namespace Aries.OpenCV.GraphModel
{
    public abstract class ProcessingBlock<T1, T2> : BlockVertex
    {
        [Category("DATAIN")] public T1 InPutMat { set; get; }

        [Category("DATAOUT")] public T2 OutPutMat { set; get; }

    }

    public abstract class MatProcessingBlock : ProcessingBlock<Mat, Mat>
    {

        public override void Reload()
        {
            InPutMat = null;
            OutPutMat = null;
            Status = BlockStatus.ToRun;
        }

    }




}
