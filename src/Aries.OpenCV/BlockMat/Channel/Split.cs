﻿using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Channel")]
    public class Split : VertexMat
    {

        [Category("DATAIN")] public Mat MatIn { set; get; }

        [Category("DATAOUT")] public Mat MatR { set; get; }
        [Category("DATAOUT")] public Mat MatG { set; get; }
        [Category("DATAOUT")] public Mat MatB { set; get; }



        public override bool CanCall()
        {
            return MatIn != null && MatIn.Channels() >= 3;
        }

        public override void Call()
        {
            var res = MatIn.Split();
            MatB = res[0];
            MatG = res[1];
            MatR = res[2];
        }
    }
}
