// ******************************************************************
// 概  述：历史预警信息结果声明常量名
// 作  者：秦凯
// 创建日期：2014/03/22
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
    public class PreWarningHistroyResultQueryDBConstNames
    {
        //表名
        public const string TABLE_NAME = "T_EARLY_WARNING_RESULT_TEMP";
        //ID
        public const string ID = "id";
        //巷道ID
        public const string TUNNEL_ID = "tunnel_id";
        //巷道名称
        public const string TUNNEL_NAME = "tunnel_name";
        //班次
        public const string DATE_SHIFT = "shift";
        //日期
        public const string DATA_TIME = "date_time";
        //超限
        public const string OVER_LIMIT = "over_limit";
        //突出
        public const string OUTBURST = "outburst";

        public const string OVER_LIMIT_GAS = "over_limit_gas";
        public const string OVER_LIMIT_COAL = "over_limit_coal";
        public const string OVER_LIMIT_GEOLOGY = "over_limit_geology";
        public const string OVER_LIMIT_VENTILATION = 
            "over_limit_ventilation";
        public const string OVER_LIMIT_MANAGEMENT = 
            "over_limit_management";

        public const string OUTBURST_GAS = "outburst_gas";
        public const string OUTBURST_COAL = "outburst_coal";
        public const string OUTBURST_GEOLOGY = "outburst_geology";
        public const string OUTBURST_VENTILATION = "outburst_ventilation";
        public const string OUTBURST_MANAGEMENT = "outburst_management";   
    }
}
