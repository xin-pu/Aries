using System.ComponentModel;
using OpenCvSharp;

namespace Aries.OpenCV.GraphModel
{
    public abstract class GeneralBlock : BlockVertex
    {
        [Category("IN_MAT")] public Mat Mask { set; get; }

        protected GeneralBlock()
        {
            BlockType = BlockType.General;
        }


        public override void Reload()
        {
            Status = BlockStatus.ToRun;
            Mask = null;
        }

    }
}
