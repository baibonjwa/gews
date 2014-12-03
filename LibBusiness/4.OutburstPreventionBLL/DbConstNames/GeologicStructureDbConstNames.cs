// ******************************************************************
// 概  述：井下数据地质构造信息数据库常量名
// 作  者：宋英杰
// 创建日期：2014/03/25
// 版本号：1.0
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibBusiness
{
    public class GeologicStructureDbConstNames
    {
        public const string TABLE_NAME = "T_GEOLOGIC_STRUCTURE";

        #region 预警数据通用数据库字段名
        //巷道编号
        public const string TUNNEL_ID = "TUNNEL_ID";

        //坐标X
        public const string X = "COORDINATE_X";

        //坐标Y
        public const string Y = "COORDINATE_Y";

        //坐标Z
        public const string Z = "COORDINATE_Z";

        //工作制式
        public const string WORK_STYLE = "WORK_STYLE";

        //班次
        public const string WORK_TIME = "WORK_TIME";

        //队别名称
        public const string TEAM_NAME = "TEAM_NAME";

        //填报人
        public const string SUBMITTER = "SUBMITTER";

        //日期
        public const string DATETIME = "DATETIME";
        #endregion

        public const string ID = "ID";

        //无计划揭露构造
        public const string NO_PLAN_STRUCTURE = "NO_PLAN_STRUCTURE";

        //过构造时措施无效
        public const string PASSED_STRUCTURE_RULE_INVALID = "PASSED_STRUCTURE_RULE_INVALID";

        //黄色预警措施无效
        public const string YELLOW_RULE_INVALID = "YELLOW_RULE_INVALID";

        //顶板破碎
        public const string ROOF_BROKEN = "ROOF_BROKEN";

        //煤层松软
        public const string COAL_SEAM_SOFT = "COAL_SEAM_SOFT";

        //工作面煤层处于分叉、合层状态
        public const string COAL_SEAM_BRANCH = "COAL_SEAM_BRANCH";

        //顶板条件发生变化
        public const string ROOF_CHANGE = "ROOF_CHANGE";

        //工作面夹矸突然变薄或消失
        public const string GANGUE_DISAPPEAR = "GANGUE_DISAPPEAR";

        //夹矸位置急剧变化
        public const string GANGUE_LOCATION_CHANGE = "GANGUE_LOCATION_CHANGE";
    }
}
