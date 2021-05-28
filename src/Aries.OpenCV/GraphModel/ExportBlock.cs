using System.ComponentModel;
using OpenCvSharp;

namespace Aries.OpenCV.GraphModel
{

    public abstract class ExportBlock<T> : BlockVertex
    {
        protected ExportBlock()
        {
            BlockType = BlockType.Export;
        }

        [Category("IN_MAT")] public Mat InPutMat { set; get; }

        [Category("OUT_DATA")] public T ExportResult { set; get; }

        public override void Reload()
        {
            InPutMat = null;
            Status = BlockStatus.ToRun;
        }

    }
}
