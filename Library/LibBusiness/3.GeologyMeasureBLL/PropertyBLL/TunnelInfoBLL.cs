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
        /// 判断巷道是否属于掘进面
        /// </summary>
        /// <param name="tunnelEntity">巷道实体</param>
        /// <returns>是否为掘进巷道？true:false</returns>
        //public static bool isTunnelJJ(Tunnel tunnelEntity)
        //{
        //    ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
        //    string sql = "SELECT " +
        //        "A." + TunnelInfoDbConstNames.TUNNEL_TYPE +
        //        ", B." + WorkingFaceDbConstNames.IS_FINISH +
        //         " FROM " +
        //        TunnelInfoDbConstNames.TABLE_NAME + " AS A, " +
        //        WorkingFaceDbConstNames.TABLE_NAME + " AS B" +
        //        " WHERE " +
        //        "A." + TunnelInfoDbConstNames.ID + " = " + tunnelEntity.TunnelId +
        //        " AND " +
        //        "A." + TunnelInfoDbConstNames.TUNNEL_TYPE + " = " + (int)TunnelTypeEnum.TUNNELLING +
        //         " AND " +
        //        "A." + TunnelInfoDbConstNames.WORKINGFACE_ID + " = B." + TunnelInfoDbConstNames.WORKINGFACE_ID;

        //    DataSet ds = db.ReturnDS(sql);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        if (Convert.ToInt32(ds.Tables[0].Rows[0][WorkingFaceDbConstNames.IS_FINISH].ToString()) == 1)
        //        {
        //            return false;
        //        }

        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        /// <summary>
        /// 返回巷道是否为回采巷道
        /// </summary>
        /// <param name="tunnelEntity">巷道实体</param>
        /// <returns>是否为回采巷道？true:false</returns>
        public static bool isTunnelHC(Tunnel tunnelEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT * FROM " + TunnelInfoDbConstNames.TABLE_NAME + " WHERE " +
                TunnelInfoDbConstNames.ID + " = " + tunnelEntity.TunnelId + " AND " +
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
                TunnelInfoDbConstNames.WORKINGFACE_ID + "=" + wfEntity.WorkingFaceId +
                " WHERE " + TunnelInfoDbConstNames.ID + " = " + tunnelEntity.TunnelId + ";";

            sql += " UPDATE " + WorkingFaceDbConstNames.TABLE_NAME +
                " SET " +
                WorkingFaceDbConstNames.WORK_TIME + "='" + wfEntity.WorkTime + "', " +
                WorkingFaceDbConstNames.START_DATE + "='" + wfEntity.StartDate.ToString() + "'," +
                WorkingFaceDbConstNames.STOP_DATE + "='" + wfEntity.StopDate.ToString() + "'," +
                WorkingFaceDbConstNames.WORK_STYLE + "='" + wfEntity.WorkStyle + "'," +
                WorkingFaceDbConstNames.IS_FINISH + "=" + wfEntity.IsFinish + "," +
                WorkingFaceDbConstNames.TEAM_NAME_ID + "=" + wfEntity.TeamNameId +
                " WHERE " + WorkingFaceDbConstNames.WORKINGFACE_ID + "= " + wfEntity.WorkingFaceId;

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
              WorkingFaceDbConstNames.TEAM_NAME_ID + "=" + wfEntity.TeamNameId +
              " WHERE " + WorkingFaceDbConstNames.WORKINGFACE_ID + "= " + wfEntity.WorkingFaceId;

            foreach (Tunnel entity in tunnelSet)
            {
                sql += " UPDATE " + TunnelInfoDbConstNames.TABLE_NAME +
                " SET " +
                TunnelInfoDbConstNames.TUNNEL_TYPE + " = " + (int)entity.TunnelType + ", " +
                TunnelInfoDbConstNames.WORKINGFACE_ID + "=" + entity.WorkingFace.WorkingFaceId +
                " WHERE " + TunnelInfoDbConstNames.ID + " = " + entity.TunnelId + ";";
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
        public static bool setTunnelAsHChuan(TunnelHChuan tunnelHChuanEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            db.Open();
            string sql = "UPDATE " + TunnelInfoDbConstNames.TABLE_NAME + " SET " +
                TunnelInfoDbConstNames.TUNNEL_TYPE + " = '" + "HENGCHUAN" +
                "' WHERE " + TunnelInfoDbConstNames.ID + " = " + tunnelHChuanEntity.TunnelId1;
            bool bResult = db.OperateDBNotOpenAndClose(sql);
            sql = "UPDATE " + TunnelInfoDbConstNames.TABLE_NAME + " SET " +
                TunnelInfoDbConstNames.TUNNEL_TYPE + " = '" + "HENGCHUAN" +
                "' WHERE " + TunnelInfoDbConstNames.ID + " = " + tunnelHChuanEntity.TunnelId2;
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
                " WHERE " + TunnelInfoDbConstNames.ID + " = " + tunnelEntity.TunnelId;
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
                " WHERE " + WireInfoDbConstNames.TUNNEL_ID + " = " + tunnelEntity.TunnelId;
            DataSet ds = db.ReturnDSNotOpenAndClose(sql);
            int wireInfoID = 0;
            if (ds.Tables[0].Rows.Count > 0)
            {
                wireInfoID = Convert.ToInt32(ds.Tables[0].Rows[0][WireInfoDbConstNames.ID].ToString());
            }
            sql = "DELETE FROM " + WirePointDbConstNames.TABLE_NAME + "WHERE " + WirePointDbConstNames.WIRE_INFO_ID + " = " + wireInfoID;
            bool bResult = db.OperateDBNotOpenAndClose(sql);
            sql = "DELETE FROM " + WireInfoDbConstNames.TABLE_NAME + " WHERE " + WireInfoDbConstNames.TUNNEL_ID + " = " + tunnelEntity.TunnelId;
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
                " WHERE " + DayReportJJDbConstNames.WORKINGFACE_ID + " = " + tunnelEntity.TunnelId;
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
                " WHERE " + DayReportHCDbConstNames.WORKINGFACE_ID + " = " + tunnelEntity.TunnelId;
            bool bResult = db.OperateDB(sql);
            return bResult;
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
