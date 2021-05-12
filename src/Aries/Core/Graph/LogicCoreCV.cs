using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Aries.Annotations;
using Aries.OpenCV.GraphModel;
using GraphX.Logic.Models;
using QuickGraph;

namespace Aries.Core
{
    public class LogicCoreCV :
        GXLogicCore<BlockVertex, BlockEdge, BidirectionalGraph<BlockVertex, BlockEdge>>,INotifyPropertyChanged
    {


        public LogicCoreCV()
        {
            WaterMaskManager = new WaterMaskManager();
            BackGroundManager = new BackGroundManager();
            Name = $"Default";
            CreateTime=DateTime.Now;
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