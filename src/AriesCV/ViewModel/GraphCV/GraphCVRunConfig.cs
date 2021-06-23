using System;
using GalaSoft.MvvmLight;

namespace AriesCV.ViewModel
{
    public class GraphCVRunConfig : ViewModelBase
    {
        private bool _autoSaveOutMat = true;
        private string _workDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

        public bool AutoSaveOutMat
        {
            get { return _autoSaveOutMat; }
            set
            {
                _autoSaveOutMat = value;
                RaisePropertyChanged(() => AutoSaveOutMat);
            }
        }

        public string WorkDirectory
        {
            get { return _workDirectory; }
            set
            {
                _workDirectory = value;
                RaisePropertyChanged(() => WorkDirectory);
            }
        }
    }
}
