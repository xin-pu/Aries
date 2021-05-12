using OpenCvSharp;

namespace Aries.OpenCV.GraphModel
{

    public abstract class ExportBlock<T> : BlockVertex
    {
        public BlockType BlockType = BlockType.Export;
        public Mat InputMat { set; get; }

        public T ExportResult { set; get; }

        public abstract void Execute();
    }
}
