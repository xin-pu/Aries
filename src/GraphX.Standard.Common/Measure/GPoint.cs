using System;
// These types are aliased to match the unmanaged names used in interop
using BOOL = System.UInt32;
using WORD = System.UInt16;
using Float = System.Single;

namespace GraphX.Measure
{
    /// <summary>
    /// Custom PCL implementation of Point class
    /// </summary>
    public struct GPoint
    {
        private static readonly GPoint ZeroPoint = new GPoint();
        public static GPoint Zero
        {
            get { return ZeroPoint; }
        }

        internal double _x;
        internal double _y;
        public double X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }
        public double Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        public GPoint(double x, double y)
        {
            _x = x;
            _y = y;
        }

        #region Custom operator overloads
        /// <summary>
        /// Compares two Point instances for exact equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </summary>
        /// <returns>
        /// bool - true if the two Point instances are exactly equal, false otherwise
        /// </returns>
        /// <param name='point1'>The first Point to compare</param>
        /// <param name='point2'>The second Point to compare</param>
        public static bool operator ==(GPoint point1, GPoint point2)
        {
            return point1.X == point2.X &&
                   point1.Y == point2.Y;
        }

        /// <summary>
        /// Compares two Point instances for exact inequality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </summary>
        /// <returns>
        /// bool - true if the two Point instances are exactly unequal, false otherwise
        /// </returns>
        /// <param name='point1'>The first Point to compare</param>
        /// <param name='point2'>The second Point to compare</param>
        public static bool operator !=(GPoint point1, GPoint point2)
        {
            return !(point1 == point2);
        }

        /// <summary>
        /// Compares two Point instances for object equality.  In this equality
        /// Double.NaN is equal to itself, unlike in numeric equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which
        /// are logically equal may fail.
        /// </summary>
        /// <returns>
        /// bool - true if the two Point instances are exactly equal, false otherwise
        /// </returns>
        /// <param name='point1'>The first Point to compare</param>
        /// <param name='point2'>The second Point to compare</param>
        public static bool Equals(GPoint point1, GPoint point2)
        {
            return point1.X.Equals(point2.X) &&
                   point1.Y.Equals(point2.Y);
        }

        /// <summary>
        /// Equals - compares this Point with the passed in object.  In this equality
        /// Double.NaN is equal to itself, unlike in numeric equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which
        /// are logically equal may fail.
        /// </summary>
        /// <returns>
        /// bool - true if the object is an instance of Point and if it's equal to "this".
        /// </returns>
        /// <param name='o'>The object to compare to "this"</param>
        public override bool Equals(object o)
        {
            if ((null == o) || !(o is GPoint))
            {
                return false;
            }

            var value = (GPoint)o;
            return GPoint.Equals(this, value);
        }

        /// <summary>
        /// Equals - compares this Point with the passed in object.  In this equality
        /// Double.NaN is equal to itself, unlike in numeric equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which
        /// are logically equal may fail.
        /// </summary>
        /// <returns>
        /// bool - true if "value" is equal to "this".
        /// </returns>
        /// <param name='value'>The Point to compare to "this"</param>
        public bool Equals(GPoint value)
        {
            return GPoint.Equals(this, value);
        }
        /// <summary>
        /// Returns the HashCode for this Point
        /// </summary>
        /// <returns>
        /// int - the HashCode for this Point
        /// </returns>
        public override int GetHashCode()
        {
            // Perform field-by-field XOR of HashCodes
            return X.GetHashCode() ^
                   Y.GetHashCode();
        }
        ///// OTHER CLASSES CONVERSIONS

        public static implicit operator GPoint(Size size)
        {
            return new GPoint(size.Width, size.Height);
        }

        public static implicit operator GPoint(Vector size)
        {
            return new GPoint(size.X, size.Y);
        }

        public static explicit operator Size(GPoint point)
        {
            return new Size(Math.Abs(point._x), Math.Abs(point._y));
        }

        public static explicit operator Vector(GPoint point)
        {
            return new Vector(point._x, point._y);
        }

        ///// OTHER CLASSES ARITHM + CONVERSIONS

        public static GPoint operator +(GPoint point, Vector vector)
        {
            return new GPoint(point._x + vector._x, point._y + vector._y);
        }

        public static GPoint operator -(GPoint point, Vector vector)
        {
            return new GPoint(point._x - vector._x, point._y - vector._y);
        }

        /// ARITHMETIC

        public static Vector operator -(GPoint value1, GPoint value2)
        {
            return new Vector(value1._x - value2._x, value1._y - value2._y);
        }

        public static GPoint operator +(GPoint value1, GPoint value2)
        {
            return new GPoint(value1._x + value2._x, value1._y + value2._y);
        }

        public static GPoint operator *(double value1, GPoint value2)
        {
            return new GPoint(value1 * value2.X, value1 * value2.Y);
        }

        public static GPoint operator *(GPoint value1, double value2)
        {
            return new GPoint(value1.X * value2, value1.Y * value2);
        }

        public static GPoint operator /(GPoint value1, double value2)
        {
            return new GPoint(value1.X * value2, value1.Y * value2);
        }

        public void Offset(double offsetX, double offsetY)
        {
            this._x += offsetX;
            this._y += offsetY;
        }

        #endregion

        public override string ToString()
        {
            return $"{_x}:{_y}";
        }
    }
}
