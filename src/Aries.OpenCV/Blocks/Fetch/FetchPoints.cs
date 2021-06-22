using System.Collections.Generic;
using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks.Fetch
{
    [Category("Fetch")]
    public class FetchPoints : MatExport<Point[]>
    {

        [Category("ARGUMENT")] public double Threshold { set; get; } = 0;

        public override void Reload()
        {
            MatIn = null;
            Result = null;
            Status = BlockStatus.ToRun;
        }

        public override bool CanExecute()
        {
            return MatIn != null;
        }

        public override void Execute()
        {
            var points = new List<Point>();
            for (int i = 0; i < MatIn.Height; i++)
            {
                for (int j = 0; j < MatIn.Width; j++)
                {
                    if (MatIn.Get<byte>(i, j) > Threshold)
                    {
                        points.Add(new Point(i, j));
                    }

                }
            }

            Result = points.ToArray();
        }
    }
}
