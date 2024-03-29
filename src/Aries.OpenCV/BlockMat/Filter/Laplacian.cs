﻿using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Filter")]
    public class Laplacian : MatProcess
    {
        [Category("ARGUMENT")] public MatType MatType { set; get; }

        [Category("ARGUMENT")] public int KSize { set; get; } = 1;

        [Category("ARGUMENT")] public double Scale { set; get; } = 1;

        [Category("ARGUMENT")] public double Delta { set; get; } = 0;

        [Category("ARGUMENT")] public BorderTypes BorderType { set; get; } = BorderTypes.Default;

        public override bool CanCall()
        {
            return MatIn != null;
        }

        public override void Call()
        {
            MatOut = new Mat();
            Cv2.Laplacian(MatIn, MatOut, MatType, KSize, Scale, Delta, BorderType);
        }
    }
}
