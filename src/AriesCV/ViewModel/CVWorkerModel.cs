using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace AriesCV.ViewModel
{
    public class CVWorkerModel
    {
        public ObservableCollection<ViewModelBase> GraphCVWorkItems { get; set; } =new ObservableCollection<ViewModelBase>();
    }
}
