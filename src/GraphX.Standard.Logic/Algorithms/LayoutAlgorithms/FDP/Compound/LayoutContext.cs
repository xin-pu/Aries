﻿using System.Collections.Generic;
using GraphX.Measure;
using QuickGraph;

namespace GraphX.Logic.Algorithms.LayoutAlgorithms
{
    public class LayoutContext<TVertex, TEdge, TGraph> : ILayoutContext<TVertex, TEdge, TGraph>
        where TEdge : IEdge<TVertex>
        where TGraph : IVertexAndEdgeListGraph<TVertex, TEdge>
    {
        public IDictionary<TVertex, GPoint> Positions { get; private set; }

        public IDictionary<TVertex, Size> Sizes { get; private set; }

        public TGraph Graph { get; private set; }

        public LayoutMode Mode { get; private set; }

        public LayoutContext( TGraph graph, IDictionary<TVertex, GPoint> positions, IDictionary<TVertex, Size> sizes, LayoutMode mode )
        {
            Graph = graph;
            Positions = positions;
            Sizes = sizes;
            Mode = mode;
        }
    }
}