using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Hist")]
    public class Hist : MatProcess
    {
   


        [Category("DATAIN")] public InputArray Mask { set; get; } = null;

      


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
            return MatIn != null;
        }


        public override void Execute()
        {
            MatOut = new Mat();
            Cv2.CalcHist(new[] {MatIn}, Channels,
                Mask,
                MatOut,
                Dims,
                new[] {HistSize},
                new[] {Ranges},
                Uniform,
                Accumulate);
        }
    }
}
