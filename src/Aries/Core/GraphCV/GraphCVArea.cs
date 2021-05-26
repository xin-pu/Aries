using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

            //we have to check if there is only one vertex and set coordinates manually 
            //because layout algorithms skip all logic if there are less than two vertices
            if (VertexList.Count == 1)
            {
                vertex.SetPosition(0, 0);
                UpdateLayout(); //update layout to update vertex size
            }
            else RelayoutGraph(true);

            AddAllConnectionPoints(vertex, blockVertex.GetType());

            vertex.SetConnectionPointsVisibility(true);

        }

        public BlockVertex SelectBlockVertex
        {
            set { UpdateProperty(ref _selectBlockVertex, value); }
            get { return _selectBlockVertex; }
        }

        private void AddAllConnectionPoints(VertexControl parentControl, Type blockType)
        {
            var properties = TypeDescriptor.GetProperties(blockType)
                .OfType<PropertyDescriptor>()
                .ToList();


            properties.Where(a => a.Category == "Input_MAT")
                .ToList()
                .ForEach(
                    p => { AddInputConnectionPoint(parentControl, p); });

            properties.Where(a => a.Category == "Output_MAT")
                .ToList()
                .ForEach(p => { AddOutPutConnectionPoint(parentControl, p); });
        }

        private void AddInputConnectionPoint(VertexControl parentControl, PropertyDescriptor propertyDescriptor)
        {
            var newId = parentControl.VertexConnectionPointsList.Count == 0
                ? 1
                : parentControl.VertexConnectionPointsList.Max(a => a.Id) + 1;

            var input = new VertexConnectionPointIn
            {
                Id = newId,
                Header = propertyDescriptor.Name,
                ToolTip = propertyDescriptor.Name
            };
            var inputBorder = new Border
            {
                Child = input
            };

            parentControl.BlockInput.Children.Add(inputBorder);
            parentControl.VertexConnectionPointsList.Add(input);
        }

        private void AddOutPutConnectionPoint(VertexControl parentControl, PropertyDescriptor propertyDescriptor)
        {
            var newId = parentControl.VertexConnectionPointsList.Count == 0
                ? 1
                : parentControl.VertexConnectionPointsList.Max(a => a.Id) + 1;

            var input = new VertexConnectionPointOut
            {
                Id = newId,
                Header = propertyDescriptor.Name,
                ToolTip = propertyDescriptor.Name
            };
            var inputBorder = new Border
            {
                Child = input
            };

            parentControl.BlockOutput.Children.Add(inputBorder);
            parentControl.VertexConnectionPointsList.Add(input);
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
