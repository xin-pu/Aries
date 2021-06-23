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

        }

        public BlockEdge()
            : base(null, null)
        {

        }

        private string _header;

        public string Header
        {
            set { UpdateProperty(ref _header, value); }
            get { return _header; }
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
