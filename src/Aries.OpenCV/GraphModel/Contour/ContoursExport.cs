using System.ComponentModel;
using OpenCvSharp;

namespace Aries.OpenCV.GraphModel
{


    public abstract class ContoursExport<T> : BlockVertex
    {
        [Category("DATAIN")] public Mat[] CosIn { set; get; }

        [Category("DATAOUT")] public T Result { set; get; }


        public override void Reload()
        {
            CosIn = null;
            Result = default;
            Status = BlockStatus.ToRun;
        }

    }
}
