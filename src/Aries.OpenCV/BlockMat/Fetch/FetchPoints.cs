using System.Collections.Generic;
using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat.Fetch
{
    [Category("Fetch")]
    public class FetchPoints : MatExport<Point[]>
    {

        [Category("ARGUMENT")] public double Threshold { set; get; } = 0;


        public override bool CanCall()
        {
            return MatIn != null;
        }

        public override void Call()
        {
            var points = new List<Point>();
            for (var i = 0; i < MatIn.Height; i++)
            {
                for (var j = 0; j < MatIn.Width; j++)
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
