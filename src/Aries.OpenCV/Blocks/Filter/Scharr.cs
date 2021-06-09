﻿using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Filter")]
    public class Scharr : MatProcessingBlock
    {
        [Category("Enter")] public MatType MatType { set; get; }

        [Category("Enter")] public int XOrder { set; get; } = 0;

        [Category("Enter")] public int YOrder { set; get; } = 0;

        [Category("Enter")] public int KSize { set; get; } = 3;

        [Category("Enter")] public double Scale { set; get; } = 1;

        [Category("Enter")] public double Delta { set; get; } = 0;

        [Category("Enter")] public BorderTypes BorderType { set; get; } = BorderTypes.Default;

        public override bool CanExecute()
        {
            var resCheck = XOrder <= 2 && XOrder >= 0;
            resCheck = resCheck && YOrder <= 2 && YOrder >= 0;
            return resCheck && InPutMat != null;
        }

        public override void Execute()
        {
            OutPutMat = new Mat();
            Cv2.Scharr(InPutMat, OutPutMat, MatType, XOrder, YOrder, Scale, Delta, BorderType);
        }
    }
}
