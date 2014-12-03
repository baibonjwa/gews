// ******************************************************************
// 概  述：瓦斯涌出量实体
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
    public class GasGushQuantityEntity
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

        /** 绝对瓦斯涌出量 **/
        private double absoluteGasGushQuantity;

        /// <summary>
        /// 绝对瓦斯涌出量 
        /// </summary>
        public double AbsoluteGasGushQuantity
        {
            get { return absoluteGasGushQuantity; }
            set { absoluteGasGushQuantity = value; }
        }

        /** 相对瓦斯涌出量 **/
        private double relativeGasGushQuantity;

        /// <summary>
        /// 相对瓦斯涌出量
        /// </summary>
        public double RelativeGasGushQuantity
        {
            get { return relativeGasGushQuantity; }
            set { relativeGasGushQuantity = value; }
        }

        /** 工作面日产量 **/
        private double workingFaceDayOutput;

        /// <summary>
        /// 工作面日产量
        /// </summary>
        public double WorkingFaceDayOutput
        {
            get { return workingFaceDayOutput; }
            set { workingFaceDayOutput = value; }
        }

        /** 回采年月 **/
        private string stopeDate;

        /// <summary>
        /// 回采年月
        /// </summary>
        public string StopeDate
        {
            get { return stopeDate; }
            set { stopeDate = value; }
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
