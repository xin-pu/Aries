using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using GraphX.Common;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Draw")]
    public class Rectangle : MatProcessingBlock
    {

        [Category("DATAIN")] public Rect[] Rects { set; get; }

        [Category("ARGUMENT")] public double Color { set; get; } = 255;

        [Category("ARGUMENT")] public int Thickness { set; get; } = 1;

        [Category("ARGUMENT")] public LineTypes LineType { set; get; } = LineTypes.Link8;

        [Category("ARGUMENT")] public int Shift { set; get; } = 0;


        public override bool CanExecute()
        {
            return InPutMat != null && Rects != null && Rects.Length > 0;
        }

        public override void Execute()
        {
            OutPutMat = new Mat();
            InPutMat.CopyTo(OutPutMat);
            Rects.ForEach(rect =>
            {
                Cv2.Rectangle(OutPutMat, rect, new Scalar(Color), Thickness, LineType, Shift);
            });
        }
    }
}
