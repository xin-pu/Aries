using Aries.OpenCV.GraphModel;
using GraphX.Common.Enums;
using GraphX.Logic.Algorithms.LayoutAlgorithms;
using GraphX.Logic.Algorithms.OverlapRemoval;
using GraphX.Logic.Models;
using QuickGraph;

namespace Aries.Core
{

    public class LogicCoreCV :
        GXLogicCore<BlockVertex, BlockEdge, BidirectionalGraph<BlockVertex, BlockEdge>>
    {

        public LogicCoreCV()
        {
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
            DefaultOverlapRemovalAlgorithm = OverlapRemovalAlgorithmTypeEnum.FSA;
            DefaultOverlapRemovalAlgorithmParams = new OneWayFSAParameters {VerticalGap = 200, HorizontalGap = 200};

            DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.SimpleER;
            EdgeCurvingEnabled = true;
        }


    
    }
}