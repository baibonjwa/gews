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
    public class PreWarningResultQuery
    {
        private int _iTunnelID = Const.INVALID_ID;
        private string _strDateShift = "";
        private DateTime _strDateTime;
        private WarningResultEnt _strOutBrustWarningResult = new WarningResultEnt();
        private WarningResultEnt _strOverLimitWarningResult = new WarningResultEnt();
        private string _strTunelName = "";

        /// <summary>
        ///     工作面名称
        /// </summary>
        public string WorkingfaceName { get; set; }

        /// <summary>
        ///     工作面ID
        /// </summary>
        public int WorkingfaceId { get; set; }

        /// <summary>
        ///     巷道名称
        /// </summary>
        public string TunelName
        {
            get { return _strTunelName; }
            set { _strTunelName = value; }
        }

        /// <summary>
        ///     设置巷道ID
        /// </summary>
        public int TunnelID
        {
            get { return _iTunnelID; }
            set { _iTunnelID = value; }
        }


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
        public DateTime DateTime
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