using System.ComponentModel;
using System.IO;
using System.Net;
using Aries.OpenCV.GraphModel;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{

    [Category("Initial")]
    public class CreateImage : MatImport
    {

        [Category("ARGUMENT")] public string FileName { protected set; get; }

        [Category("ARGUMENT")] public ImreadModes ImreadModes { set; get; } = ImreadModes.Grayscale;

        [Category("COMMAND")]
        public RelayCommand UpdataSourceCommand
        {
            get { return new RelayCommand(UpdataSourceCommand_Execute); }
        }

        private void UpdataSourceCommand_Execute()
        {
            var openFileDailog = new OpenFileDialog
            {
                Title = $"{ID}_{Name}",
                Filter = "JPG文件|*.jpg|PNG文件|*.png"
            };
            openFileDailog.ShowDialog();
            FileName = openFileDailog.FileName;
        }



        public override bool CanExecute()
        {
            return !string.IsNullOrEmpty(FileName) && File.Exists(FileName);
        }

        public override void Execute()
        {
            MatOut = Cv2.ImRead(FileName, ImreadModes);
        }
    }
}
