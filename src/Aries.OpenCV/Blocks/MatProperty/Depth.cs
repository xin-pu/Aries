﻿using System.ComponentModel;
using Aries.OpenCV.GraphModel;

namespace Aries.OpenCV.Blocks
{
    [Category("Property")]
    public class Depth : MatExport<int>
    {
        public override bool CanExecute()
        {
            return MatIn != null;
        }

        public override void Execute()
        {
            Result = MatIn.Depth();
        }

    }
}
