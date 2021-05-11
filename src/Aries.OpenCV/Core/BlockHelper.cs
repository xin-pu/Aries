using System;
using System.Collections.Generic;
using Aries.OpenCV.Blocks.Export;
using Aries.OpenCV.Blocks.Import;
using Aries.OpenCV.Blocks.Processing;

namespace Aries.OpenCV.Core
{
    public class BlockHelper
    {
        public static List<Type> GetBlockType()
        {
            return new List<Type>()
            {
                typeof(Width),
                typeof(ImageRead),
                typeof(Blur),
                typeof(GaussianBlur)
            };
        }


    }
}
