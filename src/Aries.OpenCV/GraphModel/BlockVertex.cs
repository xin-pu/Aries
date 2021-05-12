﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GraphX.Common.Models;

namespace Aries.OpenCV.GraphModel
{
    [Serializable]
    public abstract class BlockVertex : VertexBase, INotifyPropertyChanged
    {

        public string Name { set; get; }
        public string InstName { set; get; }

        public virtual string Icon { set; get; } = "&#xf0a1;";

        protected BlockVertex()
        {
            Name = "Hello";
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
