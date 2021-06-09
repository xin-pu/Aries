using System;
using System.ComponentModel;

namespace Aries.OpenCV.GraphModel
{
    public abstract class ImportBlock<T> : BlockVertex
    {
       
        protected ImportBlock()
        {
            BlockType = BlockType.Import;
        }
        
        [Category("OUTPUT")] public T OutPut { set; get; }

        public Type OutPutType => typeof(T);

        public override void Reload()
        {
            OutPut = default;
            Status = BlockStatus.ToRun;
        }
    }
}
