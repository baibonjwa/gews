// ******************************************************************
// 概  述：预警信息结果查询声明数据库字段名称
// 作  者：秦凯
// 创建日期：2014/03/15
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibBusiness
{
    public class PreWarningResultDBConstNames
    {
        /// <summary>
        /// 数据库名称
        /// </summary>
        public const string TABLE_NAME = "T_EARLY_WARNING_RESULT";

        /// <summary>
        /// 临时数据库名称
        /// </summary>
        public const string TABLE_NAME_TEMP = "T_EARLY_WARNING_RESULT_TEMP";

        /// <summary>
        /// ID
        /// </summary>
        public const string ID = "ID";

        /// <summary>
        /// 巷道ID
        /// </summary>
        public const string TUNNEL_ID = "TUNNEL_ID";

        /// <summary>
        /// 巷道名称
        /// </summary>
        //public const string TUNNEL_NAME = "tunnel_name";

        /// <summary>
        /// 班次
        /// </summary>
        public const string DATE_SHIFT = "SHIFT";

        /// <summary>
        /// 日期
        /// </summary>
        public const string DATA_TIME = "DATE_TIME";

        /// <summary>
        /// 预警点坐标_X
        /// </summary>
        public const string COORDINATE_X = "COORDINATE_X";

        /// <summary>
        /// 预警点坐标_Y
        /// </summary>
        public const string COORDINATE_Y = "COORDINATE_Y";

        /// <summary>
        /// 预警点坐标_Y
        /// </summary>
        public const string COORDINATE_Z = "COORDINATE_Z";

        /// <summary>
        /// 预警类型
        /// </summary>
        public const string WARNING_TYPE = "WARNING_TYPE";

        /// <summary>
        /// 预警结果
        /// </summary>
        public const string WARNING_RESULT = "WARNING_RESULT";

        /// <summary>
        /// 瓦斯
        /// </summary>
        public const string GAS = "GAS";

        /// <summary>
        /// 煤层
        /// </summary>
        public const string COAL = "COAL";

        /// <summary>
        /// 地质
        /// </summary>
        public const string GEOLOGY = "GEOLOGY";

        /// <summary>
        /// 通风
        /// </summary>
        public const string VENTILATION = "VENTILATION";

        /// <summary>
        /// 管理
        /// </summary>
        public const string MANAGEMENT = "MANAGEMENT";

        public const string HANDLE_STATUS = "HANDLE_STATUS";
    }
}
