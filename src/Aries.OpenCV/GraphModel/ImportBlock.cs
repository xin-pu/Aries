using System.ComponentModel;
using OpenCvSharp;

namespace Aries.OpenCV.GraphModel
{
    public abstract class ImportBlock : BlockVertex
    {
       
        protected ImportBlock()
        {
            BlockType = BlockType.Import;
        }
        

        [Category("OUT_MAT")]
        public Mat OutPutMat { set; get; }

    }
}
