using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Aries.OpenCV.Core;
using GraphX.Common.Models;

namespace Aries.OpenCV.GraphModel
{
    [Serializable]
    public class BlockVertex : VertexBase, INotifyPropertyChanged
    {

        public string Name { set; get; }
        public string InstName { set; get; }

        public string Icon { set; get; }

        public BlockVertex()
        {
            Name = "Hello";
            Icon = getICon();
        }

        private string getICon()
        {
            return BlockHelper.GetBlockICon(GetType());
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
