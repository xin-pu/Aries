using System.ComponentModel;
using OpenCvSharp;

namespace Aries.OpenCV.GraphModel
{
    public abstract class ContoursProcess : BlockVertex
    {
        [Category("DATAIN")] public Mat[] ConsIn { set; get; }

        [Category("DATAOUT")] public Mat[] ConsOut { set; get; }


        public override void Reload()
        {
            ConsIn = null;
            ConsOut = null;
            Status = BlockStatus.ToRun;
        }
    }
}
