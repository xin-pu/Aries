using System.ComponentModel;
using System.Runtime.CompilerServices;
using Aries.OpenCV.Core;
using GraphX.Common.Enums;

namespace Aries.Core
{
    public class GraphLayoutManager : INotifyPropertyChanged
    {

        public LayoutType _layoutType = LayoutType.TreeLeftToRight;
        public EdgeRoutingAlgorithmTypeEnum _edgeRoutingType = 
            EdgeRoutingAlgorithmTypeEnum.SimpleER;

        public bool _isShowEdgeLabels = false;
        public bool _isAlignEdgeLabels = true;

        public LayoutType LayoutType
        {
            set { UpdateProperty(ref _layoutType, value); }
            get { return _layoutType; }
        }

        public EdgeRoutingAlgorithmTypeEnum EdgeRoutingType
        {
            set { UpdateProperty(ref _edgeRoutingType, value); }
            get { return _edgeRoutingType; }
        }

        public bool IsShowEdgeLabels
        {
            set { UpdateProperty(ref _isShowEdgeLabels, value); }
            get { return _isShowEdgeLabels; }
        }

        public bool IsAlignEdgeLabels
        {
            set { UpdateProperty(ref _isAlignEdgeLabels, value); }
            get { return _isAlignEdgeLabels; }
        }


        #region

        internal void UpdateProperty<T>(ref T properValue, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (Equals(properValue, newValue))
            {
                return;
            }

            properValue = newValue;

            OnPropertyChanged(propertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
