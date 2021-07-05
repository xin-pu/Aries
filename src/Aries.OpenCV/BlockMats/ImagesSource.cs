using System.ComponentModel;
using System.IO;
using System.Linq;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMats
{
    [Category("Import")]
    public class ImagesSource : VertexMats
    {

        private FileInfo[] _matFilesOut;

        [Category("ARGUMENT")] public string Folder { set; get; }
        [Category("ARGUMENT")] public string ImageFilter { set; get; }
        [Category("ARGUMENT")] public ImreadModes ImreadModes { set; get; } = ImreadModes.Grayscale;

        [Category("DATAOUT")]
        public FileInfo[] MatFilesOut
        {
            get { return _matFilesOut; }
            set
            {
                _matFilesOut = value;
                RaisePropertyChanged(() => MatFilesOut);
            }
        }

        public override bool CanCall()
        {
            return Directory.Exists(Folder);
        }

        public override void Call()
        {
            var files = new DirectoryInfo(Folder).GetFiles();
            var imageFiles = ImageFilter == null
                ? files
                : files.Where(a => a.FullName.ToUpper().Contains(ImageFilter.ToUpper()))
                    .ToArray();
            if (imageFiles.Length > 0)
                MatFilesOut = imageFiles;

        }
    }
}
