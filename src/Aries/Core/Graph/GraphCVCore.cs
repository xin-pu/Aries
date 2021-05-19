using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using Aries.OpenCV.GraphModel;
using GraphX.Controls;

namespace Aries.Core
{
    [Serializable]
    public class GraphCVCore : INotifyPropertyChanged, IDisposable
    {
        private string _name;
        private string _fileName;
        private DateTime _createDateTime;
        private DateTime _lastUpdateTime;

        private WaterMaskManager _waterMaskManager;
        private BackGroundManager _backGroundManager;

        public GraphCVCore(string name)
        {
            WaterMaskManager = new WaterMaskManager();
            BackGroundManager = new BackGroundManager();
            Name = name;
            CreateTime = DateTime.Now;
            LastUpdateTime = DateTime.Now;
            AddBlock = AppendBlock;
        }

        public GraphCVCore()
        {
            AddBlock = AppendBlock;
        }

        [XmlIgnore] public GraphCVArea GraphCvArea { set; get; }

        [XmlIgnore] public ZoomControl ZoomControl { set; get; }


        public WaterMaskManager WaterMaskManager
        {
            set { UpdateProperty(ref _waterMaskManager, value); }
            get { return _waterMaskManager; }
        }

        public BackGroundManager BackGroundManager
        {
            set { UpdateProperty(ref _backGroundManager, value); }
            get { return _backGroundManager; }
        }

        public string Name
        {
            set { UpdateProperty(ref _name, value); }
            get { return _name; }
        }

        public string FileName
        {
            set { UpdateProperty(ref _fileName, value); }
            get { return _fileName; }
        }

        public DateTime CreateTime
        {
            set { UpdateProperty(ref _createDateTime, value); }
            get { return _createDateTime; }
        }

        public DateTime LastUpdateTime
        {
            set { UpdateProperty(ref _lastUpdateTime, value); }
            get { return _lastUpdateTime; }
        }


        public List<BlockEdge> BlockEdges { set; get; }

        public List<BlockVertex> BlockVertices { set; get; }


        [XmlIgnore] public Action<BlockVertex> AddBlock;

        public void AppendBlock(BlockVertex blockVertex)
        {
            GraphCvArea.AddVertexAndData(blockVertex, new VertexControl(blockVertex));

            //we have to check if there is only one vertex and set coordinates manulay 
            //because layout algorithms skip all logic if there are less than two vertices
            if (GraphCvArea.VertexList.Count == 1)
            {
                GraphCvArea.VertexList.First().Value.SetPosition(0, 0);
                GraphCvArea.UpdateLayout(); //update layout to update vertex size
            }
            else GraphCvArea.RelayoutGraph(true);

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

        public void Dispose()
        {
            GraphCvArea?.Dispose();
        }
    }
}
