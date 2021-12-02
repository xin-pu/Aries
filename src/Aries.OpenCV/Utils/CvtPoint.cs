using System;
using System.Collections.Generic;
using System.Linq;
using OpenCvSharp;

namespace Aries.OpenCV.Utils
{
    /// <summary>
    ///     Should use PointXd
    /// </summary>
    public class CvtPoint
    {
        #region Cut Point

        public static Point2d CutZToPoint2d(Point3d point3ds)
        {
            return new(point3ds.X, point3ds.Y);
        }

        #endregion

        #region Conver From PointXf to PointXd

        public static Point2d CvtToPoint2d(Point point)
        {
            return new(point.X, point.Y);
        }

        public static Point2d CvtToPoint2d(Point2f point2f)
        {
            return new(point2f.X, point2f.Y);
        }

        public static Point3d CvtToPoint3d(Point3f point3f)
        {
            return new(point3f.X, point3f.Y, point3f.Z);
        }


        /// <summary>
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static Point2d[] CvtToPoint2ds(IEnumerable<Point> points)
        {
            return points.Select(CvtToPoint2d).ToArray();
        }

        #endregion

        #region Convert From Mat to PointXd

        /// <summary>
        /// </summary>
        /// <param name="word">Mat with width=2, u,v,</param>
        /// <returns></returns>
        public static Point2d[] CvtToPoint2ds(Mat<double> word)
        {
            var size = word.Size();
            if (size.Width != 2)
                throw new ArgumentException("Width is not 2 for Parse");
            var array = word.ToRectangularArray();

            return Enumerable.Range(0, size.Height).ToList()
                .Select(row => new Point2d(array[row, 0], array[row, 1]))
                .ToArray();
        }

        /// <summary>
        /// </summary>
        /// <param name="word">Mat with width=3, X,Y,Z</param>
        /// <returns></returns>
        public static Point3d[] CvtToPoint3ds(Mat<double> world)
        {
            var size = world.Size();
            if (size.Width != 3)
                throw new ArgumentException("Width is not 3 for Parse");
            var array = world.ToRectangularArray();

            return Enumerable.Range(0, size.Height).ToList()
                .Select(row => new Point3d(array[row, 0], array[row, 1], array[row, 2]))
                .ToArray();
        }

        #endregion


        #region Convert From Point to Mat

        public static Mat CvtToMat(Point3d[] point3ds)
        {
            var len = point3ds.Length;
            var size = new Size(3, len);
            var mat = new Mat(size, MatType.CV_64F);
            foreach (var i in Enumerable.Range(0, len))
            {
                var p = point3ds[i];
                mat.Set(i, 0, p.X);
                mat.Set(i, 1, p.Y);
                mat.Set(i, 2, p.Z);
            }

            return mat;
        }

        public static Mat CvtToMat(Point2d[] point2ds)
        {
            var len = point2ds.Length;
            var size = new Size(2, len);
            var mat = new Mat(size, MatType.CV_64F);
            foreach (var i in Enumerable.Range(0, len))
            {
                var p = point2ds[i];
                mat.Set(i, 0, p.X);
                mat.Set(i, 1, p.Y);
            }

            return mat;
        }


        public static Mat CvtToMat(Point[] point2fs)
        {
            var point2ds = point2fs.Select(CvtToPoint2d).ToArray();
            return CvtToMat(point2ds);
        }

        #endregion
    }
}