// ******************************************************************
// 概  述：K1值实体
// 作  者：宋英杰
// 创建日期：2014/4/15
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using System;

namespace LibEntity
{
    public class K1Value
    {
        //主键ID

        private DateTime time;
        private DateTime typeInTime;

        /// <summary>
        ///     主键ID
        /// </summary>
        public int ID { get; set; }

        //K1值分组ID

        /// <summary>
        ///     K1值分组ID
        /// </summary>
        public int K1ValueID { get; set; }

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

        //干煤K1值

        /// <summary>
        ///     K1值
        /// </summary>
        public double ValueK1Dry { get; set; }

        //湿煤K1值

        /// <summary>
        ///     湿煤K1值
        /// </summary>
        public double ValueK1Wet { get; set; }

        /// <summary>
        ///     Sv值
        /// </summary>
        public double Sv { get; set; }

        /// <summary>
        ///     Sg值
        /// </summary>
        public double Sg { get; set; }

        /// <summary>
        ///     Q值
        /// </summary>
        public double Q { get; set; }


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