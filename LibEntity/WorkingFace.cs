// ******************************************************************
// 概  述：工作面实体
// 作  者：伍鑫
// 创建日期：2014/02/25
// 版本号：1.0
// ******************************************************************

using System;
using System.Collections.Generic;
using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord("T_WORKINGFACE_INFO")]
    public class WorkingFace : ActiveRecordBase
    {
        /** 工作面编号 **/
        private DateTime? startDate;
        private DateTime? stopDate;
        public HashSet<Tunnel> tunnelSet;

        /// <summary>
        ///     工作面编号
        /// </summary>
        [PrimaryKey]
        [Property("WORKINGFACE_ID")]
        public int WorkingFaceID { get; set; }

        /** 工作面名称 **/

        /// <summary>
        ///     工作面名称
        /// </summary>
        [Property("WORKINGFACE_NAME")]
        public string WorkingFaceName { get; set; }

        // 坐标

        public CoordinateEntity Coordinate { get; set; }

        // 开工日期

        /// <summary>
        ///     设置或获取开工日期
        /// </summary>
        [Property("START_DATE")]
        public DateTime? StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        // 是否掘进完毕

        /// <summary>
        ///     设置或获取是否掘进完毕
        /// </summary>
        [Property("IS_FINISH")]
        public int IsFinish { get; set; }

        // 停工日期

        /// <summary>
        ///     设置或获取停工日期
        /// </summary>
        [Property("STOP_DATE")]
        public DateTime? StopDate
        {
            get { return stopDate; }
            set { stopDate = value; }
        }

        // 队别编号

        /// <summary>
        ///     设置或获取队别编号
        /// </summary>
        [Property("TEAM_NAME_ID")]
        public int TeamNameID { get; set; }

        // 采区
        [BelongsTo("MININGAREA_ID")]
        public MiningArea MiningArea { get; set; }

        // 工作制式

        /// <summary>
        ///     设置或获取工作制式
        /// </summary>
        [Property("WORK_STYLE")]
        public string WorkStyle { get; set; }

        // 班次

        /// <summary>
        ///     设置或获取班次
        /// </summary>
        [Property("WORK_TIME")]
        public string WorkTime { get; set; }

        // 用于暂存关联巷道信息

        /// <summary>
        ///     巷道类型
        /// </summary>
        [Property("WORKINGFACE_TYPE")]
        public WorkingfaceTypeEnum WorkingfaceTypeEnum { get; set; }
    }
}