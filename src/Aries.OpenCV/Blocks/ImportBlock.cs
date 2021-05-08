using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    public abstract class ImportBlock:Block
    {
        public Mat OutPutMat { set; get; }
        public abstract bool CanImport();
        public abstract void Import();
    }
}
