using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using Microsoft.Win32;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks.Import
{

    [Category("Image")]
    public class CreateImage : ImportBlock<Mat>
    {

        [Category("Enter")] public string FileName { protected set; get; }

        [Category("Enter")] public ImreadModes ImreadModes { set; get; } = ImreadModes.Grayscale;


        public override bool CanExecute()
        {
            return true;
        }

        public override void Execute()
        {
            var openFileDailog = new OpenFileDialog();
            openFileDailog.ShowDialog();
            FileName = openFileDailog.FileName;
            OutPut = Cv2.ImRead(FileName, ImreadModes);
        }
    }
}
