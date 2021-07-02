using System.ComponentModel;
using System.IO;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{

    [Category("Initial")]
    public class ImageBySource : MatImport
    {

        [Category("ARGUMENT")] public string ImageSource { set; get; }

        [Category("ARGUMENT")] public ImreadModes ImreadModes { set; get; } = ImreadModes.Grayscale;


        public override bool CanExecute()
        {
            return ImageSource != null && File.Exists(ImageSource);
        }

        public override void Execute()
        {
            MatOut = Cv2.ImRead(ImageSource, ImreadModes);
        }
    }
}
