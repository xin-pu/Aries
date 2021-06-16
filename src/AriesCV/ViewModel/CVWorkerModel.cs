using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace AriesCV.ViewModel
{
    public class CVWorkerModel : ViewModelBase
    {
        public ObservableCollection<CVWorkItemModel> _graphCVWorkItems = new ObservableCollection<CVWorkItemModel>();
        public CVWorkItemModel _graphCVSelected;


        public ObservableCollection<CVWorkItemModel> GraphCVWorkItems
        {
            get { return _graphCVWorkItems; }
            set
            {
                _graphCVWorkItems = value;
                RaisePropertyChanged(() => GraphCVWorkItems);
            }
        }


        public CVWorkItemModel GraphCVSelected
        {
            get { return _graphCVSelected; }
            set
            {
                _graphCVSelected = value;
                RaisePropertyChanged(() => GraphCVSelected);
            }
        }

    }
}
