﻿using Aries.OpenCV.GraphModel;
using OpenCvSharp;
using System.ComponentModel;
using System.Linq;

namespace Aries.OpenCV.BlockContour
{


    [Category("Property")]
    public class MinEnclosingCircle : ContoursExport<CircleSegment[]>
    {

        public override bool CanCall()
        {
            return ConsIn != null && ConsIn.Length > 0;
        }

        public override void Call()
        {
            var cirs = ConsIn.Select(con =>
            {
                Point2f point;
                float radius;
                Cv2.MinEnclosingCircle(con, out point, out radius);
                return new CircleSegment(point, radius);
            });

            Result = cirs.ToArray();
        }


    }
}
