﻿using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Filter")]
    public class Canny : MatProcessingBlock
    {
        [Category("ARGUMENT")] public double ThresholdLow { set; get; } = 1;

        [Category("ARGUMENT")] public double ThresholdHigh { set; get; } = 2;


        [Category("ARGUMENT")] public int ApertureSize { set; get; } = 3;

        [Category("ARGUMENT")] public bool L2gradient { set; get; } = false;

        public override bool CanExecute()
        {
            var check = ThresholdHigh / ThresholdLow > 1 && ThresholdHigh / ThresholdLow < 5;
            return check && InPutMat != null;
        }

        public override void Execute()
        {
            OutPutMat = new Mat();
            Cv2.Canny(InPutMat, OutPutMat, ThresholdLow, ThresholdHigh, ApertureSize, L2gradient);
        }
    }
}
