// ******************************************************************
// 概  述：预警结果实体
// 作  者：秦凯
// 创建日期：2014/03/18
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************

using System;
using LibCommon;

namespace LibEntity
{
    public class PreWarningResultQueryWithWorkingfaceEnt
    {
        private string _strDateShift = "";
        private string _strDateTime = "";
        private WarningResultEnt _strOutBrustWarningResult = new WarningResultEnt();
        private WarningResultEnt _strOverLimitWarningResult = new WarningResultEnt();
        private int _workingfaceID = Const.INVALID_ID;

        /// <summary>
        ///     工作面名称
        /// </summary>
        public string WorkingfaceName { get; set; }

        /// <summary>
        ///     工作面ID
        /// </summary>
        public int WorkingfaceID
        {
            get { return _workingfaceID; }
            set { _workingfaceID = value; }
        }

        /// <summary>
        ///     巷道ID
        /// </summary>
        public int TunnelId { get; set; }

        /// <summary>
        ///     巷道名称
        /// </summary>
        public String TunnelName { get; set; }


        /// <summary>
        ///     班次
        /// </summary>
        public string Date_Shift
        {
            get { return _strDateShift; }
            set { _strDateShift = value; }
        }

        /// <summary>
        ///     日期
        /// </summary>
        public string DateTime
        {
            get { return _strDateTime; }
            set { _strDateTime = value; }
        }

        /// <summary>
        ///     超限预警
        /// </summary>
        public WarningResultEnt OverLimitWarningResult
        {
            get { return _strOverLimitWarningResult; }
            set { _strOverLimitWarningResult = value; }
        }

        /// <summary>
        ///     突出预警
        /// </summary>
        public WarningResultEnt OutBrustWarningResult
        {
            get { return _strOutBrustWarningResult; }
            set { _strOutBrustWarningResult = value; }
        }
    }
}