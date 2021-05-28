using System.ComponentModel;
using System.IO;
using Aries.OpenCV.GraphModel;
using Microsoft.Win32;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks.Import
{
    public class ImageRead : ImportBlock
    {

        [Category("Enter")] public string FileName { protected set; get; }

        [Category("Enter")] public ImreadModes ImreadModes { set; get; }

        public ImageRead()
        {
            Name = "ImageRead";
            ImreadModes = ImreadModes.Color;
            Icon = "&#xef71;";
        }


        public override bool CanExecute()
        {
            return true;
        }

        public override void Execute()
        {
            var openFileDailog = new OpenFileDialog();
            openFileDailog.ShowDialog();
            FileName = openFileDailog.FileName;
            OutPutMat = Cv2.ImRead(FileName);
        }
    }
}
