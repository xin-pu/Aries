using System;
using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Blur")]
    public class MedianBlur : ProcessingBlock
    {

        [Category("Enter")] public int KSize { set; get; } = 3;

        public override bool CanExecute()
        {
            return InPutMat != null;
        }

        public override void Execute()
        {

            OutPutMat = new Mat();
            Cv2.MedianBlur(InPutMat, OutPutMat, KSize);
        }
    }
}
