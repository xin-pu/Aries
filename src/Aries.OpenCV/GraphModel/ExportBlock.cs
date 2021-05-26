using System.ComponentModel;
using System.Xml.Serialization;
using OpenCvSharp;

namespace Aries.OpenCV.GraphModel
{

    public abstract class ExportBlock<T> : BlockVertex
    {
        protected ExportBlock()
        {
            BlockType = BlockType.Export;
        }

        [Category("Input_MAT")]
        public Mat InputMat { set; get; }

        [Category("Output")]
        public T ExportResult { set; get; }

        public abstract void Execute();
    }
}
