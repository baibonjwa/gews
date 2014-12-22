using System;

namespace LibEntity
{
    /// <summary>
    ///     Immutable Class
    /// </summary>
    public class Coordinate
    {
        public Coordinate(double xx, double yy, double zz)
        {
            X = xx;
            Y = yy;
            Z = zz;
        }

        /// <summary>
        ///     坐标X
        /// </summary>
        public double X { get; private set; }

        /// <summary>
        ///     坐标X
        /// </summary>
        public double Y { get; private set; }

        /// <summary>
        ///     坐标X
        /// </summary>
        public double Z { get; private set; }
    }
}