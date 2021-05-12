using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Aries.OpenCV.GraphModel;
using GraphX.Logic.Models;
using QuickGraph;

namespace Aries.Core
{
    [Serializable]
    public class LogicCoreCV :
        GXLogicCore<BlockVertex, BlockEdge, BidirectionalGraph<BlockVertex, BlockEdge>>,INotifyPropertyChanged
    {

        public LogicCoreCV(string name)
        {
            WaterMaskManager = new WaterMaskManager();
            BackGroundManager = new BackGroundManager();
            Name = name;
            CreateTime=DateTime.Now;
            LastUpdateTime=DateTime.Now;
        }

        public WaterMaskManager WaterMaskManager { set; get; }
        public BackGroundManager BackGroundManager { set; get; }

        public string Name { set; get; }
        public DateTime CreateTime { set; get; }
        public DateTime LastUpdateTime { set; get; }


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