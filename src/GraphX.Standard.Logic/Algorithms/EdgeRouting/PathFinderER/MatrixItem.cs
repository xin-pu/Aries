﻿using GraphX.Measure;

namespace GraphX.Logic.Algorithms.EdgeRouting
{
    public class MatrixItem
    {
        public GPoint Point;
        public bool IsIntersected;

        public int PlaceX;
        public int PlaceY;

        public int Weight { get { return IsIntersected ? 0 : 1; } }

        public MatrixItem(GPoint pt, bool inter, int placeX, int placeY)
        {
            Point = pt; IsIntersected = inter;
            PlaceX = placeX; PlaceY = placeY;
        }
    }
}
