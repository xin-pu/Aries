using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics;
using OpenCvSharp;

namespace Aries.OpenCV.Utils
{
    public class CVMath
    {
        /// <summary>
        ///     return B,K with Tuple
        /// </summary>
        /// <param name="keyPoints"></param>
        /// <returns></returns>
        public static Tuple<double, double> Linefit(IEnumerable<KeyPoint> keyPoints)
        {
            var x = new List<double>();
            var y = new List<double>();
            foreach (var p in keyPoints.Select(a => a.Pt))
            {
                x.Add(p.X);
                y.Add(p.Y);
            }

            return Fit.Line(x.ToArray(), y.ToArray());
        }

        /// <summary>
        ///     return B,K with Tuple
        /// </summary>
        /// <param name="keyPoints"></param>
        /// <returns></returns>
        public static Tuple<double, double> Linefit(IEnumerable<Point2f> points)
        {
            var x = new List<double>();
            var y = new List<double>();
            foreach (var p in points)
            {
                x.Add(p.X);
                y.Add(p.Y);
            }

            return Fit.Line(x.ToArray(), y.ToArray());
        }

        /// <summary>
        ///     Return Point By Line
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Point GetPoint(double y, Tuple<double, double> kb)
        {
            return new Point((y - kb.Item1) / kb.Item2, y);
        }

        /// <summary>
        ///     Distance= |A*x+ B*y +C | / Sqrt(A*A+B*B)
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="point2F"></param>
        /// <returns></returns>
        public static double GetDistance(double a, double b, double c, Point2f point2F)
        {
            return Math.Abs(a * point2F.X + b * point2F.Y + c)
                   / Math.Sqrt(a * a + b * b);
        }


        public static double GetDistance(Point3d p1, Point3d p2)
        {
            var x = p1.X - p2.X;
            var y = p1.Y - p2.Y;
            var z = p1.Z - p2.Z;

            return Math.Sqrt(x * x + y * y + z * z);
        }

        public static double GetDistance(Point2d p1, Point2d p2)
        {
            var x = p1.X - p2.X;
            var y = p1.Y - p2.Y;

            return Math.Sqrt(x * x + y * y);
        }

        public static double GetDistance(Point p1, Point2d p2)
        {
            var x = p1.X - p2.X;
            var y = p1.Y - p2.Y;

            return Math.Sqrt(x * x + y * y);
        }


        public static Point GetCenter(IEnumerable<Point2d> point2ds)
        {
            var array = point2ds.ToArray();
            var x = array.Average(a => a.X);
            var y = array.Average(a => a.Y);
            return new Point(x, y);
        }

        public static Point GetCenter(IEnumerable<Point> points)
        {
            var array = points.ToArray();
            var x = array.Average(a => a.X);
            var y = array.Average(a => a.Y);
            return new Point(x, y);
        }


        public static List<Point2d> GetPatternPoint(Point2d topleft, Point2d topright, Point2d bottomleft, Size size)
        {
            var rdis = bottomleft - topleft;
            var rdisX = rdis.X / (size.Width - 1);
            var rdisY = rdis.Y / (size.Height - 1);

            var cdis = topright - topleft;
            var cdisX = cdis.X / (size.Width - 1);
            var cdisY = cdis.Y / (size.Height - 1);

            var all = new List<Point2d>();
            for (var r = 0; r < size.Height; r++)
            for (var c = 0; c < size.Width; c++)
            {
                var X = topleft.X + r * rdisX + c * cdisX;
                var Y = topleft.Y + r * rdisY + c * cdisY;
                all.Add(new Point2d(X, Y));
            }

            return all;
        }

        public static double GetRatio(RotatedRect rotatedRect)
        {
            var width = rotatedRect.Size.Width;
            var height = rotatedRect.Size.Height;
            var ratio = width / height;
            return ratio > 1 ? ratio : 1 / ratio;
        }

        public static Mat GetRectMat(Mat mat, Rect rect)
        {
            var left = rect.X;
            var right = rect.X + rect.Width;
            var top = rect.Y;
            var bottom = rect.Y + rect.Height;

            var xmin = left >= 0 && left < mat.Width
                ? left
                : left < 0
                    ? 0
                    : mat.Width - 1;
            var xmax = right >= 0 && right < mat.Width
                ? right
                : right < 0
                    ? 0
                    : mat.Width - 1;

            var ymin = top >= 0 && top < mat.Height
                ? top
                : top < 0
                    ? 0
                    : mat.Height - 1;

            var ymax = bottom >= 0 && bottom < mat.Height
                ? bottom
                : bottom < 0
                    ? 0
                    : mat.Height - 1;
            var newRect = new Rect(xmin, ymin, xmax - xmin, ymax - ymin);

            return mat[newRect];
        }
    }
}