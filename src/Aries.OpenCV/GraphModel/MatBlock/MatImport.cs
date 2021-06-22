using System.ComponentModel;
using OpenCvSharp;

namespace Aries.OpenCV.GraphModel
{
    public abstract class MatImport : BlockVertex
    {
        [Category("DATAOUT")] public Mat MatOut { set; get; }


        public override void Reload()
        {
            MatOut = null;
            Status = BlockStatus.ToRun;
        }
    }
}
