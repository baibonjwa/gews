// ******************************************************************
// 概  述：历史预警详细信息数据库逻辑
// 作  者：秦凯
// 创建日期：2014/03/25
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibDatabase;
using System.Data;
using LibEntity;
using LibCommon;

namespace LibBusiness
{
    public class PreWarningResultDetailsQueryBLL
    {
        //定义成员变量,避免数据库重复开启关闭
        static ManageDataBase _database = new 
            ManageDataBase(DATABASE_TYPE.WarningManagementDB);

        /// <summary>
        /// 开启数据库
        /// </summary>
        public static void OpenDatabase()
        {
            _database.Open();
        }
        /// <summary>
        /// 关闭数据库
        /// </summary>
        public static void CloseDatabase()
        {
            _database.Close();
        }

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <param name="date"></param>
        /// <param name="date_shift"></param>
        /// <param name="tunnel_ID"></param>
        /// <returns></returns>
        public static List<PreWarningResultQuery> 
            SelectPreWarningResultInOneDateShift(string date, string date_shift,
            string tunnel_ID, string warning_type)
        {
            List<PreWarningResultQuery> ents = new 
                List<PreWarningResultQuery>();
            DateTime[] times = WorkingTime.GetDateShiftTimes(date_shift);
            if (times.Length != 2)
            {
                return null;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM(");
            strSql.Append("SELECT ROW_NUMBER() OVER (ORDER BY ");
            strSql.Append(PreWarningResultDBConstNames.DATA_TIME);
            strSql.Append("  DESC) AS rowID ");
            strSql.Append(" , * FROM ");
            strSql.Append(PreWarningResultDBConstNames.TABLE_NAME);
            strSql.Append(" WHERE ");
            strSql.Append(PreWarningResultDBConstNames.DATA_TIME);
            strSql.Append(" >= ");
            strSql.Append("'" + times[0] + "'");
            strSql.Append(" AND ");
            strSql.Append(PreWarningResultDBConstNames.DATA_TIME);
            strSql.Append(" <= ");
            strSql.Append("'" + times[1] + "'");
            strSql.Append(" AND ");
            strSql.Append(PreWarningResultDBConstNames.TUNNEL_ID);
            strSql.Append(" = ");
            strSql.Append(tunnel_ID);
            strSql.Append(" AND ");
            strSql.Append("( ");
            strSql.Append(PreWarningResultDBConstNames.WARNING_RESULT);
            strSql.Append(" < ");
            strSql.Append((int)LibCommon.WarningResult.GREEN);
            strSql.Append(") )AS T");

            DataSet ds = 
                _database.ReturnDSNotOpenAndClose(strSql.ToString());
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ents = new List<PreWarningResultQuery>();
                    int rowCount = dt.Rows.Count;
                    for (int i = 0; i < rowCount; i++)
                    {
                        #region 实体赋值
                        PreWarningResultQuery ent = new 
                            PreWarningResultQuery();
                        ent.TunelName = GetTunelNameByTunelID(tunnel_ID);
                        ent.DateTime = 
                            Convert.ToDateTime(dt.Rows[i][PreWarningResultDBConstNames.DATA_TIME]);
                        ent.Date_Shift = 
                            dt.Rows[i][PreWarningResultDBConstNames.DATE_SHIFT].ToString();

                        int defultValue = (int)WarningResult.NULL;
                        if 
                            (dt.Rows[i][PreWarningResultDBConstNames.WARNING_TYPE].ToString() 
                            == LibCommon.WarningType.OVER_LIMIT.ToString())
                        {
                            ent.OverLimitWarningResult.ID = 
                                dt.Rows[i][PreWarningResultDBConstNames.ID].ToString();
                            int.TryParse(dt.Rows[i][LibBusiness.PreWarningResultDBConstNames.WARNING_RESULT].ToString(),
                                out defultValue);
                            ent.OverLimitWarningResult.WarningResult = 
                                defultValue;
                            defultValue = (int)WarningResult.NULL;
                            int.TryParse(dt.Rows[i][LibBusiness.PreWarningResultDBConstNames.GAS].ToString(),
                                out defultValue);
                            ent.OverLimitWarningResult.Gas = defultValue;
                            defultValue = (int)WarningResult.NULL;
                            int.TryParse(dt.Rows[i][LibBusiness.PreWarningResultDBConstNames.COAL].ToString(),
                                out defultValue);
                            ent.OverLimitWarningResult.Coal = defultValue;
                            defultValue = (int)WarningResult.NULL;
                            int.TryParse(dt.Rows[i][LibBusiness.PreWarningResultDBConstNames.GEOLOGY].ToString(),
                                out defultValue);
                            ent.OverLimitWarningResult.Geology = 
                                defultValue;
                            defultValue = (int)WarningResult.NULL;
                            int.TryParse(dt.Rows[i][LibBusiness.PreWarningResultDBConstNames.VENTILATION].ToString(),
                                out defultValue);
                            ent.OverLimitWarningResult.Ventilation = 
                                defultValue;
                            defultValue = (int)WarningResult.NULL;
                            int.TryParse(dt.Rows[i][LibBusiness.PreWarningResultDBConstNames.MANAGEMENT].ToString(),
                                out defultValue);
                            ent.OverLimitWarningResult.Management = 
                                defultValue;
                        }
                        if 
                            (dt.Rows[i][PreWarningResultDBConstNames.WARNING_TYPE].ToString() 
                            == LibCommon.WarningType.OUTBURST.ToString())
                        {
                            ent.OutBrustWarningResult.ID = 
                                dt.Rows[i][PreWarningResultDBConstNames.ID].ToString();
                            int.TryParse(dt.Rows[i][LibBusiness.PreWarningResultDBConstNames.WARNING_RESULT].ToString(),
                                out defultValue);
                            ent.OutBrustWarningResult.WarningResult = 
                                defultValue;
                            defultValue = (int)WarningResult.NULL;
                            int.TryParse(dt.Rows[i][LibBusiness.PreWarningResultDBConstNames.GAS].ToString(),
                                out defultValue);
                            ent.OutBrustWarningResult.Gas = defultValue;
                            defultValue = (int)WarningResult.NULL;
                            int.TryParse(dt.Rows[i][LibBusiness.PreWarningResultDBConstNames.COAL].ToString(),
                                out defultValue);
                            ent.OutBrustWarningResult.Coal = defultValue;
                            defultValue = (int)WarningResult.NULL;
                            int.TryParse(dt.Rows[i][LibBusiness.PreWarningResultDBConstNames.GEOLOGY].ToString(),
                                out defultValue);
                            ent.OutBrustWarningResult.Geology = 
                                defultValue;
                            defultValue = (int)WarningResult.NULL;
                            int.TryParse(dt.Rows[i][LibBusiness.PreWarningResultDBConstNames.VENTILATION].ToString(),
                                out defultValue);
                            ent.OutBrustWarningResult.Ventilation = 
                                defultValue;
                            defultValue = (int)WarningResult.NULL;
                            int.TryParse(dt.Rows[i][LibBusiness.PreWarningResultDBConstNames.MANAGEMENT].ToString(),
                                out defultValue);
                            ent.OutBrustWarningResult.Management = 
                                defultValue;
                        }
                        ents.Add(ent);
                        #endregion
                    }
                }
            }
            return ents;
        }

        //该说明适用于方法“SelectPreWarningResultCountInOneDateShift”、“SelectPreWarningResultInOneDateShift”。
        //即此说明字段相邻的两个方法。
        //该方法同时应用于查询历史信息中的详细信息和查询最新预警结果中的详细信息。
        //If条件判断中给，利用是否限制预警、类型来区别属于何种查询方式。
        //两种查询的详细信息中分别对应着不同的SQL语句。
        //在历史预警信息的详细信息中，形参“date”存放某一日期，如“2014-03-08”。
        //在最新预警信息的详细信息中，借用形参“date”存放某一准确时间点，如“2014-03-08 11:31:00.000”。

        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="date"></param>
        /// <param name="date_shift"></param>
        /// <param name="tunnel_ID"></param>
        /// <returns></returns>
        public static int SelectPreWarningResultCountInOneDateShift(string 
            date,
            string date_shift, string tunnel_ID, string warning_type, bool 
                addWarningFilter)
        {
            int rowCount = 0;
            StringBuilder strSql = null;

            //用以限制是否同时显示超限和突出预警
            if (addWarningFilter)
            {
                strSql = new StringBuilder();
                DateTime[] times = 
                    WorkingTime.GetDateShiftTimes(date_shift);
                if (times.Length != 2)
                {
                    return 0;
                }
                strSql.Append("SELECT ");
                strSql.Append(PreWarningResultDBConstNames.DATA_TIME);
                strSql.Append(" FROM ");
                strSql.Append(PreWarningResultDBConstNames.TABLE_NAME);
                strSql.Append(" WHERE ");
                strSql.Append(PreWarningResultDBConstNames.DATA_TIME);
                strSql.Append(" >= ");
                strSql.Append("'" + times[0] + "'");
                strSql.Append(" AND ");
                strSql.Append(PreWarningResultDBConstNames.DATA_TIME);
                strSql.Append(" <= ");
                strSql.Append("'" + times[1] + "'");
                strSql.Append(" AND ");
                strSql.Append(PreWarningResultDBConstNames.TUNNEL_ID);
                strSql.Append(" = ");
                strSql.Append(tunnel_ID);
                //一下的代码用以限制是否只显示报警的记录,Mark By QinKai
                strSql.Append(" AND ");
                strSql.Append(PreWarningResultDBConstNames.WARNING_RESULT);
                strSql.Append(" < ");
                strSql.Append((int)LibCommon.WarningResult.GREEN);
                //结束标记
                strSql.Append(" AND ");
                strSql.Append(PreWarningResultDBConstNames.WARNING_TYPE);
                strSql.Append(" = ");
                strSql.Append("'" + warning_type + "'");
            }
            else
            {
                strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(PreWarningResultDBConstNames.DATA_TIME);
                strSql.Append(" FROM ");
                strSql.Append(PreWarningResultDBConstNames.TABLE_NAME);
                strSql.Append(" WHERE ");
                strSql.Append(PreWarningResultDBConstNames.DATA_TIME);
                strSql.Append(" = ");
                strSql.Append("'" + date + "'");
                strSql.Append(" AND ");
                strSql.Append(PreWarningResultDBConstNames.TUNNEL_ID);
                strSql.Append(" = ");
                strSql.Append(tunnel_ID);
                strSql.Append(" AND ");
                strSql.Append(PreWarningResultDBConstNames.WARNING_RESULT);
                strSql.Append(" < ");
                strSql.Append((int)LibCommon.WarningResult.GREEN);
            }
            DataSet ds = 
                _database.ReturnDSNotOpenAndClose(strSql.ToString());
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    rowCount = dt.Rows.Count;
                }
            }
            return rowCount;
        }

        /// <summary>
        /// 获取班次中的时间范围，返回值例：起始时间2014-03-18 08:00:00.000，结束时间2014-03-18 16:00:00.000
        /// </summary>
        /// <param name="strWorkTimeName"></param>
        /// <param name="strDate"></param>
        /// <returns></returns>
        //public static string[] GetDateShiftTimes(string strWorkTimeName, string strDate)
        //{
        //    //select WORK_TIME_FROM,WORK_TIME_TO from T_WORK_TIME where WORK_TIME_NAME ='早班'
        //    string[] times = null;
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("SELECT ");
        //    strSql.Append(LibBusiness.WorkTimeDbConstNames.WORK_TIME_FROM + ",");
        //    strSql.Append(LibBusiness.WorkTimeDbConstNames.WORK_TIME_TO);
        //    strSql.Append(" FROM ");
        //    strSql.Append(LibBusiness.WorkTimeDbConstNames.TABLE_NAME);
        //    strSql.Append(" WHERE ");
        //    strSql.Append(LibBusiness.WorkTimeDbConstNames.WORK_TIME_NAME);
        //    strSql.Append(" = ");
        //    strSql.Append("'" + strWorkTimeName + "'");
        //    DataSet ds = _database.ReturnDSNotOpenAndClose(strSql.ToString());
        //    if (ds != null)
        //    {
        //        DataTable dt = ds.Tables[0];
        //        if (dt != null)
        //        {
        //            //2014-03-05 15:43:45.000
        //            if (dt.Rows.Count > 0)
        //            {
        //                times = new string[2];
        //                times[0] = strDate + " " + dt.Rows[0][LibBusiness.WorkTimeDbConstNames.WORK_TIME_FROM].ToString();
        //                times[1] = strDate + " " + dt.Rows[0][LibBusiness.WorkTimeDbConstNames.WORK_TIME_TO].ToString();
        //            }
        //        }
        //    }
        //    return times;
        //}

        /// <summary>
        /// 根据tunelID,查找巷道名称。若属于回采巷道，返回工作面名称
        /// </summary>
        /// <param name="tunelId"></param>
        /// <returns></returns>
        public static string GetTunelNameByTunelID(string tunelId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(TunnelInfoDbConstNames.TUNNEL_NAME + ",");
            strSql.Append(TunnelInfoDbConstNames.TUNNEL_TYPE + ",");
            strSql.Append(TunnelInfoDbConstNames.WORKINGFACE_ID);
            strSql.Append(" FROM ");
            strSql.Append(TunnelInfoDbConstNames.TABLE_NAME);
            strSql.Append(" WHERE ");
            strSql.Append(TunnelInfoDbConstNames.ID);
            strSql.Append(" = ");
            strSql.Append(tunelId);
            DataTable dt = 
                _database.ReturnDSNotOpenAndClose(strSql.ToString()).Tables[0];
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    //判断是否属于回采巷道
                    if 
                        (TunnelUtils.isStoping((TunnelTypeEnum)Convert.ToInt32(dt.Rows[0][LibBusiness.TunnelInfoDbConstNames.TUNNEL_TYPE])))
                    {
                        //获取工作面名称
                        int workFaceId;
                        int.TryParse(dt.Rows[0][TunnelInfoDbConstNames.WORKINGFACE_ID].ToString(),
                            out workFaceId);
                        WorkingFace workingFace = 
                            WorkingFace.Find(workFaceId);
                        if (workingFace != null)
                        {
                            return workingFace.WorkingFaceName;
                        }
                    }
                    else
                    {
                        return 
                            dt.Rows[0][LibBusiness.TunnelInfoDbConstNames.TUNNEL_NAME].ToString();
                    }
                }
            }
            return "";
        }

        /// <summary>
        /// 根据规则ID获取预警规则实体
        /// <param name="ruleId">规则Id</param>
        /// </summary>
        public static PreWarningRules 
            GetPreWarningRulesEntityByRuleId(string ruleId)
        {
            try
            {
                string sql = "SELECT " +
                    PreWarningRulesDbConstNames.RULE_CODE + "," +
                    PreWarningRulesDbConstNames.RULE_TYPE + "," +
                    PreWarningRulesDbConstNames.WARNING_TYPE + "," +
                    PreWarningRulesDbConstNames.WARNING_LEVEL + "," +
                    PreWarningRulesDbConstNames.SUITABLE_LOCATION + "," +
                    PreWarningRulesDbConstNames.RULE_DESCRIPTION + "," +
                    PreWarningRulesDbConstNames.INDICATOR_TYPE + "," +
                    PreWarningRulesDbConstNames.OPERATOR + "," +
                    PreWarningRulesDbConstNames.MODIFY_DATE + "," +
                    PreWarningRulesDbConstNames.REMARKS + " FROM " +
                    PreWarningRulesDbConstNames.TABLE_NAME + " WHERE " +
                    PreWarningRulesDbConstNames.RULE_ID + "=" + 
                        ruleId.ToString();
                DataSet ds = _database.ReturnDSNotOpenAndClose(sql);
                int n = ds.Tables[0].Rows.Count;
                if (n != 1)
                {
                    Alert.alert("规则编码不唯一！");
                    return null;
                }
                else if (n < 1)
                {
                    Alert.alert("规则编码不存在！");
                    return null;
                }
                int iRuleID = -1;
                int.TryParse(ruleId, out iRuleID);
                PreWarningRules ret = new PreWarningRules(iRuleID);
                ret.RuleCode = 
                    ds.Tables[0].Rows[0][PreWarningRulesDbConstNames.RULE_CODE].ToString();
                ret.RuleType = 
                    ds.Tables[0].Rows[0][PreWarningRulesDbConstNames.RULE_TYPE].ToString();
                ret.WarningType = 
                    ds.Tables[0].Rows[0][PreWarningRulesDbConstNames.WARNING_TYPE].ToString();
                ret.WarningLevel = 
                    ds.Tables[0].Rows[0][PreWarningRulesDbConstNames.WARNING_LEVEL].ToString();
                ret.SuitableLocation = 
                    ds.Tables[0].Rows[0][PreWarningRulesDbConstNames.SUITABLE_LOCATION].ToString();
                ret.RuleDescription = 
                    ds.Tables[0].Rows[0][PreWarningRulesDbConstNames.RULE_DESCRIPTION].ToString();
                ret.IndicatorType = 
                    ds.Tables[0].Rows[0][PreWarningRulesDbConstNames.INDICATOR_TYPE].ToString();
                ret.Operator = 
                    ds.Tables[0].Rows[0][PreWarningRulesDbConstNames.OPERATOR].ToString();
                ret.ModifyDate = 
                    Convert.ToDateTime(ds.Tables[0].Rows[0][PreWarningRulesDbConstNames.MODIFY_DATE].ToString());
                ret.Remarks = 
                    ds.Tables[0].Rows[0][PreWarningRulesDbConstNames.REMARKS].ToString();
                return ret;
            }
            catch (Exception ex)
            {
                Alert.alert(ex.Message + 
                    " 函数名：GetPreWarningRulesEntityByRuleCode");
                return null;
            }
        }

        /// <summary>
        /// 获取巷道绑定的规则编码对应的参数(单条规则编码)
        /// </summary>
        /// <param name="tunnelID">巷道ID</param>
        /// <param name="ruleId">规则ID</param>
        /// <returns>当巷道未绑定规则编码时，返回null</returns>
        public static RuleInfo GetTunnelBindingParamsByRuleId(int tunnelID, 
            int ruleId)
        {
            RuleInfo[] all = GetTunnelBindingRuleIdsAndParams(tunnelID);
            if (all == null)
            {
                return null;
            }
            int n = all.Length;
            for (int i = 0; i < n; i++)
            {
                if (all[i].Id == ruleId)
                {
                    return all[i];
                }
            }
            return null;
        }

        /// <summary>
        /// 获取巷道绑定的所有规则编码和参数
        /// </summary>
        /// <param name="preWarningParamsInDB">数据库中保存的预警规则参数</param>
        /// <returns>规则参数与编码单元</returns>
        /// <summary>
        public static RuleInfo[] GetTunnelBindingRuleIdsAndParams(int 
            tunnelID)
        {
            //获取巷道绑定的规则编码字符串和预警参数字符串
            string sql = "SELECT " +
                TunnelInfoDbConstNames.RULE_IDS + ", " +
                TunnelInfoDbConstNames.PRE_WARNING_PARAMS + " FROM " +
                TunnelInfoDbConstNames.TABLE_NAME + " WHERE " +
                TunnelInfoDbConstNames.ID + "=" + tunnelID;

            DataSet ds = _database.ReturnDSNotOpenAndClose(sql);

            string allRuleIds = "";
            if (ds.Tables[0].Rows.Count < 1)
            {
                //Alert.alert("数据库中无对应巷道！");
                return null;
            }
            allRuleIds = 
                ds.Tables[0].Rows[0][TunnelInfoDbConstNames.RULE_IDS].ToString();
            string allParams = 
                ds.Tables[0].Rows[0][TunnelInfoDbConstNames.PRE_WARNING_PARAMS].ToString();

            if (allRuleIds == "")//未绑定规则编码
            {
                return null;
            }
            RuleInfo[] ret = ParseRuleIdsAndParams(allRuleIds, allParams);

            return ret;
        }

        /// </summary>
        /// 解析数据库中存储的规则编码和预警参数(所有规则编码)
        /// <param name="ruleCodes">规则编码字符串</param>
        /// <param name="preWarningParams">预警参数字符串</param>
        /// <returns>规则编码与参数单元,无规则编码和参数则返回null；注意有规则编码但无参数的情况！(此时返回值当中规则编码含有值，但预警参数为null)</returns>
        private static RuleInfo[] ParseRuleIdsAndParams(string ruleIds, 
            string preWarningParams)
        {
            //规则编码为空
            if (ruleIds == "" && preWarningParams == "")
            {
                return null;
            }
            try
            {

                char[] seperator = 
                    Const_WM.GetPreWarningRuleIdAndParamsSeperatorArr();

                string[] rules = ruleIds.Split(seperator);
                string[] warningParams = preWarningParams.Split(seperator);
                RuleInfo[] ret = new RuleInfo[rules.Length - 1];
                for (int i = 0; i < rules.Length - 1; i++)
                {
                    //ret[i] = new RuleIdAndParamInfo(ret[i].RuleId);
                    ret[i] = new RuleInfo(int.Parse(rules[i]));
                    if (warningParams[i] != "")//该规则编码含有参数
                    {
                        ret[i].PreWarningParams = 
                            PreWarningRules.ParseRuleDescriptionOfOneRuleId(warningParams[i]);
                    }
                    else//该规则编码无对应参数
                    {
                        ret[i].PreWarningParams = null;
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                Alert.alert(ex.Message);
            }
            return null;
        }

    }
}
