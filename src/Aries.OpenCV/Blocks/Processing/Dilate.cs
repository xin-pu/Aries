﻿using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks.Processing
{
    [Category("Morphology")]
    public class Dilate : ProcessingBlock
    {

        [Category("IN_MAT")] public InputArray Element { set; get; }

        public override bool CanExecute()
        {
            return InPutMat != null && Element != null;
        }

        public override void Execute()
        {
            OutPutMat = new Mat();
            Cv2.Dilate(InPutMat, OutPutMat, Element);
        }
    }



}