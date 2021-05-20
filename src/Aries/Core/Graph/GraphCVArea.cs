using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Aries.OpenCV.GraphModel;
using GraphX.Controls;
using QuickGraph;

namespace Aries.Core
{
    public class GraphCVArea :
        GraphArea<BlockVertex, BlockEdge, BidirectionalGraph<BlockVertex, BlockEdge>>, INotifyPropertyChanged
    {

        private BlockVertex _selectBlockVertex;

        public void AddBlock(BlockVertex blockVertex)
        {
            AddVertexAndData(blockVertex, new VertexControl(blockVertex));

            //we have to check if there is only one vertex and set coordinates manulay 
            //because layout algorithms skip all logic if there are less than two vertices
            if (VertexList.Count == 1)
            {
                VertexList.First().Value.SetPosition(0, 0);
                UpdateLayout(); //update layout to update vertex size
            }
            else RelayoutGraph(true);
        }

        public BlockVertex SelectBlockVertex
        {
            set { UpdateProperty(ref _selectBlockVertex, value); }
            get { return _selectBlockVertex; }
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
