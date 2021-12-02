using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using OpenCvSharp;

namespace Aries.OpenCV.GraphModel
{
    [Serializable]
    public abstract class VertexMat : VertexBasic
    {
        private bool _enableSaveMat = true;
        private string _imageSource = @"\Resource\Image\Aries.jpg";
        private bool _showImage = true;


        protected VertexMat()
        {
            Initial();
        }


        [Category("INFO")]
        public string OutImage
        {
            get => _imageSource;
            set
            {
                _imageSource = value;
                RaisePropertyChanged(() => OutImage);
            }
        }


        [Category("CHOICE")]
        public bool EnableSaveMat
        {
            get => _enableSaveMat;
            set
            {
                _enableSaveMat = value;
                RaisePropertyChanged(() => EnableSaveMat);
            }
        }

        [Category("CHOICE")]
        public bool ShowImage
        {
            get => _showImage;
            set
            {
                _showImage = value;
                RaisePropertyChanged(() => ShowImage);
            }
        }


        private void Initial()
        {
            Name = GetType().Name;
            CVCategory = BlockHelper.GetCvCategory(GetType());
            Icon = BlockHelper.GetBlockICon(CVCategory, "Mat");
        }

        public override void ExecuteCommand_Execute()
        {
            base.ExecuteCommand_Execute();
            if (EnableSaveMat)
                SaveMatOut();
        }

        public virtual void SaveMatOut()
        {
            try
            {
                if (Status != BlockStatus.Complete || !EnableSaveMat)
                    return;

                if (WorkDirectory == string.Empty || !Directory.Exists(WorkDirectory))
                    return;

                var outMatDict = TypeDescriptor.GetProperties(GetType())
                    .OfType<PropertyDescriptor>()
                    .FirstOrDefault(a => a.Category == "DATAOUT" && a.Name == "MatOut");

                var mat = GetPropertyAsMat(outMatDict?.Name) as Mat;
                var imagesource = $@"{WorkDirectory}\{Name}_{ID}_{DateTime.Now:yyyyMMdd_HHmmss}.jpg";
                var saveRes = mat?.ImWrite(imagesource);
                if (saveRes == true) OutImage = imagesource;
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public override void Reload()
        {
            OutImage = string.Empty;
            base.Reload();
        }
    }
}