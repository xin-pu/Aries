using System;
using System.ComponentModel;
using System.Linq;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Hist")]
    public class DrawHist : MatProcess
    {


        private int Column { set; get; }
        private int Row { set; get; }
        private float[,] Data { set; get; }

        [Category("ARGUMENT")] public double HistWidth { set; get; } = 512;
        [Category("ARGUMENT")] public double HistHeight { set; get; } = 512;
        [Category("ARGUMENT")] public int LineThickNess { set; get; } = -1;

        public override bool CanExecute()
        {
            if (MatIn == null)
                return false;

            if (MatIn.Type().IsInteger)
                return false;

            /// 归一化
            var outin = new Mat();
            Cv2.Normalize(MatIn, outin);

            outin.GetRectangularArray(out float[,] data);
            Data = data;

            if (Data.Rank != 2)
                return false;
            Column = Data.GetLength(0);
            Row = Data.GetLength(1);
            if (Row != 1)
                return false;

            return true;
        }

        public override void Execute()
        {

            MatOut = Mat.Zeros(new Size(HistWidth, HistHeight), MatType.CV_8UC3)
                .Add(new Scalar(255, 255, 255));

            var binWidth = Math.Round(HistWidth / Column);
            var histData = GetHistData();
            var histHeight = histData.Select(a => HistHeight * (1 - a)).ToArray();

            for (int i = 1; i < Column; i++)
            {
                //var color = RandomExt.GetRandomNumber(0, 255);
                Cv2.Rectangle(MatOut,
                    new Point(binWidth * i, HistHeight),
                    new Point(binWidth * (i - 1) - 1, histHeight[i - 1]),
                    new Scalar(0,0,0),
                    LineThickNess);
            }
        }

        private double[] GetHistData()
        {
            var binData = new double[Column];

            for (int i = 0; i < Column; i++)
            {
                binData[i] = Data[i, 0];
            }

            return binData;
        }

     }
}
