using System.ComponentModel;
using System.Linq;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Filter")]
    public class ChoiceCircleFeature : MatProcess
    {
        [Category("ARGUMENT")] public float MinCircularity { set; get; } = 0.7f;
        [Category("ARGUMENT")] public float MaxCircularity { set; get; } = float.MaxValue;

        [Category("ARGUMENT")] public double Diameter { set; get; }
        [Category("ARGUMENT")] public double Tolerance { set; get; }
        [Category("ARGUMENT")] public byte BlobColor { set; get; } = 255;

        public override bool CanCall()
        {
            return MatIn != null;
        }

        public override void Call()
        {
            /// Step 3 Set Cumstom Filter
            var paras = new SimpleBlobDetector.Params
            {
                FilterByCircularity = true,
                MinCircularity = MinCircularity,
                MaxCircularity = MaxCircularity,

                FilterByColor = true,
                BlobColor = BlobColor
            };
            var simpleBlob = SimpleBlobDetector.Create(paras);

            var key = simpleBlob.Detect(MatIn.Clone());

            key = key
                .Where(a => a.Size > Diameter - Tolerance &&
                            a.Size < Diameter + Tolerance)
                .ToArray();

            MatOut = MatIn.Clone();
            var color = 255 - BlobColor;
            foreach (var keyPoint in key)
            {
                var p = new Point((int) keyPoint.Pt.X, (int) keyPoint.Pt.Y);
                MatOut.Line(p + new Point(-50, 0), p + new Point(50, 0), color);
                MatOut.Line(p + new Point(0, 50), p + new Point(0, -50), color);
                MatOut.PutText($"{p.X:D}", p + new Point(100, 0), HersheyFonts.HersheyPlain, 2, color);
                MatOut.PutText($"{p.Y:D}", p + new Point(100, 30), HersheyFonts.HersheyPlain, 2, color);
            }
        }
    }
}