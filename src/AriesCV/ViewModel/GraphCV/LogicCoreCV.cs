using Aries.OpenCV.GraphModel;
using GraphX.Common.Enums;
using GraphX.Logic.Algorithms.LayoutAlgorithms;
using GraphX.Logic.Algorithms.OverlapRemoval;
using GraphX.Logic.Models;
using QuickGraph;

namespace AriesCV.ViewModel
{

    public class LogicCoreCv :
        GXLogicCore<BlockVertex, BlockEdge, BidirectionalGraph<BlockVertex, BlockEdge>>
    {

        public LogicCoreCv()
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