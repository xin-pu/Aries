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

        private string[] _matsOut;
        [Category("ARGUMENT")] public string Folder { set; get; }
        [Category("ARGUMENT")] public string ImageFilter { set; get; }
        [Category("ARGUMENT")] public ImreadModes ImreadModes { set; get; } = ImreadModes.Grayscale;

        [Category("DATAOUT")]
        public string[] MatsOut
        {
            get { return _matsOut; }
            set
            {
                _matsOut = value;
                RaisePropertyChanged(() => MatsOut);
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
                : files.Where(a => a.FullName.Contains(ImageFilter)).ToArray();
            if (imageFiles.Length > 0)
                MatsOut = imageFiles.Select(a=>a.FullName).ToArray();

        }
    }
}
