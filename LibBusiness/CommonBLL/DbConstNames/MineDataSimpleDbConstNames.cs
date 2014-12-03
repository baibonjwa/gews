// ******************************************************************
// 概  述：煤层信息数据库常量名
// 作  者：伍鑫
// 创建日期：2014/03/04
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibBusiness.CommonBLL.DbConstNames
{
    public static class MineDataSimpleDbConstNames
    {
        // 表名
        public const string TABLE_NAME = "T_WORK_TIME";

        public const string WORK_TIME_ID = "WORK_TIME_ID";

        public const string WORK_TIME_GROUP_ID = "WORK_TIME_GROUP_ID";

        public const string WORK_TIME_NAME = "WORK_TIME_NAME";

        public const string WORK_TIME_FROM = "WORK_TIME_FROM";

        public const string WORK_TIME_TO = "WORK_TIME_TO";
    }
}
