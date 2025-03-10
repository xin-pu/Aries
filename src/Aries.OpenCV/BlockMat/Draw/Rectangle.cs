﻿using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using GraphX.Common;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Draw")]
    public class Rectangle : MatProcess
    {

        [Category("DATAIN")] public Rect[] Rects { set; get; }

        [Category("ARGUMENT")] public Scalar Color { set; get; } = 255;

        [Category("ARGUMENT")] public int Thickness { set; get; } = 1;

        [Category("ARGUMENT")] public LineTypes LineType { set; get; } = LineTypes.Link8;

        [Category("ARGUMENT")] public int Shift { set; get; } = 0;


        public override bool CanCall()
        {
            return MatIn != null && Rects != null && Rects.Length > 0;
        }

        public override void Call()
        {
            MatOut = new Mat();
            MatIn.CopyTo(MatOut);
            Rects.ForEach(rect => { Cv2.Rectangle(MatOut, rect, Color, Thickness, LineType, Shift); });
        }
    }
}
