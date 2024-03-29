﻿using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{

    [Category("Convert")]
    public class Normalize : MatProcess
    {
        [Category("DATAIN")] public Mat Mask { set; get; }

        [Category("ARGUMENT")] public double Alpha { set; get; } = 255;

        [Category("ARGUMENT")] public double Beta { set; get; } = 1;

        [Category("ARGUMENT")] public NormTypes NormType { set; get; } = NormTypes.MinMax;
        [Category("ARGUMENT")] public int DType { set; get; } = -1;


        [Category("CHOICE")] public bool EnableMask { set; get; }



        public override bool CanCall()
        {
            return MatIn != null &&
                   (!EnableMask || Mask != null);
        }

        public override void Call()
        {
            MatOut = new Mat();
            Cv2.Normalize(MatIn, MatOut, Alpha, Beta, NormType, DType, Mask);
        }
    }
}
