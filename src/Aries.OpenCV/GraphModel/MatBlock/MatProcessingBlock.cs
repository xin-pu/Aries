using System.ComponentModel;
using OpenCvSharp;

namespace Aries.OpenCV.GraphModel
{
    public abstract class MatProcessingBlock : ProcessingBlock<Mat, Mat>
    {
        [Category("DATAIN")] public Mat MatIn { set; get; }

        [Category("DATAOUT")] public Mat MatOut { set; get; }


        public override void Reload()
        {
            MatIn = null;
            MatOut = null;
            Status = BlockStatus.ToRun;
        }

    }
}