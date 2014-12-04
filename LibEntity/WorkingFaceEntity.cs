// ******************************************************************
// 概  述：工作面实体
// 作  者：伍鑫
// 创建日期：2014/02/25
// 版本号：1.0
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGeometry;

namespace LibEntity
{
    [Serializable]
    public class WorkingFaceEntity
    {
        /** 工作面编号 **/
        private int workingfaceId;

        /// <summary>
        /// 工作面编号
        /// </summary>
        public int WorkingFaceID
        {
            get { return workingfaceId; }
            set { workingfaceId = value; }
        }

        /** 工作面名称 **/
        private string workingFaceName;

        /// <summary>
        /// 工作面名称
        /// </summary>
        public string WorkingFaceName
        {
            get { return workingFaceName; }
            set { workingFaceName = value; }
        }

        // 坐标
        private CoordinateEntity coordinate;

        public CoordinateEntity Coordinate
        {
            get { return coordinate; }
            set
            {
                coordinate = value;
            }
        }

        // 开工日期
        private DateTime? startDate;

        /// <summary>
        /// 设置或获取开工日期
        /// </summary>
        public DateTime? StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        // 是否掘进完毕
        private int isFinish;

        /// <summary>
        /// 设置或获取是否掘进完毕
        /// </summary>
        public int IsFinish
        {
            get { return isFinish; }
            set { isFinish = value; }
        }

        // 停工日期
        private DateTime? stopDate;

        /// <summary>
        /// 设置或获取停工日期
        /// </summary>
        public DateTime? StopDate
        {
            get { return stopDate; }
            set { stopDate = value; }
        }

        // 队别编号
        private int teamNameID;

        /// <summary>
        /// 设置或获取队别编号
        /// </summary>
        public int TeamNameID
        {
            get { return teamNameID; }
            set { teamNameID = value; }
        }

        // 采区
        private MiningAreaEntity miningArea;

        public MiningAreaEntity MiningArea
        {
            get { return miningArea; }
            set
            {
                miningArea = value;
            }
        }

        // 工作制式
        private string workStyle;

        /// <summary>
        /// 设置或获取工作制式
        /// </summary>
        public string WorkStyle
        {
            get { return workStyle; }
            set { workStyle = value; }
        }

        // 班次
        private string workTime;

        /// <summary>
        /// 设置或获取班次
        /// </summary>
        public string WorkTime
        {
            get { return workTime; }
            set { workTime = value; }
        }

        // 用于暂存关联巷道信息
        public HashSet<TunnelEntity> tunnelSet;

        /// <summary>
        /// 巷道类型
        /// </summary>
        public WorkingfaceTypeEnum WorkingfaceTypeEnum { get; set; }
    }
}