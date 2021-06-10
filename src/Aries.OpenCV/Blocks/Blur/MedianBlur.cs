using System;
using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    /// <summary>
    /// 中值滤波
    /// </summary>
    [Category("Blur")]
    public class MedianBlur : MatProcessingBlock
    {

        [Category("ARGUMENT")] public int KSize { set; get; } = 3;

        public override bool CanExecute()
        {
            return KSize % 2 == 1 && InPutMat != null;
        }

        public override void Execute()
        {

            OutPutMat = new Mat();
            Cv2.MedianBlur(InPutMat, OutPutMat, KSize);
        }
    }
}
