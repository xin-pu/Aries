using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GraphX.Common.Models;

namespace Aries.OpenCV.GraphModel
{
    [Serializable]
    public class BlockEdge : EdgeBase<BlockVertex>, INotifyPropertyChanged
    {
        public BlockEdge(BlockVertex source, BlockVertex target, double weight = 1)
            : base(source, target, weight)
        {
            Angle = 90;
        }

        public BlockEdge()
            : base(null, null)
        {
            Angle = 90;
        }

        private string _header;
        private double _angle;
        private bool _arrowTarget;

        public string Header
        {
            set { UpdateProperty(ref _header, value); }
            get { return _header; }
        }

        public double Angle
        {
            set { UpdateProperty(ref _angle, value); }
            get { return _angle; }
        }

        public bool ArrowTarget
        {
            set { UpdateProperty(ref _arrowTarget, value); }
            get { return _arrowTarget; }
        }

        public override string ToString()
        {
            return $"{Header}";
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
