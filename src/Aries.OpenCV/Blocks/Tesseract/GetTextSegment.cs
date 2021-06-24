using System.Collections.Generic;
using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using GraphX.Common;
using OpenCvSharp;
using Tesseract;
using Rect = OpenCvSharp.Rect;

namespace Aries.OpenCV.Blocks.Tesseract
{

    [Category("Tesseract")]
    public class GetTextSegment : MatExport<TextSegment[]>
    {

        [Category("DATAIN")] public Rect[] Rects { set; get; }

        public override void Reload()
        {
            Rects = null;
            base.Reload();
        }

        public override bool CanExecute()
        {
            return MatIn != null && Rects != null && Rects.Length > 0;
        }

        public override void Execute()
        {
            var resString = new List<TextSegment>();
            var engine = new TesseractEngine("tessdata", "eng", EngineMode.Default);
            Rects.ForEach(rect =>
            {
                var mat = new Mat();
                Cv2.GetRectSubPix(MatIn, rect.Size,
                    new Point2f(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2),
                    mat);
                using (var img = Pix.LoadFromMemory(mat.ToBytes()))
                using (var page = engine.Process(img))
                {
                    var text = page.GetText().Replace("\n", "");
                    resString.Add(new TextSegment(text,rect.Left,rect.Top));
                }
            });


            Result = resString.ToArray();

        }
    }
}
