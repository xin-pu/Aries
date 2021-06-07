﻿using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Arithmetic")]
    public abstract class ArithmeticBasic : GeneralBlock
    {
        [Category("IN_MAT")] public Mat InPut1 { set; get; }
        [Category("IN_MAT")] public Mat InPut2 { set; get; }

        [Category("OUT_MAT")] public Mat Output { set; get; }

        [Category("IN_MAT")] public Mat Mask { set; get; }

        [Category("Enter")] public bool EnableMask { set; get; }

        public override void Reload()
        {
            InPut1 = null;
            InPut2 = null;
            Mask = null;
            Output = null;
            Mask = null;
            base.Reload();
           
        }

        public override bool CanExecute()
        {
            return InPut1 != null &&
                   InPut2 != null &&
                   InPut1.Size() == InPut2.Size() &&
                   (!EnableMask || Mask != null);
        }

    }
}
