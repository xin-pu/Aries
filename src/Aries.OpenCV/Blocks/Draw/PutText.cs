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
        [Category("Enter")] public double Color { set; get; } = 255;

        [Category("Enter")] public int Thickness { set; get; } = 1;

        [Category("Enter")] public LineTypes LineType { set; get; } = LineTypes.Link8;

        [Category("Enter")] public HersheyFonts HersheyFont { set; get; } = HersheyFonts.HersheySimplex;
        [Category("Enter")] public double FontScale { set; get; } = 11;

        [Category("Enter")] public bool BottomLeftOrign { set; get; } = false;

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
