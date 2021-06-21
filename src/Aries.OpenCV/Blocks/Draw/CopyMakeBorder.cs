﻿using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Draw")]
    public class CopyMakeBorder : MatProcessingBlock
    {
        [Category("DATAIN")] public Scalar Scalar { set; get; }

        [Category("ARGUMENT")] public int Top { set; get; } = 0;
        [Category("ARGUMENT")] public int Bottom { set; get; } = 0;
        [Category("ARGUMENT")] public int Left { set; get; } = 0;
        [Category("ARGUMENT")] public int Right { set; get; } = 0;
        [Category("ARGUMENT")] public BorderTypes BorderType { set; get; } = BorderTypes.Default;
  

        public override bool CanExecute()
        {
            return InPutMat != null;
        }

        public override void Execute()
        {
            OutPutMat = new Mat();
            Cv2.CopyMakeBorder(InPutMat, OutPutMat, Top, Bottom, Left, Right, BorderType, Scalar);
        }
    }
}
