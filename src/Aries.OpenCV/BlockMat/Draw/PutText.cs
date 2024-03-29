﻿using System.ComponentModel;
using System.Linq;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Draw")]
    public class PutText : MatProcess
    {
        [Category("DATAIN")] public TextSegment[] Texts { set; get; }

        [Category("ARGUMENT")] public Scalar Color { set; get; } = 255;

        [Category("ARGUMENT")] public int Thickness { set; get; } = 1;

        [Category("ARGUMENT")] public LineTypes LineType { set; get; } = LineTypes.Link8;

        [Category("ARGUMENT")] public HersheyFonts HersheyFont { set; get; } = HersheyFonts.HersheySimplex;
       
        [Category("ARGUMENT")] public double FontScale { set; get; } = 1;

        [Category("ARGUMENT")] public bool BottomLeftOrign { set; get; } = false;



        public override bool CanCall()
        {
            return MatIn != null && Texts != null && Texts.Length > 0;
        }

        public override void Call()
        {
            MatOut = new Mat();
            MatIn.CopyTo(MatOut);
            Texts.ToList().ForEach(text =>
            {
                Cv2.PutText(MatOut, text.Text, text.Point, HersheyFont, FontScale, Color, Thickness,
                    LineType, BottomLeftOrign);
            });
        }
    }
}
