using System.IO;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks.Import
{
    class ImageRead : ImportBlock
    {

        public string FileName { set; get; }
        public ImreadModes ImreadModes { set; get; }

        public ImageRead()
        {
            ImreadModes = ImreadModes.Color;
        }

        public override bool CanImport()
        {
            return File.Exists(FileName);
        }

        public override void Import()
        {
            OutPutMat = Cv2.ImRead(FileName);
        }
    }
}
