using GalaSoft.MvvmLight;
using GraphX.Controls;

namespace AriesCV.ViewModel
{
    public class CVWorkerItemModel : ViewModelBase
    {


        public CVWorkerItemModel(string name)
        {
            Name = name;
            //ZomControl = new ZoomControl();
            //GraphCvArea = new GraphCVArea();
            //ZomControl.Content = GraphCvArea;
        }

        public string Name { set; get; }

        public GraphCVConfig GraphCvConfig { set; get; } = new GraphCVConfig();

    }
}
