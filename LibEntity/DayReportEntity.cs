using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    /// <summary>
    /// 回采和掘进日报的基类
    /// </summary>
    public class DayReportEntity
    {
        // 记录ID
        private int id;

        /// <summary>
        /// 记录ID
        /// </summary>
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        // 队别名称
        private int teamNameID;

        /// <summary>
        /// 队别名称
        /// </summary>
        public int TeamNameID
        {
            get { return teamNameID; }
            set { teamNameID = value; }
        }

        // 班次
        private string workTime;
        /// <summary>
        /// 班次
        /// </summary>
        public string WorkTime
        {
            get { return workTime; }
            set { workTime = value; }
        }

        // 工作制式
        private string workTimeStyle;

        /// <summary>
        /// 工作制式
        /// </summary>
        public string WorkTimeStyle
        {
            get { return workTimeStyle; }
            set { workTimeStyle = value; }
        }

        // 工作内容
        private string workInfo;

        /// <summary>
        /// 工作内容
        /// </summary>
        public string WorkInfo
        {
            get { return workInfo; }
            set { workInfo = value; }
        }

        // 进尺
        private double jinChi;

        /// <summary>
        /// 进尺
        /// </summary>
        public double JinChi
        {
            get { return jinChi; }
            set { jinChi = value; }
        }

        // 工作面编号
        private int workingFaceID;

        /// <summary>
        /// 工作面编号
        /// </summary>
        public int WorkingFaceID
        {
            get { return workingFaceID; }
            set { workingFaceID = value; }
        }

        // 日期
        private DateTime dateTime;

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime DateTime
        {
            get { return dateTime; }
            set { dateTime = value; }
        }

        // 填报人
        private string submitter;

        /// <summary>
        /// 填报人
        /// </summary>
        public string Submitter
        {
            get { return submitter; }
            set { submitter = value; }
        }

        // 备注
        private string other;

        /// <summary>
        /// 备注
        /// </summary>
        public string Other
        {
            get { return other; }
            set { other = value; }
        }

        // BID
        private string bindingID;

        /// <summary>
        /// BID
        /// </summary>
        public string BindingID
        {
            get { return bindingID; }
            set { bindingID = value; }
        }
    }
}
