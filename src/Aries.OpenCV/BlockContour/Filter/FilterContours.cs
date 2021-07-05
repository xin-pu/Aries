using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockContour
{
    [Category("Filter")]
    public class FilterContours : ContoursProcess
    {

        [Category("DATAOUT")] public Rect[] Rects { set; get; }

        [Category("ARGUMENT")] public Size MinSize { set; get; } = new Size(1, 1);
        [Category("ARGUMENT")] public Size MaxSize { set; get; } = new Size(1, 1);

        [Category("ARGUMENT")] public int LengthWidthRatio { set; get; } = 1;



        public override bool CanCall()
        {
            return ConsIn != null && ConsIn.Length > 0;
        }

        public override void Call()
        {
            ConsOut = new Mat[0];
            var mats = new List<Mat>();
            var boxes = new List<Rect>();
            ConsIn.ToList().ForEach(con =>
            {
                var box = Cv2.BoundingRect(con);
                var boxSize = box.Size;
                var condition = box.Size.Width / box.Size.Height > LengthWidthRatio;
                condition = condition && boxSize.Height > MinSize.Height && boxSize.Height < MaxSize.Height;
                condition = condition && boxSize.Width > MinSize.Width && boxSize.Width < MaxSize.Width;

                if (condition)
                {
                    boxes.Add(box);
                    mats.Add(con);
                }

            });
   

            Rects = boxes.ToArray();
            ConsOut = mats.ToArray();
        }
    }
}
