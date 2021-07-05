﻿using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Arithmetic")]
    public class InRange : MatProcess
    {

        [Category("DATAIN")] public Scalar Lowerb { set; get; }

        [Category("DATAIN")] public Scalar Upperb { set; get; }


        public override bool CanExecute()
        {
            return MatIn != null;
        }

        public override void Execute()
        {
            MatOut = new Mat();
            Cv2.InRange(MatIn, Lowerb, Upperb, MatOut);
        }
    }
}