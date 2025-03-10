﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using GraphX.Measure;
using GraphX.Common.Enums;
using GraphX.Common.Interfaces;
using QuickGraph;

namespace GraphX.Logic.Algorithms.LayoutAlgorithms
{
    public class CircularLayoutAlgorithm<TVertex, TEdge, TGraph> : DefaultParameterizedLayoutAlgorithmBase<TVertex,
        TEdge, TGraph, CircularLayoutParameters>
        where TVertex : class, IIdentifiableGraphDataObject
        where TEdge : IEdge<TVertex>
        where TGraph : IBidirectionalGraph<TVertex, TEdge>
    {
        readonly IDictionary<TVertex, Size> _sizes;


        public CircularLayoutAlgorithm(TGraph visitedGraph, IDictionary<TVertex, GPoint> vertexPositions,
            IDictionary<TVertex, Size> vertexSizes, CircularLayoutParameters parameters)
            : base(visitedGraph, vertexPositions, parameters)
        {
            _sizes = vertexSizes;
        }

        /// <summary>
        /// Gets if current algorithm supports vertex freeze feature (part of VAESPS)
        /// </summary>
        public override bool SupportsObjectFreeze
        {
            get { return false; }
        }

        public override void ResetGraph(IEnumerable<TVertex> vertices, IEnumerable<TEdge> edges)
        {
            //
        }

        public override void Compute(CancellationToken cancellationToken)
        {
            //calculate the size of the circle
            var ratio = DefaultParameters.Ratio;
            double perimeter = 0;
            var usableVertices = VisitedGraph.Vertices.Where(v => v.SkipProcessing != ProcessingOptionEnum.Freeze)
                .ToList();
            //if we have empty input positions list we have to fill positions for frozen vertices manualy
            if (VertexPositions.Count == 0)
                foreach (var item in VisitedGraph.Vertices.Where(v => v.SkipProcessing == ProcessingOptionEnum.Freeze))
                    VertexPositions.Add(item, new GPoint());
            double[] halfSize = new double[usableVertices.Count];
            int i = 0;
            foreach (var v in usableVertices)
            {
                cancellationToken.ThrowIfCancellationRequested();

                Size s = _sizes[v];
                halfSize[i] = Math.Sqrt(s.Width * s.Width + s.Height * s.Height) * 0.5;
                perimeter += halfSize[i] * 2;
                i++;
            }

            double radius = perimeter / (2 * Math.PI);

            //
            //precalculation
            //
            double angle = 0, a;
            i = 0;
            foreach (var v in usableVertices)
            {
                cancellationToken.ThrowIfCancellationRequested();

                a = Math.Sin(halfSize[i] * 0.5 / radius) * 2;
                angle += a;
                //if ( ReportOnIterationEndNeeded )
                VertexPositions[v] = new GPoint(Math.Cos(angle) * radius + radius, Math.Sin(angle) * radius + radius);
                angle += a;
            }


            //recalculate radius
            radius = angle / (2 * Math.PI) * radius;

            //calculation
            angle = 0;
            i = 0;
            foreach (var v in usableVertices)
            {
                cancellationToken.ThrowIfCancellationRequested();

                a = Math.Sin(halfSize[i] * 0.5 / radius) * 2;
                angle += a;
                VertexPositions[v] = new GPoint(Math.Cos(angle) * radius * ratio, Math.Sin(angle) * radius * ratio);
                angle += a;
            }
        }
    }
}