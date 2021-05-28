using System;
using System.ComponentModel;
using Aries.OpenCV.Interface;
using OpenCvSharp;

namespace Aries.OpenCV.GraphModel
{
    public abstract class ProcessingBlock : BlockVertex, ISaveBlock
    {
        protected ProcessingBlock()
        {
            BlockType = BlockType.Processing;
        }

        [Category("IN_MAT")] public Mat InPutMat { set; get; }

        [Category("OUT_MAT")] public Mat OutPutMat { set; get; }

        public bool EnableSaveBlock { get; set; } = true;
        public string SaveBlockName { set; get; }


        public virtual void SaveBlock()
        {
            SaveBlockName = $"{Name}_{DateTime.Now:yyyy-MM-dd-hh-mm-ss}.jpg";
            OutPutMat?.SaveImage(SaveBlockName);
        }

        public override void Reload()
        {
            InPutMat = null;
            OutPutMat = null;
            Status = BlockStatus.ToRun;
        }
    }

}
