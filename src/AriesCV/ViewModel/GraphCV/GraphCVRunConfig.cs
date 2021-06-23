using GalaSoft.MvvmLight;

namespace AriesCV.ViewModel
{
    public class GraphCVRunConfig : ViewModelBase
    {
        private bool _autoSaveOutMat = true;

        public bool AutoSaveOutMat
        {
            get { return _autoSaveOutMat; }
            set
            {
                _autoSaveOutMat = value;
                RaisePropertyChanged(() => AutoSaveOutMat);
            }
        }
    }
}
