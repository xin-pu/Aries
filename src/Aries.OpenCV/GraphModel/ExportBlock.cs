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

        [Category("INPUT")] public Mat InPutMat { set; get; }

        [Category("OUTPUT")] public T ExportResult { set; get; }

        public override void Reload()
        {
            InPutMat = null;
            Status = BlockStatus.ToRun;
        }

    }
}
