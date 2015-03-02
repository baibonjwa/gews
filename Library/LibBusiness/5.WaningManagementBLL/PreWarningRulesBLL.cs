// ******************************************************************
// 概  述：预警规则业务逻辑
// 作  者：杨小颖
// 创建日期：2013/12/11
// 版本号：1.0
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibEntity;
using LibDatabase;
using System.Data;
using LibCommon;
using System.Collections;


namespace LibBusiness
{
    public class PreWarningRulesBLL
    {
        public static bool InsertPreWarningRulesInfo(PreWarningRules ent)
        {
            //MARK FIELD
            string sql = "INSERT INTO " + 
                PreWarningRulesDbConstNames.TABLE_NAME + "(" +
                PreWarningRulesDbConstNames.RULE_CODE + ", " +
                PreWarningRulesDbConstNames.RULE_TYPE + ", " +
                PreWarningRulesDbConstNames.WARNING_TYPE + ", " +
                PreWarningRulesDbConstNames.WARNING_LEVEL + ", " +
                PreWarningRulesDbConstNames.SUITABLE_LOCATION + ", " +
                PreWarningRulesDbConstNames.RULE_DESCRIPTION + ", " +
                PreWarningRulesDbConstNames.INDICATOR_TYPE + ", " +
                PreWarningRulesDbConstNames.OPERATOR + ", " +
                PreWarningRulesDbConstNames.MODIFY_DATE + ", " +
                PreWarningRulesDbConstNames.REMARKS + ", " +
                PreWarningRulesDbConstNames.BINDING_TABLE_NAME + ", " +
                PreWarningRulesDbConstNames.BINDING_COLUMN_NAME + ", " +
                PreWarningRulesDbConstNames.USE_TYPE + ", " +
                PreWarningRulesDbConstNames.BINDING_SINGLERULES+ ")" + 
                    " VALUES('" +
                ent.RuleCode + "','" +
                ent.RuleType + "','" +
                ent.WarningType + "','" +
                ent.WarningLevel + "','" +
                ent.SuitableLocation + "','" +
                ent.RuleDescription + "','" +
                ent.IndicatorType + "','" +//MARK FIELD
                ent.Operator + "','" +
                ent.ModifyDate + "','" +
                ent.Remarks + "','" +
                ent.BindingTableName + "','" +
                ent.BindingColumnName + "','" +
                ent.UseType + "','" +
                ent.StrBindingSingleRuleName + "')";
            ManageDataBase db = new 
                ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            return db.OperateDB(sql);
        }

        #region 按适用位置查询
        /// <summary>
        /// 适用于回采面预警规则 (仅适用于回采+掘进和回采通用）
        /// </summary>
        /// <returns></returns>
        public static DataSet selectHuiCaiWarningRules()
        {
            //string sql = "SELECT * FROM T_PRE_WARNING_RULES";
            string sql = "SELECT * FROM " + 
                PreWarningRulesDbConstNames.TABLE_NAME +
                " WHERE " + PreWarningRulesDbConstNames.SUITABLE_LOCATION + 
                    "='" + Const_WM.SUITABLE_LOCATION_HUI_CAI + "' OR " +
                PreWarningRulesDbConstNames.SUITABLE_LOCATION + "='" + 
                    Const_WM.SUITABLE_LOCATION_JUE_JIN_HUI_CAI_COMMON + 
                    "' ORDER BY " + 
                    PreWarningRulesDbConstNames.SUITABLE_LOCATION;
            ManageDataBase db = new 
                ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            return db.ReturnDS(sql);
        }

        /// <summary>
        /// 适用于掘进面预警规则 (仅适用于掘进+掘进和回采通用）
        /// </summary>
        /// <returns></returns>
        public static DataSet selectJueJinWarningRules()
        {
            string sql = "SELECT * FROM " + 
                PreWarningRulesDbConstNames.TABLE_NAME +
                " WHERE " + PreWarningRulesDbConstNames.SUITABLE_LOCATION + 
                    "='" + Const_WM.SUITABLE_LOCATION_JUE_JIN + "' OR " +
                PreWarningRulesDbConstNames.SUITABLE_LOCATION + "='" + 
                    Const_WM.SUITABLE_LOCATION_JUE_JIN_HUI_CAI_COMMON + 
                    "' ORDER BY " + 
                    PreWarningRulesDbConstNames.SUITABLE_LOCATION;
            ManageDataBase db = new 
                ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            return db.ReturnDS(sql);
        }

        /// <summary>
        /// 同时适用掘进面和回采面的预警规则
        /// </summary>
        /// <returns></returns>
        public static DataSet selectJueJinHuiCaiCommonRules()
        {
            string sql = "SELECT * FROM " + 
                PreWarningRulesDbConstNames.TABLE_NAME +
            " WHERE " + PreWarningRulesDbConstNames.SUITABLE_LOCATION + 
                "='" + Const_WM.SUITABLE_LOCATION_JUE_JIN_HUI_CAI_COMMON +
            "' ORDER BY " + PreWarningRulesDbConstNames.SUITABLE_LOCATION;
            ManageDataBase db = new 
                ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            return db.ReturnDS(sql);
        }

        /// <summary>
        /// 其他地点
        /// </summary>
        /// <returns></returns>
        public static DataSet selectOthersRules()
        {
            string sql = "SELECT * FROM " + 
                PreWarningRulesDbConstNames.TABLE_NAME +
            " WHERE " + PreWarningRulesDbConstNames.SUITABLE_LOCATION + 
                "='" + Const_WM.SUITABLE_LOCATION_OTHERS +
            "' ORDER BY " + PreWarningRulesDbConstNames.SUITABLE_LOCATION;
            ManageDataBase db = new 
                ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            return db.ReturnDS(sql);
        }
        #endregion

        #region 按预警类型查询
        /// <summary>
        /// 查询所有超限预警规则
        /// </summary>
        /// <returns></returns>
        public static DataSet selectChaoXianWarningRules()
        {
            string sql = "SELECT * FROM " + 
                PreWarningRulesDbConstNames.TABLE_NAME +
                " WHERE " + PreWarningRulesDbConstNames.WARNING_TYPE + "='" 
                    + Const_WM.WARNING_TYPE_OUT_OF_LIMIT + "' ORDER BY " + 
                    PreWarningRulesDbConstNames.WARNING_TYPE;
            ManageDataBase db = new 
                ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            return db.ReturnDS(sql);
        }

        /// <summary>
        /// 查询所有突出预警规则
        /// </summary>
        /// <returns></returns>
        public static DataSet selectTuChuWarningRules()
        {
            string sql = "SELECT * FROM " + 
                PreWarningRulesDbConstNames.TABLE_NAME +
                " WHERE " + PreWarningRulesDbConstNames.WARNING_TYPE + "='" 
                    + Const_WM.WARNING_TYPE_GAS_OUTBURST + "' ORDER BY " + 
                    PreWarningRulesDbConstNames.WARNING_TYPE;
            ManageDataBase db = new 
                ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            return db.ReturnDS(sql);
        }
        #endregion

        /// <summary>
        /// 查询所有预警规则
        /// </summary>
        /// <returns></returns>
        public static DataSet selectAllWarningRules()
        {
            string sql = "SELECT * FROM " + 
                PreWarningRulesDbConstNames.TABLE_NAME + " ORDER BY " + 
                PreWarningRulesDbConstNames.SUITABLE_LOCATION; ;
            ManageDataBase db = new 
                ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            return db.ReturnDS(sql);
        }

        /// <summary>
        /// 根据预警规则实体信息更新数据库中预警规则信息
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public static bool updateWarningRulesInfo(PreWarningRules ent)
        {
            string sql = "UPDATE " + PreWarningRulesDbConstNames.TABLE_NAME 
                + " SET " +
                PreWarningRulesDbConstNames.RULE_CODE + "='" + ent.RuleCode 
                    + "'," +
            PreWarningRulesDbConstNames.RULE_TYPE + "='" + ent.RuleType + 
                "'," +
            PreWarningRulesDbConstNames.WARNING_TYPE + "='" + 
                ent.WarningType + "'," +
            PreWarningRulesDbConstNames.WARNING_LEVEL + "='" + 
                ent.WarningLevel + "'," +
            PreWarningRulesDbConstNames.SUITABLE_LOCATION + "='" + 
                ent.SuitableLocation + "'," +
            PreWarningRulesDbConstNames.RULE_DESCRIPTION + "='" + 
                ent.RuleDescription + "'," +
            PreWarningRulesDbConstNames.INDICATOR_TYPE + "='" + 
                ent.IndicatorType + "'," +
            PreWarningRulesDbConstNames.OPERATOR + "='" + ent.Operator + 
                "'," +
            PreWarningRulesDbConstNames.MODIFY_DATE + "='" + 
                ent.ModifyDate.ToString() + "', " +//MARK FIELD
            PreWarningRulesDbConstNames.REMARKS + "='" + ent.Remarks + "' " 
                + " WHERE " +
            PreWarningRulesDbConstNames.RULE_ID + "=" + 
                ent.RuleId.ToString();
            ManageDataBase db = new 
                ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            bool ret = db.OperateDB(sql);
            //未添加更新巷道信息表的代码，参数中需要将参数名同时保存
            //MARK YANGER

            return ret;
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
            ManageDataBase db = new 
                ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sql);

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

        /// <summary>
        /// 获取巷道绑定的规则ID
        /// </summary>
        /// <param name="tunnelID"></param>
        /// <returns>返回该巷道绑定的所有规则ID。注意：返回值可能为null</returns>
        public static string[] GetTunnelBindingRuleIds(int tunnelID)
        {
            //获取巷道绑定的规则编码ID字符串和预警参数字符串
            string sql = "SELECT " +
                TunnelInfoDbConstNames.RULE_IDS + " FROM " +
                TunnelInfoDbConstNames.TABLE_NAME + " WHERE " +
                TunnelInfoDbConstNames.ID + "=" + tunnelID;
            ManageDataBase db = new 
                ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sql);
            string allRuleIds = "";
            allRuleIds = 
                ds.Tables[0].Rows[0][TunnelInfoDbConstNames.RULE_IDS].ToString();
            return PreWarningRulesBLL.ParseRuleIds(allRuleIds);
        }

        /// <summary>
        /// 根据规则ID获取预警规则实体
        /// <param name="ruleId">规则Id</param>
        /// </summary>
        public static PreWarningRules GetPreWarningRulesEntityByRuleId(int 
            ruleId)
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
                ManageDataBase db = new 
                    ManageDataBase(DATABASE_TYPE.WarningManagementDB);
                DataSet ds = db.ReturnDS(sql);
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

                PreWarningRules ret = new PreWarningRules(ruleId);
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
                ManageDataBase db = new 
                    ManageDataBase(DATABASE_TYPE.WarningManagementDB);
                DataSet ds = db.ReturnDS(sql);
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

        /// <summary>
        /// 解析预警规则参数
        /// </summary>
        /// <param name="ruleIdsInDb">数据库中存储的规则编码字符串</param>
        /// <returns>注意返回值可能为null</returns>
        private static string[] ParseRuleIds(string ruleIdsInDb)
        {
            //规则编码为空
            if (ruleIdsInDb == "")
            {
                return null;
            }
            try
            {

                char[] seperator = 
                    Const_WM.GetPreWarningRuleIdAndParamsSeperatorArr();

                string[] rules = ruleIdsInDb.Split(seperator);
                return rules;
            }
            catch (Exception ex)
            {
                Alert.alert(ex.Message);
            }

            return null;
        }

        /// <summary>
        /// 将规则编码和参数信息转换为数据库中存储的字符串(注意：此处为单条规则编码和对应的参数)
        /// </summary>
        /// <param name="paramInfo">单条规则编码及对应的参数</param>
        /// 注意：规则编码无参数时，也要加分号。
        /// <returns>返回转换后的信息，注意：返回值可能为null</returns>
        public static RuleInfo 
            ConvertOneRuleCodeAndParamInfo2DBString(RuleInfo ruleInfo)
        {
            if (ruleInfo == null)
            {
                return null;
            }

            RuleInfo ret = new RuleInfo();

            //规则编码
            //ret.RuleCodesStr += paramInfo.RuleCode;
            ret.RuleCodesStr += ruleInfo.Id.ToString();
            ret.RuleCodesStr += 
                Const_WM.PRE_WARNING_RULE_CODE_AND_PARAMS_SEPERATOR_MULTI;

            int idx = 0;
            if (ruleInfo.PreWarningParams != null)
            {
                foreach (string paramName in 
                    ruleInfo.PreWarningParams.Keys)
                {
                    //参数名
                    ret.ParamsInfoStr += Const_WM.PARAM_START_SEPERATOR;
                    ret.ParamsInfoStr += paramName;
                    ret.ParamsInfoStr += Const_WM.PARAM_END_SEPERATOR;
                    //值
                    ret.ParamsInfoStr += Const_WM.VALUE_START_SEPERATOR;
                    ret.ParamsInfoStr += 
                        ruleInfo.PreWarningParams[paramName].ToString();
                    ret.ParamsInfoStr += Const_WM.VALUE_END_SEPERATOR;
                    if (idx != ruleInfo.PreWarningParams.Count - 1)
                    {
                        ret.ParamsInfoStr += 
                            Const_WM.PRE_WARNING_RULE_CODE_AND_PARAMS_SEPERATOR_SINGLE;
                    }
                    idx++;
                }
            }
            ret.ParamsInfoStr += 
                Const_WM.PRE_WARNING_RULE_CODE_AND_PARAMS_SEPERATOR_MULTI;
            return ret;
        }

        /// <summary>
        /// 将规则编码和参数信息更新至巷道信息表
        /// 注意：当传入的参数为null时，说明该巷道未绑定规则编码，此时仍需更新数据库
        /// </summary>
        /// <param name="tunnelID">巷道ID</param>
        /// <param name="info">巷道绑定的所有规则ID和参数信息</param>
        /// <returns>更新成功返回True</returns>
        public static bool UpdateRuleIdsAndParams2TunnelTable(int tunnelID, 
            RuleInfo[] info)
        {
            string ruleIds = "";
            string warningParams = "";
            if (info != null)
            {
                int n = info.Length;
                for (int i = 0; i < n; i++)
                {
                    RuleInfo cvtInfo = 
                        PreWarningRulesBLL.ConvertOneRuleCodeAndParamInfo2DBString(info[i]);
                    ruleIds += cvtInfo.RuleCodesStr;
                    warningParams += cvtInfo.ParamsInfoStr;
                }
            }
            string sql = "UPDATE " +
                TunnelInfoDbConstNames.TABLE_NAME + " SET " +
                TunnelInfoDbConstNames.RULE_IDS + "='" + ruleIds + "'," +
                TunnelInfoDbConstNames.PRE_WARNING_PARAMS + "='" + 
                    warningParams + "' WHERE " +
                TunnelInfoDbConstNames.ID + "=" + tunnelID;
            ManageDataBase db = new 
                ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            return db.OperateDB(sql);
        }

        public static bool ClearPreWarningDB()
        {
            string sql = "TRUNCATE TABLE[" + 
                PreWarningRulesDbConstNames.TABLE_NAME + "]";
            ManageDataBase db = new 
                ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            return db.OperateDB(sql);
        }
    }
}
