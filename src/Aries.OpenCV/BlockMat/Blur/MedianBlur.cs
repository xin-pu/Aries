using System;
using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    /// <summary>
    /// 中值滤波
    /// </summary>
    [Category("Blur")]
    public class MedianBlur : MatProcess
    {

        [Category("ARGUMENT")] public int KSize { set; get; } = 3;

        public override bool CanCall()
        {
            return KSize % 2 == 1 && MatIn != null;
        }

        public override void Call()
        {

            MatOut = new Mat();
            Cv2.MedianBlur(MatIn, MatOut, KSize);
        }
    }
}
