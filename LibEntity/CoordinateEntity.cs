using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    /// <summary>
    /// Immutable Class
    /// </summary>
    [Serializable]
    public class CoordinateEntity
    {
        public CoordinateEntity(double xx, double yy, double zz)
        {
            this.x = xx;
            this.y = yy;
            this.z = zz;
        }

        /**坐标X**/
        private double x;
        /// <summary>
        /// 坐标X
        /// </summary>
        public double X
        {
            get { return x; }
        }

        /**坐标X**/
        private double y;
        /// <summary>
        /// 坐标X
        /// </summary>
        public double Y
        {
            get { return y; }
        }

        /**坐标X**/
        private double z;
        /// <summary>
        /// 坐标X
        /// </summary>
        public double Z
        {
            get { return z; }
        }
    }
}
