using System.Linq;
using Aries.OpenCV.GraphModel;
using GraphX.Common;
using GraphX.Common.Enums;
using GraphX.Logic.Algorithms.EdgeRouting;
using GraphX.Logic.Algorithms.LayoutAlgorithms;
using EdgeRoutingType = AriesCV.ViewModel.GraphLayout.EdgeRoutingType;
using LayoutType = AriesCV.ViewModel.GraphLayout.LayoutType;

namespace AriesCV.Views
{
    public partial class CVWorkerItemView
    {
        public void ReSetLayoutCategory(LayoutType layoutType)
        {
            switch (layoutType)
            {
                case LayoutType.TreeLeftToRight:
                    GraphCVArea.LogicCore.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.Tree;
                    GraphCVArea.LogicCore.DefaultLayoutAlgorithmParams =
                        getSimpleTreeLayoutParameters(LayoutDirection.LeftToRight);
                    break;
                case LayoutType.TreeTopTpBottom:
                    GraphCVArea.LogicCore.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.Tree;
                    GraphCVArea.LogicCore.DefaultLayoutAlgorithmParams =
                        getSimpleTreeLayoutParameters(LayoutDirection.TopToBottom);
                    break;
                case LayoutType.TreeRightToLeft:
                    GraphCVArea.LogicCore.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.Tree;
                    GraphCVArea.LogicCore.DefaultLayoutAlgorithmParams =
                        getSimpleTreeLayoutParameters(LayoutDirection.RightToLeft);
                    break;
                case LayoutType.Circular:
                    GraphCVArea.LogicCore.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.Circular;
                    GraphCVArea.LogicCore.DefaultLayoutAlgorithmParams = new CircularLayoutParameters
                    {
                        Seed = 1,
                        Ratio = 2
                    };
                    break;
                default:
                    GraphCVArea.LogicCore.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.Custom;
                    break;
            }

            GraphCVArea.RelayoutGraph();
        }


        public void ResetEdgeRoutingCategory(EdgeRoutingType edgeRoutingType)
        {
            switch (edgeRoutingType)
            {
                case EdgeRoutingType.PathFinder:
                    GraphCVArea.LogicCore.DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.PathFinder;
                    GraphCVArea.LogicCore.DefaultEdgeRoutingAlgorithmParams = new EdgeRoutingParameters();
                    break;
                case EdgeRoutingType.SimpleER:
                    GraphCVArea.LogicCore.DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.SimpleER;
                    GraphCVArea.LogicCore.DefaultEdgeRoutingAlgorithmParams = new SimpleERParameters();
                    break;
                case EdgeRoutingType.Bundling:
                    GraphCVArea.LogicCore.DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.Bundling;
                    GraphCVArea.LogicCore.DefaultEdgeRoutingAlgorithmParams = new BundleEdgeRoutingParameters();
                    break;
                default:
                    GraphCVArea.LogicCore.DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.None;
                    GraphCVArea.LogicCore.DefaultEdgeRoutingAlgorithmParams = new EdgeRoutingParameters();
                    break;
            }

            GraphCVArea.RelayoutGraph();
        }


        private SimpleTreeLayoutParameters getSimpleTreeLayoutParameters(LayoutDirection layoutDirection)
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


        public void ResetShowEdgeLabel(bool isShowEdgeLabels)
        {
            GraphCVArea.ShowAllEdgesLabels(isShowEdgeLabels);
        }


        public void ResetAlignEdgeLabel(bool isAlignEdgeLabels)
        {
            GraphCVArea.AlignAllEdgesLabels(isAlignEdgeLabels);
        }

        public void ResetShowImageView(bool isAutoSave)
        {
            VertexControls.Keys
                .OfType<VertexMat>().ForEach(vc => { vc.ShowImage = isAutoSave; });
        }

        public void RelayoutGraph()
        {
            GraphCVArea.RelayoutGraph();
        }

    }
}