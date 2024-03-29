﻿using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Draw")]
    public class CopyMakeBorder : MatProcess
    {


        [Category("ARGUMENT")] public int Top { set; get; } = 0;
        [Category("ARGUMENT")] public int Bottom { set; get; } = 0;
        [Category("ARGUMENT")] public int Left { set; get; } = 0;
        [Category("ARGUMENT")] public int Right { set; get; } = 0;
        [Category("ARGUMENT")] public BorderTypes BorderType { set; get; } = BorderTypes.Default;
        [Category("ARGUMENT")] public Scalar Scalar { set; get; }

        public override bool CanCall()
        {
            return MatIn != null;
        }

        public override void Call()
        {
            MatOut = new Mat();
            Cv2.CopyMakeBorder(MatIn, MatOut, Top, Bottom, Left, Right, BorderType, Scalar);
        }
    }
}
