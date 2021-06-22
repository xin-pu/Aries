using System.ComponentModel;
using OpenCvSharp;

namespace Aries.OpenCV.GraphModel
{

    public abstract class ExportBlock<T> : BlockVertex
    {
   

        [Category("DATAIN")] public Mat MatIn { set; get; }

        [Category("DATAOUT")] public T Result { set; get; }

        public override void Reload()
        {
            MatIn = null;
            Status = BlockStatus.ToRun;
        }

    }
}
