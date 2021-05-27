using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Aries.OpenCV.Core;
using GraphX.Common.Models;

namespace Aries.OpenCV.GraphModel
{
    [Serializable]
    public abstract class BlockVertex : VertexBase, INotifyPropertyChanged
    {
        public BlockType BlockType { set; get; }

        public string Name { set; get; }
        public string InstName { set; get; }

        public string Icon { set; get; }

        public BlockVertex()
        {
            Icon = getICon();
        }

        private string getICon()
        {
            return BlockHelper.GetBlockICon(GetType());
        }

        public abstract bool CanExecute();
        public abstract void Execute();

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
