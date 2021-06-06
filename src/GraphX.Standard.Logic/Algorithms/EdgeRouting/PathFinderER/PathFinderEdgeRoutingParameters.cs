using GraphX.Common.Interfaces;

namespace GraphX.Logic.Algorithms.EdgeRouting
{
    public class PathFinderEdgeRoutingParameters : IEdgeRoutingParameters
    {
        /// <summary>
        /// Controls horizontal grid density. Smaller the value more points will be available.
        /// 控制水平网格密度。值越小，可用的点数就越多。
        /// </summary>
        public double HorizontalGridSize { get; set; }
        /// <summary>
        /// Controls vertical grid density. Smaller the value more points will be available.
        /// 控制垂直网格密度。值越小，可用的点数就越多。
        /// </summary>
        public double VerticalGridSize { get; set; }
        /// <summary>
        /// Offset from the each side to enlarge grid and leave additional space for routing.
        /// 从每边偏移以扩大网格并为路由留下额外的空间。
        /// </summary>
        public double SideGridOffset { get; set; }

        /// <summary>
        /// Use diagonal point connections while searching for the path.
        /// </summary>
        public bool UseDiagonals { get; set; }
        /// <summary>
        /// Algorithm used to search for the path.
        /// </summary>
        public PathFindAlgorithm PathFinderAlgorithm { get; set; }
        /// <summary>
        /// Gets or sets if direction change is unpreferable
        /// </summary>
        public bool PunishChangeDirection { get; set; }
        /// <summary>
        /// Gets or sets if diagonal shortcuts must be preferred
        /// </summary>
        public bool UseHeavyDiagonals { get; set; }
        /// <summary>
        /// Heuristic level
        /// </summary>
        public int Heuristic { get; set; }
        /// <summary>
        /// Use special formula for tie breaking
        /// </summary>
        public bool UseTieBreaker { get; set; }
        /// <summary>
        /// Maximum number of tries
        /// </summary>
        public int SearchTriesLimit { get; set; }


        public PathFinderEdgeRoutingParameters()
        {
            HorizontalGridSize = 100;
            VerticalGridSize = 100;
            SideGridOffset = 50;
            UseDiagonals = true;
            PathFinderAlgorithm = PathFindAlgorithm.Euclidean;
            PunishChangeDirection = false;
            UseHeavyDiagonals = false;
            Heuristic = 2;
            UseTieBreaker = false;
            SearchTriesLimit = 50000;
        }

    }

    public enum PathFindAlgorithm
    {
        Manhattan = 1,
        MaxDXDY = 2,
        DiagonalShortCut = 3,
        Euclidean = 4,
        EuclideanNoSQR = 5,
        Custom1 = 6
    }
}
