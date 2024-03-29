﻿using OpenCvSharp;
using Aries.OpenCV.GraphModel;
using System.ComponentModel;
using System.Linq;

namespace Aries.OpenCV.BlockContour
{
    [Category("Property")]
    public class ContourConvexHull : ContoursProcess
    {

        [Category("ARGUMENT")] public bool Clockwise { set; get; } = false;

        /// <summary>
        /// Return Point or Return Contour Index of Point
        /// </summary>
        [Category("ARGUMENT")]
        public bool ReturnPoints { set; get; } = true;


        public override bool CanCall()
        {
            return ConsIn != null && ConsIn.Length > 0;
        }

        public override void Call()
        {
            ConsOut = new Mat[0];
            ConsOut = ConsIn.Select(c =>
            {
                var a = new Mat();
                Cv2.ConvexHull(c, a, Clockwise, ReturnPoints);
                return a;
            }).ToArray();

        }
    }
}
