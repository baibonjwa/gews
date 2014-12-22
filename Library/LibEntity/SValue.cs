// ******************************************************************
// 概  述：S值实体
// 作  者：宋英杰
// 创建日期：2014/4/15
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using System;

namespace LibEntity
{
    public class SValue
    {
        //主键

        private DateTime time;
        private DateTime typeInTime;

        /// <summary>
        ///     主键
        /// </summary>
        public int ID { get; set; }

        //S值分组ID

        /// <summary>
        ///     S值分组ID
        /// </summary>
        public int SValueID { get; set; }

        //坐标X

        /// <summary>
        ///     坐标X
        /// </summary>
        public double CoordinateX { get; set; }

        //坐标Y

        /// <summary>
        ///     坐标Y
        /// </summary>
        public double CoordinateY { get; set; }

        //坐标Z

        /// <summary>
        ///     坐标Z
        /// </summary>
        public double CoordinateZ { get; set; }

        //Sg值

        /// <summary>
        ///     Sg值
        /// </summary>
        public double ValueSg { get; set; }

        //Sv值

        /// <summary>
        ///     Sv值
        /// </summary>
        public double ValueSv { get; set; }

        //q值

        /// <summary>
        ///     q值
        /// </summary>
        public double Valueq { get; set; }

        //孔深

        /// <summary>
        ///     孔深
        /// </summary>
        public double BoreholeDeep { get; set; }

        //记录时间

        /// <summary>
        ///     记录时间
        /// </summary>
        public DateTime Time
        {
            get { return time; }
            set { time = value; }
        }

        //录入时间

        /// <summary>
        ///     录入时间
        /// </summary>
        public DateTime TypeInTime
        {
            get { return typeInTime; }
            set { typeInTime = value; }
        }

        //绑定巷道ID

        /// <summary>
        ///     绑定巷道ID
        /// </summary>
        public int TunnelID { get; set; }
    }
}