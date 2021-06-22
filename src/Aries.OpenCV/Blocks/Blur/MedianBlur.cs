﻿using System;
using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    /// <summary>
    /// 中值滤波
    /// </summary>
    [Category("Blur")]
    public class MedianBlur : MatProcess
    {

        [Category("ARGUMENT")] public int KSize { set; get; } = 3;

        public override bool CanExecute()
        {
            return KSize % 2 == 1 && MatIn != null;
        }

        public override void Execute()
        {

            MatOut = new Mat();
            Cv2.MedianBlur(MatIn, MatOut, KSize);
        }
    }
}
