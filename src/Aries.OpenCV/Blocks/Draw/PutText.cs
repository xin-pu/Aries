using System.ComponentModel;
using System.Linq;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Draw")]
    public class PutText : MatProcessingBlock
    {
        [Category("INPUT")] public TextSegment[] Texts { set; get; }
        [Category("ARGUMENT")] public double Color { set; get; } = 255;

        [Category("ARGUMENT")] public int Thickness { set; get; } = 1;

        [Category("ARGUMENT")] public LineTypes LineType { set; get; } = LineTypes.Link8;

        [Category("ARGUMENT")] public HersheyFonts HersheyFont { set; get; } = HersheyFonts.HersheySimplex;
        [Category("ARGUMENT")] public double FontScale { set; get; } = 11;

        [Category("ARGUMENT")] public bool BottomLeftOrign { set; get; } = false;

        public override bool CanExecute()
        {
            return InPutMat != null && Texts != null && Texts.Length > 0;
        }

        public override void Execute()
        {
            OutPutMat = new Mat();
            InPutMat.CopyTo(OutPutMat);
            Texts.ToList().ForEach(text =>
            {
                Cv2.PutText(OutPutMat, text.Text, text.Point, HersheyFont, FontScale, new Scalar(Color), Thickness,
                    LineType, BottomLeftOrign);
            });
        }
    }
}
