﻿using System.ComponentModel;
using System.Linq;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockContour
{
    [Category("Property")]
    public class ContourPermeter : ContoursExport<double[]>
    {
        [Category("ARGUMENT")] public bool Closed { set; get; }

        public override bool CanCall()
        {
            return ConsIn != null && ConsIn.Length > 0;
        }

        public override void Call()
        {
            Result = ConsIn.Select(c => Cv2.ArcLength(c, Closed)).ToArray();
        }
    }
}
