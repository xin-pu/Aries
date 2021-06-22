﻿using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Blur")]
    public class Filter2D : MatProcessingBlock
    {
        [Category("DATAIN")] public InputArray Element { set; get; }

        [Category("ARGUMENT")] public MatType MatType { set; get; } = MatType.CV_32F;

        [Category("ARGUMENT")] public Point AnchorPoint { set; get; } = new Point(-1, -1);

        [Category("ARGUMENT")] public double Delta { set; get; } = 0;

        [Category("ARGUMENT")] public BorderTypes BorderTypes { set; get; } = BorderTypes.Default;



        public override bool CanExecute()
        {
            return MatIn != null && Element != null;
        }

        public override void Execute()
        {
            MatOut = new Mat();
            Cv2.Filter2D(MatIn, MatOut, MatType, Element, AnchorPoint, Delta, BorderTypes);
        }
    }
}
