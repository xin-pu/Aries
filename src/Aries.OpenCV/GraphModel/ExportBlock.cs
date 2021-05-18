﻿using System.Xml.Serialization;
using OpenCvSharp;

namespace Aries.OpenCV.GraphModel
{

    public abstract class ExportBlock<T> : BlockVertex
    {
        protected ExportBlock()
        {
            BlockType = BlockType.Export;
        }

        [XmlIgnore]
        public Mat InputMat { set; get; }
        [XmlIgnore]
        public T ExportResult { set; get; }

        public abstract void Execute();
    }
}
