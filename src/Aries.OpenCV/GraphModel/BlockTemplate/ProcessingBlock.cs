using System.ComponentModel;

namespace Aries.OpenCV.GraphModel
{
    public abstract class ProcessingBlock<T1, T2> : BlockVertex
    {
        [Category("DATAIN")] public T1 InPutMat { set; get; }

        [Category("DATAOUT")] public T2 OutPutMat { set; get; }

    }
}
