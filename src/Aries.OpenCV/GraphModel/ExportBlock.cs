﻿using System.ComponentModel;
using OpenCvSharp;

namespace Aries.OpenCV.GraphModel
{

    public abstract class ExportBlock<T> : BlockVertex
    {
   

        [Category("DATAIN")] public Mat InPutMat { set; get; }

        [Category("DATAOUT")] public T ExportResult { set; get; }

        public override void Reload()
        {
            InPutMat = null;
            Status = BlockStatus.ToRun;
        }

    }
}
