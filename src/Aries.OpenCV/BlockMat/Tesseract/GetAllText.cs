using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using Tesseract;

namespace Aries.OpenCV.BlockMat.Tesseract
{
    [Category("Tesseract")]
    public class GetAllText : MatExport<string>
    {
        public override bool CanCall()
        {
            return MatIn != null;
        }

        public override void Call()
        {
            var engine = new TesseractEngine("tessdata", "eng", EngineMode.Default);
            using (var img = Pix.LoadFromMemory(MatIn.ToBytes()))
            using (var page = engine.Process(img))
            {
                Result = page.GetText().Replace("\n", "");

            }
        }
    }
}
