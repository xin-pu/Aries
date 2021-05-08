using System;
using System.Collections.Generic;
using Aries.OpenCV.Interface;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    public abstract class ProcessingBlock : Block, ISaveBlock
    {
        public Mat InputMat { set; get; }
        public Mat OutPutMat { set; get; }
        public bool EnableSaveBlock { get; set; } = true;
        public string SaveBlockName { set; get; }


        public abstract bool CanExecute();
        public abstract void Execute();

        public virtual void SaveBlock()
        {
            SaveBlockName = $"{Name}_{DateTime.Now:yyyy-MM-dd-hh-mm-ss}.jpg";
            OutPutMat?.SaveImage(SaveBlockName);
        }
    }

    public abstract class ProcessingBlockMultiInput : Block, ISaveBlock
    {
        public List<Mat> InputMats { set; get; }
        public Mat OutPutMat { set; get; }
        public bool EnableSaveBlock { get; set; } = true;
        public string SaveBlockName { set; get; }

        public abstract bool CanExecute();
        public abstract void Execute();
        public virtual void SaveBlock()
        {
            SaveBlockName = $"{Name}_{DateTime.Now:yyyy-MM-dd-hh-mm-ss}.jpg";
            OutPutMat?.SaveImage(SaveBlockName);
        }
    }


}
