﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Virtual")]
    public class CreateLines : ImportBlock<LineSegmentPoint[]>
    {

        [Category("ARGUMENT")] public string List_Points { set; get; }



        private List<double[]> pointArray { set; get; }

        public override bool CanCall()
        {
            if (List_Points == null)
                return false;
            var point = List_Points
                .Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries);

            pointArray = point
                .Select(a => a.Split(',').Select(double.Parse).ToArray())
                .ToList();

            return pointArray.All(a => a.Length == 4);
        }

        public override void Call()
        {
            TOut = pointArray
                .Select(a => new LineSegmentPoint(new Point(a[0], a[1]), new Point(a[2], a[3])))
                .ToArray();
        }
    }
}
