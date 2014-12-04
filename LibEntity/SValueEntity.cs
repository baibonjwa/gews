// ******************************************************************
// 概  述：S值实体
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
    public class SValueEntity
    {
        //主键
        private int iD;

        /// <summary>
        /// 主键
        /// </summary>
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        //S值分组ID
        private int sValueID;

        /// <summary>
        /// S值分组ID
        /// </summary>
        public int SValueID
        {
            get { return sValueID; }
            set { sValueID = value; }
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

        //Sg值
        private double valueSg;

        /// <summary>
        /// Sg值
        /// </summary>
        public double ValueSg
        {
            get { return valueSg; }
            set { valueSg = value; }
        }

        //Sv值
        private double valueSv;

        /// <summary>
        /// Sv值
        /// </summary>
        public double ValueSv
        {
            get { return valueSv; }
            set { valueSv = value; }
        }

        //q值
        private double valueq;

        /// <summary>
        /// q值
        /// </summary>
        public double Valueq
        {
            get { return valueq; }
            set { valueq = value; }
        }

        //孔深
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
