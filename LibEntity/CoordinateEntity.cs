using System;

namespace LibEntity
{
    /// <summary>
    ///     Immutable Class
    /// </summary>
    [Serializable]
    public class CoordinateEntity
    {
        /**坐标X**/
        private readonly double x;

        /**坐标X**/
        private readonly double y;

        /**坐标X**/
        private readonly double z;

        public CoordinateEntity(double xx, double yy, double zz)
        {
            x = xx;
            y = yy;
            z = zz;
        }

        /// <summary>
        ///     坐标X
        /// </summary>
        public double X
        {
            get { return x; }
        }

        /// <summary>
        ///     坐标X
        /// </summary>
        public double Y
        {
            get { return y; }
        }

        /// <summary>
        ///     坐标X
        /// </summary>
        public double Z
        {
            get { return z; }
        }
    }
}