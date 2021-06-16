using AriesCV.Controls;
using AriesCV.Views;
using GalaSoft.MvvmLight;

namespace AriesCV.ViewModel
{
    public class CVWorkerModel : ViewModelBase
    {
        public GraphCVArea _graphCvAreaWorking;
        public CVWorkerItemView _cvWorkerItem;


        public GraphCVArea GraphCvAreaWorking
        {
            get { return _graphCvAreaWorking; }
            set
            {
                _graphCvAreaWorking = value;
                RaisePropertyChanged(() => GraphCvAreaWorking);
            }
        }

        public CVWorkerItemView CvWorkerItem
        {
            get { return _cvWorkerItem; }
            set
            {
                _cvWorkerItem = value;
                RaisePropertyChanged(() => CvWorkerItem);
            }
        }






    }
}
