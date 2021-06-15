using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace AriesCV.ViewModel
{
    public class CVWorkerModel
    {
        public ObservableCollection<ViewModelBase> Tabs { get; set; } =new ObservableCollection<ViewModelBase>();
    }
}
