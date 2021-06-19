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
                    LogicCoreCv.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.Tree;
                    LogicCoreCv.DefaultLayoutAlgorithmParams =
                        getSimpleTreeLayoutParameters(LayoutDirection.LeftToRight);
                    break;
                case LayoutType.TreeTopTpBottom:
                    LogicCoreCv.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.Tree;
                    LogicCoreCv.DefaultLayoutAlgorithmParams =
                        getSimpleTreeLayoutParameters(LayoutDirection.TopToBottom);
                    break;
                case LayoutType.TreeRightToLeft:
                    LogicCoreCv.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.Tree;
                    LogicCoreCv.DefaultLayoutAlgorithmParams =
                        getSimpleTreeLayoutParameters(LayoutDirection.RightToLeft);
                    break;
                case LayoutType.Circular:
                    LogicCoreCv.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.Circular;
                    LogicCoreCv.DefaultLayoutAlgorithmParams = new CircularLayoutParameters
                    {
                        Seed = 1,
                        Ratio = 2
                    };
                    break;
                default:
                    LogicCoreCv.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.Custom;
                    break;
            }

            GraphCVArea.RelayoutGraph();
        }


        public void ResetEdgeRoutingCategory(EdgeRoutingType edgeRoutingType)
        {
            switch (edgeRoutingType)
            {
                case EdgeRoutingType.PathFinder:
                    LogicCoreCv.DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.PathFinder;
                    LogicCoreCv.DefaultEdgeRoutingAlgorithmParams = new EdgeRoutingParameters();
                    break;
                case EdgeRoutingType.SimpleER:
                    LogicCoreCv.DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.SimpleER;
                    LogicCoreCv.DefaultEdgeRoutingAlgorithmParams = new SimpleERParameters();
                    break;
                case EdgeRoutingType.Bundling:
                    LogicCoreCv.DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.Bundling;
                    LogicCoreCv.DefaultEdgeRoutingAlgorithmParams = new BundleEdgeRoutingParameters();
                    break;
                default:
                    LogicCoreCv.DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.None;
                    LogicCoreCv.DefaultEdgeRoutingAlgorithmParams = new EdgeRoutingParameters();
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

        public void RelayoutGraph()
        {
            GraphCVArea.RelayoutGraph();
        }

    }
}