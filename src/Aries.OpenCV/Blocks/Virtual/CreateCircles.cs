using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Virtual")]
    public class CreateCircles : ImportBlock<CircleSegment[]>
    {

        [Category("ARGUMENT")] public string List_Circles { set; get; }

        private List<double[]> circleArray { set; get; }

        public override bool CanExecute()
        {
            var point = List_Circles
                .Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            circleArray = point
                .Select(a => a.Split(',').Select(double.Parse).ToArray())
                .ToList();

            return circleArray.All(a => a.Length == 3);
        }

        public override void Execute()
        {
            OutPut = circleArray
                .Select(a => new CircleSegment(new Point(a[0], a[1]), (int) a[2]))
                .ToArray();
        }
    }
}
