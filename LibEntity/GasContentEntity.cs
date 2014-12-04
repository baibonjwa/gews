// ******************************************************************
// 概  述：瓦斯含量实体
// 作  者：伍鑫
// 创建日期：2013/12/08
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    public class GasContentEntity
    {
        /** 编号 **/
        private int primaryKey;

        /// <summary>
        /// 编号
        /// </summary>
        public int PrimaryKey
        {
            get { return primaryKey; }
            set { primaryKey = value; }
        }

        /** 坐标X **/
        private double coordinateX;

        /// <summary>
        /// 坐标X
        /// </summary>
        public double CoordinateX
        {
            get { return coordinateX; }
            set { coordinateX = value; }
        }

        /** 坐标Y **/
        private double coordinateY;

        /// <summary>
        ///  坐标Y
        /// </summary>
        public double CoordinateY
        {
            get { return coordinateY; }
            set { coordinateY = value; }
        }

        /** 坐标Z **/
        private double coordinateZ;

        /// <summary>
        ///  坐标Z
        /// </summary>
        public double CoordinateZ
        {
            get { return coordinateZ; }
            set { coordinateZ = value; }
        }

        /** 埋深 **/
        private double depth;

        /// <summary>
        /// 埋深 
        /// </summary>
        public double Depth
        {
            get { return depth; }
            set { depth = value; }
        }

        /** 瓦斯含量值 **/
        private double gasContentValue;

        /// <summary>
        /// 瓦斯含量值
        /// </summary>
        public double GasContentValue
        {
            get { return gasContentValue; }
            set { gasContentValue = value; }
        }

        /** 测定时间 **/
        private DateTime measureDateTime;

        /// <summary>
        /// 测定时间
        /// </summary>
        public DateTime MeasureDateTime
        {
            get { return measureDateTime; }
            set { measureDateTime = value; }
        }

        // 巷道编号
        private int tunnelID;

        /// <summary>
        /// 巷道编号
        /// </summary>
        public int TunnelID
        {
            get { return tunnelID; }
            set { tunnelID = value; }
        }

        // 煤层编号
        private int coalSeamsId;

        /// <summary>
        /// 煤层编号
        /// </summary>
        public int CoalSeamsId
        {
            get { return coalSeamsId; }
            set { coalSeamsId = value; }
        }

        /** BID **/
        private string bindingId;

        /// <summary>
        /// BID
        /// </summary>
        public string BindingId
        {
            get { return bindingId; }
            set { bindingId = value; }
        }
    }
}
