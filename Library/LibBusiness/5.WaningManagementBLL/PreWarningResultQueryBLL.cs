// ******************************************************************
// 概  述：预警信息结果数据库逻辑
// 作  者：秦凯
// 创建日期：2014/03/15
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Data;
using LibDatabase;
using LibEntity;
using System.Windows.Forms;
using LibCommon;

namespace LibBusiness
{
    public static class PreWarningResultQueryBLL
    {
        /// <summary>
        /// 从数据库中查询巷道ID，日期，班次，预警类型，预警结果的值
        /// </summary>
        /// <returns></returns>
        //public static List<PreWarningResultQueryEnt> GetPerWarningResult(DateTime timeStart, DateTime timeEnd)
        //{
        //    //SELECT T.TUNNEL_NAME,W.tunnel_id,W.date_shift,W.data_time,W.warning_type,W.warning_result,W.detail_info 
        //    //FROM T_TUNNEL_INFO T JOIN T_EARLY_WARNING_RESULT W ON T.TUNNEL_ID = W.tunnel_id 
        //    //WHERE T.TUNNEL_ID IN  (SELECT W.tunnel_id FROM T_EARLY_WARNING_RESULT) 
        //    //And W.data_time between '2014-03-05 15:43:45.000' and '2014-03-05 15:44:45.000'
        //    //ORDER BY W.id

        //    List<PreWarningResultQueryEnt> ents = new List<PreWarningResultQueryEnt>();
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("SELECT ");
        //    strSql.Append("T." + TunnelInfoDbConstNames.TUNNEL_NAME + ",");
        //    strSql.Append("W." + PreWarningResultQueryDBConstNames.TUNNEL_ID + ",");
        //    strSql.Append("W." + PreWarningResultQueryDBConstNames.DATE_SHIFT + ",");
        //    strSql.Append("W." + PreWarningResultQueryDBConstNames.DATA_TIME + ",");
        //    strSql.Append("W." + PreWarningResultQueryDBConstNames.WARNING_TYPE + ",");
        //    strSql.Append("W." + PreWarningResultQueryDBConstNames.WARNING_RESULT + ",");
        //    strSql.Append("W." + PreWarningResultQueryDBConstNames.DETAIL_INFO);
        //    strSql.Append(" FROM ");
        //    strSql.Append(TunnelInfoDbConstNames.TABLE_NAME + " T");
        //    strSql.Append(" JOIN ");
        //    strSql.Append(PreWarningResultQueryDBConstNames.TABLE_NAME + " W");
        //    strSql.Append(" ON ");
        //    strSql.Append("T." + TunnelInfoDbConstNames.ID);
        //    strSql.Append(" = ");
        //    strSql.Append("W." + PreWarningResultQueryDBConstNames.TUNNEL_ID);
        //    strSql.Append(" WHERE ");
        //    strSql.Append("T." + TunnelInfoDbConstNames.ID);
        //    strSql.Append(" IN ");
        //    strSql.Append(" (SELECT ");
        //    strSql.Append("W." + PreWarningResultQueryDBConstNames.TUNNEL_ID);
        //    strSql.Append(" FROM ");
        //    strSql.Append(PreWarningResultQueryDBConstNames.TABLE_NAME + ")");
        //    strSql.Append(" AND ");
        //    strSql.Append("W." + PreWarningResultQueryDBConstNames.DATA_TIME);
        //    strSql.Append(" BETWEEN ");
        //    strSql.Append("'" + timeStart + "'");
        //    strSql.Append(" AND ");
        //    strSql.Append("'" + timeEnd + "'");
        //    strSql.Append(" ORDER BY ");
        //    strSql.Append("W." + PreWarningResultQueryDBConstNames.ID);

        //    ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
        //    DataTable dt = database.ReturnDS(strSql.ToString()).Tables[0];
        //    int iRowCount = dt.Rows.Count;
        //    PreWarningResultQueryEnt ent = new PreWarningResultQueryEnt();
        //    for (int i = 0; i < iRowCount; i++)
        //    {
        //        string tunel_name1 = dt.Rows[i][0].ToString();
        //        string tunel_id1 = dt.Rows[i][1].ToString();
        //        string date_shift1 = dt.Rows[i][2].ToString();
        //        string date_time1 = dt.Rows[i][3].ToString();
        //        string warning_type = dt.Rows[i][4].ToString();
        //        string warning_result = dt.Rows[i][5].ToString();
        //        string detail_info = dt.Rows[i][6].ToString();

        //        if (i % 2 == 0)
        //        {
        //            ent = new PreWarningResultQueryEnt();
        //        }
        //        ent.TunelName = tunel_name1;
        //        ent.DateTime = date_time1;
        //        ent.Date_Shift = date_shift1;
        //        if (warning_type == LibCommon.WarningType.OVER_LIMIT.ToString())
        //        {
        //            ent.OverLimitWarningResult.ID =
        //            ent.OverLimitWarningResult.WarningResult = warning_result;
        //            ent.OverLimitWarningResult.DetailInformation = detail_info;
        //        }
        //        if (warning_type == LibCommon.WarningType.OUTBURST.ToString())
        //        {
        //            ent.OutBrustWarningResult.WarningResult = warning_result;
        //            ent.OutBrustWarningResult.DetailInformation = detail_info;
        //        }
        //        ents.Add(ent);
        //    }
        //    return ents;
        //}

        /// <summary>
        /// 返回所有预警时间
        /// </summary>
        /// <returns></returns>
        public static string[] GetAllPreWarningTimes()
        {
            string[] times = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT DISTINCT ");
            strSql.Append(PreWarningResultDBConstNames.DATA_TIME);
            strSql.Append(" FROM ");
            strSql.Append(PreWarningResultDBConstNames.TABLE_NAME);
            strSql.Append(" ORDER BY ");
            strSql.Append(PreWarningResultDBConstNames.DATA_TIME);
            strSql.Append(" DESC");
            ManageDataBase database = new 
                ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataTable dt = new DataTable();
            dt = database.ReturnDS(strSql.ToString()).Tables[0];
            if (dt != null)
            {
                int rowCount = dt.Rows.Count;
                if (rowCount > 0)
                {
                    times = new string[rowCount];
                    for (int i = 0; i < rowCount; i++)
                    {
                        times[i] = 
                            dt.Rows[i][PreWarningResultDBConstNames.DATA_TIME].ToString();
                    }
                }
            }
            return times;
        }

        //SELECT ROW_NUMBER() OVER (ORDER BY W.data_time DESC , W.tunnel_id , W.id) AS rowid,* 
        //FROM T_EARLY_WARNING_RESULT AS W
        //WHERE W.data_time IN (
        //SELECT DISTINCT T.data_time FROM T_EARLY_WARNING_RESULT AS T WHERE T.data_time 
        //BETWEEN '2014-03-08 11:29:55.000' AND '2014-03-08 11:31:00.000') 
        public static List<PreWarningResultQuery> 
            GetPerWarningResult(string timeStart, string timeEnd)
        {
            List<PreWarningResultQuery> ents = new 
                List<PreWarningResultQuery>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ROW_NUMBER() OVER (ORDER BY ");
            strSql.Append("W." + PreWarningResultDBConstNames.DATA_TIME + 
                " DESC,");
            strSql.Append("W." + PreWarningResultDBConstNames.TUNNEL_ID + 
                ",");
            strSql.Append("W." + PreWarningResultDBConstNames.ID + ")");
            strSql.Append(" AS rowid , *");
            strSql.Append(" FROM ");
            strSql.Append(PreWarningResultDBConstNames.TABLE_NAME + " AS" + 
                " W");
            strSql.Append(" WHERE");
            strSql.Append(" W." + PreWarningResultDBConstNames.DATA_TIME);
            strSql.Append(" IN (");
            strSql.Append("SELECT DISTINCT");
            strSql.Append(" T." + PreWarningResultDBConstNames.DATA_TIME);
            strSql.Append(" FROM ");
            strSql.Append(PreWarningResultDBConstNames.TABLE_NAME);
            strSql.Append(" AS T");
            strSql.Append(" WHERE");
            strSql.Append(" T." + PreWarningResultDBConstNames.DATA_TIME);
            strSql.Append(" >= ");
            strSql.Append("'" + timeStart + "'");
            strSql.Append(" AND ");
            strSql.Append(" T." + PreWarningResultDBConstNames.DATA_TIME);
            strSql.Append(" < ");
            strSql.Append("'" + timeEnd + "')");
            ManageDataBase database = new 
                ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataTable dt = database.ReturnDS(strSql.ToString()).Tables[0];
            int iRowCount = dt.Rows.Count;
            PreWarningResultQuery ent = new PreWarningResultQuery();
            for (int i = 0; i < iRowCount; i++)
            {
                string warning_type = 
                    dt.Rows[i][PreWarningResultDBConstNames.WARNING_TYPE].ToString();
                if (i % 2 == 0)
                {
                    ent = new PreWarningResultQuery();
                    ent.TunelName = 
                        Tunnel.Find(dt.Rows[i][PreWarningResultDBConstNames.TUNNEL_ID]).TunnelName;
                    ent.DateTime = 
                        Convert.ToDateTime(dt.Rows[i][PreWarningResultDBConstNames.DATA_TIME]);
                    ent.Date_Shift = 
                        dt.Rows[i][PreWarningResultDBConstNames.DATE_SHIFT].ToString();
                }

                int defultValue = (int)WarningResult.NULL;
                if (warning_type == 
                    LibCommon.WarningType.OVER_LIMIT.ToString())
                {
                    ent.OverLimitWarningResult.ID = 
                        dt.Rows[i][PreWarningResultDBConstNames.ID].ToString();
                    int.TryParse(dt.Rows[i][LibBusiness.PreWarningResultDBConstNames.WARNING_RESULT].ToString(),
                        out defultValue);
                    ent.OverLimitWarningResult.WarningResult = defultValue;
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
                    ent.OverLimitWarningResult.Geology = defultValue;
                    defultValue = (int)WarningResult.NULL;
                    int.TryParse(dt.Rows[i][LibBusiness.PreWarningResultDBConstNames.VENTILATION].ToString(),
                        out defultValue);
                    ent.OverLimitWarningResult.Ventilation = defultValue;
                    defultValue = (int)WarningResult.NULL;
                    int.TryParse(dt.Rows[i][LibBusiness.PreWarningResultDBConstNames.MANAGEMENT].ToString(),
                        out defultValue);
                    ent.OverLimitWarningResult.Management = defultValue;
                }
                if (warning_type == 
                    LibCommon.WarningType.OUTBURST.ToString())
                {
                    ent.OutBrustWarningResult.ID = 
                        dt.Rows[i][PreWarningResultDBConstNames.ID].ToString();
                    int.TryParse(dt.Rows[i][LibBusiness.PreWarningResultDBConstNames.WARNING_RESULT].ToString(),
                        out defultValue);
                    ent.OutBrustWarningResult.WarningResult = defultValue;
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
                    ent.OutBrustWarningResult.Geology = defultValue;
                    defultValue = (int)WarningResult.NULL;
                    int.TryParse(dt.Rows[i][LibBusiness.PreWarningResultDBConstNames.VENTILATION].ToString(),
                        out defultValue);
                    ent.OutBrustWarningResult.Ventilation = defultValue;
                    defultValue = (int)WarningResult.NULL;
                    int.TryParse(dt.Rows[i][LibBusiness.PreWarningResultDBConstNames.MANAGEMENT].ToString(),
                        out defultValue);
                    ent.OutBrustWarningResult.Management = defultValue;
                }

                if (i % 2 == 1)
                {
                    ents.Add(ent);
                }
            }
            return ents;
        }

        /// <summary>
        /// 通过时间选择所有巷道
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static string[] GetTunnleIDBetweenSelectTime(ManageDataBase 
            database, string startTime, string endTime)
        {
            string[] tunnelIDs = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT DISTINCT ");
            strSql.Append(PreWarningResultDBConstNames.TUNNEL_ID);
            strSql.Append(" FROM ");
            strSql.Append(PreWarningResultDBConstNames.TABLE_NAME);
            strSql.Append(" WHERE ");
            strSql.Append(PreWarningResultDBConstNames.DATA_TIME);
            strSql.Append(" >= ");
            strSql.Append("'" + startTime + "' ");
            strSql.Append(" AND ");
            strSql.Append(PreWarningResultDBConstNames.DATA_TIME);
            strSql.Append(" <= ");
            strSql.Append("'" + endTime + "'");
            DataTable dt = 
                database.ReturnDSNotOpenAndClose(strSql.ToString()).Tables[0];
            #region 此处需要优化 marked by yanger
            if (dt != null)
            {
                int rowCount = dt.Rows.Count;
                if (rowCount > 0)
                {
                    tunnelIDs = new string[rowCount];
                    for (int i = 0; i < rowCount; i++)
                    {
                        tunnelIDs[i] = dt.Rows[i][0].ToString();
                    }
                }
            }
            #endregion

            return tunnelIDs;
        }

        /// <summary>
        /// 通过时间选择所有巷道
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static string[] GetTunnleIDBetweenSelectTime(ManageDataBase 
            database, string startTime, string endTime, string workfaceName)
        {
            string[] tunnelIDs = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT DISTINCT " + 
                PreWarningResultDBConstNames.TUNNEL_ID + " FROM " + 
                PreWarningResultViewDbConstNames.VIEW_NAME);
            strSql.Append(" WHERE " + 
                WorkingFaceDbConstNames.WORKINGFACE_NAME);
            strSql.Append(" = '" + workfaceName + "'");
            strSql.Append(" AND ");
            strSql.Append(PreWarningResultDBConstNames.DATA_TIME);
            strSql.Append(" >= ");
            strSql.Append("'" + startTime + "' ");
            strSql.Append(" AND ");
            strSql.Append(PreWarningResultDBConstNames.DATA_TIME);
            strSql.Append(" <= ");
            strSql.Append("'" + endTime + "'");
            DataTable dt = 
                database.ReturnDSNotOpenAndClose(strSql.ToString()).Tables[0];
            #region 此处需要优化 marked by yanger
            if (dt != null)
            {
                int rowCount = dt.Rows.Count;
                if (rowCount > 0)
                {
                    tunnelIDs = new string[rowCount];
                    for (int i = 0; i < rowCount; i++)
                    {
                        tunnelIDs[i] = dt.Rows[i][0].ToString();
                    }
                }
            }
            #endregion

            return tunnelIDs;
        }

        public static DataTable 
            GetWarningResultsWithCondition(ManageDataBase database, string 
            startTime, string endTime, string workfaceName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM " + 
                PreWarningResultViewDbConstNames.VIEW_NAME);
            strSql.Append(" WHERE 1=1 ");
            if (!String.IsNullOrEmpty(workfaceName))
            {
                strSql.Append(" AND ");
                strSql.Append(WorkingFaceDbConstNames.WORKINGFACE_NAME);
                strSql.Append(" = '" + workfaceName + "'");
            }
            strSql.Append(" AND ");
            strSql.Append(PreWarningResultDBConstNames.DATA_TIME);
            strSql.Append(" >= ");
            strSql.Append("'" + startTime + "' ");
            strSql.Append(" AND ");
            strSql.Append(PreWarningResultDBConstNames.DATA_TIME);
            strSql.Append(" <= ");
            strSql.Append("'" + endTime + "'");
            strSql.Append(" AND ");
            strSql.Append(PreWarningResultDBConstNames.WARNING_RESULT + 
                " < 2");

            DataTable dt = 
                database.ReturnDSNotOpenAndClose(strSql.ToString()).Tables[0];

            return dt;
        }

        /// <summary>
        /// 获取一条巷道在规定时间内，发生预警的日期
        /// </summary>
        /// <param name="database"></param>
        /// <param name="tunnelID"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        //public static string[] GetDateBySelectTimeAndTunnelID(ManageDataBase database, string tunnelID, string startTime, string endTime)
        //{
        //    string[] datatimes = null;
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("SELECT DISTINCT ");
        //    strSql.Append(" CONVERT(CHAR(10),");
        //    strSql.Append(PreWarningResultDBConstNames.DATA_TIME);
        //    strSql.Append(",120) AS 'DATE' ");
        //    strSql.Append(" FROM ");
        //    strSql.Append(PreWarningResultDBConstNames.TABLE_NAME);
        //    strSql.Append(" WHERE ");
        //    strSql.Append(PreWarningResultDBConstNames.TUNNEL_ID);
        //    strSql.Append(" = ");
        //    strSql.Append("'" + tunnelID + "'");
        //    strSql.Append(" AND ( ");
        //    strSql.Append(PreWarningResultDBConstNames.DATA_TIME);
        //    strSql.Append(" >= ");
        //    strSql.Append("'" + startTime + "'");
        //    strSql.Append(" AND ");
        //    strSql.Append(PreWarningResultDBConstNames.DATA_TIME);
        //    strSql.Append(" <= ");
        //    strSql.Append("'" + endTime + "')");
        //    #region 此处需要优化 marked by yanger
        //    DataTable dt = database.ReturnDSNotOpenAndClose(strSql.ToString()).Tables[0];
        //    if (dt != null)
        //    {
        //        int rowCount = dt.Rows.Count;
        //        if (rowCount > 0)
        //        {
        //            datatimes = new string[rowCount];
        //            for (int i = 0; i < rowCount; i++)
        //            {
        //                datatimes[i] = dt.Rows[i]["DATE"].ToString();
        //            }
        //        }
        //    }
        //    #endregion
        //    return datatimes;
        //}

        public static DateTime[] 
            GetDateBySelectTimeAndWorkingfaceName(ManageDataBase database, 
            string workingfaceName, string startTime, string endTime)
        {
            DateTime[] datatimes = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT DISTINCT ");
            strSql.Append(" CONVERT(CHAR(10),");
            strSql.Append(PreWarningResultDBConstNames.DATA_TIME);
            strSql.Append(",120) AS 'DATE' ");
            strSql.Append(" FROM ");
            strSql.Append(PreWarningResultViewDbConstNames.VIEW_NAME);
            strSql.Append(" WHERE 1=1 ");
            if (!String.IsNullOrEmpty(workingfaceName))
            {
                strSql.Append(" AND ");
                strSql.Append(WorkingFaceDbConstNames.WORKINGFACE_NAME);
                strSql.Append(" = ");
                strSql.Append("'" + workingfaceName + "'");
            }
            strSql.Append(" AND  ");
            strSql.Append(PreWarningResultDBConstNames.DATA_TIME);
            strSql.Append(" >= ");
            strSql.Append("'" + startTime + "'");
            strSql.Append(" AND ");
            strSql.Append(PreWarningResultDBConstNames.DATA_TIME);
            strSql.Append(" <= ");
            strSql.Append("'" + endTime + "'");
            #region 此处需要优化 marked by yanger
            DataTable dt = 
                database.ReturnDSNotOpenAndClose(strSql.ToString()).Tables[0];
            if (dt != null)
            {
                int rowCount = dt.Rows.Count;
                if (rowCount > 0)
                {
                    datatimes = new DateTime[rowCount];
                    for (int i = 0; i < rowCount; i++)
                    {
                        datatimes[i] = 
                            Convert.ToDateTime(dt.Rows[i]["DATE"]);
                    }
                }
            }
            #endregion
            return datatimes;
        }


        /// <summary>
        /// 获取默认的班次名称,和起止时间
        /// </summary>
        /// <param name="database"></param>
        /// <returns></returns>
        public static WorkingTime[] GetDateShift(ManageDataBase database)
        {
            return 
                WorkingTime.FindAllByWorkTimeGroupId(WorkingTimeDefault.FindFirst().DefaultWorkTimeGroupId);
        }

        /// <summary>
        /// 根据巷道ID和时间获取所有班次
        /// </summary>
        /// <param name="tunnelID"></param>
        /// <param name="datatime"></param>
        /// <returns></returns>
        public static string[] 
            GetDateShiftByDatatimeAndTunnelID(ManageDataBase database, string 
            tunnelID, string date)
        {
            //select distinct date_shift from T_EARLY_WARNING_RESULT where convert(char(10),data_time,120) = '2014-03-08'
            string[] datashifts = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT DISTINCT ");
            strSql.Append(PreWarningResultDBConstNames.DATE_SHIFT);
            strSql.Append(" FROM ");
            strSql.Append(PreWarningResultDBConstNames.TABLE_NAME);
            strSql.Append(" WHERE ");
            strSql.Append(" convert(char(10),");
            strSql.Append(PreWarningResultDBConstNames.DATA_TIME);
            strSql.Append(",120) ");
            strSql.Append(" = ");
            strSql.Append("'" + date + "'");
            strSql.Append(" AND ");
            strSql.Append(PreWarningResultDBConstNames.TUNNEL_ID);
            strSql.Append(" = ");
            strSql.Append("'" + tunnelID + "'");
            DataTable dt = 
                database.ReturnDSNotOpenAndClose(strSql.ToString()).Tables[0];
            if (dt != null)
            {
                int rowCount = dt.Rows.Count;
                if (rowCount > 0)
                {
                    datashifts = new string[rowCount];
                    for (int i = 0; i < rowCount; i++)
                    {
                        datashifts[i] = dt.Rows[i][0].ToString();
                    }
                }
            }
            return datashifts;
        }

        /// <summary>
        /// 获取班次时间内每一巷道ID的最糟糕结果
        /// </summary>
        /// <param name="dateshift">班次</param>
        /// <param name="tunnelID">巷道ID</param>
        /// <param name="warningType">预警类型</param>
        /// <param name="startTime">起始时间</param>
        /// <param name="endTime">终止时间</param>
        //public static void GetTunnelWorstPreWarningResultInOneDateshift(ManageDataBase database, string date, string dateshift,
        //    string tunnelID, string warningType, string fromTime, string toTime)
        //{
        //    PreWarningHistoryResultEnt ent = new PreWarningHistoryResultEnt();
        //    //共同属性
        //    int value;
        //    int.TryParse(tunnelID, out value);
        //    ent.Tunnel = value;

        //    string startTime = date + " " + fromTime;
        //    string endTime = date + " " + toTime;

        //    ent.TunelName = GetTunelNameByTunelID(tunnelID);
        //    ent.DateTime = date;
        //    ent.Date_Shift = dateshift;
        //    //超限结果
        //    ent.OverLimitWarningResult.WarningResult =
        //        GetWarningTypeTunnelWorstPreWarningResultInOneDateshift(database, dateshift,
        //            tunnelID, (int)LibCommon.WarningType.OVER_LIMIT,
        //            startTime, endTime);
        //    //突出结果
        //    ent.OutBrustWarningResult.WarningResult =
        //        GetWarningTypeTunnelWorstPreWarningResultInOneDateshift(database, dateshift,
        //        tunnelID, (int)LibCommon.WarningType.OUTBURST,
        //        startTime, endTime);

        //    //过滤绿色的预警结果
        //    if (ent.OverLimitWarningResult.WarningResult >= (int)LibCommon.WarningResult.GREEN &&
        //        ent.OutBrustWarningResult.WarningResult >= (int)LibCommon.WarningResult.GREEN)
        //    {
        //        return;
        //    }

        //    //瓦斯
        //    ent.OverLimitWarningResult.Gas =
        //        GetGistTunnelWorstPreWarningResultInOneDateshift(database, PreWarningResultDBConstNames.GAS,
        //            (int)LibCommon.WarningType.OVER_LIMIT, dateshift, tunnelID,
        //            startTime, endTime);
        //    //煤层
        //    ent.OverLimitWarningResult.Coal =
        //        GetGistTunnelWorstPreWarningResultInOneDateshift(database, PreWarningResultDBConstNames.COAL,
        //        (int)LibCommon.WarningType.OVER_LIMIT, dateshift, tunnelID,
        //        startTime, endTime);
        //    //地质
        //    ent.OverLimitWarningResult.Geology =
        //        GetGistTunnelWorstPreWarningResultInOneDateshift(database, PreWarningResultDBConstNames.GEOLOGY,
        //        (int)LibCommon.WarningType.OVER_LIMIT, dateshift, tunnelID,
        //        startTime, endTime);
        //    //通风
        //    ent.OverLimitWarningResult.Ventilation =
        //        GetGistTunnelWorstPreWarningResultInOneDateshift(database, PreWarningResultDBConstNames.VENTILATION,
        //        (int)LibCommon.WarningType.OVER_LIMIT, dateshift, tunnelID, startTime, endTime);
        //    //管理
        //    ent.OverLimitWarningResult.Management =
        //        GetGistTunnelWorstPreWarningResultInOneDateshift(database, PreWarningResultDBConstNames.MANAGEMENT,
        //        (int)LibCommon.WarningType.OVER_LIMIT, dateshift, tunnelID, startTime, endTime);

        //    //瓦斯
        //    ent.OutBrustWarningResult.Gas =
        //        GetGistTunnelWorstPreWarningResultInOneDateshift(database, PreWarningResultDBConstNames.GAS,
        //        (int)LibCommon.WarningType.OUTBURST, dateshift, tunnelID, startTime, endTime);
        //    //煤层
        //    ent.OutBrustWarningResult.Coal =
        //        GetGistTunnelWorstPreWarningResultInOneDateshift(database, PreWarningResultDBConstNames.COAL,
        //        (int)LibCommon.WarningType.OUTBURST, dateshift, tunnelID, startTime, endTime);
        //    //地质
        //    ent.OutBrustWarningResult.Geology =
        //        GetGistTunnelWorstPreWarningResultInOneDateshift(database, PreWarningResultDBConstNames.GEOLOGY,
        //        (int)LibCommon.WarningType.OUTBURST, dateshift, tunnelID, startTime, endTime);
        //    //通风
        //    ent.OutBrustWarningResult.Ventilation =
        //        GetGistTunnelWorstPreWarningResultInOneDateshift(database, PreWarningResultDBConstNames.VENTILATION,
        //        (int)LibCommon.WarningType.OUTBURST, dateshift, tunnelID, startTime, endTime);
        //    //管理
        //    ent.OutBrustWarningResult.Management =
        //        GetGistTunnelWorstPreWarningResultInOneDateshift(database, PreWarningResultDBConstNames.MANAGEMENT,
        //        (int)LibCommon.WarningType.OUTBURST, dateshift, tunnelID, startTime, endTime);

        //    InsertSortedDataIntoTable(database, ent);
        //}

        //public static void GetWorkingfaceWorstPreWarningResultInOneDateshift(ManageDataBase database, string date, string dateshift,
        //    string workfaceName, string warningType, string fromTime, string toTime)
        //{
        //    PreWarningHistoryResultWithWorkingfaceEnt ent = new PreWarningHistoryResultWithWorkingfaceEnt();
        //    //共同属性
        //    //int value;
        //    //int.TryParse(tunnelID, out value);
        //    //ent.Tunnel = value;

        //    string startTime = date + " " + fromTime;
        //    string endTime = date + " " + toTime;

        //    ent.WorkingfaceName = workfaceName;
        //    ent.DateTime = date;
        //    ent.Date_Shift = dateshift;
        //    //超限结果
        //    ent.OverLimitWarningResult.WarningResult =
        //        GetWarningTypeTunnelWorstPreWarningResultInOneDateshift(database, dateshift,
        //            workfaceName, (int)LibCommon.WarningType.OVER_LIMIT,
        //            startTime, endTime);
        //    //突出结果
        //    ent.OutBrustWarningResult.WarningResult =
        //        GetWarningTypeTunnelWorstPreWarningResultInOneDateshift(database, dateshift,
        //        workfaceName, (int)LibCommon.WarningType.OUTBURST,
        //        startTime, endTime);

        //    //过滤绿色的预警结果
        //    if (ent.OverLimitWarningResult.WarningResult >= (int)LibCommon.WarningResult.GREEN &&
        //        ent.OutBrustWarningResult.WarningResult >= (int)LibCommon.WarningResult.GREEN)
        //    {
        //        return;
        //    }

        //    //瓦斯
        //    ent.OverLimitWarningResult.Gas =
        //        GetGistWorkingfaceWorstPreWarningResultInOneDateshift(database, PreWarningResultDBConstNames.GAS,
        //            (int)LibCommon.WarningType.OVER_LIMIT, dateshift, workfaceName,
        //            startTime, endTime);
        //    //煤层
        //    ent.OverLimitWarningResult.Coal =
        //        GetGistWorkingfaceWorstPreWarningResultInOneDateshift(database, PreWarningResultDBConstNames.COAL,
        //        (int)LibCommon.WarningType.OVER_LIMIT, dateshift, workfaceName,
        //        startTime, endTime);
        //    //地质
        //    ent.OverLimitWarningResult.Geology =
        //        GetGistWorkingfaceWorstPreWarningResultInOneDateshift(database, PreWarningResultDBConstNames.GEOLOGY,
        //        (int)LibCommon.WarningType.OVER_LIMIT, dateshift, workfaceName,
        //        startTime, endTime);
        //    //通风
        //    ent.OverLimitWarningResult.Ventilation =
        //        GetGistWorkingfaceWorstPreWarningResultInOneDateshift(database, PreWarningResultDBConstNames.VENTILATION,
        //        (int)LibCommon.WarningType.OVER_LIMIT, dateshift, workfaceName, startTime, endTime);
        //    //管理
        //    ent.OverLimitWarningResult.Management =
        //        GetGistWorkingfaceWorstPreWarningResultInOneDateshift(database, PreWarningResultDBConstNames.MANAGEMENT,
        //        (int)LibCommon.WarningType.OVER_LIMIT, dateshift, workfaceName, startTime, endTime);

        //    //瓦斯
        //    ent.OutBrustWarningResult.Gas =
        //        GetGistWorkingfaceWorstPreWarningResultInOneDateshift(database, PreWarningResultDBConstNames.GAS,
        //        (int)LibCommon.WarningType.OUTBURST, dateshift, workfaceName, startTime, endTime);
        //    //煤层
        //    ent.OutBrustWarningResult.Coal =
        //        GetGistWorkingfaceWorstPreWarningResultInOneDateshift(database, PreWarningResultDBConstNames.COAL,
        //        (int)LibCommon.WarningType.OUTBURST, dateshift, workfaceName, startTime, endTime);
        //    //地质
        //    ent.OutBrustWarningResult.Geology =
        //        GetGistWorkingfaceWorstPreWarningResultInOneDateshift(database, PreWarningResultDBConstNames.GEOLOGY,
        //        (int)LibCommon.WarningType.OUTBURST, dateshift, workfaceName, startTime, endTime);
        //    //通风
        //    ent.OutBrustWarningResult.Ventilation =
        //        GetGistWorkingfaceWorstPreWarningResultInOneDateshift(database, PreWarningResultDBConstNames.VENTILATION,
        //        (int)LibCommon.WarningType.OUTBURST, dateshift, workfaceName, startTime, endTime);
        //    //管理
        //    ent.OutBrustWarningResult.Management =
        //        GetGistWorkingfaceWorstPreWarningResultInOneDateshift(database, PreWarningResultDBConstNames.MANAGEMENT,
        //        (int)LibCommon.WarningType.OUTBURST, dateshift, workfaceName, startTime, endTime);

        //    InsertSortedDataIntoTable(database, ent);
        //}

        private static int DataTableFilterReturnWarningLevel(DataTable dt, 
            int warningType, string filter)
        {
            int result = 4;
            DataRow[] drRows = 
                dt.Select(PreWarningResultDBConstNames.WARNING_TYPE + " = " + 
                warningType);
            if (drRows.Length > 0)
                result = drRows.Min(r => r.Field<int>(filter));
            return result;
        }

        public static void 
            GetWorkingfaceWorstPreWarningResultInOneDateshift(ManageDataBase 
            database, DataTable dt, DateTime date,
            string dateshift,
            string workfaceName, string warningType, DateTime fromTime, 
                DateTime toTime, string warningRorY)
        {
            PreWarningHistoryResultWithWorkingfaceEnt ent = new 
                PreWarningHistoryResultWithWorkingfaceEnt();
            //共同属性
            //int value;
            //int.TryParse(tunnelID, out value);
            //ent.Tunnel = value;

            string startTime = date + " " + fromTime;
            string endTime = date + " " + toTime;

            ent.WorkingfaceName = workfaceName;
            ent.DateTime = date;
            ent.Date_Shift = dateshift;
            //超限结果
            //DataRow[] drRows = dt.Select(PreWarningResultDBConstNames.DATE_SHIFT + " = " + dateshift + " AND " + PreWarningResultDBConstNames.DATA_TIME + " > " + fromTime + " AND " + PreWarningResultDBConstNames.DATA_TIME + " < " + toTime, PreWarningResultDBConstNames.WARNING_RESULT + " DESC ");
            //DataTable dtSelected = dt.Clone();
            //foreach (DataRow row in drRows)
            //{
            //    dtSelected.ImportRow(row);
            //}
            string queryStr = PreWarningResultDBConstNames.DATE_SHIFT + 
                " = '" + dateshift + "' AND " +
                  WorkingFaceDbConstNames.WORKINGFACE_NAME + " = '" + 
                      workfaceName + "' AND " +
                //PreWarningResultDBConstNames.WARNING_TYPE + " = " + warningType + " AND " +
                  PreWarningResultDBConstNames.DATA_TIME + " > '" + 
                      startTime + "' AND " +
                  PreWarningResultDBConstNames.DATA_TIME + " < '" + endTime 
                      + "'";
            DataRow[] drRows = dt.Select(queryStr);

            DataTable dtSelected = dt.Clone();
            foreach (var i in drRows)
            {
                dtSelected.ImportRow(i);
            }

            ent.OverLimitWarningResult.WarningResult = 
                DataTableFilterReturnWarningLevel(dtSelected, 
                (int)LibCommon.WarningType.OVER_LIMIT, 
                PreWarningResultDBConstNames.WARNING_RESULT);

            ent.OutBrustWarningResult.WarningResult =
                DataTableFilterReturnWarningLevel(dtSelected, 
                    (int)LibCommon.WarningType.OUTBURST, 
                    PreWarningResultDBConstNames.WARNING_RESULT);

            //过滤绿色的预警结果
            if (ent.OverLimitWarningResult.WarningResult >= 
                (int)LibCommon.WarningResult.GREEN &&
                ent.OutBrustWarningResult.WarningResult >= 
                    (int)LibCommon.WarningResult.GREEN)
            {
                return;
            }

            if (warningRorY == "红色预警")
            {
                if (ent.OverLimitWarningResult.WarningResult != 
                    (int)LibCommon.WarningResult.RED &&
                    ent.OutBrustWarningResult.WarningResult != 
                        (int)LibCommon.WarningResult.RED)
                {
                    return;
                }
            }

            if (warningRorY == "黄色预警")
            {
                if (ent.OverLimitWarningResult.WarningResult != 
                    (int)LibCommon.WarningResult.YELLOW &&
    ent.OutBrustWarningResult.WarningResult != 
        (int)LibCommon.WarningResult.YELLOW)
                {
                    return;
                }
            }

            //瓦斯
            ent.OverLimitWarningResult.Gas =
                 DataTableFilterReturnWarningLevel(dtSelected, 
                     (int)LibCommon.WarningType.OVER_LIMIT,
                    PreWarningResultDBConstNames.GAS);
            //煤层
            ent.OverLimitWarningResult.Coal =
               DataTableFilterReturnWarningLevel(dtSelected, 
                   (int)LibCommon.WarningType.OVER_LIMIT,
                    PreWarningResultDBConstNames.COAL);
            //地质
            ent.OverLimitWarningResult.Geology =
                DataTableFilterReturnWarningLevel(dtSelected, 
                    (int)LibCommon.WarningType.OVER_LIMIT,
                    PreWarningResultDBConstNames.GEOLOGY);
            //通风
            ent.OverLimitWarningResult.Ventilation =
                DataTableFilterReturnWarningLevel(dtSelected, 
                    (int)LibCommon.WarningType.OVER_LIMIT,
                    PreWarningResultDBConstNames.VENTILATION);
            //管理
            ent.OverLimitWarningResult.Management =
               DataTableFilterReturnWarningLevel(dtSelected, 
                   (int)LibCommon.WarningType.OVER_LIMIT,
                    PreWarningResultDBConstNames.MANAGEMENT);

            //瓦斯
            ent.OutBrustWarningResult.Gas =
                 DataTableFilterReturnWarningLevel(dtSelected, 
                     (int)LibCommon.WarningType.OUTBURST,
                    PreWarningResultDBConstNames.GAS);
            //煤层
            ent.OutBrustWarningResult.Coal =
                 DataTableFilterReturnWarningLevel(dtSelected, 
                     (int)LibCommon.WarningType.OUTBURST,
                    PreWarningResultDBConstNames.COAL);
            //地质
            ent.OutBrustWarningResult.Geology =
               DataTableFilterReturnWarningLevel(dtSelected, 
                   (int)LibCommon.WarningType.OUTBURST,
                    PreWarningResultDBConstNames.GEOLOGY);
            //通风
            ent.OutBrustWarningResult.Ventilation =
                 DataTableFilterReturnWarningLevel(dtSelected,
                    (int)LibCommon.WarningType.OUTBURST,
                    PreWarningResultDBConstNames.VENTILATION);
            //管理
            ent.OutBrustWarningResult.Management =
                DataTableFilterReturnWarningLevel(dtSelected, 
                    (int)LibCommon.WarningType.OUTBURST,
                    PreWarningResultDBConstNames.MANAGEMENT);

            InsertSortedDataIntoTable(database, ent);
        }

        /// <summary>
        /// 选出预警依据中最严重的记录
        /// </summary>
        /// <param name="item">依据类型，瓦斯，地质，煤层，管理，通风</param>
        /// <param name="dateshift">班次</param>
        /// <param name="tunnelID">巷道ID</param>
        /// <param name="warningType">类型：突出，超限</param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        //public static int GetGistTunnelWorstPreWarningResultInOneDateshift(ManageDataBase database, string item,
        //    int warning_type, string dateshift, string tunnelID, string startTime, string endTime)
        //{
        //    DataTable dt = new DataTable();
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("SELECT TOP 1 *");
        //    strSql.Append(" FROM ");
        //    strSql.Append(PreWarningResultDBConstNames.TABLE_NAME);
        //    strSql.Append(" WHERE ");
        //    strSql.Append(PreWarningResultDBConstNames.TUNNEL_ID);
        //    strSql.Append(" = ");
        //    strSql.Append("'" + tunnelID + "'");
        //    strSql.Append(" AND ( ");
        //    strSql.Append(PreWarningResultDBConstNames.DATE_SHIFT);
        //    strSql.Append(" = ");
        //    strSql.Append("'" + dateshift + "')");
        //    strSql.Append(" AND ( ");
        //    strSql.Append(PreWarningResultDBConstNames.WARNING_TYPE);
        //    strSql.Append(" = ");
        //    strSql.Append("" + warning_type + ")");
        //    strSql.Append(" AND ( ");
        //    strSql.Append(PreWarningResultDBConstNames.DATA_TIME);
        //    strSql.Append(" >= ");
        //    strSql.Append("'" + startTime + "'");
        //    strSql.Append(" AND ");
        //    strSql.Append(PreWarningResultDBConstNames.DATA_TIME);
        //    strSql.Append(" <= ");
        //    strSql.Append("'" + endTime + "')");
        //    strSql.Append("ORDER BY ");
        //    strSql.Append(item);

        //    dt = database.ReturnDSNotOpenAndClose(strSql.ToString()).Tables[0];
        //    if (dt != null)
        //    {
        //        int rowCount = dt.Rows.Count;
        //        if (dt.Rows.Count == 1)
        //        {
        //            if (dt.Rows[0][item].ToString() == ((int)LibCommon.WarningResult.RED).ToString())
        //            {
        //                return (int)LibCommon.WarningResult.RED;
        //            }
        //            else if (dt.Rows[0][item].ToString() == ((int)LibCommon.WarningResult.YELLOW).ToString())
        //            {
        //                return (int)LibCommon.WarningResult.YELLOW;
        //            }
        //            else if (dt.Rows[0][item].ToString() == ((int)LibCommon.WarningResult.GREEN).ToString())
        //            {
        //                return (int)LibCommon.WarningResult.GREEN;
        //            }
        //            else if (dt.Rows[0][item].ToString() == ((int)LibCommon.WarningResult.NOT_AVAILABLE).ToString())
        //            {
        //                return (int)LibCommon.WarningResult.NOT_AVAILABLE;
        //            }
        //            else
        //            {
        //                return (int)LibCommon.WarningResult.NULL;
        //            }
        //        }
        //    }
        //    return (int)LibCommon.WarningResult.NULL;
        //}


        public static int 
            GetGistWorkingfaceWorstPreWarningResultInOneDateshift(ManageDataBase 
            database, string item,
    int warning_type, string dateshift, string workingfaceName, string 
        startTime, string endTime)
        {
            DataTable dt = new DataTable();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT TOP 1 *");
            strSql.Append(" FROM ");
            strSql.Append(PreWarningResultViewDbConstNames.VIEW_NAME);
            strSql.Append(" WHERE ");
            strSql.Append(WorkingFaceDbConstNames.WORKINGFACE_NAME);
            strSql.Append(" = ");
            strSql.Append("'" + workingfaceName + "'");
            strSql.Append(" AND ( ");
            strSql.Append(PreWarningResultDBConstNames.DATE_SHIFT);
            strSql.Append(" = ");
            strSql.Append("'" + dateshift + "')");
            strSql.Append(" AND ( ");
            strSql.Append(PreWarningResultDBConstNames.WARNING_TYPE);
            strSql.Append(" = ");
            strSql.Append("" + warning_type + ")");
            strSql.Append(" AND ( ");
            strSql.Append(PreWarningResultDBConstNames.DATA_TIME);
            strSql.Append(" >= ");
            strSql.Append("'" + startTime + "'");
            strSql.Append(" AND ");
            strSql.Append(PreWarningResultDBConstNames.DATA_TIME);
            strSql.Append(" <= ");
            strSql.Append("'" + endTime + "')");
            strSql.Append("ORDER BY ");
            strSql.Append(item);

            dt = 
                database.ReturnDSNotOpenAndClose(strSql.ToString()).Tables[0];
            if (dt != null)
            {
                int rowCount = dt.Rows.Count;
                if (dt.Rows.Count == 1)
                {
                    if (dt.Rows[0][item].ToString() == 
                        ((int)LibCommon.WarningResult.RED).ToString())
                    {
                        return (int)LibCommon.WarningResult.RED;
                    }
                    else if (dt.Rows[0][item].ToString() == 
                        ((int)LibCommon.WarningResult.YELLOW).ToString())
                    {
                        return (int)LibCommon.WarningResult.YELLOW;
                    }
                    else if (dt.Rows[0][item].ToString() == 
                        ((int)LibCommon.WarningResult.GREEN).ToString())
                    {
                        return (int)LibCommon.WarningResult.GREEN;
                    }
                    else if (dt.Rows[0][item].ToString() == 
                        ((int)LibCommon.WarningResult.NOT_AVAILABLE).ToString())
                    {
                        return (int)LibCommon.WarningResult.NOT_AVAILABLE;
                    }
                    else
                    {
                        return (int)LibCommon.WarningResult.NULL;
                    }
                }
            }
            return (int)LibCommon.WarningResult.NULL;
        }


        /// <summary>
        /// 获取突出预警、超限预警中最严重的结果
        /// </summary>
        /// <param name="dateshift"></param>
        /// <param name="tunnelID"></param>
        /// <param name="warningType"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        //public static int GetWarningTypeTunnelWorstPreWarningResultInOneDateshift(ManageDataBase database, string dateshift,
        //    string tunnelID, int warningType, string startTime, string endTime)
        //{
        //    DataTable dt = new DataTable();
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("SELECT TOP 1 *");
        //    strSql.Append(" FROM ");
        //    strSql.Append(PreWarningResultDBConstNames.TABLE_NAME);
        //    strSql.Append(" WHERE ");
        //    strSql.Append(PreWarningResultDBConstNames.TUNNEL_ID);
        //    strSql.Append(" = ");
        //    strSql.Append("'" + tunnelID + "'");
        //    strSql.Append(" AND ( ");
        //    strSql.Append(PreWarningResultDBConstNames.DATE_SHIFT);
        //    strSql.Append(" = ");
        //    strSql.Append("'" + dateshift + "')");
        //    strSql.Append(" AND ( ");
        //    strSql.Append(PreWarningResultDBConstNames.WARNING_TYPE);
        //    strSql.Append(" = ");
        //    strSql.Append(+warningType + ")");
        //    strSql.Append(" AND ( ");
        //    strSql.Append(PreWarningResultDBConstNames.DATA_TIME);
        //    strSql.Append(" >= ");
        //    strSql.Append("'" + startTime + "'");
        //    strSql.Append(" AND ");
        //    strSql.Append(PreWarningResultDBConstNames.DATA_TIME);
        //    strSql.Append(" <= ");
        //    strSql.Append("'" + endTime + "')");
        //    strSql.Append("ORDER BY ");
        //    strSql.Append(PreWarningResultDBConstNames.WARNING_RESULT);

        //    dt = database.ReturnDSNotOpenAndClose(strSql.ToString()).Tables[0];
        //    if (dt != null)
        //    {
        //        int rowCount = dt.Rows.Count;
        //        if (dt.Rows.Count == 1)
        //        {
        //            DataRow dr = dt.Rows[0];
        //            if (dr[PreWarningResultDBConstNames.WARNING_RESULT].ToString() == ((int)LibCommon.WarningResult.RED).ToString())
        //            {
        //                return (int)LibCommon.WarningResult.RED;
        //            }
        //            else if (dr[PreWarningResultDBConstNames.WARNING_RESULT].ToString() == ((int)LibCommon.WarningResult.YELLOW).ToString())
        //            {
        //                return (int)LibCommon.WarningResult.YELLOW;
        //            }
        //            else if (dr[PreWarningResultDBConstNames.WARNING_RESULT].ToString() == ((int)LibCommon.WarningResult.GREEN).ToString())
        //            {
        //                return (int)LibCommon.WarningResult.GREEN;
        //            }
        //            else if (dr[PreWarningResultDBConstNames.WARNING_RESULT].ToString() == ((int)LibCommon.WarningResult.NOT_AVAILABLE).ToString())
        //            {
        //                return (int)LibCommon.WarningResult.NOT_AVAILABLE;
        //            }
        //            else
        //            {
        //                return (int)LibCommon.WarningResult.NULL;
        //            }
        //        }
        //    }
        //    return (int)LibCommon.WarningResult.NULL;
        //}



        public static int 
            GetWarningTypeTunnelWorstPreWarningResultInOneDateshift(ManageDataBase 
            database, string dateshift,
            string workingfaceName, int warningType, string startTime, 
                string endTime)
        {
            DataTable dt = new DataTable();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT TOP 1 *");
            strSql.Append(" FROM ");
            strSql.Append(PreWarningResultViewDbConstNames.VIEW_NAME);
            strSql.Append(" WHERE ");
            strSql.Append(WorkingFaceDbConstNames.WORKINGFACE_NAME);
            strSql.Append(" = ");
            strSql.Append("'" + workingfaceName + "'");
            strSql.Append(" AND  ");
            strSql.Append(PreWarningResultDBConstNames.DATE_SHIFT);
            strSql.Append(" = ");
            strSql.Append("'" + dateshift + "'");
            strSql.Append(" AND  ");
            strSql.Append(PreWarningResultDBConstNames.WARNING_TYPE);
            strSql.Append(" = ");
            strSql.Append(+warningType + "");
            strSql.Append(" AND  ");
            strSql.Append(PreWarningResultDBConstNames.DATA_TIME);
            strSql.Append(" >= ");
            strSql.Append("'" + startTime + "'");
            strSql.Append(" AND ");
            strSql.Append(PreWarningResultDBConstNames.DATA_TIME);
            strSql.Append(" <= ");
            strSql.Append("'" + endTime + "'");
            strSql.Append("ORDER BY ");
            strSql.Append(PreWarningResultDBConstNames.WARNING_RESULT);

            dt = 
                database.ReturnDSNotOpenAndClose(strSql.ToString()).Tables[0];
            if (dt != null)
            {
                int rowCount = dt.Rows.Count;
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    if 
                        (dr[PreWarningResultDBConstNames.WARNING_RESULT].ToString() 
                        == ((int)LibCommon.WarningResult.RED).ToString())
                    {
                        return (int)LibCommon.WarningResult.RED;
                    }
                    else if 
                        (dr[PreWarningResultDBConstNames.WARNING_RESULT].ToString() 
                        == ((int)LibCommon.WarningResult.YELLOW).ToString())
                    {
                        return (int)LibCommon.WarningResult.YELLOW;
                    }
                    else if 
                        (dr[PreWarningResultDBConstNames.WARNING_RESULT].ToString() 
                        == ((int)LibCommon.WarningResult.GREEN).ToString())
                    {
                        return (int)LibCommon.WarningResult.GREEN;
                    }
                    else if 
                        (dr[PreWarningResultDBConstNames.WARNING_RESULT].ToString() 
                        == 
                        ((int)LibCommon.WarningResult.NOT_AVAILABLE).ToString())
                    {
                        return (int)LibCommon.WarningResult.NOT_AVAILABLE;
                    }
                    else
                    {
                        return (int)LibCommon.WarningResult.NULL;
                    }
                }
            }
            return (int)LibCommon.WarningResult.NULL;
        }

        /// <summary>
        /// 向临时表中插入整理完成数据
        /// </summary>
        /// <param name="ent"></param>
        /// <returns>返回是否成功添加记录</returns>
        //public static bool InsertSortedDataIntoTable(ManageDataBase database, PreWarningHistoryResultEnt ent)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("INSERT INTO ");
        //    strSql.Append(PreWarningResultDBConstNames.TABLE_NAME_TEMP);
        //    strSql.Append(" VALUES(");
        //    strSql.Append("'" + ent.Tunnel + "',");
        //    strSql.Append("'" + ent.TunelName.TrimEnd() + "',");
        //    strSql.Append("'" + ent.Date_Shift + "',");
        //    strSql.Append("'" + ent.DateTime + "',");
        //    strSql.Append("'" + ent.OverLimitWarningResult.WarningResult + "',");
        //    strSql.Append("'" + ent.OutBrustWarningResult.WarningResult + "',");
        //    strSql.Append("'" + ent.OverLimitWarningResult.Gas + "',");
        //    strSql.Append("'" + ent.OverLimitWarningResult.Coal + "',");
        //    strSql.Append("'" + ent.OverLimitWarningResult.Geology + "',");
        //    strSql.Append("'" + ent.OverLimitWarningResult.Ventilation + "',");
        //    strSql.Append("'" + ent.OverLimitWarningResult.Management + "',");
        //    strSql.Append("'" + ent.OutBrustWarningResult.Gas + "',");
        //    strSql.Append("'" + ent.OutBrustWarningResult.Coal + "',");
        //    strSql.Append("'" + ent.OutBrustWarningResult.Geology + "',");
        //    strSql.Append("'" + ent.OutBrustWarningResult.Ventilation + "',");
        //    strSql.Append("'" + ent.OutBrustWarningResult.Management + "')");

        //    return database.OperateDBNotOpenAndClose(strSql.ToString());
        //}


        public static bool InsertSortedDataIntoTable(ManageDataBase 
            database, PreWarningHistoryResultWithWorkingfaceEnt ent)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO ");
            strSql.Append(PreWarningResultDBConstNames.TABLE_NAME_TEMP);
            strSql.Append(" VALUES(");
            strSql.Append("'" + ent.WorkingfaceID + "',");
            strSql.Append("'" + ent.WorkingfaceName.TrimEnd() + "',");
            strSql.Append("'" + ent.Date_Shift + "',");
            strSql.Append("'" + ent.DateTime + "',");
            strSql.Append("'" + ent.OverLimitWarningResult.WarningResult + 
                "',");
            strSql.Append("'" + ent.OutBrustWarningResult.WarningResult + 
                "',");
            strSql.Append("'" + ent.OverLimitWarningResult.Gas + "',");
            strSql.Append("'" + ent.OverLimitWarningResult.Coal + "',");
            strSql.Append("'" + ent.OverLimitWarningResult.Geology + "',");
            strSql.Append("'" + ent.OverLimitWarningResult.Ventilation + 
                "',");
            strSql.Append("'" + ent.OverLimitWarningResult.Management + 
                "',");
            strSql.Append("'" + ent.OutBrustWarningResult.Gas + "',");
            strSql.Append("'" + ent.OutBrustWarningResult.Coal + "',");
            strSql.Append("'" + ent.OutBrustWarningResult.Geology + "',");
            strSql.Append("'" + ent.OutBrustWarningResult.Ventilation + 
                "',");
            strSql.Append("'" + ent.OutBrustWarningResult.Management + 
                "')");

            return database.OperateDBNotOpenAndClose(strSql.ToString());
        }

        /// <summary>
        /// 预警结果数据整理
        /// </summary>
        /// <param name="startTime">起始时间</param>
        /// <param name="endTime">终止时间</param>
        //public static void PreWarningResultSort(string startTime, string endTime, ToolStripProgressBar bar)
        //{
        //    if (!LibCommon.Validator.ExistTableInDatabase(LibBusiness.PreWarningHistroyResultQueryDBConstNames.TABLE_NAME))
        //    {
        //        //不存在，创建新表
        //        CreateTable(LibBusiness.PreWarningHistroyResultQueryDBConstNames.TABLE_NAME);
        //    }
        //    //清除临时表中的数据
        //    ClearSortedData();

        //    //实例化一公用的数据库操作
        //    ManageDataBase databaseForSaveTime = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
        //    databaseForSaveTime.Open();

        //    //获取巷道ID
        //    string[] tunnelIDs = GetTunnleIDBetweenSelectTime(databaseForSaveTime, startTime, endTime);
        //    if (tunnelIDs == null)
        //    { return; }

        //    //获取所有的班次名称与起止时间
        //    List<WorkingTime> dateShifts = GetDateShift(databaseForSaveTime);

        //    //设置进度条前进的步距
        //    bar.Step = 1 + (int)bar.Maximum / tunnelIDs.Length;
        //    bar.Visible = true;
        //    bar.Value = 0;
        //    foreach (string tunnelID in tunnelIDs)
        //    {
        //        //进度条前进
        //        bar.PerformStep();
        //        //获取预警日期
        //        string[] dates = GetDateBySelectTimeAndWorkingfaceName(databaseForSaveTime, tunnelID, startTime, endTime);
        //        if (dates == null)
        //        { return; }
        //        foreach (string date in dates)
        //        {
        //            if (dateShifts == null)
        //            { return; }

        //            //从每一条巷道的，每一个日期、每一个班次中选择出报警的记录，存放到临时表中
        //            foreach (WorkingTime dateShift in dateShifts)
        //            {
        //                GetWorkingfaceWorstPreWarningResultInOneDateshift(databaseForSaveTime, date,
        //                    dateShift.WorkTimeName, tunnelID, LibCommon.WarningType.OUTBURST.ToString(),
        //                    dateShift.WorkTimeFrom, dateShift.WorkTimeTo);
        //            }
        //        }
        //    }
        //    //设置进度条该属性时，会出现进度条未充满即消失的现象，暂未找到解决方法，但不影响性能。
        //    bar.Visible = false;

        //    databaseForSaveTime.Close();
        //}

        public static void PreWarningResultSort(string startTime, string 
            endTime, ToolStripProgressBar bar, string workingfaceName, string 
            warningType)
        {
            //if (!LibCommon.Validator.ExistTableInDatabase(LibBusiness.PreWarningHistroyResultQueryDBConstNames.TABLE_NAME))
            //{
            //    //不存在，创建新表
            //    CreateTable(LibBusiness.PreWarningHistroyResultQueryDBConstNames.TABLE_NAME);
            //}
            //清除临时表中的数据
            ClearSortedData();

            //实例化一公用的数据库操作
            ManageDataBase databaseForSaveTime = new 
                ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            databaseForSaveTime.Open();

            //获取巷道ID
            //string[] tunnelIDs = GetTunnleIDBetweenSelectTime(databaseForSaveTime, startTime, endTime, workingfaceName);
            //if (tunnelIDs == null)
            //{ return; }

            //获取所有的班次名称与起止时间
            //TODO:此处可优化，但不是重点
            WorkingTime[] dateShifts = GetDateShift(databaseForSaveTime);
            List<String> workFaceList = new List<string>();
            if (String.IsNullOrEmpty(workingfaceName))
            {
                workFaceList = GetWorkingFaceList();
            }
            //设置进度条前进的步距

            //foreach (string tunnelID in tunnelIDs)
            //{
            //进度条前进
            bar.PerformStep();
            //获取预警日期
            //TODO:此处可优化，但不是重点
            DateTime[] dates = 
                GetDateBySelectTimeAndWorkingfaceName(databaseForSaveTime, 
                workingfaceName, startTime, endTime);
            bar.Step = 1 + (int)bar.Maximum;
            bar.Visible = true;
            bar.Value = 0;
            DataTable dt = 
                GetWarningResultsWithCondition(databaseForSaveTime, startTime, 
                endTime, workingfaceName);
            if (dates == null)
            { return; }
            foreach (var date in dates)
            {
                if (dateShifts == null)
                { return; }

                //从每一条巷道的，每一个日期、每一个班次中选择出报警的记录，存放到临时表中
                foreach (WorkingTime dateShift in dateShifts)
                {
                    if (workFaceList.Count > 0)
                    {
                        foreach (var item in workFaceList)
                        {
                            GetWorkingfaceWorstPreWarningResultInOneDateshift(databaseForSaveTime,
                                dt, date,
dateShift.WorkTimeName, item, LibCommon.WarningType.OUTBURST.ToString(),
dateShift.WorkTimeFrom, dateShift.WorkTimeTo, warningType);
                        }
                    }
                    else
                    {
                        GetWorkingfaceWorstPreWarningResultInOneDateshift(databaseForSaveTime,
                            dt, date,
                        dateShift.WorkTimeName, workingfaceName, 
                            LibCommon.WarningType.OUTBURST.ToString(),
                        dateShift.WorkTimeFrom, dateShift.WorkTimeTo, 
                            warningType);
                    }
                }
            }
            //}
            //设置进度条该属性时，会出现进度条未充满即消失的现象，暂未找到解决方法，但不影响性能。
            bar.Visible = false;

            databaseForSaveTime.Close();
        }
        /// <summary>
        /// 获取班次中的时间范围，返回值例：起始时间2014-03-18 08:00:00.000，结束时间2014-03-18 16:00:00.000
        /// </summary>
        /// <param name="strWorkTimeName"></param>
        /// <param name="strDate"></param>
        /// <returns></returns>
        //public static DateTime[] GetDateShiftTimes(ManageDataBase database, string strWorkTimeName)
        //{
        //    var workingTime = WorkingTime.FindOneByWorkTimeName(strWorkTimeName);
        //    return new[] { workingTime.WorkTimeFrom, workingTime.WorkTimeTo };
        //}

        /// <summary>
        /// 清空临时表中的临时信息
        /// </summary>
        /// <returns></returns>
        public static bool ClearSortedData()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM ");
            strSql.Append(LibBusiness.PreWarningHistroyResultQueryDBConstNames.TABLE_NAME);
            ManageDataBase database = new 
                ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            return database.OperateDB(strSql.ToString());
        }

        /// <summary>
        /// 获取预警数据行数
        /// </summary>
        /// <returns></returns>
        public static int GetPreWarningDataCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(*) FROM ");
            strSql.Append(PreWarningResultDBConstNames.TABLE_NAME_TEMP);
            ManageDataBase database = new 
                ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataTable dt = database.ReturnDS(strSql.ToString()).Tables[0];
            return Convert.ToInt32(dt.Rows[0][0].ToString());
        }
        /// <summary>
        /// 获取整理完成的数据，该函数
        /// </summary>
        /// <returns></returns>
        public static List<PreWarningHistoryResultEnt> 
            GetSortedPreWarningData()
        {
            List<PreWarningHistoryResultEnt> ents = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ");
            strSql.Append(PreWarningResultDBConstNames.TABLE_NAME_TEMP);
            ManageDataBase database = new 
                ManageDataBase(DATABASE_TYPE.WarningManagementDB);
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
                        //ent.TunelName = dt.Rows[i][LibBusiness.PreWarningResultQueryDBConstNames.TUNNEL_NAME].ToString();//(数据库中不包含该字段！！！)
                        //巷道ID
                        ent.TunnelID = 
                            (int)(dt.Rows[i][PreWarningResultDBConstNames.TUNNEL_ID]);
                        //日期
                        ent.DateTime = 
                            Convert.ToDateTime(dt.Rows[i][PreWarningResultDBConstNames.DATA_TIME]);
                        //班次
                        ent.Date_Shift = 
                            dt.Rows[i][PreWarningResultDBConstNames.DATE_SHIFT].ToString();

                        int defultValue = (int)WarningResult.NULL;
                        //超限预警详细信息
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OVER_LIMIT].ToString(),
                            out defultValue);
                        ent.OverLimitWarningResult.WarningResult = 
                            defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OVER_LIMIT_GAS].ToString(),
                            out defultValue);
                        ent.OverLimitWarningResult.Gas = defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OVER_LIMIT_COAL].ToString(),
                            out defultValue);
                        ent.OverLimitWarningResult.Coal = defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OVER_LIMIT_GEOLOGY].ToString(),
                            out defultValue);
                        ent.OverLimitWarningResult.Geology = defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OVER_LIMIT_VENTILATION].ToString(),
                            out defultValue);
                        ent.OverLimitWarningResult.Ventilation = 
                            defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OVER_LIMIT_MANAGEMENT].ToString(),
                            out defultValue);
                        ent.OverLimitWarningResult.Management = 
                            defultValue;

                        //突出预警详细信息
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OUTBURST].ToString(),
                            out defultValue);
                        ent.OutBrustWarningResult.WarningResult = 
                            defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OUTBURST_GAS].ToString(),
                            out defultValue);
                        ent.OutBrustWarningResult.Gas = defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OUTBURST_COAL].ToString(),
                            out defultValue);
                        ent.OutBrustWarningResult.Coal = defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OUTBURST_GEOLOGY].ToString(),
                            out defultValue);
                        ent.OutBrustWarningResult.Geology = defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OUTBURST_VENTILATION].ToString(),
                            out defultValue);
                        ent.OutBrustWarningResult.Ventilation = 
                            defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OUTBURST_MANAGEMENT].ToString(),
                            out defultValue);
                        ent.OutBrustWarningResult.Management = defultValue;

                        ents.Add(ent);
                    }
                }
            }
            return ents;
        }

        /// <summary>
        /// 通过rowid获取整理后的数据，便于分页控件控制
        /// </summary>
        /// <param name="iStartIndex"></param>
        /// <param name="iEndIndex"></param>
        /// <returns></returns>
        public static List<PreWarningHistoryResultEnt> 
            GetSortedPreWarningData(int iStartIndex, int iEndIndex)
        {
            List<PreWarningHistoryResultEnt> ents = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM (");
            strSql.Append(" SELECT ROW_NUMBER() OVER(ORDER BY " + 
                PreWarningResultDBConstNames.ID + ") AS rowid, *");
            strSql.Append(" FROM " + 
                PreWarningResultDBConstNames.TABLE_NAME_TEMP + ") AS TB");
            strSql.Append(" WHERE rowid >= " + iStartIndex);
            strSql.Append(" AND rowid <= " + iEndIndex);
            strSql.Append(" ORDER BY " + 
                PreWarningResultDBConstNames.DATA_TIME + " DESC");
            ManageDataBase database = new 
                ManageDataBase(DATABASE_TYPE.WarningManagementDB);
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
                        ent.TunelName = 
                            dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.TUNNEL_NAME].ToString().TrimEnd();
                        //巷道ID
                        ent.TunnelID = 
                            (int)(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.TUNNEL_ID]);
                        //日期
                        ent.DateTime = 
                            Convert.ToDateTime(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.DATA_TIME]);
                        //班次
                        ent.Date_Shift = 
                            dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.DATE_SHIFT].ToString();

                        int defultValue = (int)WarningResult.NULL;
                        //超限预警详细信息
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OVER_LIMIT].ToString(),
                            out defultValue);
                        ent.OverLimitWarningResult.WarningResult = 
                            defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OVER_LIMIT_GAS].ToString(),
                            out defultValue);
                        ent.OverLimitWarningResult.Gas = defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OVER_LIMIT_COAL].ToString(),
                            out defultValue);
                        ent.OverLimitWarningResult.Coal = defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OVER_LIMIT_GEOLOGY].ToString(),
                            out defultValue);
                        ent.OverLimitWarningResult.Geology = defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OVER_LIMIT_VENTILATION].ToString(),
                            out defultValue);
                        ent.OverLimitWarningResult.Ventilation = 
                            defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OVER_LIMIT_MANAGEMENT].ToString(),
                            out defultValue);
                        ent.OverLimitWarningResult.Management = 
                            defultValue;

                        //突出预警详细信息
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OUTBURST].ToString(),
                            out defultValue);
                        ent.OutBrustWarningResult.WarningResult = 
                            defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OUTBURST_GAS].ToString(),
                            out defultValue);
                        ent.OutBrustWarningResult.Gas = defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OUTBURST_COAL].ToString(),
                            out defultValue);
                        ent.OutBrustWarningResult.Coal = defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OUTBURST_GEOLOGY].ToString(),
                            out defultValue);
                        ent.OutBrustWarningResult.Geology = defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OUTBURST_VENTILATION].ToString(),
                            out defultValue);
                        ent.OutBrustWarningResult.Ventilation = 
                            defultValue;
                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][LibBusiness.PreWarningHistroyResultQueryDBConstNames.OUTBURST_MANAGEMENT].ToString(),
                            out defultValue);
                        ent.OutBrustWarningResult.Management = defultValue;
                        ents.Add(ent);
                    }
                }
            }
            return ents;
        }

        /// <summary>
        /// 新建临时表，未封装，只适用与此表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static bool CreateTable(string tableName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("CREATE TABLE [dbo].[");
            strSql.Append(tableName);
            strSql.Append("](");
            strSql.Append("[id] [int] IDENTITY(1,1) NOT NULL,");
            strSql.Append("[tunnel_id] [int] NOT NULL,");
            strSql.Append("[tunnel_name] [nchar](50) NOT NULL,");
            strSql.Append("[shift] [nvarchar](12) NOT NULL,");
            strSql.Append("[date_time] [datetime] NOT NULL,");
            strSql.Append("[over_limit] [int] NOT NULL,");
            strSql.Append("[outburst] [int] NOT NULL,	");
            strSql.Append("[over_limit_gas] [int] NULL,");
            strSql.Append("[over_limit_coal] [int] NULL,");
            strSql.Append("[over_limit_geology] [int] NULL,");
            strSql.Append("[over_limit_ventilation] [int] NULL,");
            strSql.Append("[over_limit_management] [int] NULL,");
            strSql.Append("[outburst_gas] [int] NULL,");
            strSql.Append("	[outburst_coal] [int] NULL,");
            strSql.Append("[outburst_geology] [int] NULL,");
            strSql.Append("	[outburst_ventilation] [int] NULL,");
            strSql.Append("[outburst_management] [int] NULL,)");
            ManageDataBase database = new 
                ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            return database.OperateDB(strSql.ToString());
        }

        public static List<String> GetWorkingFaceList()
        {
            string strSql = 
                "SELECT DISTINCT WORKINGFACE_NAME FROM V_EARLY_WARNING_DETAIL";
            ManageDataBase database = new 
                ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataTable dt = database.ReturnDS(strSql).Tables[0];
            var results = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                results.Add(dt.Rows[i][0].ToString());
            }
            return results;
        }
    }
}