// ******************************************************************
// 概  述：预警计算业务逻辑
// 作  者：杨小颖
// 创建日期：2013/12/22
// 版本号：1.0
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibEntity;
using LibDatabase;
using System.Data;
using System.Collections;
using LibCommon;
using LibGeometry;


namespace LibBusiness
{
    public class PreWarningCalculationBLL
    {
        //日后有时间封装检验预警规则Excel表录入的规则是否存在问题的函数

        /// <summary>
        /// 根据字段值、预警规则实体和参数信息获取预警规则计算结果
        /// 注意：该函数只能计算同一字段对应多个比较符/参数值的情况(该种情况从实际角度考虑最多有两个比较符),未考虑多个字段的情况.
        /// 如：下列情况该函数不能正确计算出结果：
        /// 1,  P1＜VALUE1 && P2≤VALUE2；其中P1、P2为字段值，＜、≤为比较符，VALUE1、VALUE2为参数值。
        /// 下列情况该函数能够计算出正确结果
        /// 1,  P＞VALUE；其中P为字段值，＞为比较符，VALUE参数值。
        /// 2,  VALUE1＜P≤VALUE2；其中P为字段值，＜、≤为比较符，VALUE1、VALUE2为参数值。
        /// </summary>
        /// <param name="fieldValue">字段值</param>
        /// <param name="preWarningEnt">预警规则实体</param>
        /// <param name="paramInfo">规则编码及参数信息</param>
        /// <returns>预警规则生效返回TRUE</returns>
        bool GetCompareResultByEntAndParamInfo(double fieldValue, PreWarningRulesEntity preWarningEnt, RuleCodeAndParamInfo paramInfo)
        {
            //获取比较符
            string[] operators = preWarningEnt.GetParsedOperators();
            int operatorCnt = operators.Length;
            if (operatorCnt < 1)
            {
                Alert.alert("预警规则未设置比较符，请及时完善预警规则表！本次计算将不考虑规则：" + preWarningEnt.RuleCode);
                return false;
            }
            //正常情况下定量指标比较符个数和参数个数一致，若不一致，说明预警规则表存在问题。
            if (preWarningEnt.IndicatorType == Const_WM.QUANTIFIED_INDICATOR)
            {
                if (operatorCnt != paramInfo.PreWarningParams.Count)
                {
                    Alert.alert("比较符个数与参数个数不一致，请检查预警规则表是否存在问题！本次计算将不考虑规则：" + preWarningEnt.RuleCode);
                    return false;
                }
            }
            else//定性指标
            {
                //定性指标参数个数为0,需要重新设置参数
                Hashtable htYesNoParams = new Hashtable();
                htYesNoParams.Add("YES", 1);
                paramInfo.PreWarningParams = htYesNoParams;
            }

            #region 遍历所有比较符和参数
            int i = 0;
            foreach (string paramName in paramInfo.PreWarningParams.Keys)
            {
                double paramValue = 0;
                if (!double.TryParse(paramInfo.PreWarningParams[paramName].ToString(), out paramValue))
                {
                    Alert.alert("预警规则参数值不能转换为double，请检查预警规则表是否存在问题！本次计算将不考虑规则：" + paramInfo.RuleCode);
                    continue;
                }
                if (operators[i] == Const_WM.GREATER_THAN)//＞
                {
                    if (!(fieldValue > paramValue))
                    {
                        return false;
                    }
                }
                else if (operators[i] == Const_WM.GREATER_TAHN_OR_EQUAL2)//≥
                {
                    if (!(fieldValue >= paramValue))
                    {
                        return false;
                    }
                }
                else if (operators[i] == Const_WM.LESS_THAN)//＜
                {
                    if (!(fieldValue < paramValue))
                    {
                        return false;
                    }
                }
                else if (operators[i] == Const_WM.LESS_THAN_OR_EQUAL2)//≤
                {
                    if (!(fieldValue <= paramValue))
                    {
                        return false;
                    }
                }
                else if (operators[i] == Const_WM.EQUAL2)//＝
                {
                    if (!(fieldValue == paramValue))
                    {
                        return false;
                    }
                }
                else if (operators[i] == Const_WM.NEQUAL2)//≠
                {
                    if (!(fieldValue != paramValue))
                    {
                        return false;
                    }
                }
                else
                {
                    Alert.alert("未定义比较符：" + operators[i] + "请检查预警规则表中比较符是否设置正确！本次计算将不考虑规则：" + paramInfo.RuleCode);
                    continue;
                }
                i++;
            }
            #endregion

            return true;
        }

        /// <summary>
        /// 判断规则编码是否与巷道绑定
        /// </summary>
        /// <param name="ruleCode">规则编码</param>
        /// <param name="tunnelBindingRuleCodeAndParamInfo">巷道绑定的所有参数及规则编码信息</param>
        /// <param name="paramInfoFound">规则编码对应的规则编码和参数信息</param>
        /// <returns>存在时返回true，同时返回对应的规则编码和参数信息；不存在时返回false，对应的规则编码和参数信息为null</returns>
        bool IsRuleCodeExistInTunnelParamInfo(string ruleCode, RuleCodeAndParamInfo[] tunnelBindingRuleCodeAndParamInfo, out RuleCodeAndParamInfo paramInfoFound)
        {
            paramInfoFound = null;
            try
            {
                if (tunnelBindingRuleCodeAndParamInfo == null || tunnelBindingRuleCodeAndParamInfo.Length < 1)
                {
                    return false;
                }
                int n = tunnelBindingRuleCodeAndParamInfo.Length;
                for (int i = 0; i < n; i++)
                {
                    if (tunnelBindingRuleCodeAndParamInfo[i].RuleCode == ruleCode)
                    {
                        paramInfoFound = tunnelBindingRuleCodeAndParamInfo[i];
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.alert(ex.Message + "函数名：IsRuleCodeExistInTunnelParamInfo");
            }
            return false;
        }

        /// <summary>
        /// 获取当前预警数据表中巷道ID、时间等符合条件的所有预警数据。
        /// </summary>
        /// <param name="dataTableName"></param>
        /// <param name="tunnelID"></param>
        /// <param name="minTime"></param>
        /// <param name="maxTime"></param>
        /// <returns></returns>
        DataSet GetPreWarningDatas(string dataTableName, int tunnelID, DateTime minTime, DateTime maxTime)
        {
            string sql = "SELECT * FROM " + dataTableName + " WHERE " + WarningDatasCommonDbConstNames.TUNNEL_ID + "=" + tunnelID +
                " AND " + WarningDatasCommonDbConstNames.DATETIME + ">='" + minTime.ToString() + "'" +
                " AND " + WarningDatasCommonDbConstNames.DATETIME + "<='" + maxTime.ToString() + "'";
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            return db.ReturnDS(sql);
        }

        /// <summary>
        /// 获取当前预警数据表中所有预警数据（不考虑巷道ID、时间等约束条件）。
        /// </summary>
        /// <param name="dataTableName"></param>
        /// <param name="tunnelID"></param>
        /// <param name="minTime"></param>
        /// <param name="maxTime"></param>
        /// <returns></returns>
        DataSet GetPreWarningDatas(string dataTableName)
        {
            string sql = "SELECT * FROM " + dataTableName;
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            return db.ReturnDS(sql);
        }
        /// <summary>
        /// 获取预警数据通用信息
        /// </summary>
        /// <param name="dt">单行数据</param>
        /// <param name="fieldName">字段名</param>
        /// <returns>注意：可能返回NULL</returns>
        PreWarningDataCommonInfoEntity GetWarningDataCommonInfo(DataRow dt, string fieldName)
        {
            PreWarningDataCommonInfoEntity ret = new PreWarningDataCommonInfoEntity();
            try
            {
                //ret.X = double.Parse(dt["COORDINATE_X"].ToString());
                //ret.Y = double.Parse(dt["COORDINATE_Y"].ToString());
                //ret.Z = double.Parse(dt["COORDINATE_Z"].ToString());

                //ret.Date = DateTime.Parse(dt["DATETIME"].ToString());

                ret.X = double.Parse(dt[WarningDatasCommonDbConstNames.X].ToString());
                ret.Y = double.Parse(dt[WarningDatasCommonDbConstNames.Y].ToString());
                ret.Z = double.Parse(dt[WarningDatasCommonDbConstNames.Z].ToString());
                ret.Date = DateTime.Parse(dt[WarningDatasCommonDbConstNames.DATETIME].ToString());

                ret.Value = double.Parse(dt[fieldName].ToString());
            }
            catch
            {
                return null;
            }
            return ret;
        }

        /// <summary>
        /// 获取字段名对应的所有规则编码(查询当前预警数据表对应的预警数据字段与规则编码关系表)
        /// 注意：各个绑定表中字段名称要一致！
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <returns>注：无数据或出错返回null</returns>
        string[] GetRuleCodesByFieldName(string fieldName, string bindingTableName)
        {
            string[] retRuleCodes = null;
            //string sql = "SELECT BINDING_WARNING_RULES FROM " + bindingTableName + " WHERE COLUMN_NAME='" + fieldName + "'";
            string sql = "SELECT " + WarningDataBindingTableCommonDbConstNames.BINDING_WARNING_RULES +
                " FROM " + bindingTableName +
                " WHERE " + WarningDataBindingTableCommonDbConstNames.COLUMN_NAME + "='" + fieldName + "'";
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataSet ds = db.ReturnDS(sql);
            try
            {
                int n = ds.Tables[0].Rows.Count;
                if (n > 0)
                {
                    retRuleCodes = new string[n];
                }
                else
                {
                    Alert.alert("字段名：" + fieldName + "未绑定规则编码！【表名：" + bindingTableName + "】");
                    return null;
                }
                for (int i = 0; i < n; i++)
                {
                    //retRuleCodes[i] = ds.Tables[0].Rows[i]["BINDING_WARNING_RULES"].ToString();
                    retRuleCodes[i] = ds.Tables[0].Rows[i][WarningDataBindingTableCommonDbConstNames.BINDING_WARNING_RULES].ToString();
                }
            }
            catch (Exception ex)
            {
                Alert.alert(ex.Message);
                return null;
            }
            return retRuleCodes;
        }

        /// <summary>
        /// 获取预警数据表管理表中所有的预警数据表和绑定的预警数据字段与规则编码对应关系表
        /// </summary>
        /// <returns>注：无数据则返回NULL</returns>
        DataTblAndBindingTblInfoEntity[] GetPreWarningDataTableAndBindingTablesInfo()
        {
            DataTblAndBindingTblInfoEntity[] ret = null;
            //string sql = "SELECT * FROM T_PRE_WARNING_DATA_TABLE_MANAGEMENT";
            string sql = "SELECT * FROM " + WarningDataTableManagementDbConstNames.TABLE_NAME;
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataSet ds = db.ReturnDS(sql);
            try
            {
                int n = ds.Tables[0].Rows.Count;
                if (n == 0)
                {
                    Alert.alert("预警数据管理表未绑定数据表！【表名：" + WarningDataTableManagementDbConstNames.TABLE_NAME + "】");
                    return null;
                }
                ret = new DataTblAndBindingTblInfoEntity[n];
                for (int i = 0; i < n; i++)
                {
                    //string tblName = ds.Tables[0].Rows[i]["DATA_TABLE_NAME"].ToString();
                    //string bindingTable = ds.Tables[0].Rows[i]["BINDING_TABLE_NAME"].ToString();
                    //bool needConstrains = int.Parse(ds.Tables[0].Rows[i]["NEED_CONSTRAINS"].ToString()) > 0 ? true : false;
                    string tblName = ds.Tables[0].Rows[i][WarningDataTableManagementDbConstNames.DATA_TABLE_NAME].ToString();
                    string bindingTable = ds.Tables[0].Rows[i][WarningDataTableManagementDbConstNames.BINDING_TABLE_NAME].ToString();
                    bool needConstrains = int.Parse(ds.Tables[0].Rows[i][WarningDataTableManagementDbConstNames.NEED_CONSTRAINS].ToString()) > 0 ? true : false;
                    ret[i] = new DataTblAndBindingTblInfoEntity();
                    ret[i].DataTableName = tblName;
                    ret[i].BindingTableName = bindingTable;
                    ret[i].NeedConstrains = needConstrains;
                }
            }
            catch (Exception ex)
            {
                Alert.alert(ex.Message);
            }
            return ret;
        }

        ///// <summary>
        /////  获取预警数据字段与规则编码关系表中所有预警数据字段。
        ///// </summary>
        ///// <param name="tableName"></param>
        ///// <returns></returns>
        //string[] GetPreWarningDataBindingTableFieldNames(string bindinTableName)
        //{
        //    //string sql = "select name from syscolumns where id=object_id(N'" + bindinTableName + "')";
        //    string sql = "SELECT DISTINCT COLUMN_NAME FROM " + bindinTableName;
        //    ManageDataBase db = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
        //    DataSet ds = db.ReturnDS(sql);
        //    string[] ret = null;
        //    try
        //    {
        //        int n = ds.Tables[0].Rows.Count;
        //        if (n > 0)
        //        {
        //            ret = new string[n];
        //        }
        //        for (int i = 0; i < n; i++)
        //        {
        //            ret[i] = ds.Tables[0].Rows[i][0].ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Alert.alert(ex.Message);
        //        return null;
        //    }
        //    return ret;
        //}

        /// <summary>
        ///  读取当前预警数据表对应的预警数据字段与规则编码关系表，获取预警数据字段与规则编码关系表中所有字段名（Distinct）及字段使用方式
        /// </summary>
        /// <param name="tableName">预警数据绑定表名</param>
        /// <returns></returns>
        BindingTableEntity[] GetPreWarningDataBindingTableEntity(string bindinTableName)
        {
            BindingTableEntity[] ret = null;
            //string sql = "SELECT COLUMN_NAME, COLUMN_USE_MANNER, BINDING_WARNING_RULES FROM " + bindinTableName;
            string sql = "SELECT " +
                WarningDataBindingTableCommonDbConstNames.COLUMN_NAME + ", " +
                WarningDataBindingTableCommonDbConstNames.COLUMN_USE_MANNER + ", " +
                WarningDataBindingTableCommonDbConstNames.BINDING_WARNING_RULES + " FROM " + bindinTableName;
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataSet ds = db.ReturnDS(sql);
            try
            {
                int n = ds.Tables[0].Rows.Count;
                if (n > 0)
                {
                    ret = new BindingTableEntity[n];
                }
                for (int i = 0; i < n; i++)
                {
                    ret[i] = new BindingTableEntity();
                    ret[i].ColumnName = ds.Tables[0].Rows[i][WarningDataBindingTableCommonDbConstNames.COLUMN_NAME].ToString();
                    ret[i].UseManner = (COLUMN_USE_MANNER)Enum.Parse(typeof(COLUMN_USE_MANNER), ds.Tables[0].Rows[i][WarningDataBindingTableCommonDbConstNames.COLUMN_USE_MANNER].ToString());
                    ret[i].BindingWarningRules = ds.Tables[0].Rows[i][WarningDataBindingTableCommonDbConstNames.BINDING_WARNING_RULES].ToString();
                }
            }
            catch (Exception ex)
            {
                Alert.alert(ex.Message);
                return null;
            }
            return ret;
        }

        /// <summary>
        /// 根据预警数据表名获取特殊预警数据实体
        /// </summary>
        /// <param name="tblName"></param>
        /// <param name="dr">一行数据</param>
        /// <returns></returns>
        PreWarningSpecialDataCalcBase GetPreWarningSpecialDataEntitiesByTblName(string tblName, DataRow dr)
        {
            PreWarningSpecialDataCalcBase ret = null;
            if (tblName == Const_WM.SPECIAL_WARNING_DATA_TABLE_FAULTAGE)//断层数据表
            {
                ret = FaultageBLL.GetFaultageEnt(dr);
            }
            else if (tblName == Const_WM.SPECIAL_WARNING_DATA_TABLE_COLLAPSE_PILLARS)//陷落柱数据表
            {
                //MARK
            }
                //瓦斯
                //钻孔
            else//未对该数据表进行维护！
            {
                Alert.alert("未对预警数据表：" + tblName + " 进行维护，预警计算将不考虑该表中的数据！");
            }
            return null;
        }

        //double CalcNearestDis(Vector3_DW pt, PreWarningSpecialDataCalcBase[] spDataEnts, out Vector3_DW outNearestPt)
        //{
        //    double minDis = 0;
        //    outNearestPt = Vector3_DW.zero;

        //    int nSpecialDataEnts = spDataEnts.Length;
        //    for (int spDataIdx = 0; spDataIdx < nSpecialDataEnts; spDataIdx++)
        //    {
        //        double tmpDis = spDataEnts[spDataIdx].CalcDis(pt, out outNearestPt);
        //        if (spDataIdx == 0)
        //        {
        //            minDis = tmpDis;
        //        }
        //        if (tmpDis < minDis)
        //        {
        //            minDis = tmpDis;
        //        }
        //    }
        //    return minDis;
        //}

        /// <summary>
        /// 计算预警结果
        /// </summary>
        /// <param name="tunnelID">巷道ID</param>
        /// <returns>预警结果列表，注：可能返回空。当该巷道未绑定预警规则时则返回null</returns>
        public PreWarningResultEntity[] CalcPreWarningResult(int tunnelID, DateTime minTime, DateTime maxTime)
        {
            List<PreWarningResultEntity> lstAllResult = new List<PreWarningResultEntity>();
            //查询巷道信息表，获取该巷道使用的规则编码和参数信息列表
            RuleCodeAndParamInfo[] allParamInfo = PreWarningRulesBLL.GetTunnelBindingRuleCodesAndParams(tunnelID);
            if (allParamInfo == null)
            {
                Alert.alert("该巷道未绑定预警规则！");
                //该巷道未绑定预警规则
                return null;
            }

            //Hashtable htTbls = GetPreWarningDataTableAndBindingTables();
            //查询预警数据表管理表，获取所有的预警数据表和对应的预警数据字段与规则编码对应关系表信息
            DataTblAndBindingTblInfoEntity[] tableInfo = GetPreWarningDataTableAndBindingTablesInfo();
            //int nTables = htTbls.Count;
            int nTables = tableInfo.Length;
            //try
            //{
            //遍历所有的预警数据表
            //foreach (string dataTblName in htTbls.Keys)
            for (int tblIdx = 0; tblIdx < nTables; tblIdx++)
            {
                string dataTblName = tableInfo[tblIdx].DataTableName;
                //获取当前预警数据表中巷道ID、时间等符合条件的所有预警数据。
                DataSet dsData = null;
                if (tableInfo[tblIdx].NeedConstrains == true)
                {
                    dsData = GetPreWarningDatas(dataTblName, tunnelID, minTime, maxTime);
                }
                else
                {
                    dsData = GetPreWarningDatas(dataTblName);
                }
                int nDataCnt = dsData.Tables[0].Rows.Count;
                //遍历当前预警数据表中所有数据
                for (int dataIdx = 0; dataIdx < nDataCnt; dataIdx++)
                {
                    //预警数据表绑定的表名（预警数据字段与规则编码表）
                    //string bindingTableName = htTbls[dataTblName].ToString();
                    string bindingTableName = tableInfo[tblIdx].BindingTableName;
                    //读取当前预警数据表对应的预警数据字段与规则编码关系表，获取预警数据字段与规则编码关系表中所有字段名、规则编码及字段使用方式
                    //string[] fieldNames = GetPreWarningDataBindingTableFieldNames(bindingTableName);
                    BindingTableEntity[] bindingEnt = GetPreWarningDataBindingTableEntity(bindingTableName);
                    //if (fieldNames == null)
                    if (bindingEnt == null)
                    {
                        Alert.alert("表：" + bindingTableName + "中未将数据字段与规则编码绑定！");
                        continue;
                    }
                    //int nFieldCnt = fieldNames.Length;
                    int nFieldCnt = bindingEnt.Length;
                    #region 遍历所有字段名(数据)[字段使用方式为 非直接使用的 一种字段使用方式只能对应一条记录！]
                    for (int fieldIdx = 0; fieldIdx < nFieldCnt; fieldIdx++)
                    {
                        //获取当前记录中的字段
                        //string curFieldName = fieldNames[fieldIdx];
                        string curFieldName = bindingEnt[fieldIdx].ColumnName;
                        //获取当前字段对应的值
                        double fieldValue = 0;
                        try
                        {
                            //当前记录中字段的 字段使用方式 是否为：直接使用
                            if (bindingEnt[fieldIdx].UseManner == COLUMN_USE_MANNER.DIRECT_USE)
                            {
                                fieldValue = Convert.ToDouble(dsData.Tables[0].Rows[dataIdx][curFieldName]);
                            }
                            //需要进行特殊预警数据计算（如：距断层、陷落柱等距离）
                            #region 特殊数据计算
                            else
                            {
                                //根据预警数据表名称获取预警数据实体（如：断层实体、陷落柱实体）
                                PreWarningSpecialDataCalcBase calcDataEnt = GetPreWarningSpecialDataEntitiesByTblName(tableInfo[tblIdx].DataTableName, dsData.Tables[0].Rows[dataIdx]);
                                if (calcDataEnt == null)
                                {
                                    //未对该数据表进行维护
                                    //获取下一预警数据表(此处为了省事，break后会获取下一预警数据，而非下一预警数据表！理论上应该获取下一预警数据表)
                                    break;
                                }
                                //字段使用方式是否为：计算最近距离
                                if (bindingEnt[fieldIdx].UseManner == COLUMN_USE_MANNER.CALC_NEAREST_DIS)
                                {
                                    Vector3_DW ptTunnel = Vector3_DW.zero;
                                    //获取当前巷道当前位置 MARK 尚未实现

                                    Vector3_DW outPtNearest = Vector3_DW.zero;
                                    fieldValue = calcDataEnt.CalcNearestDis(ptTunnel, out outPtNearest);
                                }
                                //字段使用方式是否为：计算值（如探头瓦斯浓度增加值）
                                else if (bindingEnt[fieldIdx].UseManner == COLUMN_USE_MANNER.CALC_VALUE)
                                {
                                    fieldValue = calcDataEnt.CalcValue();
                                }
                                else
                                {
                                    Alert.alert("表：" + dataTblName + " 中数据库中录入的字段使用方式不正确!");
                                    //获取下一记录（字段名、规则编码及字段使用方式）
                                    //如果一个数据表对应多种计算方法，则需要在绑定表中添加一列标识使用哪个函数。
                                    //目前尚未处理一个数据表对应多种计算方法的情况
                                    continue;
                                }
                            }
                            #endregion
                        }//end of try
                        catch (Exception ex)
                        {
                            Alert.alert("获取表（" + bindingTableName + "）中的字段(" + curFieldName + ")对应的值出错：" + ex.Message);
                            continue;
                        }
                        //获取当前字段名对应的所有规则编码
                        //string[] ruleCodes = GetRuleCodesByFieldName(curFieldName, bindingTableName);
                        //当前规则编码在规则编码和参数信息列表中是否存在
                        //int nRuleCodeCnt = ruleCodes.Length;
                        //遍历当前字段名对应的所有规则编码(该步骤不需要！！先前算法绕弯了，速度较慢)
                        //for (int ruleCodeIdx = 0; ruleCodeIdx < nRuleCodeCnt; ruleCodeIdx++)
                        //{
                        //string curRuleCode = ruleCodes[ruleCodeIdx];
                        string curRuleCode = bindingEnt[fieldIdx].BindingWarningRules;
                        //当前规则编码在规则编码和参数信息列表中是否存在
                        RuleCodeAndParamInfo curParamsInfo = null;
                        if (!IsRuleCodeExistInTunnelParamInfo(curRuleCode, allParamInfo, out curParamsInfo))
                        {
                            //不存在
                            continue;
                        }
                        //存在
                        //读取预警规则表，获取当前规则编码的预警规则实体信息
                        PreWarningRulesEntity preWarningEnt = PreWarningRulesBLL.GetPreWarningRulesEntityByRuleCode(curRuleCode);
                        //当前字段值通过预警规则实体中的比较符（注：比较符可能含多个）与规则编码和参数信息中的参数值进行比较，比较结果是否为True。
                        if (GetCompareResultByEntAndParamInfo(fieldValue, preWarningEnt, curParamsInfo))
                        {
                            //比较结果为TRUE
                            PreWarningResultEntity oneResult = new PreWarningResultEntity();
                            if (preWarningEnt.IndicatorType != Const_WM.YES_NO_INDICATOR)
                            {
                                if (!preWarningEnt.UpdateRuleDescriptionByParams(curParamsInfo.PreWarningParams))
                                {
                                    Alert.alert("更新预警规则实体规则描述信息失败！");
                                    continue;
                                }
                            }
                            //预警规则实体信息
                            oneResult.PreWarningRulesEntity = preWarningEnt;

                            //预警数据通用信息
                            PreWarningDataCommonInfoEntity dataEnt = GetWarningDataCommonInfo(dsData.Tables[0].Rows[dataIdx], curFieldName);
                            dataEnt.TunnelID = tunnelID;

                            oneResult.WarningDataCommonInfoEnt = dataEnt;
                            //将当前记录信息及预警规则实体信息添加至预警结果列表
                            lstAllResult.Add(oneResult);
                        }
                        //}//end of 遍历当前字段名对应的所有规则编码
                    }//end of 遍历所有字段名
                    #endregion 遍历所有字段名
                }//end of 遍历当前预警数据表中所有数据
            }//end of 遍历所有的预警数据表
            //}//end of try
            //catch (Exception ex)
            //{
            //    Alert.alert(ex.Message);
            //}
            return lstAllResult.ToArray();
        }//end of function
    }
}
