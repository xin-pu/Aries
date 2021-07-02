using System;
using System.ComponentModel;

namespace Aries.OpenCV.GraphModel
{
    [Serializable]
    public abstract class VertexMats  : VertexBasic
    {
        private bool _enableSaveMat = true;
        private bool _showImage = true;



        [Category("CHOICE")]
        public bool EnableSaveMat
        {
            get { return _enableSaveMat; }
            set
            {
                _enableSaveMat = value;
                RaisePropertyChanged(() => EnableSaveMat);
            }
        }

        [Category("CHOICE")]
        public bool ShowImage
        {
            get { return _showImage; }
            set
            {
                _showImage = value;
                RaisePropertyChanged(() => ShowImage);
            }
        }


        protected VertexMats()
        {
            Initial();
        }

        private void Initial()
        {
            Name = GetType().Name;
            CVCategory = BlockHelper.GetCvCategory(GetType());
            Icon = BlockHelper.GetBlockICon(CVCategory);
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
              
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
