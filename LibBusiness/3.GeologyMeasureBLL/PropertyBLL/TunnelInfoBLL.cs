// ******************************************************************
// 概  述：巷道信息业务逻辑
// 作  者：宋英杰
// 日  期：2013/11/28
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LibEntity;
using LibDatabase;
using LibBusiness;
using LibCommon;

namespace LibBusiness
{
    public class TunnelInfoBLL
    {
        /// <summary>
        /// 查询巷道表数据（不含矿井数据）
        /// </summary>
        /// <param name="tunnelID"></param>
        /// <returns></returns>
        public static DataSet selectOneTunnelInfoByTunnelID(int tunnelID)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT * FROM " + TunnelInfoDbConstNames.TABLE_NAME + " WHERE " + TunnelInfoDbConstNames.ID + " = " + tunnelID;
            DataSet ds = db.ReturnDS(sql);
            return ds;
        }

        /// <summary>
        /// 通过<工作面编号>，获取该<工作面>下所有<巷道>信息
        /// </summary>
        /// <returns><巷道>信息</returns>
        public static DataSet selectTunnelInfoByWorkingFaceId(int iWorkingFaceId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + TunnelInfoDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + TunnelInfoDbConstNames.WORKINGFACE_ID + " = " + iWorkingFaceId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 巷道类型过滤
        /// </summary>
        /// <param name="iWorkingFaceId">工作面ID</param>
        /// <param name="columnName">参数列名</param>
        /// <param name="columnValue">参数值</param>
        /// <returns>过滤后巷道信息</returns>
        public static DataSet selectTunnelInfoByWorkingFaceWithFilter(int iWorkingFaceId, string columnName, string columnValue)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM " + TunnelInfoDbConstNames.TABLE_NAME);
            sb.Append(" WHERE " + TunnelInfoDbConstNames.WORKINGFACE_ID + " = " + iWorkingFaceId);
            sb.Append(" AND " + columnName + " = '" + columnValue.ToString() + "'");
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }

        /// <summary>
        /// 巷道类型过滤
        /// </summary>
        /// <param name="iWorkingFaceId">工作面ID</param>
        /// <param name="columnName">参数列名</param>
        /// <param name="columnValue">参数值</param>
        /// <returns>过滤后巷道信息</returns>
        public static DataSet selectTunnelInfoByWorkingFaceWithFilter(int iWorkingFaceId, TunnelTypeEnum[] types)
        {
            string sqlStr = "";
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM " + TunnelInfoDbConstNames.TABLE_NAME);
            sb.Append(" WHERE " + TunnelInfoDbConstNames.WORKINGFACE_ID + " = " + iWorkingFaceId);
            if (types.Length > 0)
            {
                sb.Append(" AND ");
                sb.Append(" ( ");
                foreach (var i in types)
                {
                    sb.Append(TunnelInfoDbConstNames.TUNNEL_TYPE + " = " + (int)i);
                    sb.Append(" OR ");
                }
                sqlStr = sb.ToString();
                sqlStr = sqlStr.Substring(0, sqlStr.Length - 3);
                sqlStr += ")";
            }
            else
            {
                sqlStr = sb.ToString();
            }
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr);
            return ds;
        }

        /// <summary>
        /// 条件过滤巷道
        /// </summary>
        /// <param name="tunnelFilterRules">过滤规则</param>
        /// <param name="iWorkingFaceID">工作面ID</param>
        /// <returns>过滤后巷道信息</returns>
        public static DataSet selectTunnelInfoWithFilter(TunnelFilter.TunnelFilterRules tunnelFilterRules, int iWorkingFaceID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM " + TunnelInfoDbConstNames.TABLE_NAME);
            sb.Append(" WHERE " + TunnelInfoDbConstNames.WORKINGFACE_ID + " = " + iWorkingFaceID);
            if (tunnelFilterRules == TunnelFilter.TunnelFilterRules.IS_WIRE_INFO_BIND)
            {
                //                sb.Append(" AND " + TunnelInfoDbConstNames.ID + " IN (");//zwj 2014,7,28
                //               sb.Append("SELECT " + WireInfoDbConstNames.TUNNEL_ID + " FROM " + WireInfoDbConstNames.TABLE_NAME+")");//zwj 2014,7,28
            }
            if (tunnelFilterRules == TunnelFilter.TunnelFilterRules.IS_TUNNE_KQY)
            {
                //                sb.Append(" AND " + TunnelInfoDbConstNames.ID + " IN (");//zwj 2014,7,28
                //                sb.Append("SELECT " + TunnelHCDbConstNames.TUNNEL_ID3 + " FROM " + TunnelHCDbConstNames.TABLE_NAME+")");//zwj 2014,7,28
            }
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }


        /// <summary>
        /// 工作面下巷道名称是否存在
        /// </summary>
        /// <param name="tunnelName"></param>
        /// <param name="workingFaceID"></param>
        /// <returns></returns>
        public static bool isTunnelNameExist(string tunnelName, int workingFaceID)
        {
            string sql = "SELECT * FROM " + TunnelInfoDbConstNames.TABLE_NAME + " WHERE " + TunnelInfoDbConstNames.TUNNEL_NAME + " = '" + tunnelName + "' AND " + TunnelInfoDbConstNames.WORKINGFACE_ID + " = " + workingFaceID;
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sql);
            bool bResult = false;
            if (ds.Tables[0].Rows.Count > 0)
            {
                bResult = true;
            }
            return bResult;
        }

        /// <summary>
        /// 巷道信息登录
        /// </summary>
        /// <param name="collapsePillarsEntity">巷道实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool insertTunnelInfo(Tunnel tunnelEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("INSERT INTO " + TunnelInfoDbConstNames.TABLE_NAME + " (" +
                TunnelInfoDbConstNames.TUNNEL_NAME + ", " + TunnelInfoDbConstNames.SUPPORT_PATTERN + ", " +
                TunnelInfoDbConstNames.LITHOLOGY_ID + ", " + TunnelInfoDbConstNames.SECTION_TYPE + ", " +
                TunnelInfoDbConstNames.PARAM + ", " + TunnelInfoDbConstNames.DESIGNLENGTH + ", " +
                TunnelInfoDbConstNames.DESIGNAREA + ", " + TunnelInfoDbConstNames.TUNNEL_TYPE + ", " +
                TunnelInfoDbConstNames.COAL_OR_STONE + ", " + TunnelInfoDbConstNames.COAL_LAYER_ID + ", " +
                TunnelInfoDbConstNames.BINDINGID + ", " + TunnelInfoDbConstNames.WORKINGFACE_ID + "," + TunnelInfoDbConstNames.TUNNEL_WID + ")");
            sqlStr.Append("VALUES ('");
            sqlStr.Append(tunnelEntity.TunnelName + "','");
            sqlStr.Append(tunnelEntity.TunnelSupportPattern + "',");
            sqlStr.Append(tunnelEntity.TunnelLithologyID + ",'");
            sqlStr.Append(tunnelEntity.TunnelSectionType + "','");
            sqlStr.Append(tunnelEntity.TunnelParam + "',");
            sqlStr.Append(tunnelEntity.TunnelDesignLength + ",");
            sqlStr.Append(tunnelEntity.TunnelDesignArea + ",'");
            sqlStr.Append((int)tunnelEntity.TunnelType + "','");
            sqlStr.Append(tunnelEntity.CoalOrStone + "',");
            sqlStr.Append(tunnelEntity.CoalLayerID + ",'");
            sqlStr.Append(tunnelEntity.BindingID + "',");
            sqlStr.Append(tunnelEntity.WorkingFace.WorkingFaceID + ",");
            sqlStr.Append(tunnelEntity.TunnelWid + ")");
            //Alert.alert(sqlStr.ToString());
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// 修改巷道信息
        /// </summary>
        /// <param name="tunnelEntity">巷道实体</param>
        /// <returns></returns>
        public static bool updateTunnelInfo(Tunnel tunnelEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("UPDATE " + TunnelInfoDbConstNames.TABLE_NAME + " SET " + TunnelInfoDbConstNames.TUNNEL_NAME + " ='");
            sqlStr.Append(tunnelEntity.TunnelName + "'," + TunnelInfoDbConstNames.SUPPORT_PATTERN + " ='");
            sqlStr.Append(tunnelEntity.TunnelSupportPattern + "'," + TunnelInfoDbConstNames.LITHOLOGY_ID + " =");
            sqlStr.Append(tunnelEntity.TunnelLithologyID + "," + TunnelInfoDbConstNames.SECTION_TYPE + " ='");
            sqlStr.Append(tunnelEntity.TunnelSectionType + "'," + TunnelInfoDbConstNames.PARAM + " ='");
            sqlStr.Append(tunnelEntity.TunnelParam + "'," + TunnelInfoDbConstNames.DESIGNLENGTH + " = ");
            sqlStr.Append(tunnelEntity.TunnelDesignLength + "," + TunnelInfoDbConstNames.DESIGNAREA + " = ");
            sqlStr.Append(tunnelEntity.TunnelDesignArea + "," + TunnelInfoDbConstNames.COAL_OR_STONE + " = '");
            sqlStr.Append(tunnelEntity.CoalOrStone + "'," + TunnelInfoDbConstNames.COAL_LAYER_ID + " = ");
            sqlStr.Append(tunnelEntity.CoalLayerID + "," + TunnelInfoDbConstNames.WORKINGFACE_ID + " = ");
            sqlStr.Append(tunnelEntity.WorkingFace.WorkingFaceID + "," + TunnelInfoDbConstNames.TUNNEL_WID + " = " + tunnelEntity.TunnelWid + " WHERE " + TunnelInfoDbConstNames.ID + " =");
            sqlStr.Append(tunnelEntity.TunnelID);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        public static DataSet returnLithologyName()
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT " + LithologyDbConstNames.LITHOLOGY_NAME + " FROM " + LithologyDbConstNames.TABLE_NAME;
            DataSet ds = db.ReturnDS(sql);
            return ds;
        }

        /// <summary>
        /// 返回巷道导线所有信息
        /// </summary>
        /// <returns>巷道导线信息</returns>
        public static DataSet selectTunnelWireInfo()
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT * FROM " + WireInfoDbConstNames.TABLE_NAME + " A, " + TunnelInfoDbConstNames.TABLE_NAME + " B WHERE A." + WireInfoDbConstNames.ID + " = B." + TunnelInfoDbConstNames.ID + "";
            DataSet ds = db.ReturnDS(sql);
            return ds;
        }

        /// <summary>
        /// 分页用返回巷道所有信息
        /// </summary>
        /// <returns></returns>
        public static DataSet selectTunnelInfo(int iStartIndex, int iEndIndex)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ( ");
            sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + TunnelInfoDbConstNames.ID + ") AS rowid, * ");
            sb.Append("FROM " + TunnelInfoDbConstNames.TABLE_NAME + " ) AS TB ");
            sb.Append("WHERE rowid >= " + iStartIndex);
            sb.Append("AND rowid <= " + iEndIndex);
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }

        /// <summary>
        /// 查询巷道所有信息
        /// </summary>
        /// <returns></returns>
        public static DataSet selectTunnelInfo()
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT * FROM " + TunnelInfoDbConstNames.TABLE_NAME;
            DataSet ds = db.ReturnDS(sql);
            return ds;
        }

        /// <summary>
        /// 删除巷道信息
        /// </summary>
        /// <param name="tunnelEntity">巷道实体</param>
        /// <returns>是否成功删除？true:false</returns>
        public static bool deleteTunnelInfo(Tunnel tunnelEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "DELETE FROM " + TunnelInfoDbConstNames.TABLE_NAME + " WHERE " + TunnelInfoDbConstNames.ID + " =" + tunnelEntity.TunnelID;
            bool bResult = db.OperateDB(sql);
            return bResult;
        }

        /// <summary>
        /// 判断巷道是否属于掘进面
        /// </summary>
        /// <param name="tunnelEntity">巷道实体</param>
        /// <returns>是否为掘进巷道？true:false</returns>
        public static bool isTunnelJJ(Tunnel tunnelEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT " +
                "A." + TunnelInfoDbConstNames.TUNNEL_TYPE +
                ", B." + WorkingFaceDbConstNames.IS_FINISH +
                 " FROM " +
                TunnelInfoDbConstNames.TABLE_NAME + " AS A, " +
                WorkingFaceDbConstNames.TABLE_NAME + " AS B" +
                " WHERE " +
                "A." + TunnelInfoDbConstNames.ID + " = " + tunnelEntity.TunnelID +
                " AND " +
                "A." + TunnelInfoDbConstNames.TUNNEL_TYPE + " = " + (int)TunnelTypeEnum.TUNNELLING +
                 " AND " +
                "A." + TunnelInfoDbConstNames.WORKINGFACE_ID + " = B." + TunnelInfoDbConstNames.WORKINGFACE_ID;

            DataSet ds = db.ReturnDS(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt32(ds.Tables[0].Rows[0][WorkingFaceDbConstNames.IS_FINISH].ToString()) == 1)
                {
                    return false;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 返回巷道是否为回采巷道
        /// </summary>
        /// <param name="tunnelEntity">巷道实体</param>
        /// <returns>是否为回采巷道？true:false</returns>
        public static bool isTunnelHC(Tunnel tunnelEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT * FROM " + TunnelInfoDbConstNames.TABLE_NAME + " WHERE " +
                TunnelInfoDbConstNames.ID + " = " + tunnelEntity.TunnelID + " AND " +
                TunnelInfoDbConstNames.TUNNEL_TYPE + " = '" + (int)TunnelTypeEnum.STOPING_FY + "'";
            DataSet ds = db.ReturnDS(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 设置掘进面巷道,工作面和巷道都存在数据库中。
        /// </summary>
        /// <param name="tunnelJJEntity">掘进巷道实体</param>
        /// <returns>是否成功设置巷道为掘进巷道？true:false</returns>
        public static bool setTunnelAsJJ(WorkingFace wfEntity, Tunnel tunnelEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);

            string sql = "SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;";
            sql += "BEGIN TRANSACTION;";
            sql += "BEGIN ";
            sql +=
                "UPDATE " + TunnelInfoDbConstNames.TABLE_NAME +
                " SET " +
                TunnelInfoDbConstNames.TUNNEL_TYPE + "=" + (int)TunnelTypeEnum.TUNNELLING + ", " +
                TunnelInfoDbConstNames.WORKINGFACE_ID + "=" + wfEntity.WorkingFaceID +
                " WHERE " + TunnelInfoDbConstNames.ID + " = " + tunnelEntity.TunnelID + ";";

            sql += " UPDATE " + WorkingFaceDbConstNames.TABLE_NAME +
                " SET " +
                WorkingFaceDbConstNames.WORK_TIME + "='" + wfEntity.WorkTime + "', " +
                WorkingFaceDbConstNames.START_DATE + "='" + wfEntity.StartDate.ToString() + "'," +
                WorkingFaceDbConstNames.STOP_DATE + "='" + wfEntity.StopDate.ToString() + "'," +
                WorkingFaceDbConstNames.WORK_STYLE + "='" + wfEntity.WorkStyle + "'," +
                WorkingFaceDbConstNames.IS_FINISH + "=" + wfEntity.IsFinish + "," +
                WorkingFaceDbConstNames.TEAM_NAME_ID + "=" + wfEntity.TeamNameID +
                " WHERE " + WorkingFaceDbConstNames.WORKINGFACE_ID + "= " + wfEntity.WorkingFaceID;

            sql += " END ";
            sql += "COMMIT TRANSACTION;";

            bool bResult = db.OperateDB(sql);
            return bResult;
        }

        /// <summary>
        /// 设置巷道为回采巷道
        /// </summary>
        /// <param name="tunnelHCEntity">巷道实体</param>
        /// <returns>是否成功设置巷道为回采巷道？true:false</returns>
        public static bool setTunnelAsHC(WorkingFace wfEntity, HashSet<Tunnel> tunnelSet)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            db.Open();

            string sql = "BEGIN ";

            sql += " UPDATE " + WorkingFaceDbConstNames.TABLE_NAME +
              " SET " +
              WorkingFaceDbConstNames.WORK_TIME + "='" + wfEntity.WorkTime + "', " +
              WorkingFaceDbConstNames.START_DATE + "='" + wfEntity.StartDate.ToString() + "'," +
              WorkingFaceDbConstNames.STOP_DATE + "='" + wfEntity.StopDate.ToString() + "'," +
              WorkingFaceDbConstNames.WORK_STYLE + "='" + wfEntity.WorkStyle + "'," +
              WorkingFaceDbConstNames.IS_FINISH + "=" + wfEntity.IsFinish + "," +
              WorkingFaceDbConstNames.TEAM_NAME_ID + "=" + wfEntity.TeamNameID +
              " WHERE " + WorkingFaceDbConstNames.WORKINGFACE_ID + "= " + wfEntity.WorkingFaceID;

            foreach (Tunnel entity in tunnelSet)
            {
                sql += " UPDATE " + TunnelInfoDbConstNames.TABLE_NAME +
                " SET " +
                TunnelInfoDbConstNames.TUNNEL_TYPE + " = " + (int)entity.TunnelType + ", " +
                TunnelInfoDbConstNames.WORKINGFACE_ID + "=" + entity.WorkingFace.WorkingFaceID +
                " WHERE " + TunnelInfoDbConstNames.ID + " = " + entity.TunnelID + ";";
            }
            sql += " END ";

            bool bResult = db.OperateDBNotOpenAndClose(sql);

            db.Close();
            return bResult;
        }

        /// <summary>
        /// 设置巷道为横川巷道
        /// </summary>
        /// <param name="tunnelHCEntity">巷道实体</param>
        /// <returns>是否成功设置巷道为横川巷道？true:false</returns>
        public static bool setTunnelAsHChuan(TunnelHChuanEntity tunnelHChuanEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            db.Open();
            string sql = "UPDATE " + TunnelInfoDbConstNames.TABLE_NAME + " SET " +
                TunnelInfoDbConstNames.TUNNEL_TYPE + " = '" + "HENGCHUAN" +
                "' WHERE " + TunnelInfoDbConstNames.ID + " = " + tunnelHChuanEntity.TunnelID1;
            bool bResult = db.OperateDBNotOpenAndClose(sql);
            sql = "UPDATE " + TunnelInfoDbConstNames.TABLE_NAME + " SET " +
                TunnelInfoDbConstNames.TUNNEL_TYPE + " = '" + "HENGCHUAN" +
                "' WHERE " + TunnelInfoDbConstNames.ID + " = " + tunnelHChuanEntity.TunnelID2;
            if (bResult)
            {
                bResult = db.OperateDBNotOpenAndClose(sql);
            }
            else
            {
                db.Close();
                return bResult;
            }

            return bResult;
        }

        /// <summary>
        /// 清空巷道类型
        /// </summary>
        /// <param name="tunnelID"></param>
        /// <returns></returns>
        public static bool clearTunnelType(int tunnelID)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "UPDATE " + TunnelInfoDbConstNames.TABLE_NAME +
                " SET " + TunnelInfoDbConstNames.TUNNEL_TYPE + " = '" + (int)TunnelTypeEnum.OTHER +
                "' WHERE " + TunnelInfoDbConstNames.ID + " = " + tunnelID;
            bool bResult = db.OperateDB(sql);
            return bResult;
        }

        /// <summary>
        /// 删除掘进回采巷道相关数据（删除巷道时使用）
        /// </summary>
        /// <param name="tunnelEntity">巷道实体</param>
        /// <returns>是否成功删除掘进或回采巷道信息</returns>
        public static void deleteJJHCTunnelInfo(Tunnel tunnelEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            db.Open();

            string sql = "UPDATE " + TunnelInfoDbConstNames.TABLE_NAME + " SET " +
                TunnelInfoDbConstNames.TUNNEL_TYPE + " = " + (int)TunnelTypeEnum.OTHER +
                " WHERE " + TunnelInfoDbConstNames.ID + " = " + tunnelEntity.TunnelID;
            db.OperateDBNotOpenAndClose(sql);

            db.Close();
        }

        /// <summary>
        /// 删除关联巷道的导线信息
        /// </summary>
        /// <param name="tunnelEntity">巷道实体</param>
        /// <returns>是否成功删除导线信息</returns>
        public static bool deleteWireInfoBindingTunnelID(Tunnel tunnelEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT * FROM " + WireInfoDbConstNames.TABLE_NAME +
                " WHERE " + WireInfoDbConstNames.TUNNEL_ID + " = " + tunnelEntity.TunnelID;
            DataSet ds = db.ReturnDSNotOpenAndClose(sql);
            int wireInfoID = 0;
            if (ds.Tables[0].Rows.Count > 0)
            {
                wireInfoID = Convert.ToInt32(ds.Tables[0].Rows[0][WireInfoDbConstNames.ID].ToString());
            }
            sql = "DELETE FROM " + WirePointDbConstNames.TABLE_NAME + "WHERE " + WirePointDbConstNames.WIRE_INFO_ID + " = " + wireInfoID;
            bool bResult = db.OperateDBNotOpenAndClose(sql);
            sql = "DELETE FROM " + WireInfoDbConstNames.TABLE_NAME + " WHERE " + WireInfoDbConstNames.TUNNEL_ID + " = " + tunnelEntity.TunnelID;
            bResult = db.OperateDBNotOpenAndClose(sql);
            db.Close();
            return bResult;
        }

        /// <summary>
        /// 删除巷道的掘进进尺相关信息
        /// </summary>
        /// <param name="tunnelEntity">巷道实体</param>
        /// <returns>是否成功删除？true:false</returns>
        public static bool deleteDayReportJJBindingTunnelID(Tunnel tunnelEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            string sql = "DELETE FROM " + DayReportJJDbConstNames.TABLE_NAME +
                " WHERE " + DayReportJJDbConstNames.WORKINGFACE_ID + " = " + tunnelEntity.TunnelID;
            bool bResult = db.OperateDB(sql);
            return bResult;
        }

        /// <summary>
        /// 删除巷道的回采进尺相关信息
        /// </summary>
        /// <param name="tunnelEntity"></param>
        /// <returns></returns>
        public static bool deleteDayReportHCBindingTunnelID(Tunnel tunnelEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            string sql = "DELETE FROM " + DayReportHCDbConstNames.TABLE_NAME +
                " WHERE " + DayReportHCDbConstNames.WORKINGFACE_ID + " = " + tunnelEntity.TunnelID;
            bool bResult = db.OperateDB(sql);
            return bResult;
        }

        ///// <summary>
        ///// 根据巷道ID获取开切眼巷道ID
        ///// </summary>
        ///// <param name="nTunnelID">巷道ID</param>
        ///// <returns>开切眼巷道ID</returns>
        //public static int getQieYanIDbyTunnelID(int nTunnelID)
        //{
        //    int nId = -1;
        //    ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
        //    string sql = string.Format("SELECT {0} FROM {1} WHERE {2}={3} OR {4}={3} OR {0}={3}",
        //        TunnelHCDbConstNames.TUNNEL_ID3, TunnelHCDbConstNames.TABLE_NAME, TunnelHCDbConstNames.TUNNEL_ID1,
        //        nTunnelID, TunnelHCDbConstNames.TUNNEL_ID2);
        //    IDataReader dr = db.GetDataReader(sql);
        //    if (dr != null && dr.Read())
        //    {
        //        if (dr[0] != null)
        //        {
        //            int.TryParse(dr[0].ToString(), out nId);
        //        }
        //    }
        //    return nId;
        //}

        ///// <summary>
        ///// 根据开切眼巷道ID获取导线的起点和终点实体对象
        ///// </summary>
        ///// <param name="nTunnelID">开切眼巷道ID</param>
        ///// <returns>起点和终点实体对象</returns>
        //public static List<WirePointInfoEntity> getTunnelPointEntity(int nTunnelID)
        //{
        //    int tunnelID3 = getTunnelID3byTunnelID(nTunnelID);
        //    if (tunnelID3 < 0)
        //        return null;

        //    //根据开切眼巷道ID获取导线ID
        //    int wireInfoID = -1;
        //    TunnelEntity tunnelEntity = selectTunnelInfoByTunnelID(tunnelID3);
        //    if (tunnelEntity == null)
        //        return null;

        //    DataSet ds = WireInfoBLL.selectAllWireInfo(tunnelEntity);
        //    if (ds != null && ds.Tables[0].Rows.Count > 0)
        //    {
        //        int.TryParse(ds.Tables[0].Rows[0][WireInfoDbConstNames.ID].ToString(), out wireInfoID);
        //    }

        //    if (wireInfoID < 0)
        //        return null;

        //    //根据导线ID查找起止点编号
        //    ds = WirePointBLL.selectAllWirePointInfo(wireInfoID);
        //    if (ds != null && ds.Tables[0].Rows.Count > 1)   //至少包含2个点
        //    {
        //        DataTable dt = ds.Tables[0];
        //        DataView dv = new DataView(dt, "", WirePointDbConstNames.WIRE_POINT_NAME, DataViewRowState.CurrentRows);   //按照点号排序

        //        int firstPtID = -1;
        //        int endPtID = -1;
        //        int.TryParse(dv[0][WirePointDbConstNames.ID].ToString(), out firstPtID);                 //取第一个点作为起点
        //        int.TryParse(dv[dt.Rows.Count - 1][WirePointDbConstNames.ID].ToString(), out endPtID);   //取最后一个点作为终点

        //        if (firstPtID == -1 || endPtID == -1)
        //            return null;

        //        //根据导线点编号获取导线实体
        //        WirePointInfoEntity firstWirePtEntity = WirePointBLL.returnWirePointInfo(firstPtID);
        //        WirePointInfoEntity endWirePtEntity = WirePointBLL.returnWirePointInfo(endPtID);

        //        if (firstWirePtEntity != null && endWirePtEntity != null)
        //        {
        //            List<WirePointInfoEntity> lstWirePointsEntitiy = new List<WirePointInfoEntity>();
        //            lstWirePointsEntitiy.Add(firstWirePtEntity);
        //            lstWirePointsEntitiy.Add(endWirePtEntity);
        //            return lstWirePointsEntitiy;
        //        }
        //    }

        //    return null;
        //}        

        /// </summary>
        /// <param name="tunnelID">巷道编号</param>
        /// <returns>巷道信息</returns>
        public static DataSet selectAllTunnelInfo()
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            StringBuilder sb = new StringBuilder();
            //sb.Append("SELECT * FROM ");
            //sb.Append(TunnelInfoDbConstNames.TABLE_NAME + " T,");
            //sb.Append(WorkingFaceDbConstNames.TABLE_NAME + " W,");
            //sb.Append(MiningAreaDbConstNames.TABLE_NAME + " MA,");
            //sb.Append(HorizontalDbConstNames.TABLE_NAME + " H,");
            //sb.Append(MineDbConstNames.TABLE_NAME + " MD ");
            //sb.Append(" WHERE ");
            //sb.Append("T." + TunnelInfoDbConstNames.WORKINGFACE_ID + " = W." + WorkingFaceDbConstNames.WORKINGFACE_ID);
            //sb.Append(" AND W." + WorkingFaceDbConstNames.MININGAREA_ID + " = MA." + MiningAreaDbConstNames.MININGAREA_ID);
            //sb.Append(" AND MA." + MiningAreaDbConstNames.HORIZONTAL_ID + " = H." + HorizontalDbConstNames.HORIZONTAL_ID);
            //sb.Append(" AND H." + HorizontalDbConstNames.MINE_ID + " = M." + MineDbConstNames.MINE_ID);

            sb.Append("SELECT * FROM ");
            sb.Append(TunnelInfoDbConstNames.TABLE_NAME + " T ");
            sb.Append("left join ");
            sb.Append(WorkingFaceDbConstNames.TABLE_NAME + " W on T." + TunnelInfoDbConstNames.WORKINGFACE_ID);
            sb.Append(" = W." + WorkingFaceDbConstNames.WORKINGFACE_ID);
            sb.Append("left join");
            sb.Append(MiningAreaDbConstNames.TABLE_NAME + " MA on W." + WorkingFaceDbConstNames.MININGAREA_ID);
            sb.Append(" = MA." + MiningAreaDbConstNames.MININGAREA_ID);
            sb.Append("left join");
            sb.Append(HorizontalDbConstNames.TABLE_NAME + " H on MD." + MiningAreaDbConstNames.HORIZONTAL_ID);
            sb.Append(" = H." + HorizontalDbConstNames.HORIZONTAL_ID);
            sb.Append("left join");
            sb.Append(MineDbConstNames.TABLE_NAME + " M on H." + HorizontalDbConstNames.MINE_ID);
            sb.Append(" = M." + MineDbConstNames.MINE_ID);
            sb.Append("left join");
            sb.Append(LithologyDbConstNames.TABLE_NAME + " L on T." + TunnelInfoDbConstNames.LITHOLOGY_ID);
            sb.Append(" = L." + LithologyDbConstNames.LITHOLOGY_ID);
            sb.Append("left join");
            sb.Append(CoalSeamsDbConstNames.TABLE_NAME + " C on T." + TunnelInfoDbConstNames.COAL_LAYER_ID);
            sb.Append(" = C." + CoalSeamsDbConstNames.COAL_SEAMS_ID);
            sb.Append("left join");
            sb.Append(WireInfoDbConstNames.TABLE_NAME + " WI on T." + TunnelInfoDbConstNames.ID);
            sb.Append(" = WI." + WireInfoDbConstNames.TUNNEL_ID);

            DataSet ds = db.ReturnDS(sb.ToString());

            return ds;
        }

        /// <summary>
        /// 查询掘进面巷道数据
        /// </summary>
        /// <returns></returns>
        public static DataSet selectTunnelJJ()
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            string sql = "SELECT * FROM " + TunnelInfoDbConstNames.TABLE_NAME + " WHERE TUNNEL_TYPE=" + (int)TunnelTypeEnum.TUNNELLING;
            DataSet ds = db.ReturnDS(sql);
            return ds;
        }

        /// <summary>
        /// 返回掘进巷道所有信息
        /// </summary>
        /// <returns></returns>
        public static DataSet selectTunnelJJ(int iStartIndex, int iEndIndex)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ( ");
            sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + TunnelInfoDbConstNames.ID + ") AS rowid, * ");
            sb.Append("FROM " + TunnelInfoDbConstNames.TABLE_NAME + " WHERE " +
                TunnelInfoDbConstNames.TUNNEL_TYPE + "=" + (int)TunnelTypeEnum.TUNNELLING + " ) AS TB ");
            sb.Append("WHERE rowid >= " + iStartIndex);
            sb.Append(" AND rowid <= " + iEndIndex);
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }

        /// <summary>
        /// 分页用返回回采巷道所有信息
        /// </summary>
        /// <returns></returns>
        public static DataSet selectTunnelHC(int iStartIndex, int iEndIndex)
        {
            //            SELECT ROW_NUMBER() OVER (ORDER BY WORKINGFACE_ID) AS rowid ,*FROM
            //(
            //SELECT  
            //DISTINCT B.*
            //  FROM 
            //  T_TUNNEL_INFO AS A,
            //  T_WORKINGFACE_INFO AS B
            //   WHERE 
            //   TUNNEL_TYPE IN (0, 1, 2,3) AND A.WORKINGFACE_ID=B.WORKINGFACE_ID
            //   ) Base



            //            ;WITH TB AS
            //(
            //SELECT  
            //DISTINCT B.*
            //  FROM 
            //  T_TUNNEL_INFO AS A,
            //  T_WORKINGFACE_INFO AS B
            //   WHERE 
            //   TUNNEL_TYPE IN (0, 1, 2,3) AND A.WORKINGFACE_ID=B.WORKINGFACE_ID
            //   ) 

            //SELECT * FROM(
            //SELECT ROW_NUMBER() OVER (ORDER BY WORKINGFACE_ID) AS rowid ,* FROM
            //TB )
            //AS TB1
            //WHERE rowid>=1 AND rowid<=2


            //ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            //StringBuilder sb = new StringBuilder();
            //sb.Append("SELECT * FROM ( ");
            //sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + TunnelInfoDbConstNames.ID + ") AS rowid, * ");
            //sb.Append("FROM " + TunnelInfoDbConstNames.TABLE_NAME + " ) AS TB ");
            //sb.Append("WHERE rowid >= " + iStartIndex);
            //sb.Append("AND rowid <= " + iEndIndex);
            //DataSet ds = db.ReturnDS(sb.ToString());
            //return ds;



            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sb = new StringBuilder();
            sb.Append(";WITH TB AS" +
"(" +
"SELECT " +
"DISTINCT B.* " +
"  FROM " +
"  T_TUNNEL_INFO AS A, " +
"  T_WORKINGFACE_INFO AS B " +
"   WHERE " +
"   TUNNEL_TYPE IN (0, 1, 2,3) AND A.WORKINGFACE_ID=B.WORKINGFACE_ID" +
"   ) ");
            sb.Append("SELECT * FROM ( ");
            sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + TunnelInfoDbConstNames.WORKINGFACE_ID + ") AS rowid, * ");
            sb.Append("FROM TB ) AS TB1 ");
            sb.Append("WHERE rowid >= " + iStartIndex);
            sb.Append("AND rowid <= " + iEndIndex);
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }

        /// <summary>
        /// 查询回采面个数
        /// </summary>
        /// <returns></returns>
        public static DataSet selectTunnelHC()
        {
            //
            //            SELECT 
            //DISTINCT B.*
            //  FROM 
            //  T_TUNNEL_INFO AS A,
            //  T_WORKINGFACE_INFO AS B
            //   WHERE 
            //   TUNNEL_TYPE IN (0, 1, 2,3) AND A.WORKINGFACE_ID=B.WORKINGFACE_ID


            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            string sql = "SELECT DISTINCT B.* FROM " + TunnelInfoDbConstNames.TABLE_NAME + " AS A," +
                WorkingFaceDbConstNames.TABLE_NAME + " AS B " +
                "WHERE " + TunnelInfoDbConstNames.TUNNEL_TYPE + " IN (0, 1, 2,3) AND " +
                " A.WORKINGFACE_ID=B.WORKINGFACE_ID";
            DataSet ds = db.ReturnDS(sql);
            return ds;
        }

        /// <summary>
        /// 查询回采面个数
        /// </summary>
        /// <returns></returns>
        public static int selectTunnelHCCount()
        {

            //            SELECT 
            //DISTINCT B.*
            //  FROM 
            //  T_TUNNEL_INFO AS A,
            //  T_WORKINGFACE_INFO AS B
            //   WHERE 
            //   TUNNEL_TYPE IN (0, 1, 2,3) AND A.WORKINGFACE_ID=B.WORKINGFACE_ID

            //               SELECT 
            //COUNT(DISTINCT B.WORKINGFACE_ID)
            //  FROM 
            //  T_TUNNEL_INFO AS A,
            //  T_WORKINGFACE_INFO AS B
            //   WHERE 
            //   TUNNEL_TYPE IN (0, 1, 2,3) AND A.WORKINGFACE_ID=B.WORKINGFACE_ID


            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            string sql = "SELECT COUNT(DISTINCT B.WORKINGFACE_ID) FROM " + TunnelInfoDbConstNames.TABLE_NAME + " AS A," +
                WorkingFaceDbConstNames.TABLE_NAME + " AS B " +
                "WHERE " + TunnelInfoDbConstNames.TUNNEL_TYPE + " IN (0, 1, 2,3) AND " +
                " A.WORKINGFACE_ID=B.WORKINGFACE_ID";
            DataSet ds = db.ReturnDS(sql);
            return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
        }

        /// <summary>
        /// 根据工作面id获取与该工作面关联的所有巷道
        /// </summary>
        /// <param name="iWorkingFaceID">工作面ID</param>
        /// <returns>巷道数据集</returns>
        public static DataSet selectTunnelByWorkingFaceId(int iWorkingFaceID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM " + TunnelInfoDbConstNames.TABLE_NAME);
            sb.Append(" WHERE " + TunnelInfoDbConstNames.WORKINGFACE_ID + " = " + iWorkingFaceID);
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }

        public static DataSet selectTunnelInfoNotShowTunneling(int iWorkingFaceID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT A.*,B." + WorkingFaceDbConstNames.IS_FINISH);
            sb.Append(" FROM " + TunnelInfoDbConstNames.TABLE_NAME + " AS A," + WorkingFaceDbConstNames.TABLE_NAME + " AS B ");
            sb.Append(" WHERE A." + TunnelInfoDbConstNames.WORKINGFACE_ID + " = " + iWorkingFaceID);
            sb.Append(" AND A." + TunnelInfoDbConstNames.WORKINGFACE_ID + " = B." +
                      WorkingFaceDbConstNames.WORKINGFACE_ID);
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }
    }
}
