using System;
using Aries.OpenCV.Core;
using Aries.OpenCV.GraphModel;
using GraphX.Common.Enums;
using GraphX.Logic.Algorithms.LayoutAlgorithms;
using GraphX.Logic.Algorithms.OverlapRemoval;
using GraphX.Logic.Models;
using QuickGraph;

namespace AriesCV.ViewModel
{
    public class LogicCoreCV :
        GXLogicCore<BlockVertex, BlockEdge, BidirectionalGraph<BlockVertex, BlockEdge>>
    {

        public LogicCoreCV()
        {
            DefaultOverlapRemovalAlgorithm = OverlapRemovalAlgorithmTypeEnum.FSA;
            DefaultOverlapRemovalAlgorithmParams = new OneWayFSAParameters { VerticalGap = 250, HorizontalGap = 250 };
        }

        public LayoutType LayoutType { set; get; }

        public EdgeRoutingType EdgeRoutingType { set; get; }

        public void SetLayout(LayoutType layout)
        {
            switch (layout)
            {
                case LayoutType.TreeLeftToRight:
                    DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.Tree;
                    DefaultLayoutAlgorithmParams = new SimpleTreeLayoutParameters
                    {
                        VertexGap = 200,
                        LayerGap = 200,
                        ComponentGap = 100,
                        Direction = LayoutDirection.LeftToRight,
                        OptimizeWidthAndHeight = true,
                        SpanningTreeGeneration = SpanningTreeGeneration.BFS
                    };
                    break;
                case LayoutType.TreeTopTpBottom:
                    DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.Tree;
                    DefaultLayoutAlgorithmParams = new SimpleTreeLayoutParameters
                    {
                        VertexGap = 200,
                        LayerGap = 200,
                        ComponentGap = 100,
                        Direction = LayoutDirection.TopToBottom,
                        OptimizeWidthAndHeight = true,
                        SpanningTreeGeneration = SpanningTreeGeneration.BFS
                    };
                    break;
                case LayoutType.TreeRightToLeft:
                    DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.Tree;
                    DefaultLayoutAlgorithmParams = new SimpleTreeLayoutParameters
                    {
                        VertexGap = 200,
                        LayerGap = 200,
                        ComponentGap = 100,
                        Direction = LayoutDirection.RightToLeft,
                        OptimizeWidthAndHeight = true,
                        SpanningTreeGeneration = SpanningTreeGeneration.BFS
                    };
                    break;
                case LayoutType.Circular:
                    DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.Circular;
                    DefaultLayoutAlgorithmParams = new CircularLayoutParameters()
                    {
                        Ratio = 150
                    };
                    break;
                case LayoutType.Custom:
                    DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.Custom;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(layout), layout, null);
            }

            LayoutType = layout;
        }

        public void SetEdgeRouting(EdgeRoutingType edgeRoutingType)
        {
            switch (edgeRoutingType)
            {
                case EdgeRoutingType.SimpleER:
                    DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.SimpleER;
                    EdgeCurvingEnabled = true;
                    break;
                case EdgeRoutingType.Bundling:
                    DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.Bundling;
                    EdgeCurvingEnabled = true;
                    break;
                case EdgeRoutingType.PathFinder:
                    DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.PathFinder;
                    EdgeCurvingEnabled = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(edgeRoutingType), edgeRoutingType, null);
            }

            EdgeRoutingType = edgeRoutingType;
        }




    }
}
