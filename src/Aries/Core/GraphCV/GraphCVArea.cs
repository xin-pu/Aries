using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
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
            var vertex = new VertexControl(blockVertex);

            AddVertexAndData(blockVertex, vertex);

            //we have to check if there is only one vertex and set coordinates manulay 
            //because layout algorithms skip all logic if there are less than two vertices
            if (VertexList.Count == 1)
            {
                vertex.SetPosition(0, 0);
                UpdateLayout(); //update layout to update vertex size
            }
            else RelayoutGraph(true);

            AddInput(vertex);
            AddOutPut(vertex);
            vertex.SetConnectionPointsVisibility(true);

        }

        public BlockVertex SelectBlockVertex
        {
            set { UpdateProperty(ref _selectBlockVertex, value); }
            get { return _selectBlockVertex; }
        }

        private void AddInput(VertexControl parentControl)
        {
            var newId = parentControl.VertexConnectionPointsList.Count == 0
                ? 1
                : parentControl.VertexConnectionPointsList.Max(a => a.Id) + 1;

            var input = new StaticVertexConnectionPoint
            {
                Id = newId,
                Header = "Test1"
            };
            var inputBorder = new Border
            {
                ToolTip = "Test1",
                Margin = new Thickness(2),
                Padding = new Thickness(0),
                Child = input
            };

            parentControl.BlockInput.Children.Add(inputBorder);
        }
        private void AddOutPut(VertexControl parentControl)
        {
            var newId = parentControl.VertexConnectionPointsList.Count == 0
                ? 1
                : parentControl.VertexConnectionPointsList.Max(a => a.Id) + 1;

            var input = new StaticVertexConnectionPoint
            {
                Id = newId,
                Header = "Test1"
            };
            var inputBorder = new Border
            {
                ToolTip = "Test1",
                Margin = new Thickness(2),
                Padding = new Thickness(0),
                Child = input
            };

            parentControl.BlockOutput.Children.Add(inputBorder);
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
