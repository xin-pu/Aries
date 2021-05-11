using System;
using System.Collections.Generic;

namespace Aries.Core
{
    public class BlockToolKitManager
    {


        private static readonly Lazy<BlockToolKitManager> lazy =
            new Lazy<BlockToolKitManager>(() => new BlockToolKitManager());

        public static BlockToolKitManager Instance
        {
            get { return lazy.Value; }
        }

        



    }
}
