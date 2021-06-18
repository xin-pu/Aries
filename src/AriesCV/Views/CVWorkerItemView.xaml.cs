using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Aries.OpenCV.GraphModel;
using AriesCV.ViewModel;
using GalaSoft.MvvmLight.Command;
using GraphX.Controls;
using GraphX.Controls.Animations;
using GraphX.Controls.Models;

namespace AriesCV.Views
{
    /// <summary>
    /// Interaction logic for CVWorkerItemView.xaml
    /// </summary>
    public partial class CVWorkerItemView : IDisposable
    {
        private FileInfo _fileInfo;
        private string __fileName;

        public CVWorkerItemView(string name)
        {
            InitializeComponent();
            Name = name;
            GraphCVArea.Name = name;
            GraphCvConfig = new GraphCVConfig();
            InitialForNew();
        }

        public CVWorkerItemView(GraphCVFileStruct graphCvFileStruct)
        {
            InitializeComponent();
            Name = graphCvFileStruct.Name;
            GraphCVArea.Name = graphCvFileStruct.Name;
            GraphCvConfig = graphCvFileStruct.GraphCVConfig;
            InitialForNew();
            GraphCVArea.RebuildFromSerializationData(graphCvFileStruct.GraphSerializationDatas);
            GraphCVArea.SetVerticesDrag(true, true);
            GraphCVArea.UpdateAllEdges();
        }


        public FileInfo FileInfo
        {
            set { UpdateProperty(ref _fileInfo, value); }
            get { return _fileInfo; }
        }

        public string FileName
        {
            set { UpdateProperty(ref __fileName, value); }
            get { return __fileName; }
        }

        public GraphCVConfig GraphCvConfig { set; get; }

        public GraphCVEditManager EditorManager { set; get; }

        public LogicCoreCV LogicCoreCv { set; get; }

        private void InitialForNew()
        {
            EditorManager = new GraphCVEditManager(GraphCVArea, ZoomControl);

            LogicCoreCv = new LogicCoreCV();
            LogicCoreCv.SetEdgeRouting(GraphCvConfig.EdgeRoutingType);
            LogicCoreCv.SetLayout(GraphCvConfig.LayoutAlgorithm);


            GraphCVArea.LogicCore = LogicCoreCv;

            InitialGraphArea();
            InitialZoomControl();
            //GraphLayoutControl = new GraphLayoutControl(GraphArea);
            //GraphCvRunManager = new GraphCVRunManager(GraphArea)
            //{
            //    AppendMatRecordAction = MatRecordManager.AppendMatRecords,
            //    ClearMatRecordsAction = MatRecordManager.ClearRecords
            //};

            Loaded += DynamicGraph_Loaded;
            Unloaded += DynamicGraph_Unloaded;

        }


        private void InitialGraphArea()
        {
            /// 设置元素块可拖动
            GraphCVArea.SetVerticesDrag(true, true);
            GraphCVArea.SetEdgesDrag(true);

            GraphCVArea.LogicCore = LogicCoreCv;

            GraphCVArea.MoveAnimation =
                AnimationFactory.CreateMoveAnimation(MoveAnimation.Move, TimeSpan.FromSeconds(0.5));
            GraphCVArea.MoveAnimation.Completed += MoveAnimationCompleted;
            GraphCVArea.VertexSelected += BlockVertexSelected;
            GraphCVArea.EdgeSelected += BlockEdgeSelected;
            GraphCVArea.EdgeDoubleClick += BlockEdgeDoubleClick;

            GraphCVArea.ShowAllEdgesLabels(false);
            GraphCVArea.ShowAllEdgesArrows();

        }

        private void InitialZoomControl()
        {
            ZoomControl.AllowDrop = true;
            ZoomControl.AreaSelected += SelectZoomAreaVertex;

            ZoomControl.PreviewMouseLeftButtonDown += ZoomControlDropPreviewMouseLeftButtonDown;
            ZoomControl.PreviewDrop += ZoomControlDrop;
            ZoomControl.DragEnter += ZoomControlEnter;
        }


        #region Mouse Event

        private void MoveAnimationCompleted(object sender, EventArgs e)
        {
            ZoomControl.ZoomToFill();
        }

        private void BlockVertexSelected(object sender, VertexSelectedEventArgs args)
        {
            var vertex = args.VertexControl;
            GraphCVArea.SelectBlockVertex = vertex.GetDataVertex<BlockVertex>();
            /// Right Mouse Clicked
            if (args.MouseArgs.RightButton == MouseButtonState.Pressed)
            {
                CreateContextMenu(vertex);
            }

            if (args.MouseArgs.LeftButton == MouseButtonState.Pressed)
            {
                if (Keyboard.IsKeyDown(Key.LeftCtrl))
                {
                    CreateEdgeControl(args.VertexControl);

                }
                else if (Keyboard.IsKeyDown(Key.S))
                {
                    SelectVertexIsTagged(args.VertexControl);
                }
            }
        }


        private void CreateContextMenu(VertexControl vertexControl)
        {
            if (vertexControl == null)
                return;
            if (vertexControl.ContextMenu == null)
            {
                vertexControl.ContextMenu = new ContextMenu();
                var menuItem1 = new MenuItem
                    {Header = "Remove Select", Command = RemoveSelectCommand, CommandParameter = vertexControl};
                vertexControl.ContextMenu.Items.Add(menuItem1);
                var menuItem2 = new MenuItem
                    {Header = "Remove All Tagged", Command = RemoveAllTaggedCommand};
                vertexControl.ContextMenu.Items.Add(menuItem2);
                var menuItem3 = new MenuItem
                    {Header = "Remove All", Command = RemoveAllCommand};
                vertexControl.ContextMenu.Items.Add(menuItem3);
            }

            vertexControl.ContextMenu.IsOpen = true;
        }

        private void BlockEdgeSelected(object sender, EdgeSelectedEventArgs args)
        {
            var vc = args.EdgeControl;
            HighlightBehaviour.SetHighlighted(vc, false);
            DragBehaviour.SetIsTagged(vc, false);
        }

        private void BlockEdgeDoubleClick(object sender, EdgeSelectedEventArgs args)
        {
            GraphCVArea.RemoveEdge(args.EdgeControl.Edge as BlockEdge, true);
        }

        #endregion

        #region ContextMenu Command

        private RelayCommand<object> RemoveSelectCommand
        {
            get { return new RelayCommand<object>(RemoveSelectCommand_Execute); }
        }

        private RelayCommand RemoveAllCommand
        {
            get { return new RelayCommand(RemoveAllCommand_Execute); }
        }

        private RelayCommand RemoveAllTaggedCommand
        {
            get { return new RelayCommand(RemoveAllTaggedCommand_Execute); }
        }

        private void RemoveAllTaggedCommand_Execute()
        {
            GraphCVArea.VertexList.Values
                .Where(DragBehaviour.GetIsTagged)
                .ToList()
                .ForEach(SafeRemoveVertex);
            ZoomControl.ZoomToFill();
        }

        private void RemoveAllCommand_Execute()
        {
            GraphCVArea.VertexList.Values
                .ToList()
                .ForEach(SafeRemoveVertex);
            ZoomControl.ZoomToFill();
        }

        private void RemoveSelectCommand_Execute(object obj)
        {
            var vc = (VertexControl) obj;
            if (vc != null) SafeRemoveVertex(vc);
            ZoomControl.ZoomToFill();
        }

        /// <summary>
        /// Remove vertex and do all cleanup necessary for current demo
        /// </summary>
        /// <param name="vc">vertexControl object</param>
        private void SafeRemoveVertex(VertexControl vc)
        {
            GraphCVArea.RemoveVertexAndEdges(vc.Vertex as BlockVertex);
        }

        /// <summary>
        /// Select vertex by setting its tag and highlight value
        /// </summary>
        /// <param name="vc">VertexControl object</param>
        private void SelectVertexIsTagged(DependencyObject vc)
        {
            if (DragBehaviour.GetIsTagged(vc))
            {
                HighlightBehaviour.SetHighlighted(vc, false);
                DragBehaviour.SetIsTagged(vc, false);
                vc.ClearValue(DragBehaviour.XSnapModifierProperty);
                vc.ClearValue(DragBehaviour.YSnapModifierProperty);
            }
            else
            {
                HighlightBehaviour.SetHighlighted(vc, true);
                DragBehaviour.SetIsTagged(vc, true);
                DragBehaviour.SetXSnapModifier(vc, ExaggeratedSnappingXModifier);
                DragBehaviour.SetYSnapModifier(vc, ExaggeratedSnappingYModifier);
            }
        }

        private void SelectZoomAreaVertex(object sender, AreaSelectedEventArgs args)
        {
            var r = args.Rectangle;
            foreach (var item in from item in GraphCVArea.VertexList
                let offset = item.Value.GetPosition()
                let irect = new Rect(offset.X, offset.Y, item.Value.ActualWidth, item.Value.ActualHeight)
                where irect.IntersectsWith(r)
                select item)
            {
                SelectVertexIsTagged(item.Value);
            }
        }

        private double ExaggeratedSnappingXModifier(GraphAreaBase area, DependencyObject obj, double val)
        {
            return DragBehaviour.GlobalXSnapModifier(area, obj, val);
        }

        private double ExaggeratedSnappingYModifier(GraphAreaBase area, DependencyObject obj, double val)
        {
            return DragBehaviour.GlobalYSnapModifier(area, obj, val);
        }

        #endregion

        #region Edge

        private VertexControl _vertexTemp;

        private void CreateEdgeControl(VertexControl vc)
        {
            if (_vertexTemp == null)
            {
                EditorManager.CreateVirtualEdge(vc, vc.GetPosition());
                _vertexTemp = vc;
                HighlightBehaviour.SetHighlighted(_vertexTemp, true);
                return;
            }

            if (_vertexTemp == vc) return;

            var source = (BlockVertex) _vertexTemp.Vertex;
            var target = (BlockVertex) vc.Vertex;

            var data = new BlockEdge(source, target)
            {
                Header = $"{source.BlockType} --> {target.BlockType}",
            };
            var ec = new EdgeControl(_vertexTemp, vc, data);
            GraphCVArea.InsertEdgeAndData(data, ec, 0, true);

            HighlightBehaviour.SetHighlighted(_vertexTemp, false);
            _vertexTemp = null;
            EditorManager.DestroyVirtualEdge();
        }



        #endregion

        #region Drag 

        void ZoomControlDropPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //var data = new DataObject(typeof(object), new object());
            //DragDrop.DoDragDrop(dg_dragsource, data, DragDropEffects.Link);
        }

        private void ZoomControlEnter(object sender, DragEventArgs e)
        {
            //don't show drag effect if we are on drag source or
            //don't have any item of needed type dragged
            if (!e.Data.GetDataPresent(typeof(object)) || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void ZoomControlDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(object))) return;

            //var pos = ZoomControl.TranslatePoint(e.GetPosition(ZoomControl), GraphCVArea);
            //var data = new Blur();
            //var vc = new VertexControl(data);
            //vc.SetPosition(pos);
            //GraphCVArea.AddVertexAndData(data, vc);
        }

        #endregion

        #region Load Unload

        private bool loaded = false;
        private Predicate<DependencyObject> _originalGlobalIsSnapping = null;
        private Predicate<DependencyObject> _originalGlobalIsSnappingIndividually = null;

        private void DynamicGraph_Loaded(object sender, RoutedEventArgs e)
        {
            if (loaded)
                return;
            loaded = true;

            _originalGlobalIsSnapping = DragBehaviour.GlobalIsSnappingPredicate;
            _originalGlobalIsSnappingIndividually = DragBehaviour.GlobalIsIndividualSnappingPredicate;

            DragBehaviour.GlobalIsSnappingPredicate = IsSnapping;
            DragBehaviour.GlobalIsIndividualSnappingPredicate = IsSnappingIndividually;
        }

        private void DynamicGraph_Unloaded(object sender, RoutedEventArgs e)
        {
            loaded = false;

            DragBehaviour.GlobalIsSnappingPredicate = _originalGlobalIsSnapping;
            DragBehaviour.GlobalIsIndividualSnappingPredicate = _originalGlobalIsSnappingIndividually;
        }

        private bool IsSnapping(DependencyObject obj)
        {
            return true;
        }

        private bool IsSnappingIndividually(DependencyObject obj)
        {
            return true;
        }

        #endregion

        public void Dispose()
        {

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
