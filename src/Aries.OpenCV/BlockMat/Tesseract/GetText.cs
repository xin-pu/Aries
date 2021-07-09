using System.Collections.Generic;
using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using GraphX.Common;
using OpenCvSharp;
using Tesseract;
using Rect = OpenCvSharp.Rect;

namespace Aries.OpenCV.BlockMat.Tesseract
{

    [Category("Tesseract")]
    public class GetText : MatExport<string[]>
    {

        [Category("DATAIN")] public Rect[] Rects { set; get; }


        public override bool CanCall()
        {
            return MatIn != null && Rects != null && Rects.Length > 0;
        }

        public override void Call()
        {
            var resString = new List<string>();
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
                    resString.Add(page.GetText().Replace("\n", ""));
                }
            });
            

            Result = resString.ToArray();

        }
    }
}
