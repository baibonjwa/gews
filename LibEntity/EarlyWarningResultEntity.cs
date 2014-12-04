using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    /// <summary>
    /// 预警结果
    /// </summary>
    public class EarlyWarningResultEntity
    {
        string _strWorkingFaceName;
        /// <summary>
        /// 工作面名称
        /// </summary>
        public string WarkingFaceName
        {
            set { _strWorkingFaceName = value; }
            get { return _strWorkingFaceName; }
        }

        List<string> _warningIDList;
        /// <summary>
        /// 预警ID--对应预警结果表中的ID（主键）
        /// </summary>
        public List<string> WarningIDList
        {
            set { _warningIDList = value; }
            get { return _warningIDList; }
        }

        string _strTunnelID;
        /// <summary>
        /// 巷道ID
        /// </summary>
        public string TunnelID
        {
            set { _strTunnelID = value; }
            get { return _strTunnelID; }
        }

        string _strDateTime;
        /// <summary>
        /// 日期
        /// </summary>
        public string DateTime
        {
            set { _strDateTime = value; }
            get { return _strDateTime; }
        }

        string _strDateShift;
        /// <summary>
        /// 班次
        /// </summary>
        public string DateShift
        {
            set { _strDateShift = value; }
            get { return _strDateShift; }
        }
    }
}
