using System.ComponentModel;
using System.Linq;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockContour
{
    [Category("Filter")]
    public class FilterConByMR : ContoursProcess
    {
        [Category("DATAOUT")] public Rect[] Rects { set; get; }

        [Category("ARGUMENT")] public Size MinSize { set; get; } = new Size(1, 1);
        [Category("ARGUMENT")] public Size MaxSize { set; get; } = new Size(1, 1);


        public int LengthIn => ConsIn?.Length ?? 0;
        public int LentthOut => ConsOut?.Length ?? 0;

        public override bool CanCall()
        {
            return ConsIn != null && ConsIn.Length > 0;
        }

        public override void Call()
        {
            ConsOut = new Mat[0];
            var allMinRect = ConsIn.Select(a => Cv2.MinAreaRect(a));

            var filterRect = ConsIn.Zip(allMinRect, (a, b) => new {a, b})
                .Where(con =>
                {
                    var res = con.b.Size.Width > MinSize.Width && con.b.Size.Width < MaxSize.Width;
                    res = res && con.b.Size.Height > MinSize.Height && con.b.Size.Height < MaxSize.Height;
                    return res;
                })
                .ToList();


            Rects = filterRect.Select(a => a.b.BoundingRect()).ToArray();
            ConsOut = filterRect.Select(a => a.a).ToArray();
        }

        public override string ToString()
        {
            return string.Join("\r", ConsOut.Select(a => a.ToString()).ToArray());
        }
    }
}