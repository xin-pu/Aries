using System.ComponentModel;

namespace Aries.OpenCV.GraphModel
{
    public abstract class ProcessBlock<T1, T2> : BlockVertex
    {
        [Category("DATAIN")] public T1 TIn { set; get; }

        [Category("DATAOUT")] public T2 TOut { set; get; }

    }
}
