// ******************************************************************
// 概  述：
// 作  者：
// 日  期：
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
    public class TunnelHCEntityNew
    {
        //主键
        private int iD;

        /// <summary>
        /// 设置或获取主键
        /// </summary>
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        // 巷道1ID
        private int tunnelID_ZY;

        /// <summary>
        /// 设置或获取巷道ID
        /// </summary>
        public int TunnelID_ZY
        {
            get { return tunnelID_ZY; }
            set { tunnelID_ZY = value; }
        }

        // 巷道2ID
        private int tunnelID_FY;

        /// <summary>
        /// 设置或获取巷道2ID
        /// </summary>
        public int TunnelID_FY
        {
            get { return tunnelID_FY; }
            set { tunnelID_FY = value; }
        }

        // 巷道3ID
        private int tunnelID_KQY;

        /// <summary>
        /// 设置或获取巷道3ID
        /// </summary>
        public int TunnelID_KQY
        {
            get { return tunnelID_KQY; }
            set { tunnelID_KQY = value; }
        }

        // 其它巷道ID
        private string tunnelID;

        /// <summary>
        /// 设置或获取其它巷道ID
        /// </summary>
        public string TunnelID
        {
            get { return tunnelID; }
            set { tunnelID = value; }
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

        // 开工日期
        private DateTime startDate;

        /// <summary>
        /// 设置或获取开工日期
        /// </summary>
        public DateTime StartDate
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
        private DateTime stopDate;

        /// <summary>
        /// 设置或获取停工日期
        /// </summary>
        public DateTime StopDate
        {
            get { return stopDate; }
            set { stopDate = value; }
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

    }
}
