using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Aries.OpenCV.GraphModel.Controls;
using GraphX.Common.Enums;
using GraphX.Common.Exceptions;
using GraphX.Common.Models;
using GraphX.Controls;
using QuickGraph;

namespace Aries.OpenCV.GraphModel.Core
{
    public class GraphCVArea :
        GraphArea<VertexBasic, BlockEdge, BidirectionalGraph<VertexBasic, BlockEdge>>, INotifyPropertyChanged
    {

        public GraphCVArea()
        {
            ControlFactory = new GraphCVControlFactory(this);
        }

        private VertexBasic _selectBlockVertex;

        public VertexBasic SelectBlockVertex
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
                LogicCore.Graph = Activator.CreateInstance<BidirectionalGraph<VertexBasic, BlockEdge>>();
            else LogicCore.Graph.Clear();


            /// Restore Vertexts
            var vlist = graphSerializationDatas
                .Where(a => a.Data is VertexBasic)
                .ToList();

            var pList = graphSerializationDatas
                .Where(a => a.Data is ConnectionPointData)
                .Select(a => a.Data as ConnectionPointData)
                .ToList();

            foreach (var item in vlist)
            {
                var vertexdata = item.Data as VertexBasic;
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

        public void AddMatBlock(VertexBasic blockVertex)
        {
            var vertex = new MatVertexControl(blockVertex);
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

        public void AddContourBlock(VertexBasic blockVertex)
        {
            var vertex = new ContourVertexControl(blockVertex);
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

            properties.Where(a => a.Category == "DATAIN")
                .ToList()
                .ForEach(
                    p => { AddInputConnectionPoint(parentControl, p); });

            properties.Where(a => a.Category == "DATAOUT")
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
                ParentID = parentControl.GetDataVertex<VertexBasic>().ID,
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
                ParentID = parentControl.GetDataVertex<VertexBasic>().ID,
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


        #region Run Function

        public async Task RunGraphByDataAsync()
        {
            var datas = ExtractSerializationData();

            var verDatas = new List<VertexBasic>();
            var Edges = new List<BlockEdge>();
            var ConnectionPoints = new List<ConnectionPointData>();

            foreach (var data in datas)
            {
                if (data.Data.GetType().IsSubclassOf(typeof(VertexBasic)))
                {
                    var a = data.Data as VertexBasic;
                    verDatas.Add(a);
                    continue;
                }

                if (data.Data.GetType() == typeof(ConnectionPointData))
                {
                    var a = data.Data as ConnectionPointData;
                    ConnectionPoints.Add(a);
                    continue;

                }

                if (data.Data.GetType() == typeof(BlockEdge))
                {
                    var a = data.Data as BlockEdge;
                    Edges.Add(a);
                }
            }
            verDatas.ForEach(a =>
            {
                a.Reload();
                a.WorkDirectory = "D:\\";
            });

            while (verDatas.Any(a => a.CanExecute() && a.Status == BlockStatus.ToRun))
            {
                /// Get Vertext CanRun
                var vertexsRun = verDatas
                    .Where(a => a.CanExecute() &&
                                a.Status == BlockStatus.ToRun)
                    .ToList();

                /// Run Core Execute
                var executeTask = vertexsRun.Select(a => Task.Run(a.ExecuteCommand_Execute));
                await Task.WhenAll(executeTask);


                /// Check Run Status
                if (vertexsRun.Any(a => a.Status == BlockStatus.Exception))
                    break;

                /// Update From Source's OutPut to Target's Input
                vertexsRun.ForEach(vertexRun =>
                {
                    var edgesFromRun = Edges
                        .Where(a => a.Source.ID == vertexRun.ID)
                        .ToList();


                    foreach (var edgeActive in edgesFromRun)
                    {

                        var sourceAtEdge = edgeActive.Source;
                        var targetAtEdge = edgeActive.Target;

                        var sourceReal = verDatas.FirstOrDefault(a => a.ID == sourceAtEdge.ID);
                        var targetReal = verDatas.FirstOrDefault(a => a.ID == targetAtEdge.ID);

                        if (sourceReal == null || targetReal == null)
                            break;

                        var sourceID = sourceReal.ID;
                        var targetID = targetReal.ID;

                        /// 从所有连接点中找到唯一目标
                        var sourcePoint = ConnectionPoints.FirstOrDefault(a =>
                            a.ParentID == sourceID &&
                            a.ID == edgeActive.SourceConnectionPointId);
                        var targetPoint = ConnectionPoints.FirstOrDefault(a =>
                            a.ParentID == targetID &&
                            a.ID == edgeActive.TargetConnectionPointId);
                        if (sourcePoint == null || targetPoint == null)
                            break;

                        var sourceHeaderName = sourcePoint.Header;
                        var targetHeaderName = targetPoint.Header;

                        if (targetPoint.TypeFullName == sourcePoint.TypeFullName)
                        {
                            var obj = sourceReal.GetProperty(sourceHeaderName);
                            targetReal.SetProperty(targetHeaderName, obj);
                        }
                        else
                        {
                            var mat = sourceReal.GetPropertyAsMat(sourceHeaderName);
                            targetReal.SetPropertyAsMat(targetHeaderName, mat);
                        }

                    }
                });

            }
        }

        public async Task RunGraphAsync()
        {
            try
            {

                var verDatas = VertexList.Keys.ToList();
                var edges = EdgesList.Keys.ToList();
                verDatas.ForEach(vertex => vertex.Reload());


                while (verDatas.Any(a => a.CanExecute() && a.Status == BlockStatus.ToRun))
                {
                    /// Get Vertext CanRun
                    var vertexsRun = verDatas
                        .Where(a => a.CanExecute() &&
                                    a.Status == BlockStatus.ToRun)
                        .ToList();

                    /// Run Core Execute
                    var executeTask = vertexsRun.Select(a => Task.Run(a.ExecuteCommand_Execute));
                    await Task.WhenAll(executeTask);


                    /// Check Run Status
                    if (vertexsRun.Any(a => a.Status == BlockStatus.Exception))
                        break;

                    /// Update From Source's OutPut to Target's Input
                    vertexsRun.ForEach(vertexRun =>
                    {
                        var edgesFromRun = edges
                            .Where(a => a.Source == vertexRun)
                            .ToList();


                        foreach (var edgeActive in edgesFromRun)
                        {
                            var source = edgeActive.Source;
                            var sourcePoint = VertexList[source].VertexConnectionPointsList
                                .FirstOrDefault(a => a.Id == edgeActive.SourceConnectionPointId);
                            var sourceHeaderName = sourcePoint?.Header;
                            if (sourcePoint == null)
                                continue;

                            var target = edgeActive.Target;
                            var targetPoint = VertexList[target].VertexConnectionPointsList
                                .FirstOrDefault(a => a.Id == edgeActive.TargetConnectionPointId);
                            var targetHeaderName = targetPoint?.Header;
                            if (targetPoint == null)
                                continue;

                            if (targetPoint.TypeFullName == sourcePoint.TypeFullName)
                            {
                                var obj = source.GetProperty(sourceHeaderName);
                                target.SetProperty(targetHeaderName, obj);
                            }
                            else
                            {
                                var mat = source.GetPropertyAsMat(sourceHeaderName);
                                target.SetPropertyAsMat(targetHeaderName, mat);
                            }
                        }
                    });

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public async Task ReloadAllBlockAsync()
        {
            var tasks = VertexList.Keys.Select(vc => Task.Run(vc.Reload));
            await Task.WhenAll(tasks);
        }

        public async Task SetEnableSaveImageAsync(bool isAutoSave)
        {
            var tasks = VertexList.Keys
                .OfType<VertexMat>()
                .Select(vc => { return Task.Run(() => vc.EnableSaveMat = isAutoSave); });
            await Task.WhenAll(tasks);
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
            try
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception)
            {
                // ignored
            }
        }

        #endregion
    }
}
