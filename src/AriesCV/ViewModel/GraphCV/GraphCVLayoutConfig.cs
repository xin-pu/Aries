using AriesCV.ViewModel.GraphLayout;
using GalaSoft.MvvmLight;

namespace AriesCV.ViewModel
{
    public class GraphCVLayoutConfig : ViewModelBase
    {
        private bool _isShowEdgeLabels = false;
        private bool _isAlignEdgeLabels = true;
        public LayoutType layoutType = LayoutType.TreeTopTpBottom;
        public EdgeRoutingType _edgeRoutingType = EdgeRoutingType.SimpleER;


        public LayoutType LayoutType
        {
            get { return layoutType; }
            set
            {
                layoutType = value;
                RaisePropertyChanged(() => LayoutType);
            }
        }

        public EdgeRoutingType EdgeRoutingType
        {
            get { return _edgeRoutingType; }
            set
            {
                _edgeRoutingType = value;
                RaisePropertyChanged(() => EdgeRoutingType);
            }
        }


        public bool IsShowEdgeLabels
        {
            get { return _isShowEdgeLabels; }
            set
            {
                _isShowEdgeLabels = value;
                RaisePropertyChanged(() => IsShowEdgeLabels);
            }
        }

        public bool IsAlignEdgeLabels
        {
            get { return _isAlignEdgeLabels; }
            set
            {
                _isAlignEdgeLabels = value;
                RaisePropertyChanged(() => IsAlignEdgeLabels);
            }
        }
    }
}