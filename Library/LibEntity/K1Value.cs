// ******************************************************************
// 概  述：K1值实体
// 作  者：宋英杰
// 创建日期：2014/4/15
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using System;
using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord("T_VALUE_K1")]
    public class K1Value : ActiveRecordBase<K1Value>
    {
        //主键ID

        private DateTime _time;
        private DateTime _typeInTime;

        /// <summary>
        ///     主键ID
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity)]
        public int Id { get; set; }

        //K1值分组ID

        /// <summary>
        ///     K1值分组ID
        /// </summary>
        [Property("VALUE_K1_ID")]
        public int K1ValueId { get; set; }

        //坐标X

        /// <summary>
        ///     坐标X
        /// </summary>
        [Property("COORDINATE_X")]
        public double CoordinateX { get; set; }

        //坐标Y

        /// <summary>
        ///     坐标Y
        /// </summary>
        [Property("COORDINATE_Y")]
        public double CoordinateY { get; set; }

        //坐标Z

        /// <summary>
        ///     坐标Z
        /// </summary>
        [Property("COORDINATE_Z")]
        public double CoordinateZ { get; set; }

        //干煤K1值

        /// <summary>
        ///     K1值
        /// </summary>
        [Property("VALUE_K1_DRY")]
        public double ValueK1Dry { get; set; }

        //湿煤K1值

        /// <summary>
        ///     湿煤K1值
        /// </summary>
        [Property("VALUE_K1_WET")]
        public double ValueK1Wet { get; set; }

        /// <summary>
        ///     Sv值
        /// </summary>
        [Property("VALUE_SV")]
        public double Sv { get; set; }

        /// <summary>
        ///     Sg值
        /// </summary>
        [Property("VALUE_SG")]
        public double Sg { get; set; }

        /// <summary>
        ///     Q值
        /// </summary>
        [Property("VALUE_Q")]
        public double Q { get; set; }


        /// <summary>
        ///     孔深
        /// </summary>
        [Property("BOREHOLE_DEEP")]
        public double BoreholeDeep { get; set; }

        //记录时间

        /// <summary>
        ///     记录时间
        /// </summary>
        [Property("TIME")]
        public DateTime Time
        {
            get { return _time; }
            set { _time = value; }
        }

        //录入时间

        /// <summary>
        ///     录入时间
        /// </summary>
        [Property("TYPE_IN_TIME")]
        public DateTime TypeInTime
        {
            get { return _typeInTime; }
            set { _typeInTime = value; }
        }

        //绑定巷道ID

        /// <summary>
        ///     绑定巷道ID
        /// </summary>
        [BelongsTo("TUNNEL_ID")]
        public Tunnel Tunnel { get; set; }
    }
}