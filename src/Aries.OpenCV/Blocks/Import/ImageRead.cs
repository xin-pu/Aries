﻿using System.IO;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks.Import
{
    public class ImageRead : ImportBlock
    {

        public string FileName { set; get; }
        public ImreadModes ImreadModes { set; get; }

        public ImageRead()
        {
            Name = "ImageRead";
            ImreadModes = ImreadModes.Color;
            Icon = "&#xef71;";
        }

        public override bool CanImport()
        {
            return File.Exists(FileName);
        }

        public override void Import()
        {
            OutPutMat = Cv2.ImRead(FileName);
        }
    }
}
