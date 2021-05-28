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
            EnableSaveBlock = true;
            BlockType = BlockType.Processing;
        }

        [Category("IN_MAT")] public Mat InPutMat { set; get; }

        [Category("OUT_MAT")] public Mat OutPutMat { set; get; }

        

        public virtual void SaveBlock()
        {
            OutPutMat?.SaveImage(SaveBlockName);
        }

        public override void Reload()
        {
            InPutMat = null;
            OutPutMat = null;
            Status = BlockStatus.ToRun;
        }

        public override void ExecuteCommand_Execute()
        {
            base.ExecuteCommand_Execute();
            if (Status == BlockStatus.Complete && EnableSaveBlock)
            {
                SaveBlockName = $"{Name}_{DateTime.Now:yyyy-MM-dd-hh-mm-ss}.jpg";
                OutPutMat.SaveImage(SaveBlockName);
            }

        }
    }

}
