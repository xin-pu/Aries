using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Aries.OpenCV.Interface;
using OpenCvSharp;

namespace Aries.OpenCV.GraphModel
{
    public abstract class ProcessingBlock : BlockVertex, ISaveBlock
    {
       
        [XmlIgnore]
        public Mat InputMat { set; get; }
        [XmlIgnore]
        public Mat OutPutMat { set; get; }

        public BlockType BlockType = BlockType.Processing;
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

    public abstract class ProcessingBlockMultiInput : BlockVertex, ISaveBlock
    {
        [XmlIgnore]
        public List<Mat> InputMats { set; get; }
        [XmlIgnore]
        public Mat OutPutMat { set; get; }


        public BlockType BlockType = BlockType.Processing;
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
