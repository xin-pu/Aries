﻿using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Property")]
    public class Shape : MatExport<Size>
    {
        public override bool CanExecute()
        {
            return MatIn != null;
        }

        public override void Execute()
        {
            Result = MatIn.Size();
        }

    }

}