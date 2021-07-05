using GalaSoft.MvvmLight;

namespace Aries.OpenCV.GraphModel
{
    public class GraphCVLayoutConfig : ViewModelBase
    {
        private bool _isShowImageView = false;
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

        public bool IsShowImageView
        {
            get { return _isShowImageView; }
            set
            {
                _isShowImageView = value;
                RaisePropertyChanged(() => IsShowImageView);
            }
        }
    }
}