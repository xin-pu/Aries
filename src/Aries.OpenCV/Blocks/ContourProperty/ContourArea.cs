﻿using System.ComponentModel;
using System.Linq;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("ContourProperty")]
    public class ContourArea : ContoursExport<double[]>
    {
        [Category("ARGUMENT")] public bool Oriented { set; get; }

        public override bool CanExecute()
        {
            return ConsIn != null && ConsIn.Length > 1;
        }

        public override void Execute()
        {
            Result = ConsIn.Select(c => Cv2.ContourArea(c, Oriented)).ToArray();
        }
    }
}
