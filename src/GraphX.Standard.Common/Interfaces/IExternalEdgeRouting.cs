using System.Collections.Generic;
using System.Threading;
using GraphX.Measure;

namespace GraphX.Common.Interfaces
{
    /// <summary>
    /// Base interface for an edge routing algorithm
    /// </summary>
    /// <typeparam name="TVertex">Vertex data class</typeparam>
    /// <typeparam name="TEdge">Edge data class</typeparam>
    public interface IExternalEdgeRouting<TVertex, TEdge>
    {
        /// <summary>
        /// Run algorithm calculation
        /// </summary>
        void Compute(CancellationToken cancellationToken);
        /// <summary>
        /// Compute edge routing for single edge
        /// </summary>
        /// <param name="edge">Supplied edge data</param>
        GPoint[] ComputeSingle(TEdge edge);

        /// <summary>
        /// Update data stored in algorithm for specified vertex
        /// </summary>
        /// <param name="vertex">Data vertex</param>
        /// <param name="position">Vertex position</param>
        /// <param name="size">Vertex size</param>
        void UpdateVertexData(TVertex vertex, GPoint position, Rect size);

        /// <summary>
        /// Gets or sets visual vertices sizes (autofilled before Compute() call)
        /// </summary>
        IDictionary<TVertex, Rect> VertexSizes { get; set; }

        /// <summary>
        /// Gets or sets visual vertices positions (autofilled before Compute() call)
        /// </summary>
        IDictionary<TVertex, GPoint> VertexPositions { get; set; }

        /// <summary>
        /// Gets resulting edge routes collection 
        /// </summary>
        IDictionary<TEdge, GPoint[]> EdgeRoutes { get; }

        /// <summary>
        /// Gets or sets GraphArea allowed rendering size
        /// </summary>
        Rect AreaRectangle { get; set; }

    }
}
