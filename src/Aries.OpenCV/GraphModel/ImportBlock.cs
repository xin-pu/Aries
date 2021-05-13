using System.Xml.Serialization;
using OpenCvSharp;

namespace Aries.OpenCV.GraphModel
{
    public abstract class ImportBlock : BlockVertex
    {
        public BlockType BlockType = BlockType.Import;
        [XmlIgnore]
        public Mat OutPutMat { set; get; }
        public abstract bool CanImport();
        public abstract void Import();
    }
}
