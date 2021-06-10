using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Aries.OpenCV.Core;
using Aries.OpenCV.GraphModel;
using GraphX.Common.Enums;
using GraphX.Common.Exceptions;
using GraphX.Common.Models;
using GraphX.Controls;
using QuickGraph;

namespace Aries.Core
{
    public class GraphCVArea :
        GraphArea<BlockVertex, BlockEdge, BidirectionalGraph<BlockVertex, BlockEdge>>, INotifyPropertyChanged
    {

        private BlockVertex _selectBlockVertex;

        public BlockVertex SelectBlockVertex
        {
            set { UpdateProperty(ref _selectBlockVertex, value); }
            get { return _selectBlockVertex; }
        }


        public override void RebuildFromSerializationData(IEnumerable<GraphSerializationData> data)
        {

            if (LogicCore == null)
                throw new GX_InvalidDataException("LogicCore -> Not initialized!");
            if (data == null)
                return;

            var graphSerializationDatas = data.ToList();

            /// Clean first
            RemoveAllEdges();
            RemoveAllVertices();

            if (LogicCore.Graph == null)
                LogicCore.Graph = Activator.CreateInstance<BidirectionalGraph<BlockVertex, BlockEdge>>();
            else LogicCore.Graph.Clear();


            /// Restore Vertexts
            var vlist = graphSerializationDatas
                .Where(a => a.Data is BlockVertex)
                .ToList();

            var pList = graphSerializationDatas
                .Where(a => a.Data is ConnectionPointData)
                .Select(a => a.Data as ConnectionPointData)
                .ToList();

            foreach (var item in vlist)
            {
                var vertexdata = item.Data as BlockVertex;
                if (vertexdata == null)
                    throw new ArgumentNullException();

                var ctrl = ControlFactory.CreateVertexControl(vertexdata);
                ctrl.Visibility = item.IsVisible
                    ? Visibility.Visible
                    : Visibility.Collapsed;
                ctrl.SetPosition(item.Position.X, item.Position.Y);
                AddVertex(vertexdata, ctrl);

                LogicCore.Graph.AddVertex(vertexdata);
                ctrl.ApplyTemplate();
                if (item.HasLabel)
                    GenerateVertexLabel(ctrl);

                /// Restore Connection Point
                pList.Where(a => a.ParentID == vertexdata.ID).ToList().ForEach(p =>
                {
                    switch (p.ConnectType)
                    {
                        case ConnectType.INPUT:
                            AddInputConnectionPoint(ctrl, p);
                            break;
                        case ConnectType.OUTPUT:
                            AddOutputConnectionPoint(ctrl, p);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                });
            }

            /// Restore Edges
            var elist = graphSerializationDatas
                .Where(a => a.Data is BlockEdge)
                .ToList();

            foreach (var item in elist)
            {
                var edgedata = item.Data as BlockEdge;
                if (edgedata == null)
                    continue;

                var sourceid = edgedata.Source.ID;
                var targetid = edgedata.Target.ID;

                var datasource = _vertexlist.Keys.FirstOrDefault(a => a.ID == sourceid);
                var datatarget = _vertexlist.Keys.FirstOrDefault(a => a.ID == targetid);

                edgedata.Source = datasource;
                edgedata.Target = datatarget;

                if (datasource == null || datatarget == null)
                    throw new GX_SerializationException(
                        "DeserializeFromFile() -> Serialization logic is broken! Vertex not found. All vertices must be processed before edges!");
                var ecc = ControlFactory.CreateEdgeControl(_vertexlist[datasource], _vertexlist[datatarget], edgedata,
                    true, item.IsVisible
                        ? Visibility.Visible
                        : Visibility.Collapsed);
                InsertEdge(edgedata, ecc);
                LogicCore.Graph.AddEdge(edgedata);
                if (item.HasLabel)
                    GenerateEdgeLabel(ecc);
            }

            if (AutoAssignMissingDataId)
                AutoresolveIds(true);

            //update edge layout and shapes manually
            //to correctly draw arrows in any case except they are manually disabled
            UpdateLayout();
            foreach (var item in EdgesList.Values)

                item.OnApplyTemplate();

            RestoreAlgorithmStorage();

        }


        #region Add Block Function

        public void AddBlock(BlockVertex blockVertex)
        {
            var vertex = new VertexControl(blockVertex);
            AddVertexAndData(blockVertex, vertex);
            vertex.SetConnectionPointsVisibility(true);

            //we have to check if there is only one vertex and set coordinates manually 
            //because layout algorithms skip all logic if there are less than two vertices
            if (VertexList.Count == 1)
            {
                vertex.SetPosition(0, 0);
                //update layout to update vertex size
                UpdateLayout();
            }
            else
            {
                RelayoutGraph(true);
            }

            AddAllConnectionPoints(vertex, blockVertex.GetType());

        }

        public void ReloadBlocks()
        {
            VertexList.Keys.ToList().ForEach(v => v.Reload());
        }

        private void AddAllConnectionPoints(VertexControl parentControl, Type blockType)
        {
            var properties = TypeDescriptor.GetProperties(blockType)
                .OfType<PropertyDescriptor>()
                .ToList();

            properties.Where(a => a.Category == "INPUT")
                .ToList()
                .ForEach(
                    p => { AddInputConnectionPoint(parentControl, p); });

            properties.Where(a => a.Category == "OUTPUT")
                .ToList()
                .ForEach(p => { AddOutputConnectionPoint(parentControl, p); });
        }

        private void AddInputConnectionPoint(VertexControl parentControl, PropertyDescriptor propertyDescriptor)
        {
            var connectionID = parentControl.VertexConnectionPointsList.Count == 0
                ? 1
                : parentControl.VertexConnectionPointsList.Max(a => a.Id) + 1;

            var data = new ConnectionPointData
            {
                ID = connectionID,
                ParentID = parentControl.GetDataVertex<BlockVertex>().ID,
                Header = propertyDescriptor.Name,
                ConnectType = ConnectType.INPUT,
                Icon = BlockHelper.GetPointICon(propertyDescriptor.PropertyType.Name),
                TypeFullName = propertyDescriptor.PropertyType.FullName
            };

            AddInputConnectionPoint(parentControl, data);
        }

        private void AddInputConnectionPoint(VertexControl parentControl, ConnectionPointData connectionPointData)
        {
            var input = new VertexConnectionPointIn
            {
                Id = connectionPointData.ID,
                Header = connectionPointData.Header,
                ToolTip = connectionPointData.Header,
                ConnectType = connectionPointData.ConnectType,
                Icon = connectionPointData.Icon,
                TypeFullName = connectionPointData.TypeFullName,
                Shape = VertexShape.Rectangle
            };
            var inputBorder = new Border
            {
                Child = input,
                Margin = new Thickness(5)
            };
            parentControl.BlockInput.Children.Add(inputBorder);
            parentControl.VertexConnectionPointsList.Add(input);
        }

        private void AddOutputConnectionPoint(VertexControl parentControl, PropertyDescriptor propertyDescriptor)
        {
            var connectionID = parentControl.VertexConnectionPointsList.Count == 0
                ? 1
                : parentControl.VertexConnectionPointsList.Max(a => a.Id) + 1;

            var data = new ConnectionPointData
            {
                ID = connectionID,
                ParentID = parentControl.GetDataVertex<BlockVertex>().ID,
                Header = propertyDescriptor.Name,
                ConnectType = ConnectType.OUTPUT,
                Icon = BlockHelper.GetPointICon(propertyDescriptor.PropertyType.Name),
                TypeFullName = propertyDescriptor.PropertyType.FullName,
            };

            AddOutputConnectionPoint(parentControl, data);
        }

        private void AddOutputConnectionPoint(VertexControl parentControl, ConnectionPointData connectionPointData)
        {
            var input = new VertexConnectionPointOut
            {
                Id = connectionPointData.ID,
                Header = connectionPointData.Header,
                ToolTip = connectionPointData.Header,
                ConnectType = connectionPointData.ConnectType,
                Icon = connectionPointData.Icon,
                TypeFullName = connectionPointData.TypeFullName,
                Shape = VertexShape.Rectangle
            };
            var inputBorder = new Border
            {
                Child = input,
                Margin = new Thickness(5)
            };
            parentControl.BlockOutput.Children.Add(inputBorder);
            parentControl.VertexConnectionPointsList.Add(input);
        }

        #endregion



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
