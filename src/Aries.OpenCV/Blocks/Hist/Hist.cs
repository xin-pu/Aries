using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Hist")]
    public class Hist : MatProcessingBlock
    {
   


        [Category("INPUT")] public InputArray Mask { set; get; } = null;

      


        [Category("ARGUMENT")] public float Ranges_Min { set; get; } = 0;
        [Category("ARGUMENT")] public float Ranges_Max { set; get; } = 256;

        [Category("ARGUMENT")] public int Dims { set; get; } = 1;

        [Category("ARGUMENT")] public int HistSize { set; get; } = 256;

        [Category("ARGUMENT")] public bool Uniform { set; get; } = true;

        [Category("ARGUMENT")] public bool Accumulate { set; get; } = false;

        private int[] Channels { get; } = { 0 };

        private Rangef Ranges
        {
            get { return new Rangef(Ranges_Min, Ranges_Max); }
        }


        public override bool CanExecute()
        {
            return InPutMat != null;
        }


        public override void Execute()
        {
            OutPutMat = new Mat();
            Cv2.CalcHist(new[] {InPutMat}, Channels,
                Mask,
                OutPutMat,
                Dims,
                new[] {HistSize},
                new[] {Ranges},
                Uniform,
                Accumulate);
        }
    }
}
