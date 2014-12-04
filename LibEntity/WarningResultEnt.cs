// ******************************************************************
// 概  述：超限、突出预警结果实体
// 作  者：秦凯
// 创建日期：2014/03/18
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibCommon;

namespace LibEntity
{
    public class WarningResultEnt
    {
        private string _strID = LibCommon.Const.INVALID_ID.ToString();
        /// <summary>
        /// 数据库中的主键ID
        /// </summary>
        public string ID
        {
            get { return _strID; }
            set { _strID = value; }
        }

        private int _strWaringResult = 4;
        /// <summary>
        /// 预警结果
        /// </summary>
        public int WarningResult
        {
            get { return _strWaringResult; }
            set { _strWaringResult = value; }
        }

        private int _strGas = 4;
        /// <summary>
        /// 瓦斯
        /// </summary>
        public int Gas
        {
            get { return _strGas; }
            set { _strGas = value; }
        }

        private int _strCoal = 4;
        /// <summary>
        /// 煤层
        /// </summary>
        public int Coal
        {
            get { return _strCoal; }
            set { _strCoal = value; }
        }

        private int _strGeology = 4;
        /// <summary>
        /// 地质
        /// </summary>
        public int Geology
        {
            get { return _strGeology; }
            set { _strGeology = value; }
        }

        private int _strVentilation = 4;
        /// <summary>
        /// 通风
        /// </summary>
        public int Ventilation
        {
            get { return _strVentilation; }
            set { _strVentilation = value; }
        }

        private int _strManagement = 4;
        /// <summary>
        /// 管理
        /// </summary>
        public int Management
        {
            get { return _strManagement; }
            set { _strManagement = value; }
        }

        private int _iOther = 4;
        /// <summary>
        /// 其他
        /// </summary>
        public int Other
        {
            get { return _iOther; }
            set { _iOther = value; }
        }

        string _strHandleStatus;
        /// <summary>
        /// 处理状态
        /// </summary>
        public string HandleStatus
        {
            set { _strHandleStatus = value; }
            get { return _strHandleStatus; }
        }
    }
}
