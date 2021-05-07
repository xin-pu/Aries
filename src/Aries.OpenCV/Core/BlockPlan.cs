using System;
using System.Collections.Generic;
using Aries.OpenCV.Blocks;

namespace Aries.OpenCV.Core
{
    [Serializable]
    public class BlockPlan : ICloneable
    {
        public string PlanName { set; get; }
        public DateTime CreateTime { set; get; }
        public DateTime LastUpdateTime { set; get; }
        public IList<Block> Blocks { set; get; }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"{PlanName}\t{CreateTime}\t{LastUpdateTime}";
        }
    }
}
