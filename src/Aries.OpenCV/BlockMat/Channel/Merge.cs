﻿using System.ComponentModel;
using System.Linq;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Channel")]
    public class Merge : VertexMat
    {
        [Category("DATAIN")] public Mat MatR { set; get; }
        [Category("DATAIN")] public Mat MatG { set; get; }
        [Category("DATAIN")] public Mat MatB { set; get; }

        [Category("DATAOUT")] public Mat MatOut { set; get; }


        public override bool CanCall()
        {
            var mats = new[] {MatR, MatG, MatB};
            var matsNotNull = mats.Where(a => a != null).ToList();
            if (matsNotNull.Count == 0)
                return false;

            var targetSize = matsNotNull.First().Size();

            return matsNotNull.All(a => a.Size() == targetSize);
        }

        public override void Call()
        {
            MatOut = new Mat();
            var mats = new[] {MatB, MatG, MatR};
            var matsNotNull = mats.Where(a => a != null).ToArray();
            Cv2.Merge(matsNotNull, MatOut);
        }
    }
}
