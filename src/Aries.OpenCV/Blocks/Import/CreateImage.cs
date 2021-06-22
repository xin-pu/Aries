using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using Microsoft.Win32;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{

    [Category("Initial")]
    public class CreateImage : MatImport
    {

        [Category("ARGUMENT")] public string FileName { protected set; get; }

        [Category("ARGUMENT")] public ImreadModes ImreadModes { set; get; } = ImreadModes.Grayscale;


        public override bool CanExecute()
        {
            return true;
        }

        public override void Execute()
        {
            var openFileDailog = new OpenFileDialog
            {
                Title = $"{ID}_{Name}",
                Filter = "JPG文件|*.jpg|PNG文件|*.png"
            };
            openFileDailog.ShowDialog();
            FileName = openFileDailog.FileName;
            MatOut = Cv2.ImRead(FileName, ImreadModes);
        }
    }
}
