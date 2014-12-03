using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibEntity;
using LibDatabase;
using LibCommon;

using System.Data;

namespace LibBusiness
{
    public class WarningResultsQuery
    {
        /// <summary>
        /// 查询未解除预警
        /// </summary>
        /// <returns>返回查询实体的数组</returns>
        public static List<PreWarningHistoryResultEnt> QueryUnLiftedWarningResult()
        {
            List<PreWarningHistoryResultEnt> unliftedResultEnts = new List<PreWarningHistoryResultEnt>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ");
            strSql.Append(PreWarningResultDBConstNames.TABLE_NAME);
            strSql.Append(" WHERE ");
            strSql.Append(" [clearWarning]=0 AND ([warning_result]=0 OR [warning_result]=1)");
            strSql.Append(" ORDER BY ");
            strSql.Append(PreWarningResultDBConstNames.TUNNEL_ID);

            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            database.Open();
            DataTable dt = database.ReturnDSNotOpenAndClose(strSql.ToString()).Tables[0];
            if (dt != null)
            {
                int rowCount = dt.Rows.Count;
                PreWarningHistoryResultEnt ent = null;
                for (int i = 0; i < rowCount; i++)
                {
                    ent = new PreWarningHistoryResultEnt();
                    //巷道名称
                    int tunelId = LibCommon.Const.INVALID_ID;
                    int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.TUNNEL_ID].ToString(), out tunelId);
                    ent.TunnelID = tunelId;
                    //ent.TunelName = GetTunelNameByTunelID(database, tunelId);
                    //日期
                    //ent.DateTime = time;
                    //班次
                    ent.Date_Shift = dt.Rows[i][PreWarningResultDBConstNames.DATE_SHIFT].ToString();

                    int defultValue = (int)WarningResult.NULL;
                    //突出预警结果
                    if (dt.Rows[i][PreWarningResultDBConstNames.WARNING_TYPE].ToString() == LibCommon.WarningType.OUTBURST.ToString())
                    {
                        WarningResultEnt outburstEnt = new WarningResultEnt();
                        outburstEnt.ID = dt.Rows[i][PreWarningResultDBConstNames.ID].ToString();

                        int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.WARNING_RESULT].ToString(), out defultValue);
                        outburstEnt.WarningResult = defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.GAS].ToString(), out defultValue);
                        outburstEnt.Gas = defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.COAL].ToString(), out defultValue);
                        outburstEnt.Coal = defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.GEOLOGY].ToString(), out defultValue);
                        outburstEnt.Geology = defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.VENTILATION].ToString(), out defultValue);
                        outburstEnt.Ventilation = defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.MANAGEMENT].ToString(), out defultValue);
                        outburstEnt.Management = defultValue;

                        ent.OutBrustWarningResult = outburstEnt;
                    }
                    //超限预警结果
                    if (dt.Rows[i][PreWarningResultDBConstNames.WARNING_TYPE].ToString() == LibCommon.WarningType.OVER_LIMIT.ToString())
                    {
                        WarningResultEnt overlimitEnt = new WarningResultEnt();
                        overlimitEnt.ID = dt.Rows[i][PreWarningResultDBConstNames.ID].ToString();

                        int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.WARNING_RESULT].ToString(), out defultValue);
                        overlimitEnt.WarningResult = defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.GAS].ToString(), out defultValue);
                        overlimitEnt.Gas = defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.COAL].ToString(), out defultValue);
                        overlimitEnt.Coal = defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.GEOLOGY].ToString(), out defultValue);
                        overlimitEnt.Geology = defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.VENTILATION].ToString(), out defultValue);
                        overlimitEnt.Ventilation = defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.MANAGEMENT].ToString(), out defultValue);
                        overlimitEnt.Management = defultValue;

                        ent.OverLimitWarningResult = overlimitEnt;
                    }

                    unliftedResultEnts.Add(ent);
                }
            }
            database.Close();
            return unliftedResultEnts;
        }

        /// <summary>
        /// 通过rowid获取整理后的数据，便于分页控件控制
        /// </summary>
        /// <param name="iStartIndex"></param>
        /// <param name="iEndIndex"></param>
        /// <returns></returns>
        public static List<PreWarningHistoryResultEnt> GetSortedPreWarningData(int iStartIndex, int iEndIndex)
        {
            List<PreWarningHistoryResultEnt> ents = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM (");
            strSql.Append(" SELECT ROW_NUMBER() OVER(ORDER BY " + PreWarningResultDBConstNames.ID + ") AS rowid, *");
            strSql.Append(" FROM " + PreWarningResultDBConstNames.TABLE_NAME_TEMP + ") AS TB");
            strSql.Append(" WHERE rowid >= " + iStartIndex);
            strSql.Append(" AND rowid <= " + iEndIndex);
            strSql.Append(" AND [clearWarning]=0 AND ([warning_result]=0 OR [warning_result]=1)");
            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataTable dt = database.ReturnDS(strSql.ToString()).Tables[0];
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ents = new List<PreWarningHistoryResultEnt>();
                    int rowCount = dt.Rows.Count;
                    PreWarningHistoryResultEnt ent = null;
                    for (int i = 0; i < rowCount; i++)
                    {
                        ent = new PreWarningHistoryResultEnt();
                        //巷道名称
                        ent.TunelName = dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.TUNNEL_NAME].ToString().TrimEnd();
                        //巷道ID
                        ent.TunnelID = (int)(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.TUNNEL_ID]);
                        //日期
                        ent.DateTime = dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.DATA_TIME].ToString();
                        //班次
                        ent.Date_Shift = dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.DATE_SHIFT].ToString();

                        int defultValue = (int)WarningResult.NULL;
                        //超限预警详细信息
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OVER_LIMIT].ToString(), out defultValue);
                        ent.OverLimitWarningResult.WarningResult = defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OVER_LIMIT_GAS].ToString(), out defultValue);
                        ent.OverLimitWarningResult.Gas = defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OVER_LIMIT_COAL].ToString(), out defultValue);
                        ent.OverLimitWarningResult.Coal = defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OVER_LIMIT_GEOLOGY].ToString(), out defultValue);
                        ent.OverLimitWarningResult.Geology = defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OVER_LIMIT_VENTILATION].ToString(), out defultValue);
                        ent.OverLimitWarningResult.Ventilation = defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OVER_LIMIT_MANAGEMENT].ToString(), out defultValue);
                        ent.OverLimitWarningResult.Management = defultValue;

                        //突出预警详细信息
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OUTBURST].ToString(), out defultValue);
                        ent.OutBrustWarningResult.WarningResult = defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OUTBURST_GAS].ToString(), out defultValue);
                        ent.OutBrustWarningResult.Gas = defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OUTBURST_COAL].ToString(), out defultValue);
                        ent.OutBrustWarningResult.Coal = defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OUTBURST_GEOLOGY].ToString(), out defultValue);
                        ent.OutBrustWarningResult.Geology = defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OUTBURST_VENTILATION].ToString(), out defultValue);
                        ent.OutBrustWarningResult.Ventilation = defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OUTBURST_MANAGEMENT].ToString(), out defultValue);
                        ent.OutBrustWarningResult.Management = defultValue;
                        ents.Add(ent);
                    }
                }
            }
            return ents;
        }
    }
}
