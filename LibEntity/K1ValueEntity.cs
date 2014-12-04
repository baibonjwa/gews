// ******************************************************************
// 概  述：K1值实体
// 作  者：宋英杰
// 创建日期：2014/4/15
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
    public class K1ValueEntity
    {
        //主键ID
        private int iD;
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        //K1值分组ID
        private int k1ValueID;
        /// <summary>
        /// K1值分组ID
        /// </summary>
        public int K1ValueID
        {
            get { return k1ValueID; }
            set { k1ValueID = value; }
        }

        //坐标X
        private double coordinateX;
        /// <summary>
        /// 坐标X
        /// </summary>
        public double CoordinateX
        {
            get { return coordinateX; }
            set { coordinateX = value; }
        }

        //坐标Y
        private double coordinateY;
        /// <summary>
        /// 坐标Y
        /// </summary>
        public double CoordinateY
        {
            get { return coordinateY; }
            set { coordinateY = value; }
        }

        //坐标Z
        private double coordinateZ;
        /// <summary>
        /// 坐标Z
        /// </summary>
        public double CoordinateZ
        {
            get { return coordinateZ; }
            set { coordinateZ = value; }
        }

        //干煤K1值
        private double valueK1Dry;
        /// <summary>
        /// K1值
        /// </summary>
        public double ValueK1Dry
        {
            get { return valueK1Dry; }
            set { valueK1Dry = value; }
        }

        //湿煤K1值
        private double valueK1Wet;
        /// <summary>
        /// 湿煤K1值
        /// </summary>
        public double ValueK1Wet
        {
            get { return valueK1Wet; }
            set { valueK1Wet = value; }
        }

        /// <summary>
        /// Sv值
        /// </summary>
        public double Sv { get; set; }

        /// <summary>
        /// Sg值
        /// </summary>
        public double Sg { get; set; }

        /// <summary>
        /// Q值
        /// </summary>
        public double Q { get; set; }


        private double boreholeDeep;
        /// <summary>
        /// 孔深
        /// </summary>
        public double BoreholeDeep
        {
            get { return boreholeDeep; }
            set { boreholeDeep = value; }
        }
        //记录时间
        private DateTime time;
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime Time
        {
            get { return time; }
            set { time = value; }
        }

        //录入时间
        private DateTime typeInTime;
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime TypeInTime
        {
            get { return typeInTime; }
            set { typeInTime = value; }
        }

        //绑定巷道ID
        private int tunnelID;
        /// <summary>
        /// 绑定巷道ID
        /// </summary>
        public int TunnelID
        {
            get { return tunnelID; }
            set { tunnelID = value; }
        }
    }
}
