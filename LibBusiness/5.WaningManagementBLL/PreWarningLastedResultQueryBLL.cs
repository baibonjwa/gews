// ******************************************************************
// 概  述：最新预警结果查询业务逻辑
// 作  者：秦凯
// 创建日期：2014/03/18
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Windows.Forms.VisualStyles;
using LibEntity;
using LibDatabase;
using System.Data;
using LibCommon;

namespace LibBusiness
{
    public class PreWarningLastedResultQueryBLL
    {
        /// <summary>
        /// 通过时间查询预警结果
        /// </summary>
        /// <param name="time">查询时间</param>
        /// <returns>返回查询实体的数组</returns>
        public static List<PreWarningResultQueryEnt> QueryLastedPreWarningResult(string time)
        {
            List<PreWarningResultQueryEnt> lastedResultEnts = new List<PreWarningResultQueryEnt>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ");
            strSql.Append(PreWarningResultViewDbConstNames.VIEW_NAME);
            strSql.Append(" WHERE ");
            strSql.Append(PreWarningResultDBConstNames.DATA_TIME);
            strSql.Append(" = ");
            strSql.Append("'" + time + "'" + " AND WARNING_STATUS = 0");
            //strSql.Append("(SELECT TOP (1) DATE_TIME FROM " + PreWarningResultDBConstNames.TABLE_NAME + " ORDER BY ID DESC) AND WARNING_STATUS = 0");
            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            database.Open();
            DataTable dt = database.ReturnDSNotOpenAndClose(strSql.ToString()).Tables[0];
            if (dt != null)
            {
                int rowCount = dt.Rows.Count;
                PreWarningResultQueryEnt ent = null;
                for (int i = 0; i < rowCount; i++)
                {
                    //if (i % 2 == 0)
                    //{
                    ent = new PreWarningResultQueryEnt();
                    //巷道名称
                    //int tunelId = LibCommon.Const.INVALID_TUNNEL_ID;
                    //int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.TUNNEL_ID].ToString(), out tunelId);
                    ent.TunnelID = Convert.ToInt32(dt.Rows[i][TunnelInfoDbConstNames.ID]);
                    ent.TunelName = GetTunelNameByTunelID(ent.TunnelID);
                    ent.WorkingfaceId = dt.Rows[i][WorkingFaceDbConstNames.WORKINGFACE_ID] != DBNull.Value ? Convert.ToInt32(dt.Rows[i][WorkingFaceDbConstNames.WORKINGFACE_ID]) : 0;
                    ent.WorkingfaceName = dt.Rows[i][WorkingFaceDbConstNames.WORKINGFACE_ID] != DBNull.Value ? dt.Rows[i][WorkingFaceDbConstNames.WORKINGFACE_NAME].ToString() : "";
                    //日期
                    ent.DateTime = dt.Rows[i][PreWarningResultDBConstNames.DATA_TIME].ToString();
                    //班次
                    ent.Date_Shift = dt.Rows[i][PreWarningResultDBConstNames.DATE_SHIFT].ToString();
                    //}
                    int defultValue = (int)WarningResult.NULL;
                    //突出预警结果
                    if ((WarningType)(dt.Rows[i][PreWarningResultDBConstNames.WARNING_TYPE]) == WarningType.OUTBURST)
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
                    if ((WarningType)(dt.Rows[i][PreWarningResultDBConstNames.WARNING_TYPE]) == WarningType.OVER_LIMIT)
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
                    //if (i % 2 == 1)
                    //{
                    //设置过滤条件，忽略掉突出、超限预警均无效的
                    if (ent.OutBrustWarningResult == null && ent.OverLimitWarningResult == null)
                    {
                        continue;
                    }
                    lastedResultEnts.Add(ent);
                    //}
                }
            }
            database.Close();
            return lastedResultEnts;
        }

        //UNHANDLED   (0, "未处理"),
        //ACCESSING   (1, "待评价"),
        //WAITING_LIFT(2, "待解除"),
        //HANDLED     (3, "评价通过,预警解除");
        /// <summary>
        /// 获取待解除预警结果
        /// </summary>
        /// <param name="time">查询时间</param>
        /// <returns>返回查询实体的数组</returns>
        //public static List<PreWarningResultQueryEnt> QueryHoldWarningResult()
        //{
        //    List<PreWarningResultQueryEnt> lastedResultEnts = new List<PreWarningResultQueryEnt>();

        //    // 一条巷道，有的记录有两条“突出和超限”，有的记录只有一条“”，我们把两条的合并成一条
        //    Dictionary<int, PreWarningResultQueryEnt> container = new Dictionary<int, PreWarningResultQueryEnt>();

        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("SELECT * FROM ");
        //    strSql.Append(PreWarningResultViewDbConstNames.VIEW_NAME);
        //    strSql.Append(" WHERE ");
        //    strSql.Append(PreWarningResultDBConstNames.WARNING_RESULT);
        //    strSql.Append(" < 2 ");
        //    strSql.Append(" AND " + PreWarningResultDBConstNames.HANDLE_STATUS + " < 3"); // 3指 HANDLED
        //    strSql.Append(" AND WARNING_STATUS = 1");
        //    //strSql.Append(" AND " + PreWarningResultDBConstNames.TUNNEL_ID + " IN (SELECT TUNNEL_ID FROM " + PreWarningResultViewDbConstNames.VIEW_NAME + " WHERE WARNING_STATUS=1)");
        //    //strSql.Append(" ORDER BY ");
        //    //strSql.Append(PreWarningResultDBConstNames.TUNNEL_ID);
        //    ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
        //    database.Open();
        //    DataTable dt = database.ReturnDSNotOpenAndClose(strSql.ToString()).Tables[0];
        //        if (dt != null)
        //    {
        //        int rowCount = dt.Rows.Count;
        //        PreWarningResultQueryEnt ent = null;
        //        for (int i = 0; i < rowCount; i++)
        //        {
        //            //巷道名称
        //            int tunelId = LibCommon.Const.INVALID_ID;
        //            int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.TUNNEL_ID].ToString(), out tunelId);
        //            if (container.ContainsKey(tunelId))
        //                ent = container[tunelId];// 巷道已经存在字典中
        //            else
        //            {
        //                ent = new PreWarningResultQueryEnt();
        //                container.Add(tunelId, ent);
        //            }

        //            ent.TunnelID = tunelId;
        //            ent.TunelName = GetTunelNameByTunelID(tunelId);
        //            ent.WorkingfaceId = dt.Rows[i][WorkingFaceDbConstNames.WORKINGFACE_ID] != DBNull.Value ? Convert.ToInt32(dt.Rows[i][WorkingFaceDbConstNames.WORKINGFACE_ID]) : 0;
        //            ent.WorkingfaceName = dt.Rows[i][WorkingFaceDbConstNames.WORKINGFACE_ID] != DBNull.Value ? dt.Rows[i][WorkingFaceDbConstNames.WORKINGFACE_NAME].ToString() : "";

        //            //日期
        //            ent.DateTime = dt.Rows[i][PreWarningResultDBConstNames.DATA_TIME].ToString();
        //            //班次
        //            ent.Date_Shift = dt.Rows[i][PreWarningResultDBConstNames.DATE_SHIFT].ToString();



        //            int defultValue = (int)WarningResult.NULL;
        //            //突出预警结果
        //            if ((WarningType)(dt.Rows[i][PreWarningResultDBConstNames.WARNING_TYPE]) == WarningType.OUTBURST)
        //            {
        //                WarningResultEnt outburstEnt = new WarningResultEnt();
        //                outburstEnt.ID = dt.Rows[i][PreWarningResultDBConstNames.ID].ToString();

        //                int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.WARNING_RESULT].ToString(), out defultValue);
        //                outburstEnt.WarningResult = defultValue;
        //                defultValue = (int)WarningResult.NULL;
        //                int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.GAS].ToString(), out defultValue);
        //                outburstEnt.Gas = defultValue;
        //                defultValue = (int)WarningResult.NULL;
        //                int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.COAL].ToString(), out defultValue);
        //                outburstEnt.Coal = defultValue;
        //                defultValue = (int)WarningResult.NULL;
        //                int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.GEOLOGY].ToString(), out defultValue);
        //                outburstEnt.Geology = defultValue;
        //                defultValue = (int)WarningResult.NULL;
        //                int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.VENTILATION].ToString(), out defultValue);
        //                outburstEnt.Ventilation = defultValue;
        //                defultValue = (int)WarningResult.NULL;
        //                int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.MANAGEMENT].ToString(), out defultValue);
        //                outburstEnt.Management = defultValue;

        //                // 处理状态
        //                outburstEnt.HandleStatus = dt.Rows[i][PreWarningResultDBConstNames.HANDLE_STATUS].ToString();

        //                ent.OutBrustWarningResult = outburstEnt;
        //            }
        //            //超限预警结果
        //            if ((WarningType)(dt.Rows[i][PreWarningResultDBConstNames.WARNING_TYPE]) == WarningType.OVER_LIMIT)
        //            {
        //                WarningResultEnt overlimitEnt = new WarningResultEnt();
        //                overlimitEnt.ID = dt.Rows[i][PreWarningResultDBConstNames.ID].ToString();

        //                int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.WARNING_RESULT].ToString(), out defultValue);
        //                overlimitEnt.WarningResult = defultValue;
        //                defultValue = (int)WarningResult.NULL;
        //                int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.GAS].ToString(), out defultValue);
        //                overlimitEnt.Gas = defultValue;
        //                defultValue = (int)WarningResult.NULL;
        //                int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.COAL].ToString(), out defultValue);
        //                overlimitEnt.Coal = defultValue;
        //                defultValue = (int)WarningResult.NULL;
        //                int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.GEOLOGY].ToString(), out defultValue);
        //                overlimitEnt.Geology = defultValue;
        //                defultValue = (int)WarningResult.NULL;
        //                int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.VENTILATION].ToString(), out defultValue);
        //                overlimitEnt.Ventilation = defultValue;
        //                defultValue = (int)WarningResult.NULL;
        //                int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.MANAGEMENT].ToString(), out defultValue);
        //                overlimitEnt.Management = defultValue;

        //                overlimitEnt.HandleStatus = dt.Rows[i][PreWarningResultDBConstNames.HANDLE_STATUS].ToString();

        //                ent.OverLimitWarningResult = overlimitEnt;

        //            }
        //            if (ent.OutBrustWarningResult == null && ent.OverLimitWarningResult == null)
        //            {
        //                continue;
        //            }
        //            lastedResultEnts.Add(ent);
        //        }

        //        //foreach (KeyValuePair<int, PreWarningResultQueryEnt> kvp in container)
        //        //{
        //        //    lastedResultEnts.Add(kvp.Value);
        //        //}
        //    }
        //    database.Close();
        //    return lastedResultEnts;
        //}


        /// <summary>
        /// 以巷道为单位
        /// </summary>
        /// <returns></returns>
        public static List<PreWarningResultQueryEnt> QueryHoldWarningResult()
        {
            List<PreWarningResultQueryEnt> lastedResultEnts = new List<PreWarningResultQueryEnt>();

            // 一条巷道，有的记录有两条“突出和超限”，有的记录只有一条“”，我们把两条的合并成一条
            Dictionary<int, PreWarningResultQueryEnt> container = new Dictionary<int, PreWarningResultQueryEnt>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ");
            strSql.Append(PreWarningResultViewDbConstNames.VIEW_NAME);
            strSql.Append(" WHERE ");
            strSql.Append(PreWarningResultDBConstNames.WARNING_RESULT);
            strSql.Append(" < 2 ");
            strSql.Append(" AND " + PreWarningResultDBConstNames.HANDLE_STATUS + " < 3"); // 3指 HANDLED
            strSql.Append(" AND WARNING_STATUS = 1");
            //strSql.Append(" AND " + PreWarningResultDBConstNames.TUNNEL_ID + " IN (SELECT TUNNEL_ID FROM " + PreWarningResultViewDbConstNames.VIEW_NAME + " WHERE WARNING_STATUS=1)");
            //strSql.Append(" ORDER BY ");
            //strSql.Append(PreWarningResultDBConstNames.TUNNEL_ID);
            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            database.Open();
            DataTable dt = database.ReturnDSNotOpenAndClose(strSql.ToString()).Tables[0];
            if (dt != null)
            {
                int rowCount = dt.Rows.Count;
                PreWarningResultQueryEnt ent = null;
                for (int i = 0; i < rowCount; i++)
                {
                    //巷道名称
                    int tunelId = LibCommon.Const.INVALID_ID;
                    int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.TUNNEL_ID].ToString(), out tunelId);
                    if (container.ContainsKey(tunelId))
                        ent = container[tunelId];// 巷道已经存在字典中
                    else
                    {
                        ent = new PreWarningResultQueryEnt();
                        container.Add(tunelId, ent);
                    }

                    ent.TunnelID = tunelId;

                    //TunnelEntity entTunnel = BasicInfoManager.getInstance().getTunnelByID(tunelId);
                    //ent.TunelName = entTunnel.WorkingFace.WorkingfaceTypeEnum == WorkingfaceTypeEnum.JJ ? entTunnel.WorkingFace.WorkingFaceName : entTunnel.WorkingFace.WorkingFaceName + " - " + entTunnel.TunnelName;
                    ent.TunelName = GetTunelNameByTunelID(tunelId);
                    ent.WorkingfaceId = dt.Rows[i][WorkingFaceDbConstNames.WORKINGFACE_ID] != DBNull.Value ? Convert.ToInt32(dt.Rows[i][WorkingFaceDbConstNames.WORKINGFACE_ID]) : 0;
                    ent.WorkingfaceName = dt.Rows[i][WorkingFaceDbConstNames.WORKINGFACE_ID] != DBNull.Value ? dt.Rows[i][WorkingFaceDbConstNames.WORKINGFACE_NAME].ToString() : "";

                    //日期
                    ent.DateTime = dt.Rows[i][PreWarningResultDBConstNames.DATA_TIME].ToString();
                    //班次
                    ent.Date_Shift = dt.Rows[i][PreWarningResultDBConstNames.DATE_SHIFT].ToString();



                    int defultValue = (int)WarningResult.NULL;
                    //突出预警结果
                    if ((WarningType)(dt.Rows[i][PreWarningResultDBConstNames.WARNING_TYPE]) == WarningType.OUTBURST)
                    {
                        WarningResultEnt outburstEnt = new WarningResultEnt();
                        outburstEnt.ID = dt.Rows[i][PreWarningResultDBConstNames.ID].ToString();

                        int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.WARNING_RESULT].ToString(), out defultValue);
                        outburstEnt.WarningResult = defultValue;

                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.GAS].ToString(), out defultValue);
                        if (defultValue < outburstEnt.Gas) outburstEnt.Gas = defultValue;

                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.COAL].ToString(), out defultValue);
                        if (defultValue < outburstEnt.Coal) outburstEnt.Coal = defultValue;

                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.GEOLOGY].ToString(), out defultValue);
                        if (defultValue < outburstEnt.Geology) outburstEnt.Geology = defultValue;

                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.VENTILATION].ToString(), out defultValue);
                        if (defultValue < outburstEnt.Ventilation) outburstEnt.Ventilation = defultValue;

                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.MANAGEMENT].ToString(), out defultValue);
                        if (defultValue < outburstEnt.Management) outburstEnt.Management = defultValue;

                        // 处理状态
                        outburstEnt.HandleStatus = dt.Rows[i][PreWarningResultDBConstNames.HANDLE_STATUS].ToString();

                        ent.OutBrustWarningResult = outburstEnt;
                    }
                    //超限预警结果
                    if ((WarningType)(dt.Rows[i][PreWarningResultDBConstNames.WARNING_TYPE]) == WarningType.OVER_LIMIT)
                    {
                        WarningResultEnt overlimitEnt = new WarningResultEnt();
                        overlimitEnt.ID = dt.Rows[i][PreWarningResultDBConstNames.ID].ToString();

                        int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.WARNING_RESULT].ToString(), out defultValue);
                        if (defultValue < overlimitEnt.WarningResult) overlimitEnt.WarningResult = defultValue;

                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.GAS].ToString(), out defultValue);
                        if (defultValue < overlimitEnt.Gas) overlimitEnt.Gas = defultValue;

                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.COAL].ToString(), out defultValue);
                        if (defultValue < overlimitEnt.Coal) overlimitEnt.Coal = defultValue;

                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.GEOLOGY].ToString(), out defultValue);
                        if (defultValue < overlimitEnt.Geology) overlimitEnt.Geology = defultValue;

                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.VENTILATION].ToString(), out defultValue);
                        if (defultValue < overlimitEnt.Ventilation) overlimitEnt.Ventilation = defultValue;

                        defultValue = (int)WarningResult.NULL;
                        int.TryParse(dt.Rows[i][PreWarningResultDBConstNames.MANAGEMENT].ToString(), out defultValue);
                        if (defultValue < overlimitEnt.Management) overlimitEnt.Management = defultValue;

                        overlimitEnt.HandleStatus = dt.Rows[i][PreWarningResultDBConstNames.HANDLE_STATUS].ToString();

                        ent.OverLimitWarningResult = overlimitEnt;

                    }
                    if (ent.OutBrustWarningResult == null && ent.OverLimitWarningResult == null)
                    {
                        continue;
                    }
                    //lastedResultEnts.Add(ent);
                }

                foreach (KeyValuePair<int, PreWarningResultQueryEnt> kvp in container)
                {
                    lastedResultEnts.Add(kvp.Value);
                }
            }
            database.Close();
            return lastedResultEnts;
        }

        private static System.Collections.Hashtable tunnelNames = new System.Collections.Hashtable();
        /// <summary>
        /// 根据tunelID,查找巷道名称。若属于回采巷道，返回工作面名称
        /// </summary>
        /// <param name="tunelId"></param>
        /// <returns></returns>
        public static void loadTunnelNames()
        {
            tunnelNames.Clear();
            string sSql = "SELECT A." + TunnelInfoDbConstNames.ID +
                  " , A." + TunnelInfoDbConstNames.TUNNEL_NAME +
                  " , B." + WorkingFaceDbConstNames.WORKINGFACE_NAME +
                  " , A." + TunnelInfoDbConstNames.TUNNEL_TYPE +
                  " FROM " + TunnelInfoDbConstNames.TABLE_NAME + " AS A, " + WorkingFaceDbConstNames.TABLE_NAME + " AS B " +
                  "WHERE A." + TunnelInfoDbConstNames.WORKINGFACE_ID + "= B." + WorkingFaceDbConstNames.WORKINGFACE_ID;

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataSet ds = db.ReturnDS(sSql);
            DataTable dtWF = ds.Tables[0];

            foreach (DataRow dr in dtWF.Rows)
            {
                TunnelTypeEnum tunnelType = (TunnelTypeEnum)Convert.ToInt32(dr[TunnelInfoDbConstNames.TUNNEL_TYPE]);
                try
                {
                    if (!TunnelUtils.isStoping(tunnelType))
                        tunnelNames.Add(dr[TunnelInfoDbConstNames.ID], dr[WorkingFaceDbConstNames.WORKINGFACE_NAME]);
                    else
                        tunnelNames.Add(dr[TunnelInfoDbConstNames.ID], dr[WorkingFaceDbConstNames.WORKINGFACE_NAME].ToString() + " - " + dr[TunnelInfoDbConstNames.TUNNEL_NAME].ToString());
                }
                catch (Exception ex)
                {
                    Log.Error(ex.ToString());
                }

            }
        }

        /// <summary>
        /// 根据tunelID,查找巷道名称。若属于回采巷道，返回工作面名称
        /// </summary>
        /// <param name="tunelId"></param>
        /// <returns></returns>
        public static string GetTunelNameByTunelID(int tunnelId)
        {
            return tunnelNames[tunnelId] as string;
        }

        public static List<PreWarningResultQueryWithWorkingfaceEnt> MergePreWarningInfo(List<PreWarningResultQueryEnt> list)
        {
            var result = new List<PreWarningResultQueryWithWorkingfaceEnt>();
            var workingfaceList = new List<int>();
            foreach (var i in list)
            {
                if (!workingfaceList.Contains(i.WorkingfaceId))
                    workingfaceList.Add(i.WorkingfaceId);
            }
            foreach (var i in workingfaceList)
            {
                List<PreWarningResultQueryEnt> temp = list.Where(u => u.WorkingfaceId == i).ToList();
                var item = new PreWarningResultQueryWithWorkingfaceEnt();
                item.OutBrustWarningResult = new WarningResultEnt();
                item.OverLimitWarningResult = new WarningResultEnt();

                foreach (var j in temp)
                {
                    item.WorkingfaceID = j.WorkingfaceId;
                    item.WorkingfaceName = j.WorkingfaceName;
                    item.TunnelId = j.TunnelID;
                    item.TunnelName = j.TunelName;
                    item.DateTime = j.DateTime;
                    item.Date_Shift = j.Date_Shift;

                    if (j.OutBrustWarningResult != null)
                    {
                        if (j.OutBrustWarningResult.WarningResult < item.OutBrustWarningResult.WarningResult)
                        {
                            item.OutBrustWarningResult.WarningResult = j.OutBrustWarningResult.WarningResult;
                        }
                        if (j.OutBrustWarningResult.Coal < item.OutBrustWarningResult.Coal)
                        {
                            item.OutBrustWarningResult.Coal = j.OutBrustWarningResult.Coal;
                        }
                        if (j.OutBrustWarningResult.Gas < item.OutBrustWarningResult.Gas)
                        {
                            item.OutBrustWarningResult.Gas = j.OutBrustWarningResult.Gas;
                        }
                        if (j.OutBrustWarningResult.Geology < item.OutBrustWarningResult.Geology)
                        {
                            item.OutBrustWarningResult.Geology = j.OutBrustWarningResult.Geology;
                        }
                        if (j.OutBrustWarningResult.Management < item.OutBrustWarningResult.Management)
                        {
                            item.OutBrustWarningResult.Management = j.OutBrustWarningResult.Management;
                        }
                        if (j.OutBrustWarningResult.Other < item.OutBrustWarningResult.Other)
                        {
                            item.OutBrustWarningResult.Other = j.OutBrustWarningResult.Other;
                        }
                        if (j.OutBrustWarningResult.Ventilation < item.OutBrustWarningResult.Ventilation)
                        {
                            item.OutBrustWarningResult.Ventilation = j.OutBrustWarningResult.Ventilation;
                        }
                        if (j.OutBrustWarningResult.WarningResult < item.OutBrustWarningResult.WarningResult)
                        {
                            item.OutBrustWarningResult.WarningResult = j.OutBrustWarningResult.WarningResult;
                        }
                    }
                    if (j.OverLimitWarningResult != null)
                    {
                        if (j.OverLimitWarningResult.WarningResult < item.OverLimitWarningResult.WarningResult)
                        {
                            item.OverLimitWarningResult.WarningResult = j.OverLimitWarningResult.WarningResult;
                        }
                        if (j.OverLimitWarningResult.Coal < item.OverLimitWarningResult.Coal)
                        {
                            item.OverLimitWarningResult.Coal = j.OverLimitWarningResult.Coal;
                        }
                        if (j.OverLimitWarningResult.Gas < item.OverLimitWarningResult.Gas)
                        {
                            item.OverLimitWarningResult.Gas = j.OverLimitWarningResult.Gas;
                        }
                        if (j.OverLimitWarningResult.Geology < item.OverLimitWarningResult.Geology)
                        {
                            item.OverLimitWarningResult.Geology = j.OverLimitWarningResult.Geology;
                        }
                        if (j.OverLimitWarningResult.Management < item.OverLimitWarningResult.Management)
                        {
                            item.OverLimitWarningResult.Management = j.OverLimitWarningResult.Management;
                        }
                        if (j.OverLimitWarningResult.Other < item.OverLimitWarningResult.Other)
                        {
                            item.OverLimitWarningResult.Other = j.OverLimitWarningResult.Other;
                        }
                        if (j.OverLimitWarningResult.Ventilation < item.OverLimitWarningResult.Ventilation)
                        {
                            item.OverLimitWarningResult.Ventilation = j.OverLimitWarningResult.Ventilation;
                        }
                        if (j.OverLimitWarningResult.WarningResult < item.OverLimitWarningResult.WarningResult)
                        {
                            item.OverLimitWarningResult.WarningResult = j.OverLimitWarningResult.WarningResult;
                        }
                    }
                }
                result.Add(item);
            }
            return result;
        }

        public static List<String> GetWarningIdListWithWorkingfaceName(string workingfaceName)
        {
            List<String> results = new List<string>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ");
            strSql.Append(PreWarningResultViewDbConstNames.VIEW_NAME);
            strSql.Append(" WHERE ");
            strSql.Append(PreWarningResultDBConstNames.WARNING_RESULT);
            strSql.Append(" < 2 ");
            strSql.Append(" AND " + PreWarningResultDBConstNames.HANDLE_STATUS + " < 3"); // 3指 HANDLED
            strSql.Append(" AND WARNING_STATUS = 1");
            strSql.Append(" AND ");
            strSql.Append(WorkingFaceDbConstNames.WORKINGFACE_NAME + " = '" + workingfaceName + "'");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataTable dt = db.ReturnDS(strSql.ToString()).Tables[0];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                results.Add(dt.Rows[i][PreWarningResultDBConstNames.ID].ToString());
            }

            return results;
        }

        public static List<String> GetWarningIdListWithTunnelId(string tunnelId)
        {
            List<String> results = new List<string>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ");
            strSql.Append(PreWarningResultViewDbConstNames.VIEW_NAME);
            strSql.Append(" WHERE ");
            strSql.Append(PreWarningResultDBConstNames.WARNING_RESULT);
            strSql.Append(" < 2 ");
            strSql.Append(" AND " + PreWarningResultDBConstNames.HANDLE_STATUS + " < 3"); // 3指 HANDLED
            strSql.Append(" AND WARNING_STATUS = 1");
            strSql.Append(" AND ");
            strSql.Append(TunnelInfoDbConstNames.ID + " = '" + tunnelId + "'");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataTable dt = db.ReturnDS(strSql.ToString()).Tables[0];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                results.Add(dt.Rows[i][PreWarningResultDBConstNames.ID].ToString());
            }

            return results;
        }
    }
}
