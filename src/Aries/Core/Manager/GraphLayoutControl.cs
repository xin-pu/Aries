using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Aries.OpenCV.Core;
using Aries.Utility;
using GraphX.Common.Enums;
using GraphX.Logic.Algorithms.EdgeRouting;
using GraphX.Logic.Algorithms.LayoutAlgorithms;

namespace Aries.Core
{
    public class GraphLayoutControl : INotifyPropertyChanged
    {

        private GraphLayoutManager _graphLayoutManager = new GraphLayoutManager();
        private LayoutCategory _layoutCategorySelect;
        private EdgeRoutingCategory _edgeRoutingCategory;
        public bool _isShowEdgeLabels = false;
        public bool _isAlignEdgeLabels = true;

        private GraphCVArea GraphCvArea { get; }


        private LogicCoreCV logicCoreCv
        {
            get { return GraphCvArea.GetLogicCore<LogicCoreCV>(); }
        }

        public GraphLayoutManager GraphLayoutManager
        {
            set { UpdateProperty(ref _graphLayoutManager, value); }
            get { return _graphLayoutManager; }
        }


        public LayoutCategory LayoutCategorySelect
        {
            set { UpdateProperty(ref _layoutCategorySelect, value); }
            get { return _layoutCategorySelect; }
        }

        public EdgeRoutingCategory EdgeRoutingCategorySelect
        {
            set { UpdateProperty(ref _edgeRoutingCategory, value); }
            get { return _edgeRoutingCategory; }
        }

        public bool IsShowEdgeLabels
        {
            set { UpdateProperty(ref _isShowEdgeLabels, value); }
            get { return _isShowEdgeLabels; }
        }

        public bool IsAlignEdgeLabels
        {
            set { UpdateProperty(ref _isAlignEdgeLabels, value); }
            get { return _isAlignEdgeLabels; }
        }

        public GraphLayoutControl(GraphCVArea graphCvArea)
        {
            GraphCvArea = graphCvArea;
        }


        #region Command

        public ICommand ShowEdgeLabelCommand
        {
            get { return new RelayCommand(ShowEdgeLabelCommand_Execute); }
        }

        public ICommand AlignEdgeLabelsCommand
        {
            get { return new RelayCommand(AlignEdgeLabelsCommand_Execute); }
        }

        
        public ICommand LayoutCategorySelectedChangeCommand
        {
            get { return new RelayCommand(LayoutCategorySelectedChangeCommand_Execute); }
        }

        public ICommand EdgeRoutingCategorySelectedChangeCommand
        {
            get { return new RelayCommand(EdgeRoutingCategorySelectedChangeCommand_Execute); }
        }

        private void LayoutCategorySelectedChangeCommand_Execute()
        {
            switch (LayoutCategorySelect.LayoutType)
            {
                case LayoutType.TreeLeftToRight:
                    logicCoreCv.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.Tree;
                    logicCoreCv.DefaultLayoutAlgorithmParams = getSimpleTreeLayoutParameters(LayoutDirection.LeftToRight);
                    break;
                case LayoutType.TreeTopTpBottom:
                    logicCoreCv.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.Tree;
                    logicCoreCv.DefaultLayoutAlgorithmParams = getSimpleTreeLayoutParameters(LayoutDirection.TopToBottom);
                    break;
                case LayoutType.TreeRightToLeft:
                    logicCoreCv.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.Tree;
                    logicCoreCv.DefaultLayoutAlgorithmParams = getSimpleTreeLayoutParameters(LayoutDirection.RightToLeft);
                    break;
                case LayoutType.Circular:
                    logicCoreCv.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.Circular;
                    logicCoreCv.DefaultLayoutAlgorithmParams = new CircularLayoutParameters {Seed = 1};
                    break;
                case LayoutType.Custom:
                    logicCoreCv.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.Custom;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            GraphCvArea.RelayoutGraph();
        }


        private void EdgeRoutingCategorySelectedChangeCommand_Execute()
        {
            switch (EdgeRoutingCategorySelect.EdgeRoutingAlgorithmType)
            {
                case EdgeRoutingAlgorithmTypeEnum.None:
                    logicCoreCv.DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.None;
                    logicCoreCv.DefaultEdgeRoutingAlgorithmParams = new EdgeRoutingParameters();
                    break;
                case EdgeRoutingAlgorithmTypeEnum.PathFinder:
                    logicCoreCv.DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.PathFinder;
                    logicCoreCv.DefaultEdgeRoutingAlgorithmParams = new PathFinderEdgeRoutingParameters()
                    {
                        
                    };
                    break;
                case EdgeRoutingAlgorithmTypeEnum.SimpleER:
                    logicCoreCv.DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.SimpleER;
                    logicCoreCv.DefaultEdgeRoutingAlgorithmParams = new SimpleERParameters();
                    break;
                case EdgeRoutingAlgorithmTypeEnum.Bundling:
                    logicCoreCv.DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.Bundling;
                    logicCoreCv.DefaultEdgeRoutingAlgorithmParams = new BundleEdgeRoutingParameters();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            GraphCvArea.RelayoutGraph();
        }

        private void ShowEdgeLabelCommand_Execute()
        {
            GraphCvArea.ShowAllEdgesLabels(IsShowEdgeLabels);
        }


        private void AlignEdgeLabelsCommand_Execute()
        {
            GraphCvArea.AlignAllEdgesLabels(IsAlignEdgeLabels);
        }


        public SimpleTreeLayoutParameters getSimpleTreeLayoutParameters(LayoutDirection layoutDirection)
        {
            return new SimpleTreeLayoutParameters
            {
                ComponentGap = 200,
                VertexGap = 200,
                LayerGap = 200,
                OptimizeWidthAndHeight = true,
                SpanningTreeGeneration = SpanningTreeGeneration.BFS,
                WidthPerHeight = 1,
                Direction = layoutDirection
            };
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
