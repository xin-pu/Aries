using OpenCvSharp;

namespace Aries.OpenCV.Blocks.GraphModel
{
    public abstract class ImportBlock : BlockVertex
    {
        public Mat OutPutMat { set; get; }
        public abstract bool CanImport();
        public abstract void Import();
    }
}
