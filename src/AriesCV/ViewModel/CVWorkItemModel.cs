using AriesCV.Controls;
using GalaSoft.MvvmLight;

namespace AriesCV.ViewModel
{
    public class CVWorkItemModel : ViewModelBase
    {

        public string Name { set; get; }

        public string FileInfo { set; get; }

        public GraphCVArea GraphCvArea { set; get; }

    }
}
