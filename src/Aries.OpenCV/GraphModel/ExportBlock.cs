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

        [Category("IN_MAT")] public Mat InputMat { set; get; }

        [Category("OUT_DATA")] public T ExportResult { set; get; }


        public override string ToString()
        {
            return $"{ExportResult}";
        }
    }
}
