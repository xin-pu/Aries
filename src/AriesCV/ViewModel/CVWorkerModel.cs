using AriesCV.Controls;
using AriesCV.Views;
using GalaSoft.MvvmLight;

namespace AriesCV.ViewModel
{
    public class CVWorkerModel : ViewModelBase
    {
        public GraphCVArea _graphCvAreaAtWorkSpace;
        public CVWorkerItemView _cvWorkerItem;


        public GraphCVArea GraphCvAreaAtWorkSpace
        {
            get { return _graphCvAreaAtWorkSpace; }
            set
            {
                _graphCvAreaAtWorkSpace = value;
                RaisePropertyChanged(() => GraphCvAreaAtWorkSpace);
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
