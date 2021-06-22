using System.Collections.Generic;
using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks.Fetch
{
    [Category("Fetch")]
    public class FetchPoints : ProcessingBlock<Mat, Point[]>
    {

        [Category("ARGUMENT")] public double Threshold { set; get; } = 0;

        public override void Reload()
        {
            InPutMat = null;
            OutPutMat = null;
            Status = BlockStatus.ToRun;
        }

        public override bool CanExecute()
        {
            return InPutMat != null;
        }

        public override void Execute()
        {
            var points = new List<Point>();
            for (int i = 0; i < InPutMat.Height; i++)
            {
                for (int j = 0; j < InPutMat.Width; j++)
                {
                    if (InPutMat.Get<byte>(i, j) > Threshold)
                    {
                        points.Add(new Point(i, j));
                    }

                }
            }

            OutPutMat = points.ToArray();
        }
    }
}
