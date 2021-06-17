using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace AriesCV.ViewModel
{
    public class CVWorkerContainerModel : ViewModelBase
    {
        public GraphCVArea _graphCvAreaWorking;
        public CVWorkerItemModel _cvWorkerItem;
        public ObservableCollection<CVWorkerItemModel> _CvWorkerItems=new ObservableCollection<CVWorkerItemModel>();

        public GraphCVArea GraphCvAreaWorking
        {
            get { return _graphCvAreaWorking; }
            set
            {
                _graphCvAreaWorking = value;
                RaisePropertyChanged(() => GraphCvAreaWorking);
            }
        }

        public CVWorkerItemModel CVWorkerItem
        {
            get { return _cvWorkerItem; }
            set
            {
                _cvWorkerItem = value;
                RaisePropertyChanged(() => CVWorkerItem);
            }
        }

        public ObservableCollection<CVWorkerItemModel> CVWorkerItems
        {
            get { return _CvWorkerItems; }
            set
            {
                _CvWorkerItems = value;
                RaisePropertyChanged(() => CVWorkerItems);
            }
        }




    }
}
