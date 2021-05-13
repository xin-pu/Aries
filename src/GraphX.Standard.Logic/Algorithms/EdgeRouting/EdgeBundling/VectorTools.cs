using System;
using GraphX.Measure;

namespace GraphX.Logic.Algorithms.EdgeRouting
{
    /// <summary>
    /// Used for vector calculations
    /// </summary>
    internal static class VectorTools
    {
        public static GPoint Minus(GPoint p1, GPoint p2)
        {
            return new GPoint(p1.X - p2.X, p1.Y - p2.Y);
        }

        public static GPoint Plus(GPoint p1, GPoint p2)
        {
            return new GPoint(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static GPoint Multiply(GPoint p, double f)
        {
            return new GPoint(p.X * f, p.Y * f);
        }

        public static GPoint MidPoint(GPoint p1, GPoint p2)
        {
            //CHANGED
            return Multiply(Plus(p1, p2), 0.5f);
        }

        public static float Length(GPoint p)
        {
            return (float)Math.Sqrt(p.X * p.X + p.Y * p.Y);
        }

        public static GPoint Normalize(GPoint p)
        {
            float l = Length(p);
            if (l == 0)
                return p;
            else
                return Multiply(p, 1f / l);
        }

        public static float Angle(GPoint p1, GPoint p2, GPoint q1, GPoint q2)
        {
            return (float)(Math.Atan2(p2.Y - p1.Y, p2.X - p1.X) - Math.Atan2(q2.Y - q1.Y, q2.X - q1.X));
        }

        public static float Distance(GPoint p, GPoint q)
        {
            //CHANGED
            return Length(Minus(q, p));
        }
    }
}
