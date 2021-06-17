using Aries.OpenCV.Core;
using GalaSoft.MvvmLight;

namespace AriesCV.ViewModel
{
    public class GraphCVConfig : ViewModelBase
    {
        private bool _isShowEdgeLabels;
        private bool _isAlignEdgeLabels;
        public LayoutType _layoutAlgorithm = LayoutType.TreeTopTpBottom;
        public EdgeRoutingType _edgeRoutingType = EdgeRoutingType.SimpleER;


        public LayoutType LayoutAlgorithm
        {
            get { return _layoutAlgorithm; }
            set
            {
                _layoutAlgorithm = value;
                RaisePropertyChanged(() => LayoutAlgorithm);
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